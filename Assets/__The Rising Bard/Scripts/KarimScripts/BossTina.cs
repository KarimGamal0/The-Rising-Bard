using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTina : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    Transform player;

    [SerializeField] HelthBarController healthSlider;

    [SerializeField] float attackRadius;
    [SerializeField] Transform hitBox;
    [SerializeField] LayerMask playersMask;
    [SerializeField] float damageAmout;

    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject Bullet;

    float[] attackDetails = new float[2];

    [SerializeField] float maxHealth = 100;
    float currentHealth;

    bool m_inRange;
    bool m_stateTwoPlayed = false;


    Vector2 direction;

    [Header("Death")]
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject deathChunkParticle;
    [SerializeField] private GameObject deathBloodParticle;



    [Header("Player touch")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform touchDamageCheck;
    [SerializeField] private float touchDamageCooldown;
    [SerializeField] private float touchDamage;
    [SerializeField] private float touchDamageWidth;
    [SerializeField] private float touchDamageHeight;

    private Vector2 touchDamageBotLeft;
    private Vector2 touchDamageTopRight;

    private float lastTouchDamageTime;

    [SerializeField] float damgeTaken;

    void Start()
    {
        currentHealth = maxHealth;

        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        healthSlider.SetHealthAmount(currentHealth, maxHealth);

        if (currentHealth <= 50 && m_stateTwoPlayed == false)
        {
            Debug.Log("state 2 change");
            animator.SetBool("isStateTwo", true);

            //animator.Play("Rapiada_State_Change_anim");
            m_stateTwoPlayed = true;
        }

        CheckTouchDamage();

        Debug.Log(animator.GetBool("isStateTwo"));
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
        currentHealth -= playerAttackDetails[0] * damgeTaken;

        animator.SetTrigger("isHurt");
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            Die();
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


    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
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

    public Vector2 GetDirection()
    {
        return direction;
    }

    private void CheckTouchDamage()
    {

        if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {

            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);
            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitBox.position, attackRadius);

        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }
}
