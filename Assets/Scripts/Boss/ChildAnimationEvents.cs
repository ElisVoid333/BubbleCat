using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAnimationEvents : MonoBehaviour
{

    [SerializeField]Animator thisanimator;
    [SerializeField]Animator StateMachine;

    private void Start()
    {
        thisanimator = GetComponent<Animator>();


        StateMachine = this.gameObject.transform.parent.GetComponentInParent<Animator>();
    }

   
    public void AnimationFinish()
    {
        StateMachine.SetTrigger("EndAnim");

    }

    public void AttackHit()
    {

    }



}
