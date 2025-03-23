using UnityEngine;

public class RandomChance
{
    public static bool TryCrit(int criticalRate)
    {
        return Random.value*100 < criticalRate;
    }
}
