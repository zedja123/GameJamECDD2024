using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeShoot : MonoBehaviour
{

    public float speed = 5f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Physics.IgnoreLayerCollision(10, 6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerRework player = collision.GetComponent<PlayerRework>();
        if (player != null)
        {
            player.playerTakeDamage(1, transform.position);
            Destroy(gameObject);
        }
    }
}
