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
        GameObject dieEffect; // Tạo 1 tham chiếu tới particle
        if(dieEffectPool.Count >0) dieEffect = dieEffectPool.Dequeue(); // Lấy tham chiếu nếu đang có
        else dieEffect = Object.Instantiate(ObjectHolder.Instance.dieEffect); // Chưa có thì tạo vào lấy tham chiếu
        dieEffect.transform.position = pos; // Sửa vị trí
        dieEffect.SetActive(true); 
        DG.Tweening.DOVirtual.DelayedCall(0.5f , ()=> ReturnToPool(dieEffect)); // Trả về pool sau 0.5f
    }
    void ReturnToPool(GameObject dieEffect)
    {
        dieEffect.SetActive(false);
        dieEffectPool.Enqueue(dieEffect);
    }
}
