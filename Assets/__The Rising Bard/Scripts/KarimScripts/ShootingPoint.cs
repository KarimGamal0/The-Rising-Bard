using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPoint : MonoBehaviour
{
    [SerializeField] Transform m_player;
    [SerializeField] float m_offset;

    [SerializeField] GameObject m_projectile;

    float m_timeBetweenShots;
    [SerializeField] float m_startTimeBetweenShots;
    

    void Update()
    {
        Vector3 direction = m_player.position - transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + m_offset);

        if(m_timeBetweenShots <= 0)
        {
            Instantiate(m_projectile, transform.position, transform.rotation);
            m_timeBetweenShots = m_startTimeBetweenShots;
        }
        else
        {
            m_timeBetweenShots -= Time.deltaTime;
        }
    }
}
