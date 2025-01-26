using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBoxController : MonoBehaviour
{
    [SerializeField] bool GoTostart;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!GoTostart)
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GoToNextScene();
            }
            else
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GoToStart();
            }

            
        }
    }
}
