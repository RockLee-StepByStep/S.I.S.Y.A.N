using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveVector;
    public float speed = 2f;
    public SpriteRenderer sr;
    public GameObject floor;
    
    private Animator _animator;

    //добавляем пражок 
    public float jumpForce = 10.0f;
    //добавляем боксколлайдер 
    private BoxCollider2D _box;
    
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
    }

    void Update ()
    {
        walk();
        Reflect();
        Jump();
       
    }
    
    public bool faceRight = true;

    void Jump()
    {

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;

        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);

        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        //Проверяем знаение минимальной У-координаты коллайдера

        bool grounded = false;

        if (hit != null)
        {         // если под персонажем обнаружен коллайдер 
            grounded = true;
        }

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //добавил экземпляр класса Rigidbody.AddForce-вектор напрвленный вверх на силу.
            _animator.SetBool("jumping", true);
        }
    }

    void walk () 
    {
        _animator.SetFloat("speed",moveVector.sqrMagnitude);
        _animator.SetBool("run",false);
        // добавил ускорение при зажатии шифта
        if (Input.GetKey(KeyCode.LeftShift))  
        {
            _animator.SetBool("run",true);
            speed = 12.0f;
        }
        else   
        {
            speed = 2.0f;
        };
        moveVector.x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
       // rb.AddForce(moveVector * speed); // физика
    }

    void Reflect ()
    {
        if ((moveVector.x > 0 && faceRight) || (moveVector.x < 0 && !faceRight))
        {
            transform.localScale *= new Vector2 (-1,1);
            faceRight = !faceRight;
            //поворот персонажа
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == floor)
        {
            Debug.Log("получилось");
            this.transform.parent = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == floor)
        {
            this.transform.parent = null;
        }
    }


  

   

}
