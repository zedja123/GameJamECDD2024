using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour
{
    [SerializeField] private EnemyMaster enemyMaster;

    public Rigidbody2D rb;

    public float speed;
    private float curSpeed;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;


    public Transform attackPos;
    public float attackRange;
    public LayerMask playerLayerMask;





    void Start()
    {
        enemyMaster = GetComponent<EnemyMaster>();
        curSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyMaster.dazed)
        {
            curSpeed = 0;

        }
        else
        {
            curSpeed = speed;
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(curSpeed, rb.velocity.y);

        if (HasTouchedWall())
        {
            curSpeed *= -1;
            speed *= -1;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }




    }

    private bool HasTouchedWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }


    private void Attack()
    {
        Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, playerLayerMask);
        for (int i = 0; i < playerToDamage.Length; i++)
        {
            playerToDamage[i].GetComponent<PlayerRework>().playerTakeDamage();
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            Attack();
        }
    }

}
