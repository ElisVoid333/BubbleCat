using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIndicatorSquare : MonoBehaviour
{

    public bool playerInside;
    public Transform growEffect;
    public Collider2D collider;
    [SerializeField] float AttackSpeed;
    [Range(0, 1)] public float size;

    [SerializeField] GameObject AttackPrefab;

    
    float currentTime = 0;
    

    private void Start()
    {
        
    }

    private void Update()
    {

        currentTime += Time.deltaTime;
        size = Mathf.Lerp(0, 1, currentTime / AttackSpeed);

        // scale based on player 
        growEffect.localScale = new Vector3(growEffect.localScale.x, size, 0);

        if (size == 1 )
        {
            if ( playerInside )
            {
                // deal dammage to player
                // then destroy this object
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

}
