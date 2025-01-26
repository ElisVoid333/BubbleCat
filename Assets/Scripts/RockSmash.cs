using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSmash : MonoBehaviour
{
    [SerializeField]SpriteRenderer SR;
    public ParticleSystem PS;
    [SerializeField] AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void collapse()
    {       
        PS.Play();
        AS.Play();

        gameObject.SetActive(false);
    }
}
