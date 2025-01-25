using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] float BossMaxHP;
    [SerializeField]float BossCurrentHp;
    [SerializeField] Rigidbody2D RB;
    [SerializeField] Transform Player;
    
    bool isflipped = false;
    public bool CanSpecialAttack = false;

    public Transform BackgroundPos;
    public float JumpForce;
    public float Basespeed = 2.5f;
    public float Ragespeed = 2.5f;
    public LayerMask groundlayer;

    [SerializeField] GameObject ClawAttackPrefab;
    [SerializeField] BossHPControl HpBar;


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

    public void CrabSpecial(Animator anim, int attackDirection)
    {
        CanSpecialAttack = false;
        Vector3 pos = new Vector3(0, Player.transform.position.y, 0);
        Instantiate(ClawAttackPrefab, pos,Quaternion.identity );

        StartCoroutine(EndSpecialCooldown(anim));
    }

    IEnumerator EndSpecialCooldown(Animator anim)
    {
        yield return new WaitForSeconds(3);
        anim.SetTrigger("EndSpecial");
    }


    public void CharacterIdleTime(Animator anim, int idletime)
    {
        StartCoroutine(IdleTime(anim,idletime));
    }
    IEnumerator IdleTime(Animator anim, int IdleTime)
    {
        yield return new WaitForSeconds(IdleTime);
        anim.SetTrigger("EndIdle");
    }

        public void EndspecialAttack(Animator anim)
    {

    }
}
