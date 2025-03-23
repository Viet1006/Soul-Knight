using System.Collections.Generic;
using UnityEngine;
public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;
    Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
    void Awake()
    {
        instance = this;
    }
    private void AddNewPool(GameObject newBullet)
    {
        if (poolDictionary.ContainsKey(newBullet.name)) return; // Đã có pool này
        Queue<GameObject> newPools = new Queue<GameObject>();
        GameObject bullet = Instantiate(newBullet);
        bullet.SetActive(false);
        newPools.Enqueue(bullet);
        poolDictionary.Add(newBullet.name,newPools);
    }
    public GameObject GetBullet(GameObject newBullet,Vector2 pos,Quaternion quaternion)
    {
        if (!poolDictionary.ContainsKey(newBullet.name))
        {
            AddNewPool(newBullet);
        }
        if (poolDictionary[newBullet.name].Count > 0)
        {
            GameObject bullet = poolDictionary[newBullet.name].Dequeue();
            bullet.SetActive(true);
            bullet.transform.SetPositionAndRotation(pos, quaternion);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(newBullet);
            bullet.transform.SetPositionAndRotation(pos, quaternion);
            return bullet;
        }
    }
    public GameObject GetBullet(GameObject newBullet,Vector2 pos,Vector2 target)
    {
        if (!poolDictionary.ContainsKey(newBullet.name))
        {
            AddNewPool(newBullet);
        }
        if (poolDictionary[newBullet.name].Count > 0)
        {
            GameObject bullet = poolDictionary[newBullet.name].Dequeue();
            bullet.SetActive(true);
            bullet.transform.position = pos;
            bullet.transform.right = target-(Vector2)bullet.transform.position;
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(newBullet);
            bullet.transform.position = pos;
            bullet.transform.right = target-(Vector2)bullet.transform.position;
            return bullet;
        }
    }
    public GameObject GetBullet(GameObject newBullet,Vector2 pos)
    {
        if (!poolDictionary.ContainsKey(newBullet.name))
        {
            AddNewPool(newBullet);
        }
        if (poolDictionary[newBullet.name].Count > 0)
        {
            GameObject bullet = poolDictionary[newBullet.name].Dequeue();
            bullet.SetActive(true);
            bullet.transform.position = pos;
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(newBullet);
            bullet.transform.position = pos;
            return bullet;
        }
    }
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        poolDictionary[bullet.name.Replace("(Clone)", "").Trim()].Enqueue(bullet);
    }
}