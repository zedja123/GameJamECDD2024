using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBase : MonoBehaviour
{
    
    [SerializeField] public float health;
    [SerializeField] public float recoilLength;
    [SerializeField] public float recoilFactor;
    [SerializeField] public bool isRecoiling = false;
    [SerializeField] public bool seeingPlayer;
    public bool isPlat;
    public bool isObstacle;
    [SerializeField] public float speed;
    public bool facingRight;
    public float recoilTimer;
    public bool isHitted;
    public Rigidbody2D rb;
    public bool isInvincible = false;
    public Transform fallCheck;
    public Transform wallCheck;
    public Transform attackCheck;
    public Animator animator;
    [SerializeField] public LayerMask turnLayerMask;
    [SerializeField] public LayerMask floorLayerMask;
    [SerializeField] public GameObject player;
    [SerializeField] public float aggroRange;
    [SerializeField] public Transform castPoint;



    // Start is called before the first frame update

    public void Awake()
    {
            fallCheck = transform.Find("FallCheck");
            wallCheck = transform.Find("WallCheck");
            attackCheck = transform.Find("AttackCheck");
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
    }
    public void Start()
    {

    }
    // Update is called once per frame
    public void Update()
    {

        if (health <= 0)
        {
            transform.GetComponent<Animator>().SetBool("IsDead", true);
            StartCoroutine(DestroyEnemy());
        }
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        isPlat = Physics2D.OverlapCircle(fallCheck.position, .2f, floorLayerMask);
        isObstacle = Physics2D.OverlapCircle(wallCheck.position, .2f, turnLayerMask);


        if (CanSeePlayer(aggroRange))
        {
            Debug.Log("SeeingPlayer");
            ChasePlayer();
        }
        else
        {
            if (!isHitted && health > 0 && Mathf.Abs(rb.velocity.y) < 0.5f)
            {
                if (seeingPlayer && health > 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                }
                if (isPlat && !isObstacle && !isHitted)
                {
                    if (facingRight)
                    {
                        rb.velocity = new Vector2(-speed, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                    }
                }
                else
                {
                    Debug.Log("Flipping");
                    Flip();
                }
            }
        }



        
    }

    public bool CanSeePlayer(float distance)
    {

        bool val = false;
        float castDist = distance;

        Vector2 endPos = castPoint.position + Vector3.right * distance;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));

        if(hit.collider != null) 
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }

        }

        return val;
    }

    public void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x) 
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }
    public void ApplyDamage(float _damageDone, Vector3 _hitDirection)
    {
        Vector2 damageDir = Vector3.Normalize(transform.position -_hitDirection) * 40f;
        transform.GetComponent<Animator>().SetBool("Hit", true);
        health -= _damageDone;
        rb.velocity = Vector2.zero;
        StartCoroutine(HitTime());
    }
    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    IEnumerator DestroyEnemy()
    {
        CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
        capsule.size = new Vector2(1f, 0.25f);
        capsule.offset = new Vector2(0f, -0.8f);
        capsule.direction = CapsuleDirection2D.Horizontal;
        yield return new WaitForSeconds(0.25f);
        rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    IEnumerator HitTime()
    {
        isHitted = true;
        isInvincible = true;
        yield return new WaitForSeconds(0.25f);
        isHitted = false;
        isInvincible = false;
    }

}