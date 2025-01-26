using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTrigger : MonoBehaviour
{

    public int AbilityNum;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().HasAbility(AbilityNum))
        {
            Destroy(this.gameObject);
        }

        StartCoroutine(delayAbilty());
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GiveAbility(AbilityNum);
            Destroy(this.gameObject);
        }
    }
    public void GiveAbility()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GiveAbility(AbilityNum);
        Destroy(this.gameObject.transform.parent.gameObject);
    }
    IEnumerator delayAbilty()
    {
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(4);
        this.gameObject.GetComponent<Collider2D>().enabled = true;
    }

}
