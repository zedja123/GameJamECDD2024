using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{
    public int health;
    public bool dazed = false;
    public float dazedTime = 0;
    public float startDazedTime;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Material flashMat;
    [SerializeField] private Material originalMat;
    [SerializeField] private float flashTime = 0.5f;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        }
    }

    public void takeDamage(int damage)
    {
        initDazed();
        StartCoroutine(flashSprite(flashTime));

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

    IEnumerator flashSprite(float time)
    {
        spriteRenderer.material = flashMat;
        yield return new WaitForSeconds(time);
        spriteRenderer.material = originalMat;

    }
}
