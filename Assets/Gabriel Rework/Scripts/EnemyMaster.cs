using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{
    public int health;
    public bool dazed = false;
    public float dazedTime = 0;
    public float startDazedTime;

    public Color damageColor;
    public Color startColor;
    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = startColor;
        Physics2D.IgnoreLayerCollision(6, 6);
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
            spriteRenderer.color = startColor;

        }
    }

    public void takeDamage(int damage)
    {
        initDazed();
        spriteRenderer.color = damageColor;

        health -= damage;


        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void initDazed()
    {
        dazed = true;
        dazedTime = startDazedTime;

    }
}
