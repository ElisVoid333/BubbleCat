using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss_Special : StateMachineBehaviour
{
    Transform lastPosition;
    Rigidbody2D rigidboyd;
    float speed = 10;
    Transform currentPos;

    BossScript boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidboyd = animator.GetComponent<Rigidbody2D>();
        lastPosition = animator.gameObject.transform;
        boss = animator.GetComponent<BossScript>(); 
        boss.CrabSpecial(animator,1);
        //currentPos.position = boss.gameObject.transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // boss.gameObject.transform.position = boss.BackgroundPos.position;
       
        //Vector2 newPos = Vector2.MoveTowards(rigidboyd.position, OffScreenPos, speed * Time.fixedDeltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //boss.gameObject.transform.position = currentPos.position;
        animator.ResetTrigger("EndSpecial");
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
