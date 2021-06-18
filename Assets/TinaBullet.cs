using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinaBullet : MonoBehaviour
{

    [SerializeField] float m_lifeTime;
    public float distance;
    public int damage;

    public LayerMask whatIsSolid;
    [SerializeField] float damgeAmountToPlayer;

    private float[] attackDetails = new float[2];
    void Start()
    {
        Invoke("DestroyProjectile", m_lifeTime);
        attackDetails[0] = damgeAmountToPlayer;
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                //Player Damage
                attackDetails[1] = transform.position.x;
                hitInfo.collider.SendMessage("Damage", attackDetails);
            }
            DestroyProjectile();
        }

    }



    void DestroyProjectile()
    {
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
