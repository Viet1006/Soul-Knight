using System.Collections;
using UnityEngine;
public class EnemyBrainRandomWeapon : EnemyBrain
{
    public RandomEnemyList RandomEnemyList;
    public override int InitEnemy()
    {
        EnemyType enemyType = RandomEnemyList.GetRandomEnemyType(); // Lấy random Enemy type
        enemyData = enemyType.GetRandomEnemyData();
        gameObject.AddComponent(enemyType.GetMovementMethod()); // Thêm cách di chuyển cho loại enemy đấy

        AttackMethod attackMethod = enemyType.GetRandomAttackMethod(); // Lấy random cách tấn công từ loại enemy đấy
        gameObject.AddComponent(attackMethod.GetAttackMethod()); // Thêm cách tấn công cho enemy
        GameObject weaponObject = attackMethod.GetRandomWeapon(); // Lấy random vũ khí từ cách tấn công
        
        // Lấy tham chiếu đến weapon trong AttackMethod xong đó gán cho weaponobject lấy được
        GetComponent<EnemyWithWeapon>().weapon = Instantiate(weaponObject,transform).GetComponent<BaseWeapon>(); 
        return base.InitEnemy();
    }
    protected override IEnumerator DieIEnum()
    {
        Destroy(GetComponent<AttackMethodEnemy>());
        Destroy(GetComponent<MoveToStatus>());
        return base.DieIEnum();
    }
}