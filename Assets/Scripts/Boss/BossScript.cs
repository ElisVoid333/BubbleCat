using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] float BossMaxHP;
    [SerializeField]float BossCurrentHp;
    [SerializeField] Animator BossAnimator;
    [SerializeField] Animator modelanimator;
    [SerializeField] Rigidbody2D RB;
    [SerializeField] Transform Player;
    
    bool isflipped = false;
    public bool CanSpecialAttack = false;

    public Transform BackgroundPos;
    public float JumpForce;
    public float Basespeed = 2.5f;
    public float Ragespeed = 2.5f;
    public LayerMask groundlayer;
    public bool enraged = false;
    [SerializeField] GameObject ClawAttackPrefab;
    [SerializeField] BossHPControl HpBar;
    public bool isInvincible=false;
   


    // Start is called before the first frame update
    void Start()
    {
        BossCurrentHp = BossMaxHP;
        RB = GetComponent<Rigidbody2D>();   
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        HpBar.SetupHPBar(BossMaxHP);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && CanSpecialAttack == false)
        {
            CanSpecialAttack = true;
        }else if(Input.GetKeyDown(KeyCode.C) && CanSpecialAttack == true)
        {
            CanSpecialAttack = false;
        }

        HpBar.UpdateHP(BossCurrentHp);


        if(BossCurrentHp < (BossMaxHP / 2))
        {
            this.gameObject.GetComponent<Animator>().SetBool("Enraged", true);
        }


    }



    public void StartFight()
    {
        modelanimator.SetTrigger("StartFight");
        isInvincible = false;
        StartCoroutine(resetSpecial());
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > Player.position.x+.4 && isflipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isflipped = false;
        }
        else if (transform.position.x < Player.position.x-.4 && !isflipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isflipped = true;
        }

    }



    public void startCrabSpecial()
    {
        isInvincible = true;
        this.gameObject.GetComponentInChildren<Animator>().SetTrigger("CrabInvincible");
    }
    public void EndCrabSpecial()
    {
        isInvincible = false;
        this.gameObject.GetComponentInChildren<Animator>().SetTrigger("CrabInvincible");
    }

    public void CrabSpecial(Animator anim, float attackDirection, int numberofAttack)
    {
        CanSpecialAttack = false;
        

        StartCoroutine(startAttack(anim,numberofAttack,4));
    }


    IEnumerator startAttack(Animator animator,int NumberOfSpecial, int speed)
    {
        for (int i = 0; i < NumberOfSpecial; i++)
        {
            Vector3 pos = new Vector3(0, Player.transform.position.y, 0);
            Instantiate(ClawAttackPrefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(speed);
        }
        
        StartCoroutine(EndSpecialCooldown(animator));
    }




    IEnumerator EndSpecialCooldown(Animator anim)
    {
        yield return new WaitForSeconds(3);
        anim.SetTrigger("EndSpecial");
        StartCoroutine(resetSpecial());
    }

    IEnumerator resetSpecial()
    {
        yield return new WaitForSeconds(20);
        CanSpecialAttack = true;

    }


    public void CharacterIdleTime(Animator anim, float idletime)
    {
        StartCoroutine(IdleTime(anim,idletime));
    }
    IEnumerator IdleTime(Animator anim, float IdleTime)
    {
        yield return new WaitForSeconds(IdleTime);
        anim.SetTrigger("EndIdle");
    }

        public void EndspecialAttack(Animator anim)
    {

    }

    public bool CheckEnraged()
    {
        if (BossCurrentHp < (BossMaxHP / 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    


    public void TakeDammage(int Dammage)
    {
        if(!isInvincible)
        {
            BossCurrentHp-=Dammage;

            if (BossCurrentHp <= 0)
            {
                BossAnimator.SetTrigger("Death");
                modelanimator.SetTrigger("Death");
            }
        }
        
    }

    public void EelFlip()
    {
        if (gameObject.transform.localRotation.z < -90)
        {

            gameObject.GetComponentInChildren<SpriteRenderer>().flipY = true;
        }
        else if (gameObject.transform.localRotation.z > -90)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipY = false;
        }
    }


}
