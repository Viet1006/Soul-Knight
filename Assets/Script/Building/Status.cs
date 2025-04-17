using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class Status : MonoBehaviour , ICanInteract
{
    public static Status instance;
    int health = 50;
    [SerializeField] Slider slider;
    public event System.Action OnGetDamage;
    [SerializeField] GameObject textSelect; 
    [SerializeField] GameObject blinkPanel; // Nháy đỏ khi get damage
    Material blinkMaterial;
    void Awake()
    {
        instance = this;
        slider.maxValue = health;
        slider.value = health;
        blinkMaterial = blinkPanel.GetComponent<Image>().material; 
    }
    public void GetDamage(int damage)
    {
        health -= damage;
        slider.value = health;
        OnGetDamage?.Invoke();
        blinkPanel.SetActive(true);
        blinkMaterial.DOFloat(0.7f,"_EdgeSmooth",0.2f).SetLoops(2,LoopType.Yoyo).OnComplete(() => blinkPanel.SetActive(false));
        if(health <= 0)
        {
            health =0;
            slider.value = health;
            Debug.Log("You lose");
        }
    }
    public void Interact()
    {
        if(ManageSpawnEnemy.instance.IsFinishWave()) 
        {
            ManageSpawnEnemy.instance.timeWaveRemain = 0;
        }
    }
    public void ShowSelectObject()
    {
        if(ManageSpawnEnemy.instance.IsFinishWave()) textSelect.SetActive(true);
    }
    public void HideSelectObject() => textSelect.SetActive(false);
}
