using UnityEngine;

public class EnemyBehavoir : MonoBehaviour
{
    [SerializeField] Transform hitPostion;
    [SerializeField] Transform hitParticles;

    [SerializeField] HelthBarController helthBarController;
    [SerializeField] float hurtCooldownSet = .5f;

    [Header("Enemy Stats")]
    [SerializeField] float m_moveSpeed;
    [SerializeField] float maxHelth;
    [SerializeField] float m_attackDistance; // Minimum distance for attack
    [SerializeField] float m_cooldownTimer; // time of cooldown between attacks

    [Header("Player Detection")]
    [SerializeField] Transform m_rayCast;
    [SerializeField] float m_rayCastDistance;
    [SerializeField] float m_rayCastLenght;
    [SerializeField] LayerMask m_rayCastMask;

    [Header("GroundDetection")]
    [SerializeField] Transform m_groundDetection;
    [SerializeField] float m_groundhitDistance = 2.0f;
    [SerializeField] LayerMask m_groundLayers;
    RaycastHit2D groundInfo;

    [Header("Knockback")]
    [SerializeField] private Vector2 knockbackSpeed;
    [SerializeField] private float knockbackDuration;

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


    private Vector2 movement;
    private Vector2 touchDamageBotLeft;
    private Vector2 touchDamageTopRight;

    private float lastTouchDamageTime;
    private float knockbackStartTime;


    private float[] attackDetails = new float[2];

    RaycastHit2D m_hit;
    Transform m_target;
    Animator m_animtor;
    float m_distance; // distance between the enemy and player
    bool m_attackMode;
    bool m_inRange;  //check the player in range
    bool m_cooling; // check if the enemy is cooling after attack
    private float m_intTimer;
    private float currentHealth;
    private int damageDirection;

    private int facingDirection = -1;

    private float hurtCooldown;

    Vector2 rayCastDirection = Vector2.left;
    Rigidbody2D rb;

    [Header("DamageInfo")]
    [SerializeField] float damgeTaken;
    [SerializeField] float damgeJasserToPlayer;

    private void Awake()
    {
        m_intTimer = m_cooldownTimer;
        currentHealth = maxHelth;
        hurtCooldown = hurtCooldownSet;

        m_animtor = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        helthBarController.SetHealthAmount(currentHealth, maxHelth);

        groundInfo = Physics2D.Raycast(m_groundDetection.position, Vector2.down, m_groundhitDistance, m_groundLayers);

        if (currentHealth <= 0)
        {
            Die();
        }

        RaycastDebugger();

        //ExtDebug.DrawBoxCastBox(m_rayCast.position, new Vector3(2, 2, 0), Quaternion.Euler(0f, 0f, 180.0f), rayCastDirection, 5, Color.green);
        if (m_inRange)
        {
            //m_hit = Physics2D.Raycast(m_rayCast.position, rayCastDirection, m_rayCastLenght, m_rayCastMask);
            m_hit = Physics2D.BoxCast(m_rayCast.position, new Vector2(2, 2), 0.0f, rayCastDirection, 5, m_rayCastMask);
        }

        if (m_hit.collider != null && groundInfo.collider == true)
        {
            EnemyLogic();
        }
        else if (m_hit.collider == null)
        {
            m_inRange = false;
        }

        if (m_inRange == false || groundInfo.collider == false)
        {
            m_animtor.SetBool("canWalk", false);
            StopAttack();
        }

        if (m_animtor.GetBool("isHurt") == true)
        {
            hurtCooldown -= Time.deltaTime;
        }

        if (hurtCooldown <= 0)
        {
            m_animtor.SetBool("isHurt", false);
            hurtCooldown = hurtCooldownSet;
        }


        CheckTouchDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_target = collision.gameObject.transform;
            m_inRange = true;
            Flip();
        }
    }

    void EnemyLogic()
    {
        m_distance = Vector2.Distance(transform.position, m_target.transform.position);

        if (m_distance > m_attackDistance)
        {
            Move();
            StopAttack();
        }
        else if (m_attackDistance >= m_distance && m_cooling == false)
        {
            Attack();
        }

        if (m_cooling)
        {
            Cooldown();
            m_animtor.SetBool("Attack", false);
        }
    }

    void Move()
    {
        m_animtor.SetBool("canWalk", true);


        if (!m_animtor.GetCurrentAnimatorStateInfo(0).IsName("Jester_Attack_anim"))
        {
            Debug.Log("Move");
            Vector2 targetPosition = new Vector2(m_target.position.x, this.transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, m_moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        m_cooldownTimer = m_intTimer;
        m_attackMode = true;

        m_animtor.SetBool("canWalk", false);
        m_animtor.SetBool("Attack", true);
    }

    void Cooldown()
    {
        m_cooldownTimer -= Time.deltaTime;

        if (m_cooldownTimer <= 0 && m_cooling && m_attackMode)
        {
            m_cooling = false;
            m_cooldownTimer = m_intTimer;
        }
    }

    void StopAttack()
    {
        m_cooling = false;
        m_attackMode = false;

        m_animtor.SetBool("Attack", false);
    }

    void RaycastDebugger()
    {
        if (m_distance > m_attackDistance)
        {
            Debug.DrawRay(m_rayCast.position, rayCastDirection * m_rayCastLenght, Color.red);
        }
        else if (m_attackDistance > m_distance)
        {
            Debug.DrawRay(m_rayCast.position, rayCastDirection * m_rayCastLenght, Color.green);
        }
    }

    public void TriggerCooling()
    {
        m_cooling = true;
    }

    void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > m_target.position.x)
        {
            rotation.y = 0.0f;
            rayCastDirection = Vector2.left;
            facingDirection = -1;
        }
        else
        {
            rotation.y = 180.0f;
            rayCastDirection = Vector2.right;
            facingDirection = 1;
        }

        transform.eulerAngles = rotation;
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(hitPostion.position, m_rayCastDistance, m_rayCastMask);

        attackDetails[0] = damgeJasserToPlayer;
        attackDetails[1] = transform.position.x;
        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", attackDetails);
            //Instantiate hit particle
        }
    }

   private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0] * damgeTaken;
        Instantiate(hitParticle, hitParticles.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        m_animtor.SetBool("isHurt", true);

        if (attackDetails[1] > transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }
        EnterKnockbackState();
    }

    private void EnterKnockbackState()
    {
        // knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        rb.AddForce(movement);
    }

    private void Die()
    {
        Debug.Log("Die");
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
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


        Gizmos.DrawWireSphere(hitPostion.position, m_rayCastDistance);
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
