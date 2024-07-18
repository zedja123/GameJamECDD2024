using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] public Defend defend;
    public float dmgValue = 4;
	public GameObject throwableObject;
	public Transform attackCheck;
	[SerializeField] public Rigidbody2D m_Rigidbody2D;
	public Animator animator;
	public bool canAttack = true;
	public bool isTimeToCheck = false;
	public bool isAttacking = false;
	[SerializeField] public CharacterController2D characterController;

	public GameObject cam;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetAxisRaw("Fire1") > 0 && canAttack && !defend.isDefending)
		{
			canAttack = false;
			isAttacking = true;
            characterController.canMove = false;
            m_Rigidbody2D.velocity = Vector2.zero;
            animator.SetBool("IsAttacking", true);
			DoAttackDamage();
            StartCoroutine(AttackCooldown());
		}

	}

	IEnumerator AttackCooldown()
	{
		yield return new WaitForSeconds(0.5f);
		canAttack = true;
        characterController.canMove = true;
        isAttacking = false;
    }

	public void DoAttackDamage()
	{
		dmgValue = Mathf.Abs(dmgValue);
		Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 1.5f);
		for (int i = 0; i < collidersEnemies.Length; i++)
		{
			if (collidersEnemies[i].gameObject.tag == "Enemy")
			{
				collidersEnemies[i].GetComponent<EnemyBase>().ApplyDamage(dmgValue, characterController.m_Rigidbody2D.transform.position);
				cam.GetComponent<CameraFollow>().ShakeCamera();
			}
		}
	}
}
