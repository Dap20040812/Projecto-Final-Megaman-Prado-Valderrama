using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1 : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float nextFire;
    [SerializeField] GameObject bullet;
    [SerializeField] Animator T1;
    [SerializeField] GameObject player;
    [SerializeField] int hp;
    [SerializeField] AudioClip sfx_death;
    RaycastHit2D fuego;
    Vector2 direction = Vector2.left;
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
        fuego = Physics2D.Raycast(this.transform.position, direction, range, LayerMask.GetMask("Player"));
        Debug.DrawRay(this.transform.position, direction * range, Color.green);

        if (Time.time >= canFire && fuego.collider.name!="kk")
        {
            Debug.Log(fuego.collider.name);
            Debug.Log(fuego.collider.transform.position);

            Instantiate(bullet, transform.position - new Vector3((range/range), 0, 0), transform.rotation);
            canFire = Time.time + nextFire;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            cont++;
            if (cont == hp)
            {
                T1.SetTrigger("death_T1");
                AudioSource.PlayClipAtPoint(sfx_death, Camera.main.transform.position);
                Destroy(this.gameObject, 0.9f);
            }
        }
    }
}
