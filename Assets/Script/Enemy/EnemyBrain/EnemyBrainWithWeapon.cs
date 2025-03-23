using System.Collections;
public class EnemyBrainWithWeapon : EnemyBrain
{
    protected override IEnumerator DieIEnum()
    {
        GetComponent<EnemyWithWeapon>().weapon.ResetToOringin();
        return base.DieIEnum();
    }
}