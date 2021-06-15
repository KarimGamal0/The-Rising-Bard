using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashHandlerUI : MonoBehaviour
{
    // Start is called before the first frame update
    Image image;
    private void Start()
    {
         image = GetComponent<Image>();
        image.enabled = false;

    }
    public void CanDashEffect()
    {

        StartCoroutine("imageFader");
    }

    public void CantDashEffect()
    {
        StopCoroutine("imageFader");
        image.color = new Color32(135, 135,135, 135);// (byte)(Mathf.Sin(255f)


    }
    IEnumerator imageFader()
    {
        while (true)
        {
            Debug.Log("dashcalled");
           image.color = new Color32(255, 255, 255,255);// (byte)(Mathf.Sin(255f)
            yield return new WaitForSeconds(1f);
           
        }

    }
    public void showDashPanel()
    {
        image.enabled = true;
    }

}
