using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image fillImage;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealthBarChange(float currentValue, float maxHealth)
    {
        slider.value = currentValue / maxHealth;
    }

    public void HealthBarColorChange(Color color)
    {
        fillImage.color = color;
    }
}
