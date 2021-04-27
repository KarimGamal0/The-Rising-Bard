using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : MonoBehaviour
{
    [SerializeField] PlayerData PlayerData;
    float movingSpeed = 1 ;


    // Update is called once per frame
    void Update()
    {
        if (!PlayerData.abilities[4].abilityActive)
        {

            GetComponent<Rigidbody2D>().velocity = new Vector2(movingSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
