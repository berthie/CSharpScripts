using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed = 3;
    public float speed = 50;
    public float jumpPower = 150;

    public bool grounded;

    private Rigidbody2D rb2d;
    private Animator anim;
    private Animator _SwordAnimation;
    public TimeManager timeManager;
    



    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        _SwordAnimation = transform.GetChild(1).GetComponent<Animator>();
    }

    private void Update()
    {
       

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            _SwordAnimation.SetTrigger("SwordAnimation");
        }

        anim.SetBool("Grounded",grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        if(Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb2d.AddForce(Vector2.up * jumpPower);
            
            Debug.Log("jumping!");
        }

        if (Input.GetButtonDown("Use"))
        {
            timeManager.doSlowMotion();
        }
        

    }

    private void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal");

        
        rb2d.AddForce((Vector2.right * speed) * h);
        if(rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

       
    }

    public void Attack()
    {
        anim.SetTrigger("Attacking");
    }



}
