using System.Collections.Generic;
using UnityEngine;

public class TextDamePool : MonoBehaviour
{
    public static TextDamePool instance;
    void Awake()
    {
        instance = this;
    }
    Queue<GameObject> textDamePool = new();
    [SerializeField] List<Sprite> iconImage;
    [SerializeField] private GameObject textDamePrefab;
    public GameObject GetTextDamage(Vector2 Pos,BulletElement bulletElement,int damage)
    {
        GameObject newTextDamage;
        if(textDamePool.Count >0)
        {
            newTextDamage = textDamePool.Dequeue();
            newTextDamage.SetActive(true);
        }else newTextDamage = Instantiate(textDamePrefab);
        newTextDamage.GetComponent<TextDamage>().SetText(damage,SetColor.SetElementColor(bulletElement),IconElement.GetIcon(bulletElement));
        newTextDamage.transform.position = Pos;
        return newTextDamage;
    }
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        textDamePool.Enqueue(obj);
    }
}
