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
    Queue<GameObject> iconPool = new();
    public GameObject GetIconEffect(Vector2 Pos,BulletBuffType buffType, Transform parent)
    {
        GameObject newIcon;
        if(iconPool.Count >0)
        {
            newIcon = iconPool.Dequeue();
            newIcon.SetActive(true);
        }else newIcon = Object.Instantiate(ObjectHolder.Instance.iconEffect);
        newIcon.GetComponent<SpriteRenderer>().sprite = GetIcon(buffType);
        newIcon.transform.SetParent(parent);
        newIcon.transform.localPosition = Pos;
        return newIcon;
    }
    public void ReTurnToPool(GameObject icon)
    {
        icon.SetActive(false);
        iconPool.Enqueue(icon);
    }
    Sprite GetIcon(BulletBuffType buffType) => ObjectHolder.Instance.iconSprite[buffType];
}
