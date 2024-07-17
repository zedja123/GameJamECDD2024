using UnityEngine;

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

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        isPlat = Physics2D.OverlapCircle(fallCheck.position, .2f, floorLayerMask);
        isObstacle = Physics2D.OverlapCircle(wallCheck.position, .2f, turnLayerMask);



        if (isRecoiling)
        {
            if (recoilTimer < recoilLength)
            {
                recoilTimer += Time.deltaTime;
            }
            else
            {
                isRecoiling = false;
                recoilTimer = 0;
            }
        }


        if (!isRecoiling && health > 0 && Mathf.Abs(rb.velocity.y) < 0.5f)
        {
            if (isPlat && !isObstacle && !isRecoiling)
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
        health -= _damageDone;
        if (!isRecoiling)
        {
            rb.AddForce(recoilFactor * _hitDirection);
            isRecoiling = true;
        }
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
}