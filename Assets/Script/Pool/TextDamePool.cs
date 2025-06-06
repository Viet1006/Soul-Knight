using System.Collections.Generic;
using UnityEngine;

public class TextDamePool
{
    static TextDamePool instance;
    public static TextDamePool Instance
    {
        get
        {
            instance ??= new TextDamePool();
            return instance;
        }
    }
    Queue<TextDamage> textDamePool = new();
    Queue<CritTextControl> critTextPool = new();
    public void GetTextDamage(Vector2 pos,BulletElement bulletElement,int damage , bool isCrit = false)
    {
        if(isCrit)
        {
            CritTextControl critText;
            if(critTextPool.Count >0) critText = critTextPool.Dequeue();
            else critText = Object.Instantiate(ObjectHolder.Instance.critTextPrefab).GetComponent<CritTextControl>();
            critText.SetCritText(pos);
        }
        TextDamage newTextDamage;
        if(textDamePool.Count >0)
        {
            newTextDamage = textDamePool.Dequeue();
            newTextDamage.gameObject.SetActive(true);
        }else newTextDamage = Object.Instantiate(ObjectHolder.Instance.textDamePrefab).GetComponent<TextDamage>();
        newTextDamage.SetText(damage,SetColor.SetElementColor(bulletElement),IconElement.GetIcon(bulletElement));
        newTextDamage.transform.position = pos;
    }
    public void ReturnToPool(TextDamage obj)
    {
        obj.gameObject.SetActive(false);
        textDamePool.Enqueue(obj);
    }
    public void ReturnCritText(CritTextControl obj)
    {
        obj.gameObject.SetActive(false);
        critTextPool.Enqueue(obj);
    }
}
