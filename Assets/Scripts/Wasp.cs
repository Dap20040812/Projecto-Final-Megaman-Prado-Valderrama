using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Wasp : MonoBehaviour
{
    public AIPath aipath;
    [SerializeField] float range;
    [SerializeField] GameObject player;
    [SerializeField] Animator wasp;
    [SerializeField] int hp;
    [SerializeField] AudioClip sfx_death;
    int cont;
    // Start is called before the first frame update
    void Start()
    {
        cont = 0;
        aipath.canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Vector2.Distance(player.transform.position, transform.position) <= range)
        {
            Debug.Log("FP");
        }*/

        if(Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Player")) != null)
        {
            //Debug.Log("FP");
            aipath.canMove = true;

        }

        if (aipath.desiredVelocity.x >= 1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aipath.desiredVelocity.x >= -1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.51f,0.25f,0.46f,0.6f);
        Gizmos.DrawSphere(transform.position, range);
        //Gizmos.DrawLine(transform.position, player.transform.position);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            cont++;
            if (cont == hp)
            {
                aipath.maxSpeed = 0;
                wasp.SetTrigger("death_w");
                AudioSource.PlayClipAtPoint(sfx_death, Camera.main.transform.position);
                Destroy(this.gameObject, 0.9f);
            }
        }
    }

}
