using System.Collections.Generic;
using UnityEngine;

public class IconEffectPool : MonoBehaviour
{
    public static IconEffectPool instance;
    Queue<GameObject> iconPool = new();
    [SerializeField] List<Sprite> iconSprite;
    [SerializeField] private GameObject iconPrefab;
    private void Awake()
    {
        instance = this;
    }
    public GameObject GetIconEffect(Vector2 Pos,BulletElement bulletElement, Transform parent)
    {
        GameObject newIcon;
        if(iconPool.Count >0)
        {
            newIcon = iconPool.Dequeue();
            newIcon.SetActive(true);
        }else newIcon = Instantiate(iconPrefab);
        newIcon.GetComponent<SpriteRenderer>().sprite = GetIcon(bulletElement);
        newIcon.transform.SetParent(parent);
        newIcon.transform.localPosition = Pos;
        return newIcon;
    }
    public void ReTurnToPool(GameObject icon)
    {
        icon.SetActive(false);
        iconPool.Enqueue(icon);
    }
    Sprite GetIcon(BulletElement bulletElements)
    {
        return bulletElements switch
        {
            BulletElement.Fire => iconSprite[0],
            BulletElement.Frozen=> iconSprite[1],
            BulletElement.Lightning=> iconSprite[2],
            BulletElement.Poison=> iconSprite[3],
            _ => null,
        };
    }
}
