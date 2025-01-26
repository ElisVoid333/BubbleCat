using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    public GameObject PlayerEnter;
    public float countdown;
    [SerializeField] GameObject ExplosionPrefab;
    [SerializeField] GameObject mask;
   
    bool isExploding;

    private void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("PlayerEnter");
            PlayerEnter = collision.gameObject;
            //Start Countdown
            if(isExploding == false)
            {
                isExploding = true;
                ArmBomb();
                StartCoroutine(bombCountdown());
            }
           
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerEnter = null;
            

        }
    }


    public void ArmBomb()
    {


        //mineMaterial.color = Color.red;
        mask.SetActive(true);
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }


    IEnumerator bombCountdown()
    {
        yield return new WaitForSeconds(countdown);
        Instantiate(ExplosionPrefab,this.transform.position,Quaternion.identity);
        if(PlayerEnter != null)
        {
            //dammagePlayer

            Debug.Log("dammagePlayer");

        }
        else
        {
            Debug.Log("MissPLayer");
        }
        Destroy(this.gameObject,0.3f);
    }
}
