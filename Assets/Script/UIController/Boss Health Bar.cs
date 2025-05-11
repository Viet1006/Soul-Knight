using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public static BossHealthBar instance;
    [SerializeField] Slider slider;
    void Awake()
    {
        instance= this;
        gameObject.SetActive(false);
    }
    public void SetHealth(int health)
    {
        
        if(health<=0)
        {
            slider.gameObject.SetActive(false);
            slider.value = 0;
            return;
        }
        slider.value = health;
    }
    public void SetMaxValue(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
}
