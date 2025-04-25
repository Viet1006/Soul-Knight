using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    List<GameObject> fireEffect = new();
    float burnRate;
    public float timeLife;
    float radius = 2;
    void Start()
    {
        int count = Random.Range(7,9);
        for(int i = 0 ; i< count;i++)
        {
            fireEffect.Add(Instantiate(ObjectHolder.Instance.Fire,transform));
        }
        OnEnable();
        
    }
    void OnEnable()
    {
        foreach(GameObject fire in fireEffect) // Random lại các đốm lửa
        {
            int angle = Random.Range(0,360);
            fire.transform.localPosition = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * Random.Range(0,radius);
        }
        timeLife = 3f;
        transform.localScale = Vector2.zero;
        transform.DOScale(Vector2.one , 0.4f);
    }
    void Update()
    {
        burnRate -= Time.deltaTime;
        timeLife -= Time.deltaTime;
        if(timeLife <=0) BulletPool.instance.ReturnBullet(gameObject);
        if(burnRate <= 0)
        {
            Collider2D[] hittedObjects = Physics2D.OverlapCircleAll(transform.position,radius);
            foreach (Collider2D hittedObject in hittedObjects)
            {
                if(hittedObject.TryGetComponent(out IGetHit getHit)) getHit.GetHit(1,BulletElement.Fire); 
            }
            burnRate = 0.5f;
        }
    }
}
