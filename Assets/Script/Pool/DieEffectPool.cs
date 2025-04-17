using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEffectPool : MonoBehaviour
{
    public static DieEffectPool instance;
    [SerializeField] GameObject DieEffect;
    Queue<GameObject> dieEffectPool = new();
    void Awake()
    {
        instance = this;
    }
    public void GetDieEffect(Vector2 Pos)
    {
        GameObject newEffect;
        if(dieEffectPool.Count >0) newEffect = dieEffectPool.Dequeue();
        else newEffect = Instantiate(DieEffect);
        newEffect.transform.position = Pos;
        newEffect.GetComponent<ParticleSystem>().Play();
        StartCoroutine(Counter(newEffect));
    }
    IEnumerator Counter(GameObject dieEffect) // Đếm thời gian để trả về Pool
    {
        yield return new WaitForSeconds(0.5f);
        ReturnToPool(dieEffect);
    }
    public void ReturnToPool(GameObject dieEffect)
    {
        dieEffectPool.Enqueue(dieEffect);
    }
}
