using System.Collections.Generic;
using UnityEngine;

public class UpgradePrice
{
    static readonly Dictionary<int,PriceEachColor> upgradePrice = new()
    {
        { 0, new PriceEachColor(20, 30, 45, 68) },    // Level 0
        { 1, new PriceEachColor(24, 36, 54, 81) },    // Level 1
        { 2, new PriceEachColor(29, 44, 65, 98) },    // Level 2
        { 3, new PriceEachColor(35, 53, 79, 118) },    // Level 3
        { 4, new PriceEachColor(41, 62, 92, 138) },    // Level 4
        { 5, new PriceEachColor(50, 75, 113, 169) },    // Level 5
        { 6, new PriceEachColor(59, 88, 132, 198) },    // Level 6
        { 7, new PriceEachColor(71, 107, 160, 240) },   // Level 7
        { 8, new PriceEachColor(86, 129, 194, 289) },   // Level 8
        { 9, new PriceEachColor(103, 154, 231, 346) }, // Level 9
    };
    public static int GetUpgradePrice(RareColor rareColor , int level)
    {
        if(level == upgradePrice.Count) 
        return -1;
        switch (rareColor)
        {
            case RareColor.White:
                return upgradePrice[level].whitePrice;
            case RareColor.Blue:
                return upgradePrice[level].bluePrice;
            case RareColor.Orange:
                return upgradePrice[level].orangePrice;
            case RareColor.Red:
                return upgradePrice[level].redPrice;
            default:
                Debug.LogError("Unknown rare color: " + rareColor);
                return 0; // Default fallback
        }
    }
}
[SerializeField]
class PriceEachColor
{
    public int whitePrice;
    public int bluePrice;
    public int orangePrice;
    public int redPrice;
    public PriceEachColor(int whitePrice , int bluePrice , int orangePrice , int redPrice)
    {
        this.whitePrice = whitePrice;
        this.bluePrice = bluePrice;
        this.orangePrice = orangePrice;
        this.redPrice = redPrice;
    }
}
