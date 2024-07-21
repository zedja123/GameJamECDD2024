using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;
//using Unity.VisualScripting;


public class PlayerRework : MonoBehaviour
{

    //player movement
    private float horizontal;
    [SerializeField] private float speed = 8f;
    private bool isFacingRight = true;
    public bool walking;
   
    //ladder
    public bool onLadder;
    [SerializeField] public float climbSpeed = 3.5f;
    private float climbVelocity;
    private float gravityStore;


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

    //hearts UI
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public int numOfHearts;

    //general components
    [SerializeField] public Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    //conditions
    public bool invincible = false; //If player can die
    public bool canMove = true; //If player can move
    public bool canAttack = true; //If player can attack
    public bool canDefend = true; // If player can defend
    public bool isDefending; // If player is defending


    //Start
    private void Start()
    {
        Cursor.visible = false;


        rb = GetComponent<Rigidbody2D>();

        playerDamage = 1;
        timeBtwAttack = 0f;
        gravityStore = rb.gravityScale;

        Physics2D.IgnoreLayerCollision(9, 11);
    }

    //Update
    private void FixedUpdate()
    {
        if(canMove)
        Move();
        ClimbLadder();

    }

    private void Update()
    {
        UpdateHearts();
 
        if (canDefend)
        {
            Deffend();
        }
        if (canAttack)
        {
            Attack();
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (canMove)
        {
            Flip();
        }

    }



    //My functions

    //movement
    private void ClimbLadder()
    {
        if (onLadder)
        {
            rb.gravityScale = 0f;
            float climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, climbVelocity);
            if (rb.velocity.y != 0)
            {
                animator.SetBool("isClimbing", true);
            }
            else if (!IsGrounded() && rb.velocity.y == 0)
            {
            }
        }
        else if (!onLadder)
        {
            animator.SetBool("isClimbing", false);
            rb.gravityScale = gravityStore;
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Move()
    {
        if (timeBtwAttack <= 0f && !isDefending)
        {
            
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
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    //attack
    private void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            animator.SetBool("isAttacking", false);

            //then can attack
            if (Input.GetButton("Fire1"))
            {
                SoundManager.PlaySound(SoundManager.Sound.PlayerAttack);

               // AttackCheck();
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
    }

    private void AttackCheck()
    {   
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayerMask);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyMaster>().takeDamage(1);
        }
    }

    private void Deffend()
    {
        if (Input.GetAxisRaw("Fire2") > 0 && canDefend)
        {
            isDefending = true;
            animator.SetBool("isDefending", true);
            rb.velocity = new Vector2(0, rb.velocity.y);

        }
        else if (Input.GetAxisRaw("Fire2") < 1 && canDefend)
        {
            isDefending = false;
            animator.SetBool("isDefending", false);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    //Damage handle
    public void playerTakeDamage(int damage, Vector3 position)
    {
        
        if (!isDefending)
        {
            SoundManager.PlaySound(SoundManager.Sound.PlayerHurt);
            health -= damage;
            Vector2 damageDir = Vector3.Normalize(transform.position - position) * 40f;
            rb.velocity = Vector2.zero;
            rb.AddForce(damageDir * 5);

            StartCoroutine(Stun(0.5f));
            StartCoroutine(MakeInvincible(1f));

            animator.SetBool("Hit", true);

            if (health <= 0)
            {
                playerDie();
            }
        }else if (isDefending) 
        {
            SoundManager.PlaySound(SoundManager.Sound.ShieldDeflect);
        }
    }
    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void playerDie()
    {
        Debug.Log("Player Died");
        gameOver = true;
        SceneManager.LoadScene("GameOver");

    }


    //Corroutines
    IEnumerator Stun(float time)
    {
        canMove = false;
        canAttack = false;
        canDefend = false;
        yield return new WaitForSeconds(time);
        animator.SetBool("Hit", false);
        canAttack = true;
        canDefend = true;
        canMove = true;
    }
    IEnumerator MakeInvincible(float time)
    {
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }
}
