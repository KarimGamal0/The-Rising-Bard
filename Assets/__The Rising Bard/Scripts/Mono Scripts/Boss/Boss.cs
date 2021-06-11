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

    Vector2 direction;

    float[] attackDetails = new float[2];

    [SerializeField] float maxHealth = 60;
    float currentHealth;

    bool m_inRange;
    bool m_stateTwoPlayed = false;

    void Start()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(currentHealth <= 50 && m_stateTwoPlayed == false)
        { 
            Debug.Log("state 2 change");
            animator.SetBool("isStateTwo", true);
          
            //animator.Play("Rapiada_State_Change_anim");
            m_stateTwoPlayed = true;
        }
    }

    public void LookAtPlayer()
    {
        if (player.position.x <= transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            direction = Vector2.left;
        }
        else 
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            direction = Vector2.right;

        }
    }

    public Vector2 GetDirection()
    {
        return direction;
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

        //if (currentHealth <= 50)
        //{
        //    animator.SetBool("isStateTwo", true);
        //}
    }

    public void FinishHit()
    {
        animator.SetBool("isHurt", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitBox.position, attackRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("in range");
            m_inRange = true;
            animator.SetBool("inRange", m_inRange);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("off range");
            m_inRange = false;
            animator.SetBool("inRange", m_inRange);
        }
    }

}
