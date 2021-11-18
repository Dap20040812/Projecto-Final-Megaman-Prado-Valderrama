using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float nextFire;
    [SerializeField] float dashTime;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject vfx_death;
    [SerializeField] BoxCollider2D pies;
    [SerializeField] Sprite idleSprite;
    [SerializeField] Sprite fallingSprite;
    [SerializeField] AudioClip sfx_death;
    [SerializeField] AudioClip sfx_fire;
    [SerializeField] AudioClip sfx_fall;
    [SerializeField] AudioClip sfx_jump;
    [SerializeField] AudioClip sfx_dash;
    Animator myAnimator;
    Rigidbody2D myBody;
    BoxCollider2D mycollider;
    public bool vivo = true;
    bool pause = false;
    float tamX, canFire, canDash;
    // Start is called before the first frame update
    void Start()
    {
        tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        mycollider = GetComponent<BoxCollider2D>();
        // StartCoroutine(Showtime());
        myAnimator.SetTrigger("spawn");
    }

    // Update is called once per frame
    void Update()
    {

        if (!pause)
        {
            Mover();
            Saltar();
            Falling();
            Fire();
            Dash();
        }
    }

    IEnumerator Showtime()
    {

        int count = 0;
        while(true){
            yield return new WaitForSeconds(1f);
            count++;
            //Debug.Log("tiempo: " + count);
        }

    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.X) && isGrounded() && Time.time >= canDash)
        {
            AudioSource.PlayClipAtPoint(sfx_dash, Camera.main.transform.position);
            myAnimator.SetTrigger("dash");
            myBody.velocity += new Vector2(speed * 1.5f * gameObject.transform.localScale.x, 0);
            canDash = Time.time + dashTime;
        }

    }

    void Fire()
    {

        if (Input.GetKey(KeyCode.L))
        {
            myAnimator.SetLayerWeight(1, 1);
            if (Time.time >= canFire && Input.GetKeyDown(KeyCode.L))
            {
                AudioSource.PlayClipAtPoint(sfx_fire, Camera.main.transform.position);
                Instantiate(bullet, transform.position - new Vector3(-tamX * gameObject.transform.localScale.x, 0, 0), transform.rotation);
                canFire = Time.time + nextFire;
            }
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }

    }
    void Mover()
    {
        float mov = Input.GetAxis("Horizontal");

        if (mov != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(mov), 1);
            myAnimator.SetBool("running", true);
            transform.Translate(new Vector2(mov * speed * Time.deltaTime, 0));
        }
        else
        {
            myAnimator.SetBool("running", false);
        }
    }
    void Saltar()
    {
        if (isGrounded() && !myAnimator.GetBool("jumping"))
        {
            myAnimator.SetBool("falling", false);
            myAnimator.SetBool("jumping", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioSource.PlayClipAtPoint(sfx_jump, Camera.main.transform.position);
                myAnimator.SetTrigger("Takeoff");
                myAnimator.SetBool("jumping", true);
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }
        }
    }

    void Falling()
    {
        if(myBody.velocity.y < 0 && !myAnimator.GetBool("jumping"))
        {
            myAnimator.SetBool("falling", true);

        }
        if (myBody.velocity.y < 0 && !myAnimator.GetBool("jumping") && isGrounded())
        {
            AudioSource.PlayClipAtPoint(sfx_fall, Camera.main.transform.position);

        }
    }
    bool isGrounded()
    {
        //return pies.IsTouchingLayers(LayerMask.GetMask("Ground"));
        RaycastHit2D myRaycast =  Physics2D.Raycast(mycollider.bounds.center, Vector2.down, mycollider.bounds.extents.y + 0.2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(mycollider.bounds.center, new Vector2(0, ((mycollider.bounds.extents.y + 0.2f) * -1)), Color.green);
        return myRaycast.collider != null;
    }
    void AfterTakeOfEvent()
    {
        myAnimator.SetBool("jumping", false);
        myAnimator.SetBool("falling", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemies") || collision.gameObject.CompareTag("Tower1") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            StartCoroutine("Die");
        }
    }
    IEnumerator Die()
    {
        pause = true;
        myBody.isKinematic = true;
        myAnimator.SetBool("death", true);
        yield return new WaitForSeconds(1);
        AudioSource.PlayClipAtPoint(sfx_death, Camera.main.transform.position);
        Instantiate(vfx_death, transform.position, transform.rotation);
        vivo = false;
        Destroy(gameObject);

    }

}
