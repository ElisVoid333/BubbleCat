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

    void OnCollisionEnter(Collision collision)
    {
        /*
        if(collision)
        {
            collapse();
        }
        */
    }

    public void collapse()
    {
        //play earth spell        
        PS.Play();
        AS.Play();
        Debug.Log("DestroyRock");


        gameObject.SetActive(false);
        //StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float alphaVal = SR.color.a;
        Color tmp = SR.color;

        while (SR.color.a < 1)
        {
            alphaVal += 0.01f;
            tmp.a = alphaVal;
            SR.color = tmp;
        }

        if(SR.color.a >= 1)
        {
            PS.Stop();
        }

        yield break;
    }

}
