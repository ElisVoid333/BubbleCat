using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightChecker : MonoBehaviour
{
    public bool bossfightStarted;
    public bool bossfightEnded;
    public Camera Camera;
    [SerializeField] GameObject Ability;
    [SerializeField] Transform spawnLocation;
    [SerializeField] GameObject square;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(bossfightStarted == false)
            {
                bossfightStarted = true;
                this.gameObject.GetComponentInChildren<BossScript>().StartFight();
                StartCoroutine(camrachange());
            }            
        }
    }

    IEnumerator camrachange()
    {
        Camera.depth = 2;
        yield return new WaitForSeconds(4);
        Camera.depth = -1;
    }



    public void EndBossFight()
    {
        bossfightEnded = true;
        if(square != null)
        {
            square.SetActive(false);
        }
        Instantiate(Ability, spawnLocation.position, Quaternion.identity);
    }


}
