using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBoxController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GoToNextScene();
        }
    }
}
