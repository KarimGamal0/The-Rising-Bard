using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTina : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    Transform player;

    [SerializeField] Slider healthSlider;

    [SerializeField] float attackRadius;
    [SerializeField] Transform hitBox;
    [SerializeField] LayerMask playersMask;
    [SerializeField] float damageAmout;

    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject Bullet;

    float[] attackDetails = new float[2];

    float maxHealth = 100;
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
        if (currentHealth <= 50 && m_stateTwoPlayed == false)
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

    public void Shoot()
    {

        Instantiate(Bullet, shootingPoint.position - new Vector3(Random.Range(0f, 5f), Random.Range(0f, 3f), 0), shootingPoint.rotation, shootingPoint);
        Instantiate(Bullet, shootingPoint.position - new Vector3(Random.Range(0f, 5f), 0, 0), shootingPoint.rotation, shootingPoint);
        Instantiate(Bullet, shootingPoint.position + new Vector3(Random.Range(0f, 5f), 0, 0), shootingPoint.rotation, shootingPoint);
        Instantiate(Bullet, shootingPoint.position + new Vector3(Random.Range(0f, 5f), Random.Range(0f, 3f), 0), shootingPoint.rotation, shootingPoint);

    }

    public void Damage(float[] playerAttackDetails)
    {
        Debug.Log("Damage Tina");
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
