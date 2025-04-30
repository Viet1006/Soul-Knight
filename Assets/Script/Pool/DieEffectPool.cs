using System.Collections.Generic;
using UnityEngine;

public class DieEffectPool 
{
    static DieEffectPool instance;
    public static DieEffectPool Instance
    {
        get
        {
            instance ??= new DieEffectPool();
            return instance;
        }
    }
    readonly Queue<GameObject> dieEffectPool = new();
    public void GetDieEffect(Vector2 pos)
    {
        GameObject dieEffect;
        if(dieEffectPool.Count >0) dieEffect = dieEffectPool.Dequeue();
        else dieEffect = Object.Instantiate(ObjectHolder.Instance.dieEffect);
        dieEffect.transform.position = pos;
        dieEffect.SetActive(true);
        dieEffect.GetComponent<ParticleSystem>().Play();
        DG.Tweening.DOVirtual.DelayedCall(0.5f , ()=> ReturnToPool(dieEffect));
    }
    void ReturnToPool(GameObject dieEffect)
    {
        dieEffect.SetActive(false);
        dieEffectPool.Enqueue(dieEffect);
    }
}
