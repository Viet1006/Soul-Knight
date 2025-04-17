using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public abstract class WeaponUIManager : ItemManagement , ISetSelectingWeapon , ICanInteract
{
    public Image selectingWeaponImage;
    [SerializeField] Image panel;
    public TextMeshProUGUI AtkText;
    public TextMeshProUGUI MpText;
    public TextMeshProUGUI CritText;
    public TextMeshProUGUI PoiText; // Độ lệch
    protected GameObject selectingWeaponPrefab;
    protected override void Start()
    {
        base.Start();
        panel.gameObject.SetActive(true);
    }
    public virtual void SetSelectingSlot(GameObject weaponPrefab) // Set các thuộc tính cho ô đang chọn
    {
        SetSelectingWeapon(weaponPrefab);
        WeaponData weaponData = weaponPrefab.GetComponent<BaseWeapon>().weaponData;
        
        AtkText.text = weaponData.damage.ToString();
        MpText.text = weaponData.energyCost.ToString();
        CritText.text = weaponData.critChance.ToString();
        PoiText.text = weaponData.inaccuracy.ToString();
    }
    public void AddNewSlot(GameObject weaponPrefab ) // Thêm weaponSlot mới với các weapon tham chiếu đến prefab
    {
        Instantiate(itemBorder,content).GetComponent<WeaponSlot>().SetWeaponSlot(weaponPrefab,this);
    }
    public void DeleteSlot(string weaponName) // xóa weaponSlot 
    {
        foreach (Transform child in content)
        {
            if (child.GetComponent<WeaponSlot>().itemName.text == weaponName)
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }
    protected GameObject FindWeaponPreFab(string name) // Tìm tham chiếu tới prefab trong all weapon prefab
    {
        foreach (var weapon in ObjectHolder.instance.allWeaponPrefab)
        {
            if (weapon.name == name)
            {
                return weapon;
            }
        }
        return null;
    }
    public override void Interact()
    {
        appearTween.PlayForward();
        UIManageShowAndHide.instance.OpenShop();
        DOVirtual.DelayedCall(0.1f,() => panel.enabled = true); // Bật panel sau 0,3s
        SetSelectingWeapon(null); // Đặt selectingWeaponPrefab về null
        SetInterractButtons( true); // bật tất cả các button
    }
    public void Close()
    {
        appearTween.PlayBackwards();
        DOVirtual.DelayedCall(0.4f,()=> panel.enabled = false); // Tắt panel sau 0.4s
        UIManageShowAndHide.instance.CloseShop();
    }
    public void SetSelectingWeapon(GameObject selectingWeaponPrefab) // Set hình ảnh đang chọn
    {
        this.selectingWeaponPrefab = selectingWeaponPrefab;
        if (selectingWeaponPrefab != null)
        {
            selectingWeaponImage.sprite = selectingWeaponPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
            selectingWeaponImage.enabled = true;
        } else
        {
            selectingWeaponImage.sprite = null;
            selectingWeaponImage.enabled = false;
        }
    }
}
