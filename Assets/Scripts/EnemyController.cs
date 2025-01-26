using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool vertical;
    public float speed;
    public float distance;
    float initial;

    // Start is called before the first frame update
    void Start()
    {
        if (vertical == true)
        {
            initial = transform.position.y;
        }
        else
        {
            initial = transform.position.x;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        Vector3 pos = transform.position;
        Vector3 squash = transform.localScale;

        if(vertical == true)
        {
            pos.y = (Mathf.PingPong(Time.time * speed, distance) * -1) + initial;

            squash.y = (Mathf.PingPong(Time.time * speed, 0.5f)) + 0.25f;
        }
        else
        {
            pos.x = (Mathf.PingPong(Time.time * speed, distance) * -1) + initial;

            if (pos.x >= (initial - 0.1))
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (pos.x <= (initial - (distance - 0.1f)))
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        
        transform.position = pos;
        gameObject.transform.localScale = squash;
    }
}
