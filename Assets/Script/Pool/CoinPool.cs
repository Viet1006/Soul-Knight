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
        newCoin.SetCoin( value );
    }
    public void ReturnToPool(CoinController coin)
    {
        coin.gameObject.SetActive(false);
        coinPool.Enqueue(coin);
    }
}
