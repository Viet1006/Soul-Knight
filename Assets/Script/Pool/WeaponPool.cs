using System.Collections.Generic;
using UnityEngine;

public class WeaponPool
{
    static WeaponPool instance;
    public static WeaponPool Instance
    {
        get
        {
            instance ??= new WeaponPool();
            return instance;
        }
    }
    Dictionary<string, Queue<GameObject>> poolDictionary = new();
    private void AddNewPool(GameObject newWeapon)
    {
        if (poolDictionary.ContainsKey(newWeapon.name)) return; // Đã có pool này
        Queue<GameObject> newPools = new ();
        poolDictionary.Add(newWeapon.name,newPools);
    }
    public GameObject GetWeapon(GameObject newWeapon,Transform parent)
    {
        if (!poolDictionary.ContainsKey(newWeapon.name))
        {
            AddNewPool(newWeapon);
        }
        GameObject weapon;
        if (poolDictionary[newWeapon.name].Count > 0)
        {
            weapon = poolDictionary[newWeapon.name].Dequeue();
            weapon.SetActive(true);
        }
        else weapon= Object.Instantiate(newWeapon);
        weapon.transform.SetParent(parent);
        weapon.transform.localPosition = Vector2.zero;
        return weapon;
    }
    public void ReturnWeapon(GameObject weapon)
    {
        weapon.SetActive(false);
        poolDictionary[weapon.name.Replace("(Clone)", "").Trim()].Enqueue(weapon);
    }
}
