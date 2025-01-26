using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAttack : MonoBehaviour
{
    public int CurrentDammage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Boss")
        {
            Debug.Log("dammageBoss");
            collision.gameObject.GetComponent<BossScript>().TakeDammage(CurrentDammage);
            Destroy(this.gameObject);
        }
    }
}
