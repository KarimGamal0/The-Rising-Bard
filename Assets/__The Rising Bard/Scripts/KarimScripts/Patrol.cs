using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] float m_speed;
    [SerializeField] Transform[] m_moveSpots;

    int randomSpot;
    float m_waitTime;
    float m_startWaitTime;

    void Start()
    {
        m_waitTime = m_startWaitTime;
        randomSpot = Random.Range(0, m_moveSpots.Length);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_moveSpots[randomSpot].position, m_speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, m_moveSpots[randomSpot].position) < 0.2f)
        {
            if(m_waitTime <= 0)
            {
                randomSpot = Random.Range(0, m_moveSpots.Length);
                m_waitTime = m_startWaitTime;
            }
            else
            {
                m_waitTime -= Time.deltaTime;
            }
        }

    }
}
