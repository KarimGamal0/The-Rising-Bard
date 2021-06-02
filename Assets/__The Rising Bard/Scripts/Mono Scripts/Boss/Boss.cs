using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    Transform player;

    [SerializeField] Slider healthSlider;

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

        healthSlider.maxValue = maxHealth;
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
        healthSlider.value = currentHealth;

        animator.SetTrigger("isHurt");
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }

        if (currentHealth <= 50)
        {
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
