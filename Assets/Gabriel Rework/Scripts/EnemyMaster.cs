using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{
    public int health;
    public bool dazed = false;
    public float dazedTime = 0;
    public float startDazedTime;

    public void Start()
    {
        Physics2D.IgnoreLayerCollision(7, 9);
        Physics2D.IgnoreLayerCollision(7, 7);

    }


    public void Update()
    {

        if (dazedTime > 0)
        {
            dazedTime -= Time.deltaTime;
        }
        if (dazedTime <= 0)
        {
            dazed = false;
        }
    }

    public void takeDamage(int damage)
    {

        dazed = true;
        dazedTime = startDazedTime;

        health -= damage;


        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
