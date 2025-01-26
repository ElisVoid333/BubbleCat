using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boss_Move : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    float currentspeed;
    
    float AttackRange = 3f;
    BossScript boss;


    private bool shouldJump;
    private bool isGrounded;
    [SerializeField] bool Enraged;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.gameObject.GetComponent<Rigidbody2D>();
        boss = animator.gameObject.GetComponent<BossScript>();
        currentspeed = boss.Basespeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        boss.LookAtPlayer();
        isGrounded = Physics2D.Raycast(rb.transform.position, Vector2.down, 2f, boss.groundlayer);
        Debug.DrawRay(rb.transform.position, Vector2.down, Color.red, 2f);
        float direction = Mathf.Sign(player.position.x- rb.transform.position.x);

        bool isPlayerAbove = Physics2D.Raycast(rb.transform.position,Vector2.up, 8f, 1<< player.gameObject.layer);
        //enrage speed vs normal speed
        if (Enraged)
        {
            currentspeed = boss.Ragespeed;
        }
        else
        {
            currentspeed = boss.Basespeed;
        }


        if (isGrounded)
        {
            
            rb.velocity = new Vector2(direction*currentspeed, rb.velocity.y);
        

            RaycastHit2D groundhitInfront = Physics2D.Raycast(rb.transform.position, new Vector2(direction, 0), 2f, boss.groundlayer);
            RaycastHit2D gapAhead = Physics2D.Raycast(rb.transform.position + new Vector3(direction, 0,0),Vector2.down ,2f, boss.groundlayer);
            RaycastHit2D PlatformAbove = Physics2D.Raycast(rb.transform.position, Vector2.up, 8f, boss.groundlayer);


            if(!groundhitInfront.collider && !gapAhead.collider)
            {
                shouldJump = true;
                

            }else if(isPlayerAbove && PlatformAbove.collider)
            {
                // jump
                shouldJump=true;
               
            }
        }

        //Vector2 target = new Vector2(player.position.x, rb.position.y);

        //Vector2 newPos =  Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        //rb.MovePosition(newPos);

        if ( Vector2.Distance(player.position, rb.position)<= AttackRange)
        {
            animator.SetTrigger("AttackClose");
            animator.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("AttackClose");
        }else if(boss.CanSpecialAttack)
        {
            animator.SetTrigger("AttackSpecial");
        }
        Jump();



    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.ResetTrigger("AttackClose");
        animator.ResetTrigger("AttackSpecial");
    }

    private void Jump()
    {
        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 playerpos =new Vector2(player.position.x, player.position.y);
            Vector2 direction = (playerpos - rb.position).normalized;
            Vector2 jumpdirection = direction * boss.JumpForce;

            rb.AddForce(new Vector2(jumpdirection.x, boss.JumpForce),ForceMode2D.Impulse);
            Debug.Log("jumping");
        }
    }


    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
