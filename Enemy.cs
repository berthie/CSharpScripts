using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected Transform pointA, pointB;
    [SerializeField]
    protected GameObject effect;


    public virtual void Attack()
    {

    }

    

}
