using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameController gc;

    //Character Variables
    private int health = 6;
    private int stamina = 5;
    public Animator animator;
    [Header("Health Hearts")]
    [SerializeField] private GameObject HealthHalf1;
    [SerializeField] private GameObject HealthHalf2;
    [SerializeField] private GameObject HealthHalf3;
    [SerializeField] private GameObject HealthHalf4;
    [SerializeField] private GameObject HealthHalf5;
    [SerializeField] private GameObject HealthHalf6;

    [Header("Stamina Bubbles")]
    [SerializeField] private GameObject bubble1;
    [SerializeField] private GameObject bubble2;
    [SerializeField] private GameObject bubble3;
    [SerializeField] private GameObject bubble4;
    [SerializeField] private GameObject bubble5;

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
    [SerializeField] public int Lightdamage = 5;
    [SerializeField] private GameObject scratch;
    [SerializeField] private float lightAttackTime;
    [SerializeField] private float lightAttackCooldown;

    [Header("Range Attack Variables")]
    [SerializeField] private GameObject bubble;
    [SerializeField] private float bubbleForce;
    [SerializeField] private float bubbleOffSet;
    [SerializeField] private float rangeAttackTime;
    [SerializeField] private float rangeAttackCooldown;
    [SerializeField] private AudioClip rangeAttackSound;

    [Header("Dash Variables")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;
    private bool canDash = true;
    private bool isDashing;

    [Header("Abilities Unlocked")]
    [SerializeField] private bool upgradedRangedAttack;
    [SerializeField] private float secondRangedAttackTime;
    [SerializeField] private bool bubbleExplosionAbility;
    [SerializeField] private GameObject bubbleExplosion; 
    [SerializeField] private float explodeAttackTime;
    [SerializeField] private float explodeAttackCooldown;

    void Awake()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scratch.SetActive(false);
        bubbleExplosion.SetActive(false);

        upgradedRangedAttack = gc.IncreasedRange;
        bubbleExplosionAbility = gc.ExplosiveBubbles;
    }

    private void Update()
    {
        upgradedRangedAttack = gc.IncreasedRange;
        bubbleExplosionAbility = gc.ExplosiveBubbles;

        //Jump
        if (Input.GetKeyDown(KeyCode.W) && stamina > 0 || Input.GetKeyDown(KeyCode.Space) && stamina > 0)
        {
            //Debug.Log("Jumping");
            animator.SetBool("Jumping", true);
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
                //Debug.Log("Is flipped");
                this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            if (!isFlipped)
            {
                //Debug.Log("Not Flipped");
                this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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
        /*-- Abilities --*/
        if (bubbleExplosionAbility) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(bubbleExplode());
            }
        }

        /*-- HUD Changes --*/
        //Stamina
        if (stamina == 5)
        {
            bubble1.SetActive(true);
            bubble2.SetActive(true);
            bubble3.SetActive(true);
            bubble4.SetActive(true);
            bubble5.SetActive(true);
        }
        else if (stamina == 4)
        {
            bubble5.SetActive(false);
        }
        else if (stamina == 3)
        {
            bubble4.SetActive(false);
        }
        else if (stamina == 2)
        {
            bubble3.SetActive(false);
        }
        else if (stamina == 1)
        {
            bubble2.SetActive(false);
        }
        else if (stamina == 0)
        {
            bubble1.SetActive(false);
        }

        //Health
        if (health == 6)
        {
            HealthHalf1.SetActive(true);
            HealthHalf2.SetActive(true);
            HealthHalf3.SetActive(true);
            HealthHalf4.SetActive(true);
            HealthHalf5.SetActive(true);
            HealthHalf6.SetActive(true);
        }
        else if (health == 5)
        {
            HealthHalf6.SetActive(false);
        }
        else if (health == 4)
        {
            HealthHalf5.SetActive(false);
        }
        else if (health == 3)
        {
            HealthHalf4.SetActive(false);
        }
        else if (health == 2)
        {
            HealthHalf3.SetActive(false);
        }
        else if (health == 1)
        {
            HealthHalf2.SetActive(false);
        }
        else if (health == 0)
        {
            HealthHalf1.SetActive(false);
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

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

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

        if (upgradedRangedAttack)
        {
            yield return new WaitForSeconds(secondRangedAttackTime);
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
        }

        yield return new WaitForSeconds(rangeAttackTime);


        isAttacking = false;
        canDash = true;

        yield return new WaitForSeconds(rangeAttackCooldown);
    }

    private IEnumerator bubbleExplode()
    {
        isAttacking = true;
        canDash = false;

        rb.velocity = new Vector3(0f, 0f, 0f);

        //Debug.Log(bubbleExplosion.transform.localScale);
        Vector3 originalScale = bubbleExplosion.transform.localScale;

        bubbleExplosion.SetActive(true);

        yield return new WaitForSeconds(explodeAttackTime);

        bubbleExplosion.transform.localScale = originalScale;
        bubbleExplosion.SetActive(false);
        isAttacking = false;
        canDash = true;

        yield return new WaitForSeconds(explodeAttackCooldown);
    }

    /*-- Collision Functions --*/
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            //Debug.Log("On the ground");
            stamina = 5;
            animator.SetBool("Jumping", false);
        }
    }

    public void TakeDammage()
    {
        health--;

        if (health <= 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlayerDeath();
        }
    }


}