using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightChecker : MonoBehaviour
{
    bool bossfightStarted;
    public Camera Camera;
    [SerializeField] GameObject Ability;
    [SerializeField] Transform spawnLocation;
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
        yield return new WaitForSeconds(6);
        Camera.depth = -1;
    }



    public void EndBossFight()
    {
        Instantiate(Ability, spawnLocation.position, Quaternion.identity);
    }


}
