using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAnimationEvents : MonoBehaviour
{

    [SerializeField]Animator thisanimator;
    [SerializeField]Animator StateMachine;
    [SerializeField]Collider2D collision2D;

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
        collision2D.gameObject.SetActive(true);
    }
    public void DeactivateHitbox()
    {
        collision2D.gameObject.SetActive(false);
    }




}
