using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    [SerializeField] public float speed = 4f;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKey(KeyCode.UpArrow))
            {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        }else if (collision.tag == "Player" && Input.GetKey(KeyCode.DownArrow))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        }
        else
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
        }
    }
}
