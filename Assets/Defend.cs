using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public Attack attack;
    [SerializeField] public Animator animator;
    [SerializeField] public CharacterController2D characterController;
    public bool canDefend = true;
    public bool isDefending;
    private Rigidbody2D m_Rigidbody2D;
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X) && canDefend && !attack.isAttacking)
        {
            isDefending = true;
            m_Rigidbody2D.velocity = Vector2.zero;
            characterController.canMove = false;
            animator.SetBool("IsDefending", true);
        }
        else if (Input.GetKeyUp(KeyCode.X) && canDefend && !attack.isAttacking)
        {
            isDefending = false;
            characterController.canMove = true;
            animator.SetBool("IsDefending", false);
        }
    }


}
