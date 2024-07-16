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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X) && canDefend && !attack.isAttacking)
        {
            isDefending = true;
            animator.SetBool("IsDefending", true);
        }
        else
        {
            isDefending = false;
            animator.SetBool("IsDefending", false);
        }
    }


}
