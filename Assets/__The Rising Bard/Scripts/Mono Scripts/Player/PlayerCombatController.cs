using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{

    public delegate void zeroParamE();
    public static event zeroParamE playerDeathE;

    [SerializeField] GameEvent healthChange;


    [SerializeField] private bool combatEnabled;
    [SerializeField] private float inputTimer;

    [Header("HitBox Compenet")]
    [SerializeField] private Transform attack1HitBoxPos;
    [SerializeField] private float attack1Radius;
    [SerializeField] private float attack1Damage = 1;
    [SerializeField] private LayerMask whatIsDamageable;

    [SerializeField] private GameObject deathChunkParticle;
    [SerializeField] private GameObject deathBloodParticle;

    [Header("CoolDown Attack")]
    [SerializeField] private float coolDownAttackTimerSet;

    private bool deathFlag = false;
    private bool gotInput;
    private bool isAttacking;
    private bool isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity;
    private float[] attackDetails = new float[2];
    private float cooldownTimer;



    private Animator anim;
    private PlayerOldControlles POC;
    [SerializeField] private PlayerData PD;


    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        POC = GetComponent<PlayerOldControlles>();
        cooldownTimer = coolDownAttackTimerSet;
    }


    private void OnEnable()
    {
        FallingObstacle.playerDeathE += Die;
        PlayerDeath.playerDaeth += Die;
    }


    private void OnDisable()
    {
        FallingObstacle.playerDeathE -= Die;
        PlayerDeath.playerDaeth -= Die;
    }


    void Update()
    {
        CheckCombatInput();
        CheckAttacks();

    }



    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (combatEnabled)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }


        }
    }


    private void CheckAttacks()
    {
        if (gotInput)
        {
            if (cooldownTimer >= 0)
            {
               
                cooldownTimer -= Time.deltaTime;
            }
            else
            {
                cooldownTimer = coolDownAttackTimerSet;
                if (!isAttacking)
                {
                    gotInput = false;
                    isAttacking = true;
                    //isFirstAttack = !isFirstAttack;
                   //anim.SetBool("attack1", true);
                   // anim.SetBool("firstAttack", isFirstAttack);
                    anim.SetBool("isAttacking", isAttacking);
                }
            }


        }
        if (Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        attackDetails[0] = attack1Damage;
        attackDetails[1] = transform.position.x;
        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
    }


    private void Damage(float[] attackDetails)
    {
        if (!POC.GetDashStatus())
        {
            int direction;
            if (attackDetails[1] < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            POC.Knockback(direction);
        }
        PD.playerHP -= attackDetails[0];
        HealthChangeAction();
    }

    private void HealthChangeAction()
    {
        healthChange.Raise();
        if (PD.playerHP <= 0)
            Die();
    }


    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        cooldownTimer = coolDownAttackTimerSet;
        //anim.SetBool("attack1", false);
    }
    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        //GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<BoxCollider2D>().enabled = false;

        playerDeathE.Invoke();
        //  Destroy(gameObject);
    }




    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
