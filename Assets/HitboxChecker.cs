using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxChecker : MonoBehaviour
{

    public int CurrentDammage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("dammagePlayer");
            collision.gameObject.GetComponent<PlayerController>().TakeDammage();
        }

        if(collision.gameObject.tag == "Boss")
        {
            Debug.Log("dammageBoss");
            collision.gameObject.GetComponent<BossScript>().TakeDammage(CurrentDammage);
        }
        if (collision.gameObject.tag == "Bosschild")
        {
            Debug.Log("dammageBoss");
            collision.gameObject.transform.parent.GetComponent<BossScript>().TakeDammage(CurrentDammage);
        }
        if (collision.gameObject.tag == "Ability")
        {
            collision.GetComponent<AbilityTrigger>().GiveAbility();
        }

    }
}
