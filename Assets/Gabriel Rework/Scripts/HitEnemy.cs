using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyMaster>().takeDamage(1);
    
        }

        if (collision != null && collision.tag == "Lock")
        {
            collision.GetComponent<EnemyMaster>().takeDamage(1);
             SoundManager.PlaySound(SoundManager.Sound.Lock);
        }

    }
}
