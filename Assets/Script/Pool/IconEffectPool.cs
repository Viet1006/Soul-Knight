using System.Collections.Generic;
using UnityEngine;
public class IconEffectPool
{
    static IconEffectPool instance;
    public static IconEffectPool Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }
    Queue<SpriteRenderer> iconPool = new();
    public SpriteRenderer GetIconEffect(Vector2 Pos,BuffIconEnum buffType, Transform parent)
    {
        SpriteRenderer newIcon;
        if(iconPool.Count >0)
        {
            newIcon = iconPool.Dequeue();
            newIcon.gameObject.SetActive(true);
        }else newIcon = Object.Instantiate(ObjectHolder.Instance.iconEffect).GetComponent<SpriteRenderer>();
        newIcon.sprite = GetIcon(buffType);
        newIcon.transform.SetParent(parent);
        newIcon.transform.localPosition = Pos;
        return newIcon;
    }
    public void ReTurnToPool(SpriteRenderer icon)
    {
        icon.gameObject.SetActive(false);
        iconPool.Enqueue(icon);
    }
    Sprite GetIcon(BuffIconEnum buffType) => ObjectHolder.Instance.iconSprite[buffType];
}
