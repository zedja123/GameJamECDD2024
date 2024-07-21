using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour
{
    [SerializeField] private EnemyMaster enemyMaster;

    public Rigidbody2D rb;
    [SerializeField] public Animator animator;


    public float speed;
    private float curSpeed;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    //attack
    public Transform attackPos;
    public float attackRange;
    public LayerMask playerLayerMask;

    [SerializeField] private float timeBtwAttack = 0f;
    public float startTimeBtwAttack;
    public bool startLeft = false;
    private bool attacked;




    void Start()
    {
        enemyMaster = GetComponent<EnemyMaster>();
        curSpeed = speed;
        timeBtwAttack = 0f;

        if (startLeft)
        {
            ChangeDirection();
        }

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

        if (timeBtwAttack <= 0)
        {
            animator.SetBool("isAttacking", false);
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(curSpeed, rb.velocity.y);

        if (HasTouchedWall())
        {
            ChangeDirection();
        }

    }

    private void ChangeDirection()
    {
        curSpeed *= -1;
        speed *= -1;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private bool HasTouchedWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }


    private void Attack()
    {
        animator.SetBool("isAttacking", true);
        timeBtwAttack = startTimeBtwAttack;

        /*
        if (timeBtwAttack <= 0)
        {
            animator.SetBool("isAttacking", true);

            
            Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, playerLayerMask);
            for (int i = 0; i < playerToDamage.Length; i++)
            {
                playerToDamage[i].GetComponent<PlayerRework>().playerTakeDamage(1, transform.position);
                timeBtwAttack = startTimeBtwAttack;
            }
            
           
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;

        }
         */
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }


    public void OnTriggerStay2D(Collider2D target)
    {
        if (target.tag == "Player" && timeBtwAttack <= 0)
        {

            Attack();
            enemyMaster.initDazed();

        }

    }

    public void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            animator.SetBool("isAttacking", false);
        }
    }


}
