using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = player.transform.position;
    }
}
