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
    Queue<GameObject> textDamePool = new();
    public GameObject GetTextDamage(Vector2 pos,BulletElement bulletElement,int damage)
    {
        GameObject newTextDamage;
        if(textDamePool.Count >0)
        {
            newTextDamage = textDamePool.Dequeue();
            newTextDamage.SetActive(true);
        }else newTextDamage = Object.Instantiate(ObjectHolder.Instance.textDamePrefab);
        newTextDamage.GetComponent<TextDamage>().SetText(damage,SetColor.SetElementColor(bulletElement),IconElement.GetIcon(bulletElement));
        newTextDamage.transform.position = pos;
        return newTextDamage;
    }
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        textDamePool.Enqueue(obj);
    }
}
