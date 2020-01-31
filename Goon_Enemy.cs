using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goon_Enemy : Enemy, IDamageable 
{

    private Vector3 _currentTarget;
    private Animator _anim;
    private SpriteRenderer _goonSprite;
   // public GameObject effect;
   // public GameObject bloodSplash;


    
    public int Health { get; set; }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _goonSprite = GetComponent<SpriteRenderer>();

        Health = base.health;
    }


    public void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }

        

        Movement();


    }


    void Movement()
    {
        if (_currentTarget == pointA.position)
        {
            _goonSprite.flipX = false;
        }
        else
        {
            _goonSprite.flipX = true;
        }

        if (transform.position == pointA.position)
        {
            _currentTarget = pointB.position;
            _anim.SetTrigger("Idle");
            
            
        }
        else if (transform.position == pointB.position)
        {
            _currentTarget = pointA.position;
            _anim.SetTrigger("Idle");
            
            
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);



    }

    public void Damage()
    {
        Health--;

        if(Health < 1)
        {
           // Instantiate(bloodSplash, transform.position, Quaternion.identity);
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
