using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] public CharacterController2D controller;
	private Rigidbody2D rb;
	public Animator animator;
    public bool onLadder;
    [SerializeField] public float climbSpeed = 3.5f;
    private float climbVelocity;
    private float gravityStore;
    [SerializeField] public float runSpeed = 4f;

	float horizontalMove = 0f;
    bool jump = false;
	bool dash = false;
	public bool canMove = true;

    //bool dashAxis = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityStore = rb.gravityScale;
    }
    // Update is called once per frame
    void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		/*if (Input.GetKeyDown(KeyCode.Z))
		{
			jump = true;
		}*/

		if (Input.GetKeyDown(KeyCode.C))
		{
			dash = true;
		}

		/*if (Input.GetAxisRaw("Dash") == 1 || Input.GetAxisRaw("Dash") == -1) //RT in Unity 2017 = -1, RT in Unity 2019 = 1
		{
			if (dashAxis == false)
			{
				dashAxis = true;
				dash = true;
			}
		}
		else
		{
			dashAxis = false;
		}
		*/

	}

	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()
	{
		if(canMove)
		{
            if (onLadder)
            {
                rb.gravityScale = 0f;
                float climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");
                rb.velocity = new Vector2(rb.velocity.x, climbVelocity);
            }
            else if (!onLadder)
            {
                rb.gravityScale = gravityStore;
            }
            if (controller.m_Grounded)
            {
                float runVelocity = runSpeed * Input.GetAxisRaw("Horizontal");
                rb.velocity = new Vector2(runVelocity, rb.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (horizontalMove > 0 && !controller.m_FacingRight)
                {
                    // ... flip the player.
                    controller.Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (horizontalMove < 0 && controller.m_FacingRight)
                {
                    // ... flip the player.
                    controller.Flip();
                }
            }

		}
	}
}
