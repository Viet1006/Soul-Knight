using UnityEngine;

public class PlayerWeapon : MonoBehaviour , ICanSelect
{
    SpriteRenderer sprite;
    Collider2D weaponCollider;
    GameObject selectedItemName;
    void Awake()
    {
        selectedItemName = transform.Find("SelectedItemName").gameObject;
        TextMesh textMesh = selectedItemName.GetComponent<TextMesh>();
        textMesh.text = name;
        textMesh.color = SetColor.SetRareColor(GetComponent<BaseWeapon>().weaponData.rareColor);
        weaponCollider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        if(!sprite) sprite = GetComponentInChildren<SpriteRenderer>();
    }
    public void PickUp(Transform parent) // Nhặt vũ khí
    {
        transform.SetParent(parent);
        weaponCollider.enabled = false;
        transform.localPosition = Vector2.zero;
        transform.rotation = Quaternion.identity;
        GetWeapon();
    }
    public void Drop() // Thả vũ khí
    {
        transform.parent = null;
        weaponCollider.enabled= true;
        transform.rotation = Quaternion.identity;
    }
    public virtual void GetWeapon() // lấy vũ khí
    {
        sprite.sortingOrder = 4;
        transform.localRotation = Quaternion.identity;
    }
    public void PutAwayWeapon() // Cất vũ khí
    {
        sprite.sortingOrder = 2;
        transform.localRotation = Quaternion.Euler(0,180,-20);
    } 
    public void HideSelectObject()
    {
        selectedItemName.SetActive(false);
    }

    public void ShowSelectObject()
    {
        selectedItemName.SetActive(true);
    }
}
