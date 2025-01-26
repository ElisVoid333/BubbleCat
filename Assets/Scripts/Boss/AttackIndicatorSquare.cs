using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIndicatorSquare : MonoBehaviour
{

    public GameObject playerInside;
    public Transform growEffect;
    public Collider2D collider;
    [SerializeField] float AttackSpeed;
    [Range(0, 1)] public float size;
    [Range(0, 1)] public float PosLerp;
    bool Attacked =false;

    [SerializeField] GameObject AttackPrefab;

    
    float currentTime = 0;
    

    private void Start()
    {
        
    }

    private void Update()
    {

        currentTime += Time.deltaTime;
        size = Mathf.Lerp(0, 0.005023f, currentTime / AttackSpeed);

        PosLerp = Mathf.Lerp(55, 50, currentTime / AttackSpeed);
        // scale based on player 
        

        growEffect.localScale = new Vector3(growEffect.localScale.x, size, 0);

        if (size == 0.005023f && Attacked == false)
        {
            Attacked = true;
            var spawnedAttack = Instantiate(AttackPrefab, this.gameObject.transform.position, Quaternion.identity);

            //spawnedAttack.gameObject.transform.GetChild(0).transform.localScale = this.transform.parent.localScale;

            if ( playerInside!=null)
            {
                
                // deal dammage to player
                playerInside.GetComponent<PlayerController>().TakeDammage();
                // then destroy this object
            }
            else
            {
                
            }
            Destroy(this.gameObject,1);
        }
    }

}
