using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBase : MonoBehaviour
{
    
    [SerializeField] protected float health;
    [SerializeField] protected float recoilLength;
    [SerializeField] protected float recoilFactor;
    [SerializeField] protected bool isRecoiling = false;
    protected bool isPlat;
    protected bool isObstacle;
    [SerializeField] protected float speed;
    protected bool facingRight;
    protected float recoilTimer;
    protected bool isHitted;
    protected Rigidbody2D rb;
    protected bool isInvincible = false;
    protected Transform fallCheck;
    protected Transform wallCheck;
    protected Transform attackCheck;
    protected Animator animator;
    [SerializeField] protected LayerMask turnLayerMask;
    [SerializeField] protected LayerMask floorLayerMask;
    
    
    
    // Start is called before the first frame update

    protected virtual void Awake()
    {
            fallCheck = transform.Find("FallCheck");
            wallCheck = transform.Find("WallCheck");
            attackCheck = transform.Find("AttackCheck");
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
    }
    protected virtual void Start()
    {

    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (health <= 0)
        {
            transform.GetComponent<Animator>().SetBool("IsDead", true);
            StartCoroutine(DestroyEnemy());
        }
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        isPlat = Physics2D.OverlapCircle(fallCheck.position, .2f, floorLayerMask);
        isObstacle = Physics2D.OverlapCircle(wallCheck.position, .2f, turnLayerMask);


        if (!isHitted && health > 0 && Mathf.Abs(rb.velocity.y) < 0.5f)
        {
            if (isPlat && !isObstacle && !isHitted)
            {
                if (facingRight)
                {
                    Debug.Log("FacingRight");
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                else
                {
                    Debug.Log("FacingLeft");
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

    public virtual void ApplyDamage(float _damageDone, Vector3 _hitDirection)
    {
        Vector2 damageDir = Vector3.Normalize(transform.position -_hitDirection) * 40f;
        transform.GetComponent<Animator>().SetBool("Hit", true);
        health -= _damageDone;
        rb.velocity = Vector2.zero;
        StartCoroutine(HitTime());
    }
    public virtual void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public virtual IEnumerator DestroyEnemy()
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

    public virtual IEnumerator HitTime()
    {
        isHitted = true;
        isInvincible = true;
        yield return new WaitForSeconds(0.25f);
        isHitted = false;
        isInvincible = false;
    }

}