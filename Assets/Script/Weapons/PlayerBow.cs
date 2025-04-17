using UnityEngine;

public class PlayerBow : PlayerWeapon
{
    public SpriteRenderer currentSquare;
    public void OnAttack()
    {
        currentSquare = ChargingBar.instance.GetSquare(0);
        ChargingBar.instance.gameObject.SetActive(true);
        ChargingBar.instance.target = transform;
    }
    public void OnCharging(float timeToNextLevelCharge , float timeForEachLevelCharge)
    {
        currentSquare.color = Color.Lerp(Color.black,Color.white,1-timeToNextLevelCharge/timeForEachLevelCharge);
    }
    public void ResetAllSquare()
    {
        for (int i = 0 ; i< 5 ;i++)
        {
            ChargingBar.instance.GetSquare(i).color = Color.black;
            ChargingBar.instance.gameObject.SetActive(false);
        }
    }
}
