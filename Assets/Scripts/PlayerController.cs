using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Character Variables
    private int health = 6;
    private int stamina = 5;

    //Movement Variables
    private Rigidbody2D rb;
    private float horizontal;
    private bool isFlipped = false;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Movement 
        horizontal = Input.GetAxisRaw("Horizontal");
      
        //Jump
        if (Input.GetKeyDown(KeyCode.W) && stamina > 0)
        {
            Debug.Log("Jumping");
            rb.AddForce(new Vector2(0f, jumpForce));
            stamina -= 1;
        }   
      
        //Dash
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Dashing");

            stamina -= 1;
        }

        //Player facing direction
        if (Input.GetKeyDown(KeyCode.D))
        {
            isFlipped = false;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            isFlipped = true;
        }

        if (isFlipped)
        {
            this.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (!isFlipped)
        {
            this.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
    }

    /* FUNCTIONS */
    //Player dashes in direction it is facing
    private void Dash()
    {
        Debug.Log("Dashing");
    }
}
