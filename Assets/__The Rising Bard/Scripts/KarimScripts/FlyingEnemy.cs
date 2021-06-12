using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] float m_speed;
    [SerializeField] Transform[] m_moveSpots;

    int randomSpot;
    float m_waitTime;
    float m_startWaitTime;


    [SerializeField] Transform m_player;
    [SerializeField] GameObject shootingPoint;
    [SerializeField] float m_offset;
    [SerializeField] GameObject m_projectile;
    [SerializeField] float m_startTimeBetweenShots;

    float m_timeBetweenShots;

    bool m_inRange;

    Animator anim;
    void Start()
    {
        m_waitTime = m_startWaitTime;
        randomSpot = Random.Range(0, m_moveSpots.Length);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_moveSpots[randomSpot].position, m_speed * Time.deltaTime);

        Flip();

        if (Vector2.Distance(transform.position, m_moveSpots[randomSpot].position) < 0.2f)
        {
            if (m_waitTime <= 0)
            {
                randomSpot = Random.Range(0, m_moveSpots.Length);
                m_waitTime = m_startWaitTime;
            }
            else
            {
                m_waitTime -= Time.deltaTime;
            }
        }

        if (m_inRange)
        {
            Shoot();
        }
    }

    public void Flip()
    {
        if (m_moveSpots[randomSpot].position.x >= transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void Shoot()
    {
        Vector3 direction = m_player.position - shootingPoint.transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shootingPoint.transform.rotation = Quaternion.Euler(0, 0, rotZ + m_offset);

        if (m_timeBetweenShots <= 0)
        {
            anim.SetBool("isShooting", true);
            Instantiate(m_projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);
            m_timeBetweenShots = m_startTimeBetweenShots;
        }
        else
        {
            m_timeBetweenShots -= Time.deltaTime;
        }
    }

    public void StopShooting()
    {
        anim.SetBool("isShooting", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("sss");
            m_inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_inRange = false;
        }
    }
}
