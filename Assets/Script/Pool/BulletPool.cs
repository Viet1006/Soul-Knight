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
    Dictionary<string, Queue<MonoBehaviour>> poolDictionary = new ();
    private void AddNewPool(GameObject newBullet)
    {
        if (poolDictionary.ContainsKey(newBullet.name)) return; // Đã có pool này
        Queue<MonoBehaviour> newPools = new (); // Chưa có pool
        poolDictionary.Add(newBullet.name,newPools);
    }
    public T GetBullet<T>(GameObject bulletPrefab, Vector2 pos, Transform parent ,Quaternion quaternion = default ) where T : MonoBehaviour
    {
        if (!poolDictionary.ContainsKey(bulletPrefab.name)) AddNewPool(bulletPrefab);
        T bullet;
        if(poolDictionary[bulletPrefab.name].Count > 0)
        {
            bullet = (T)poolDictionary[bulletPrefab.name].Dequeue();
            bullet.gameObject.SetActive(true);
        }else bullet = Object.Instantiate(bulletPrefab).GetComponent<T>();
        bullet.transform.SetPositionAndRotation(pos, quaternion);
        bullet.transform.SetParent(parent);
        bullet.transform.localScale = Vector2.one;
        return bullet;
    }
    public T GetBullet<T>(GameObject bulletPrefab, Vector2 pos,Quaternion quaternion = default) where T : MonoBehaviour
    {
        if (!poolDictionary.ContainsKey(bulletPrefab.name)) AddNewPool(bulletPrefab);
        T bullet;
        if(poolDictionary[bulletPrefab.name].Count > 0)
        {
            bullet = (T)poolDictionary[bulletPrefab.name].Dequeue();
            bullet.gameObject.SetActive(true);
        }else bullet = Object.Instantiate(bulletPrefab).GetComponent<T>();
        bullet.transform.SetPositionAndRotation(pos, quaternion);
        bullet.transform.localScale = Vector2.one;
        return bullet;
    }
    public T GetBullet<T>(GameObject bulletPrefab, Vector2 pos,Vector2 scale , Quaternion quaternion = default) where T : MonoBehaviour
    {
        if (!poolDictionary.ContainsKey(bulletPrefab.name)) AddNewPool(bulletPrefab);
        T bullet;
        if(poolDictionary[bulletPrefab.name].Count > 0)
        {
            bullet = (T)poolDictionary[bulletPrefab.name].Dequeue();
            bullet.gameObject.SetActive(true);
        }else bullet = Object.Instantiate(bulletPrefab).GetComponent<T>();
        bullet.transform.SetPositionAndRotation(pos, quaternion);
        bullet.transform.localScale = scale;
        return bullet;
    }
    public T GetBullet<T>(GameObject bulletPrefab, Vector2 pos,Vector2 target) where T : MonoBehaviour
    {
        T newbullet = GetBullet<T>(bulletPrefab, pos);
        newbullet.transform.right = target-(Vector2)newbullet.transform.position;
        newbullet.transform.localScale = Vector2.one;
        return newbullet;
    }
    public void ReturnBullet(MonoBehaviour bullet)
    {
        bullet.gameObject.SetActive(false);
        poolDictionary[bullet.name.Replace("(Clone)", "").Trim()].Enqueue(bullet);
    }
    
}