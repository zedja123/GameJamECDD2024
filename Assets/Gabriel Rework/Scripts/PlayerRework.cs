using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using Unity.VisualScripting;


public class PlayerRework : MonoBehaviour
{

    //player movement
    private float horizontal;
    [SerializeField] private float speed = 8f;

    private bool isFacingRight = true;
    public bool walking;


    // Health and Damage
    public int health = 3;
    [SerializeField] private float timeBtwAttack = 0f;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyLayerMask;
    public static int playerDamage;
    public static bool gameOver = false;
    private bool hasShield = false;

    [SerializeField] public Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    //Start
    private void Start()
    {
        playerDamage = 1;
        //startTimeBtwAttack = 0.3f;
        timeBtwAttack = 0f;
    }

    //Update
    private void FixedUpdate()
    {
        if (timeBtwAttack <= 0f)
        {
            //movement
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            if (rb.velocity.x != 0f)
            {
                walking = true;
                animator.SetBool("isRunning", true);
            }
            else
            {
                walking = false;
                animator.SetBool("isRunning", false);
              

            }
        }
        
    }

    private void Update()
    {
        if (timeBtwAttack <= 0)
        {           
            animator.SetBool("isAttacking", false);

            //then can attack
            if (Input.GetButton("Fire1"))
            {
                Attack();
                animator.SetBool("isAttacking", true);
                timeBtwAttack = startTimeBtwAttack;
                rb.velocity = new Vector2(0, rb.velocity.y);

            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
           // animator.SetBool("isAttacking", false);

        }


        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();

    }



    //My functions
    private void Attack()
    {   
        Debug.Log("player has attacked");
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayerMask);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyMaster>().takeDamage(1);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    private void Flip()
    {
            if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
            {
                isFacingRight = !isFacingRight;
                transform.Rotate(0f, 180f, 0f);
            }
        
    }

    public void playerTakeDamage()
    {
        health -= 1;
        if(health <= 0)
        {
            playerDie();
        }
    }

    public void playerDie()
    {
        Debug.Log("Player Died");
        gameOver = true;
    }
}
