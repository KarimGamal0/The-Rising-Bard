using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform respawnPoint;
    public GameObject playerPrefab;
    public CinemachineVirtualCameraBase cam;

    [SerializeField] float spwanDelaySet = 1;

    [SerializeField] private PlayerData PD;

    private float spwanDelay;
    private bool isDead = false;

    private void Start()
    {
        spwanDelay = spwanDelaySet;
    }
    private void Update()
    {
        if (isDead || PD.playerHP <= 0)
        {
            spwanDelay -= Time.deltaTime;
        }
        if (spwanDelay < 0)
        {
            Respawn();
            spwanDelay = spwanDelaySet;
            isDead = false;
        }

    }
    public void Respawn()
    {
      GameObject player=  Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        cam.Follow = player.transform;
    }


    public void Dead()
    {
        isDead = true;

    }


}
