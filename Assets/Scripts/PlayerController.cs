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
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }


    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
    }
}
