using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] Image helth;
    [SerializeField] Image mana;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        helth.fillAmount = (playerData.playerHP / 100.0f);
        mana.fillAmount = (playerData.playerMana / 100.0f);
    }
}
