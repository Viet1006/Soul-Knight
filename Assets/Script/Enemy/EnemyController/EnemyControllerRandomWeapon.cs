using UnityEngine;
public class EnemyControllerRandomWeapon : EnemyController
{
    public RandomEnemyList randomEnemyList;
    public override int InitEnemy()
    {
        AttackMethod attackMethod = randomEnemyList.GetRandomAttackMethod(); // Lấy random cách tấn công từ list
        this.attackMethod = gameObject.AddComponent(attackMethod.GetAttackMethod()).GetComponent<AttackMethodEnemy>(); // Thêm cách tấn công cho enemy
        
        GameObject weaponObject = attackMethod.GetRandomWeapon(transform); // Lấy random vũ khí từ cách tấn công
        // Lấy tham chiếu đến weapon trong AttackMethod xong đó gán cho weaponobject lấy được
        GetComponent<EnemyWithWeapon>().weapon = weaponObject.GetComponent<BaseWeapon>();
        return base.InitEnemy();
    }
    public override void ResetToOringin()
    {
        base.ResetToOringin();
        Destroy(GetComponent<EnemyWithWeapon>());
    }
}