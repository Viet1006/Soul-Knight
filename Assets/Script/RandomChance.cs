using UnityEngine;

public class RandomChance
{
    public static bool RollChance(int chance)
    {
        return Random.value*100 < chance; // Trả về true nếu thành công
    }
}
