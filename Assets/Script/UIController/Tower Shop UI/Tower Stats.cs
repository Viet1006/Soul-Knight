using TMPro;
using UnityEngine;

public class TowerStats : MonoBehaviour
{
    TextMeshProUGUI damage;
    TextMeshProUGUI fireRate;
    TextMeshProUGUI attackRadius;
    UnityEngine.UI.Image elementIcon;
    protected virtual void Awake()
    {
        damage = transform.Find("Damage Line").GetComponentInChildren<TextMeshProUGUI>();
        fireRate = transform.Find("Fire Rate Line").GetComponentInChildren<TextMeshProUGUI>();
        attackRadius = transform.Find("Attack Radius Line").GetComponentInChildren<TextMeshProUGUI>();
        elementIcon = transform.Find("Element Icon").GetComponent<UnityEngine.UI.Image>();
    }
    public virtual void SetTowerStats(TowerData towerData , int level)
    {
        damage.text = towerData.Damage(level).ToString();
        fireRate.text = towerData.FireRate(level).ToString();
        attackRadius.text = towerData.RadiusAttack(level).ToString();
        Sprite iconSprite = IconElement.GetIcon(towerData.element);
        
        if(iconSprite) // Nếu có element thì set
        {
            elementIcon.enabled = true;
            elementIcon.sprite = iconSprite;
        } 
        else elementIcon.enabled = false; // Không thì tắt hình ảnh
    }
}