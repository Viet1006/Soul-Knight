using System.Collections.Generic;
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
            fireEffect.Add(Instantiate(ObjectHolder.instance.Fire,transform));
        }
        OnEnable();
    }
    void OnEnable()
    {
        foreach(GameObject fire in fireEffect)
        {
            int angle = Random.Range(0,360);
            fire.transform.localPosition = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * Random.Range(0,radius);
        }
        burnRate = 0.5f;
        timeLife = 3f;
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
