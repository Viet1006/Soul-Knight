using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffectPool 
{
    static ExplodeEffectPool instance;
    public static ExplodeEffectPool Instance
    {
        get
        {
            instance ??= new ExplodeEffectPool();
            return instance;
        }
    }
    Dictionary<string, Queue<GameObject>> poolDictionary = new ();
    private void AddNewPool(GameObject newEffect)
    {
        if (poolDictionary.ContainsKey(newEffect.name)) return; // Đã có pool này
        Queue<GameObject> newPools = new (); // Chưa có pool
        poolDictionary.Add(newEffect.name,newPools);
    }
    public void GetExplodeEffect(GameObject explodePrefab, Vector2 pos , float timeLife = 0.25f) 
    {
        if (!poolDictionary.ContainsKey(explodePrefab.name)) AddNewPool(explodePrefab);
        GameObject explode;
        if(poolDictionary[explodePrefab.name].Count > 0)
        {
            explode = poolDictionary[explodePrefab.name].Dequeue();
            explode.SetActive(true);
        }else explode = Object.Instantiate(explodePrefab);
        explode.transform.position =pos;
        
        DG.Tweening.DOVirtual.DelayedCall(timeLife , ()=> ReturnToPool(explode));
    }
    void ReturnToPool(GameObject effectController)
    {
        effectController.SetActive(false);
        poolDictionary[effectController.name.Replace("(Clone)", "").Trim()].Enqueue(effectController);
    }
}
