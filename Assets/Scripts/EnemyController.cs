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

        pos.x = (Mathf.PingPong(Time.time * speed, distance) * -1) + initialX;
        transform.position = pos;

        if(pos.x >= (initialX - 0.1))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(pos.x <= (initialX - (distance - 0.1f)))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
