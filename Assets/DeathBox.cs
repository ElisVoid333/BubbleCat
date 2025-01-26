using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField] bool doeesnotcange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlayerDeath();
        }
    }


}
