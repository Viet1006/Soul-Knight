using UnityEngine;

public static class SetColor 
{
    public static Color SetRareColor(RareColor color)
    {
        if(color == RareColor.White){
            return Color.white;
        }else if(color == RareColor.Green)
        {
            return Color.green;
        }else if(color == RareColor.Blue)
        {
            return new Color(50f / 255f, 161f / 255f, 226f / 255f);

        }else if(color == RareColor.Purple)
        {
            return new Color(0.5f, 0f, 0.5f, 1f); // Màu tím tương đươngrgb(127, 3, 127);
        }else if(color == RareColor.Orange)
        {
            return new Color(1f, 0.1f, 0f, 1f); // Cam đậm

        }else if(color == RareColor.Red)
        {
            return Color.red;
        }else
        {
            return Color.magenta;
        }
    }
    public static Color SetElementColor(BulletElements bulletElements)
    {
        if(bulletElements == BulletElements.NoElement){
            return Color.red;
        } else if (bulletElements == BulletElements.Fire)
        {
            return new Color(1f, 0.5f, 0f); 
        } else if (bulletElements == BulletElements.Frozen)
        {
            return Color.blue;
        } else if (bulletElements == BulletElements.Lightning)
        {
            return Color.yellow;
        } else
        {
            return Color.green; // Poison
        }
    }
}
