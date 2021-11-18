using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] Animator bullet_EB;
    [SerializeField] float range;
    GameObject StaticEnemy;
    float boom = 1;
    // Start is called before the first frame update
    void Start()
    {
        StaticEnemy = GameObject.FindGameObjectWithTag("Tower1");

        bulletSpeed = -1* bulletSpeed * StaticEnemy.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(bulletSpeed * Time.deltaTime * boom, 0));
        Destroy(gameObject, range);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block")|| collision.gameObject.CompareTag("Player"))
        {
            boom = 0;
            Destroy(this.gameObject, 0.2f);
            bullet_EB.SetBool("Shoot_EB", true);
        }
        else
        {
            boom = 1;
        }
    }
}
