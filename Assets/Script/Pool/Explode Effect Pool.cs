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
            instance.explodeEffectPrefab = ObjectHolder.Instance.explodeEffectPrefab;
            return instance;
        }
    }
    readonly Queue<GameObject> explodeEffectPool = new();
    GameObject explodeEffectPrefab;
    public void GetExplodeEffect(Vector2 pos)
    {
        GameObject explodeEffect;
        if(explodeEffectPool.Count >0)
        {
            explodeEffect = explodeEffectPool.Dequeue();
        } 
        else
        {
            explodeEffect = Object.Instantiate(explodeEffectPrefab) ;
        } 
        explodeEffect.transform.position = pos;
        explodeEffect.SetActive(true);
        DG.Tweening.DOVirtual.DelayedCall(10 , ()=> ReturnToPool(explodeEffect));
    }
    void ReturnToPool(GameObject effectController)
    {
        effectController.SetActive(false);
        explodeEffectPool.Enqueue(effectController);
    }
}
