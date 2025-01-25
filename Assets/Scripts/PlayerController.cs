using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Character Variables
    private int health = 6;
    private int stamina = 5;
    private SpriteRenderer sprite;
    public Animator animator;

    //Movement Variables
    private Rigidbody2D rb;

    [Header("Movement Variables")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    private float horizontal;
    private bool isFlipped = false;

    //Attack Variables
    private bool isAttacking;
    [Header("Light Attack Variables")]
    [SerializeField] public int damage;
    [SerializeField] private GameObject scratch;
    [SerializeField] private float lightAttackTime;
    [SerializeField] private float lightAttackCooldown;

    [Header("Range Attack Variables")]
    [SerializeField] private GameObject bubble;
    [SerializeField] private float bubbleForce;
    [SerializeField] private float bubbleOffSet;
    [SerializeField] private float rangeAttackTime;
    [SerializeField] private float rangeAttackCooldown;

    [Header("Dash Variables")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;
    private bool canDash = true;
    private bool isDashing;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //sprite = GetComponent<SpriteRenderer>();
        scratch.SetActive(false);
    }

    private void Update()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.W) && stamina > 0)
        {
            //Debug.Log("Jumping");
            rb.AddForce(new Vector2(0f, jumpForce));
            stamina -= 1;
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash && stamina > 0)
        {
            //Debug.Log("Dashing");
            StartCoroutine(Dash());
            stamina -= 1;
        }

        //Player facing direction
        if (!isDashing) {
            if (Input.GetKey(KeyCode.D))
            {
                isFlipped = false;
            }
            if (Input.GetKey(KeyCode.A))
            {
                isFlipped = true;
            }

            if (isFlipped)
            {
                Debug.Log("Is flipped");
                this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                //sprite.flipX = true;
            }
            if (!isFlipped)
            {
                Debug.Log("Not Flipped");
                this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                //sprite.flipX = false;
            }
        }

        /*-- Attack --*/
        if (Input.GetMouseButtonDown(0) && canDash)
        {
            //Debug.Log("Light Attack");
            StartCoroutine(lightAttack());
            
        }
        if (Input.GetMouseButtonDown(1) && canDash)
        {
            //Debug.Log("Range Attack");
            StartCoroutine(rangeAttack());
        }
    }

    void FixedUpdate()
    {
        if (isDashing || isAttacking)
        {
            return;
        }

        /*-- Movement --*/
        horizontal = Input.GetAxisRaw("Horizontal");

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

    //Light Attack
    private IEnumerator lightAttack()
    {
        canDash = false;

        scratch.SetActive(true);

        yield return new WaitForSeconds(lightAttackTime);
        
        scratch.SetActive(false);

        yield return new WaitForSeconds(lightAttackCooldown);
        canDash = true;
    }

    //Range Attack
    private IEnumerator rangeAttack()
    {
        canDash = false;
        isAttacking= true;

        rb.velocity = new Vector3(0f, 0f, 0f);

        if (isFlipped)
        {
            GameObject newBubble = Instantiate(bubble, new Vector2(this.transform.position.x - bubbleOffSet, this.transform.position.y), Quaternion.identity);
            newBubble.GetComponent<Rigidbody2D>().AddForce(new Vector2(-bubbleForce, 0f));
        }
        else
        {
            GameObject newBubble = Instantiate(bubble, new Vector2(this.transform.position.x + bubbleOffSet, this.transform.position.y), Quaternion.identity);
            newBubble.GetComponent<Rigidbody2D>().AddForce(new Vector2(bubbleForce, 0f));
        }

        yield return new WaitForSeconds(rangeAttackTime);

        isAttacking = false;
        canDash = true;

        yield return new WaitForSeconds(rangeAttackCooldown);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            //Debug.Log("On the ground");
            stamina = 5;
        }
    }
}
