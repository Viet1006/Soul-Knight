using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public static CoinPool instance;
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 minSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float dropTime;
    [SerializeField] float timeLife;
    void Awake() => instance = this;
    Queue<GameObject> coinPool = new();
    [SerializeField] private GameObject coinPrefab;
    public GameObject GetCoin(Vector2 startPos,int value)
    {
        GameObject newCoin;
        if(coinPool.Count >0)
        {
            newCoin = coinPool.Dequeue();
            newCoin.transform.position = startPos;
            newCoin.SetActive(true);
        }else newCoin = Instantiate(coinPrefab,startPos,Quaternion.identity);
        newCoin.GetComponent<CoinController>().SetCoin(new Vector2(Random.Range(minSpeed.x,maxSpeed.x) * Random.Range(-1,1)==0? 1 : -1,
        Random.Range(minSpeed.y,maxSpeed.y)),acceleration, dropTime , value , timeLife);
        return newCoin;
    }
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        coinPool.Enqueue(obj);
    }
}
