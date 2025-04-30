using System.Collections.Generic;
using UnityEngine;
public class BulletPool
{
    static BulletPool instance;
    public static BulletPool Instance
    {
        get
        {
            instance ??= new BulletPool();
            return instance;
        }
    }
    Dictionary<string, Queue<GameObject>> poolDictionary = new ();
    private void AddNewPool(GameObject newBullet)
    {
        if (poolDictionary.ContainsKey(newBullet.name)) return; // Đã có pool này
        Queue<GameObject> newPools = new ();
        poolDictionary.Add(newBullet.name,newPools);
    }
    public GameObject GetBullet(GameObject newBullet,Vector2 pos,Quaternion quaternion)
    {
        if (!poolDictionary.ContainsKey(newBullet.name)) AddNewPool(newBullet);
        GameObject bullet;
        if (poolDictionary[newBullet.name].Count > 0)
        {
            bullet = poolDictionary[newBullet.name].Dequeue();
            bullet.SetActive(true);
        }
        else bullet = Object.Instantiate(newBullet);
        bullet.transform.SetPositionAndRotation(pos, quaternion);
        return bullet;
    }
    public GameObject GetBullet(GameObject newBullet,Vector2 pos,Vector2 target)
    {
        if (!poolDictionary.ContainsKey(newBullet.name)) AddNewPool(newBullet);
        GameObject bullet;
        if (poolDictionary[newBullet.name].Count > 0)
        {
            bullet = poolDictionary[newBullet.name].Dequeue();
            bullet.SetActive(true);
            
        }
        else bullet = Object.Instantiate(newBullet);
        bullet.transform.position = pos;
        bullet.transform.right = target-(Vector2)bullet.transform.position;
        return bullet;
    }
    public GameObject GetBullet(GameObject newBullet,Vector2 pos)
    {
        if (!poolDictionary.ContainsKey(newBullet.name)) AddNewPool(newBullet);
        GameObject bullet;
        if (poolDictionary[newBullet.name].Count > 0)
        {
            bullet = poolDictionary[newBullet.name].Dequeue();
            bullet.SetActive(true);
        }
        else bullet = Object.Instantiate(newBullet);
        bullet.transform.position = pos;
        return bullet;
    }
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        poolDictionary[bullet.name.Replace("(Clone)", "").Trim()].Enqueue(bullet);
    }
}