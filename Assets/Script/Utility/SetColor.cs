using UnityEngine;

public static class SetColor 
{
    public static Color SetRareColor(RareColor color)
    {
        if(color == RareColor.White){
            return Color.white;
        }else if(color == RareColor.Blue)
        {
            return new Color(50f / 255f, 161f / 255f, 226f / 255f);
        }else if(color == RareColor.Orange)
        {
            return new Color(1f, 0.5f, 0f, 1f); // Cam đậm
        }else
        {
            return Color.red;
        }
    }
    public static Color SetElementColor(BulletElement bulletElement)
    {
        if(bulletElement == BulletElement.NoElement){
            return Color.red;
        } else if (bulletElement == BulletElement.Fire)
        {
            return new Color(1f, 0.1f, 0f); 
        } else if (bulletElement == BulletElement.Frozen)
        {
            return Color.blue;
        } else if (bulletElement == BulletElement.Lightning)
        {
            return Color.yellow;
        } else
        {
            return Color.green; // Poison
        }
    }
}
