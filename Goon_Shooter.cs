using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goon_Shooter : Enemy, IDamageable
{
    public int Health { get; set; }

    public float enemySpeed;
    Animator enemyAnimator;

    //facing
    public GameObject enemyGraphic;
    bool canFlip = true;
    bool facingRight = true;
    float flipTime = 5f;
    float nextFlipChance = 0f;

    //attacking
    public float chargeTime;
    float startChargeTime;
    bool fireing;                               //Animator/Charging
    Rigidbody2D enemyRb2d;
    bool Range = false;


    private void Start()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyRb2d = GetComponent<Rigidbody2D>();

        Health = base.health;


    }


    private void Update()
    {
        if (Time.time > nextFlipChance)
        {
            if (Random.Range(0, 10) >= 5) flipFacing();                 //Every 5 seconds there is a 50% chance it will flip
            nextFlipChance = Time.time + flipTime;

        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (facingRight && other.transform.position.x < transform.position.x)
            {
                flipFacing();
            }
            else if (!facingRight && other.transform.position.x > transform.position.x)
            {
                flipFacing();
            }
            canFlip = false;
            fireing = true;
            Range = true;
            startChargeTime = Time.time + chargeTime;
            enemyAnimator.SetTrigger("inRange");
            enemyAnimator.SetBool("Range", Range);


        }
    }

                            // Ändra Shoot/ Instanceiate Bullet Prefab

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Range = true;
            fireing = true;  //
            if (startChargeTime < Time.time)
            {
                if (!facingRight)
                {
                    enemyRb2d.AddForce(new Vector2(-1, 0) * enemySpeed);
                }
                else
                {
                    enemyRb2d.AddForce(new Vector2(1, 0) * enemySpeed);
                }
                Range = true;
                fireing = true; //
                enemyAnimator.SetBool("isCharging", fireing);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canFlip = true;
            fireing = false;
            Range = false;
            enemyRb2d.velocity = new Vector2(0f, 0f);

            enemyAnimator.SetBool("isCharging", fireing);
            enemyAnimator.SetBool("Range", Range);
        }
        Range = false;
        fireing = false;
    }


    void flipFacing()
    {
        if (!canFlip) return;

        float facingX = enemyGraphic.transform.localScale.x;

        facingX *= -1f;
        enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
        facingRight = !facingRight;

    }

    public void Damage()
    {

        Health--;

        if (Health < 1)
        {
            // Instantiate(bloodSplash, transform.position, Quaternion.identity);
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
