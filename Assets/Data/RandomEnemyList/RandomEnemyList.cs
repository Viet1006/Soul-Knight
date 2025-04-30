using System.Collections.Generic;
using UnityEngine;
// Lưu các thông số để spawn 1 enemy với data ,cách tấn công , vũ khí khác nhau
// Có nhiều loại enemy mỗi enemy có các cách tấn công khác nhau , mỗi cách tấn công có nhiều loại vũ khí khác nhau
// Random dựa trên vũ khí để tỉ lệ xuất hiện của mọi cũ khí là như nhau
[CreateAssetMenu(fileName = "NewRandomEnemyList", menuName = "RandomEnemyList")]
public class RandomEnemyList : ScriptableObject
{
    public List<AttackMethod> attackMethods;
    public AttackMethod GetRandomAttackMethod()
    {
        int totalWeight = GetNumberOfWeaponInList(); // Tổng trọng số để random là tổng số vũ khí trong danh sách này
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
    int GetNumberOfWeaponInList()
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
        switch (attackMethod)
        {
            case AttackMethodEnum.BowWeapon:
                return typeof(EnemyAttackWithBow);
            case AttackMethodEnum.ContinuousWeapon:
                return typeof(EnemyUseContinuousWeapon);
            default:
                return typeof(EnemyUseSingleWeapon);
        }
    }
    public int GetNumberOfWeapon()
    {
        return weaponList.Count;
    }
    public GameObject GetRandomWeapon(Transform parent) // Lấy random 1 vũ khí trong loại này
    {
        GameObject newWeapom = WeaponPool.Instance.GetWeapon(weaponList[Random.Range(0, weaponList.Count)],parent);
        newWeapom.transform.localRotation = Quaternion.identity; // Đặt lại góc quay
        return newWeapom;
    }
}