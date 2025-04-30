using UnityEngine;

public class IconElement
{
    public static Sprite GetIcon(BulletElement bulletElement)
    {
        return bulletElement switch
        {
            BulletElement.Fire => ObjectHolder.Instance.elementIcon[BulletElement.Fire],
            BulletElement.Frozen=> ObjectHolder.Instance.elementIcon[BulletElement.Frozen],
            BulletElement.Lightning => ObjectHolder.Instance.elementIcon[BulletElement.Lightning],
            BulletElement.Poison => ObjectHolder.Instance.elementIcon[BulletElement.Poison],
            _ => null,
        };
    }
}
