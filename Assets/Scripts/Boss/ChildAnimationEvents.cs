using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAnimationEvents : MonoBehaviour
{

    [SerializeField]Animator thisanimator;
    [SerializeField]Animator StateMachine;
    [SerializeField]Collider2D collision2D;
    [SerializeField] GameObject containter;

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

    public void ActivateHitbox()
    {
        collision2D.enabled = true;
    }
    public void DeactivateHitbox()
    {
        collision2D.enabled=false;
    }

    public void Die()
    {
        containter.SetActive(false);
    }


}
