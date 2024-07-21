using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Player")
        {
            collision.GetComponent<PlayerRework>().playerTakeDamage(1, transform.position);
        }

    }
}
