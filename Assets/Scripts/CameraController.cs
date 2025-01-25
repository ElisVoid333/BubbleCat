using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offset, -10f);
    }
}
