using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    Transform player;

    [SerializeField] float attackRadius;
    [SerializeField] Transform hitBox;
    [SerializeField] LayerMask playersMask;
    [SerializeField] float damageAmout;

    float[] attackDetails = new float[2];

    float maxHealth = 100;
    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void LookAtPlayer()
    {
        if (player.position.x <= transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitBox.position, attackRadius, playersMask);
        attackDetails[0] = damageAmout;
        attackDetails[1] = transform.position.x;

        foreach (Collider2D collider in colliders)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
    }

    public void Damage(float[] playerAttackDetails)
    {
        currentHealth -= playerAttackDetails[0];

        animator.SetTrigger("isHurt");

        if(currentHealth <= 50)
        {
            Debug.Log("State 2");
            animator.SetBool("isStateTwo", true);
        }
    }

    public void FinishHit()
    {
        animator.SetBool("isHurt", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitBox.position, attackRadius);
    }
}
