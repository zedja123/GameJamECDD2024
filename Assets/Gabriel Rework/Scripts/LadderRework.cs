using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderRework : MonoBehaviour
{

    private PlayerRework characterController;
    // Start is called before the first frame update

    void Start()
    {
        characterController = FindObjectOfType<PlayerRework>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            characterController.onLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            characterController.onLadder = false;
        }
    }
}
