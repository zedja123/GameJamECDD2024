using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public CharacterController2D characterController;
    public bool canDefend = true;
    public bool defending;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X) && canDefend)
        {
            defending = true;
        }
        else
        {
            defending = false;
        }
    }


}
