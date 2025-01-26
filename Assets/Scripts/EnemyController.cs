using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float distance;
    float initialX;

    // Start is called before the first frame update
    void Start()
    {
        initialX = transform.position.x;        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        Vector3 pos = transform.position;
        Vector3 direction = transform.localScale;

        pos.x = Mathf.PingPong(Time.time * speed, distance) + initialX;
        transform.position = pos;

        if(pos.x <= ((0.1) + initialX))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(pos.x >= ((distance - 0.1) + initialX))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
