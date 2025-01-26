using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;



public class Boss_Eel_Move : StateMachineBehaviour
{
    Transform player;
    //Rigidbody2D rb;
    float currentspeed;

    float AttackRange = 3f;
    BossScript boss;
    [SerializeField]float unitlAttack;

    //private bool shouldJump;
    //private bool isGrounded;
    [SerializeField] bool Enraged;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //rb = animator.gameObject.GetComponent<Rigidbody2D>();
        boss = animator.gameObject.GetComponent<BossScript>();
        currentspeed = boss.Basespeed;
        unitlAttack = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        if (Enraged)
        {
            currentspeed = boss.Ragespeed;
        }
        else
        {
            currentspeed = boss.Basespeed;
        }

        unitlAttack += Time.deltaTime;

       // float directionx = Mathf.Sign(player.position.x - rb.transform.position.x);
       // float directiony = Mathf.Sign(player.position.x - rb.transform.position.x);

        
        animator.gameObject.transform.right = -(player.position - animator.gameObject.transform.position); 
        boss.EelFlip();



        if (unitlAttack >= 7)
        {
            
            animator.SetTrigger("LungeAttack");
            animator.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("LungeAttack");

        }
        else if (boss.CanSpecialAttack)
        {
            animator.SetTrigger("AttackSpecial");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unitlAttack = 0;
        animator.ResetTrigger("LungeAttack");
        animator.ResetTrigger("AttackSpecial");
        animator.gameObject.transform.GetChild(0).GetComponent<Animator>().ResetTrigger("LungeAttack");
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
