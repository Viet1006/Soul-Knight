using System.Collections.Generic;
using UnityEngine;

public class CoinPool
{
    static CoinPool instance;
    public static CoinPool Instance
    {
        get
        {
            instance ??= new CoinPool();
            instance.coinPrefab = ObjectHolder.Instance.coinPrebfab;
            return instance;
        }
    }
    readonly Vector2 maxSpeed = new(4,6);
    readonly Vector2 minSpeed = new(2,4);
    readonly float acceleration = 11;
    readonly float dropTime = 0.9f;
    readonly float timeLife = 20;
    Queue<CoinController> coinPool = new();
    private GameObject coinPrefab;
    public void GetCoin(Vector2 startPos,int value)
    {
        CoinController newCoin;
        if(coinPool.Count >0)
        {
            newCoin = coinPool.Dequeue();
            newCoin.gameObject.SetActive(true);
        }else newCoin = Object.Instantiate(coinPrefab).GetComponent<CoinController>();
        newCoin.transform.position = startPos;
        newCoin.SetCoin(new Vector2(Random.Range(minSpeed.x,maxSpeed.x) * Random.Range(-1,1)==0? 1 : -1
            ,Random.Range(minSpeed.y,maxSpeed.y))
            ,acceleration
            ,dropTime
            ,value
            ,timeLife);
    }
    public void ReturnToPool(CoinController coin)
    {
        coin.gameObject.SetActive(false);
        coinPool.Enqueue(coin);
    }
}
