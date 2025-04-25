using System.Collections.Generic;
using UnityEngine;

public class UpgradePrice
{
    static readonly Dictionary<int,PriceEachColor> upgradePrice = new()
    {
        { 0, new PriceEachColor(20, 30, 35, 40) },    // Level 0
        { 1, new PriceEachColor(22, 34, 39, 47) },    // Level 1
        { 2, new PriceEachColor(25, 38, 44, 55) },    // Level 2
        { 3, new PriceEachColor(28, 43, 50, 64) },    // Level 3
        { 4, new PriceEachColor(32, 49, 57, 74) },    // Level 4
        { 5, new PriceEachColor(36, 56, 65, 85) },    // Level 5
        { 6, new PriceEachColor(41, 64, 74, 98) },    // Level 6
        { 7, new PriceEachColor(47, 73, 84, 113) },   // Level 7
        { 8, new PriceEachColor(54, 83, 96, 130) },   // Level 8
        { 9, new PriceEachColor(62, 95, 110, 150) }, // Level 9
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
