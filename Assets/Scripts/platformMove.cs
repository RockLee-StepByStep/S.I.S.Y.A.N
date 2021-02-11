using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMove : MonoBehaviour
{
    float speed = 4.0f;
    public GameObject Player;
   
   

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision) // нажатие клавиши приводит в движение платформы 
    {
        if (collision.gameObject == Player)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Vector2 UPmove = new Vector2(-31, 15);
                transform.position = Vector2.MoveTowards(transform.position, UPmove, Time.deltaTime * speed);
            }
            
        }
    }

 
  
}
