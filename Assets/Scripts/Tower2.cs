using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower2 : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float nextFire;
    [SerializeField] GameObject bullet;
    [SerializeField] Animator T2;
    [SerializeField] GameObject player;
    [SerializeField] int hp;
    [SerializeField] AudioClip sfx_death;

    int cont;
    float canFire;
    // Start is called before the first frame update
    void Start()
    {
        cont = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= canFire && Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Player")) != null)
        {
            T2.SetTrigger("Shoot_T2");
            Instantiate(bullet, transform.position - new Vector3(0, 0, 0), Quaternion.Euler(0,0,-135));
            Instantiate(bullet, transform.position - new Vector3(0, 0, 0), Quaternion.Euler(0, 0, -45));
            canFire = Time.time + nextFire;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.51f, 0.25f, 0.46f, 0.6f);
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
                T2.SetTrigger("death_T2");
                AudioSource.PlayClipAtPoint(sfx_death, Camera.main.transform.position);
                Destroy(this.gameObject, 0.9f);
            }
        }
    }
}
