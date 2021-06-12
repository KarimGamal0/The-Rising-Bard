using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float m_speed;
    [SerializeField] float m_lifeTime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    void Start()
    {
        Invoke("DestroyProjectile", m_lifeTime);
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                //Player Damage
            }
            DestroyProjectile();
        }

        transform.Translate(Vector2.up * m_speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
