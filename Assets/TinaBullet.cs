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

    RaycastHit2D hitInfo;
    void Start()
    {
        Invoke("DestroyProjectile", m_lifeTime);
        attackDetails[0] = damgeAmountToPlayer;
    }

    void Update()
    {
        hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hitInfo = Physics2D.Raycast(transform.position, -transform.up, distance, whatIsSolid);
        hitInfo = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0.0f, -transform.up, distance, whatIsSolid);
        if (collision.gameObject.tag == "Player")
        {
            attackDetails[1] = transform.position.x;
            hitInfo.collider.SendMessage("Damage", attackDetails);
            DestroyProjectile();
        }

        if (collision.transform.tag == "Ground")
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
