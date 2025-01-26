using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleExplosionAttack : MonoBehaviour
{
    public int CurrentDammage;
    public bool CanReopen = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            Debug.Log("dammageBoss");
            collision.gameObject.GetComponent<BossScript>().TakeDammage(CurrentDammage);
            StartCoroutine(delayDammage());

        }
        if (collision.gameObject.tag == "Bosschild")
        {
            Debug.Log("dammageBoss");
            collision.gameObject.transform.parent.GetComponent<BossScript>().TakeDammage(CurrentDammage);
            StartCoroutine(delayDammage());
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("dammageEnemy");
            collision.gameObject.GetComponent<EnemyController>().TakeDammage();
            StartCoroutine(delayDammage());
        }

        if (collision.gameObject.tag == "RockBreak")
        {
            
            collision.gameObject.GetComponent<RockSmash>().collapse();
        }

    }

    IEnumerator delayDammage()
    {
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(.7f);
        if (CanReopen == true)
        {
            this.gameObject.GetComponent<Collider2D>().enabled = true;
        }

    }
}
