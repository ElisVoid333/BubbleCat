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
    [Header("Movement Variables")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float DashForce;

    private bool canDash = true;
    private bool isDashing;
    [Header("Dash Variables")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    private bool isFlipped = false;


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
            //Debug.Log("Jumping");
            rb.AddForce(new Vector2(0f, jumpForce));
            stamina -= 1;
        }   
      
        //Dash
        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash)
        {
            Debug.Log("Dashing");
            StartCoroutine(Dash());
            stamina -=1;
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
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
    }

    /* FUNCTIONS */
    //Player dashes in direction it is facing
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;

        rb.gravityScale = 0f;

        if (isFlipped)
        {
            rb.velocity = new Vector2(-transform.localScale.x * dashForce, 0f);
        }
        else 
        { 
            rb.velocity = new Vector2(transform.localScale.x * dashForce, 0f);
        }

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
