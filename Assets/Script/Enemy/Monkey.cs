
using UnityEngine;

public class Monkey : BaseEnemy
{
    
    public override void GetHit(float damage,Color colorDamage)
    {
        GameObject newTextDamage =  Instantiate(textDamage);
        newTextDamage.GetComponent<TextDamage>().SetText(damage,colorDamage);
    }
}
