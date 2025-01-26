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

        if (collision.gameObject.tag == "Bosschild")
        {
            Debug.Log("dammageBoss");
            collision.gameObject.transform.parent.GetComponent<BossScript>().TakeDammage(CurrentDammage);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("dammageEnemy");
            collision.gameObject.GetComponent<EnemyController>().TakeDammage();
            Destroy(this.gameObject);
        }
    }
}
