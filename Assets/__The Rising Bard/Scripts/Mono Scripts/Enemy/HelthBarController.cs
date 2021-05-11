
using UnityEngine;
using UnityEngine.UI;

public class HelthBarController : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Color low;
    [SerializeField] Color hight;
    [SerializeField] Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

    public void SetHealthAmount(float health , float maxHealth)
    {
        slider.value = health;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponent<Image>().color = Color.Lerp(low, hight, slider.normalizedValue);
    }
}
