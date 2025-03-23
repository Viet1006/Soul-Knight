using System.Collections.Generic;
using UnityEngine;
// Lưu các thông số để spawn 1 enemy với cách di chuyển , tấn công , vũ khí khác nhau
// Có nhiều loại enemy mỗi enemy có các cách tấn công khác nhau , mỗi cách tấn công có nhiều loại vũ khí khác nhau
// Random dựa trên vũ khí để tỉ lệ xuất hiện của mọi cũ khí là như nhau
[CreateAssetMenu(fileName = "NewRandomEnemyList", menuName = "RandomEnemyList", order = 5)]
public class RandomEnemyList : ScriptableObject
{
    public List<EnemyType> enemyTypes;
    public EnemyType GetRandomEnemyType()
    {
        int totalWeight = 0;
        foreach (EnemyType enemyType in enemyTypes)
        {
            totalWeight += enemyType.GetNumberOfWeaponInType();
        }
        int randomWeight = Random.Range(1, totalWeight+1);
        foreach (EnemyType enemyType in enemyTypes)
        {
            randomWeight-=enemyType.GetNumberOfWeaponInType(); // trọng số của method này
            if(randomWeight <= 0)
            {
                return enemyType; // trả về cách tấn công vừa random được
            }
        }
        return null;
    }
}
[System.Serializable]
public class EnemyType // Chứa loại enemy , cách di chuyển và random AttackMethod
{
    public EnemyTypeEnum enemyType; // Loại enemy (Đánh gần , đánh xa)
    public List<EnemyData> enemyDatas; // Data của các quái thuộc loại này 
    public List<AttackMethod> attackMethods; // Phương thức tấn công và vũ khí thuộc quái này
    public AttackMethod GetRandomAttackMethod()
    {
        int totalWeight = GetNumberOfWeaponInType(); // Tổng trọng số để random là tổng số vũ khí của EnemyType này (Tổng trọng số của các method)
        int randomWeight = Random.Range(1, totalWeight +1); // Random từ 1 - totalWeight
        foreach (AttackMethod method in attackMethods)
        {
            randomWeight-=method.GetNumberOfWeapon(); // trọng số của method này
            if(randomWeight <= 0)
            {
                return method; // trả về cách tấn công vừa random được
            }
        }
        return null;
    }
    public System.Type GetMovementMethod()
    {
        if(enemyType == EnemyTypeEnum.RangedEnemmy) return typeof(MoveToStatus);
        else return typeof(MeleeEnemyMove);
    }
    public EnemyData GetRandomEnemyData() // Lấy random enemydata
    {
        return enemyDatas[Random.Range(0, enemyDatas.Count)];
    }
    public int GetNumberOfWeaponInType() // trả về số lượng vũ khí của loại enemy này
    {
        int numberOfWeapon = 0;
        foreach (AttackMethod  attackMethod in attackMethods) // Duyệt toàn bộ cách tấn công để lấy số lượng vũ khí của mỗi cách tấn công
        {
            numberOfWeapon += attackMethod.GetNumberOfWeapon();
        }
        return numberOfWeapon;
    }
}
[System.Serializable]
public class AttackMethod
{
    public AttackMethodEnum attackMethod; // Phương thức tấn công
    public List<GameObject> weaponList;  // Các vũ khí phù hợp với cách tấn công này
    public System.Type GetAttackMethod() // Chuyển attackmethod qua monobehaviour để add vào gameobject
    {
        if(attackMethod == AttackMethodEnum.BowWeapon) return typeof(EnemyAttackWithBow);
        else if(attackMethod == AttackMethodEnum.ContinuousWeapon) return typeof(EnemyUseContinuousWeapon);
        else return typeof(EnemyUseSingleWeapon);
    }
    public int GetNumberOfWeapon()
    {
        return weaponList.Count;
    }
    public GameObject GetRandomWeapon() // Lấy random 1 vũ khí trong loại này
    {
        return weaponList[Random.Range(0, weaponList.Count)];
    }
}