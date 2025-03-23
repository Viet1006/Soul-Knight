using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ManageSpawnEnemy : MonoBehaviour
{
    public static ManageSpawnEnemy instance;
    int currentWave;
    [SerializeField] List<SquareSpawn> spawnArea;
    [SerializeField] List<Enemy> enemies;
    int totalWeight;
    private Dictionary<string, Queue<GameObject>> enemyPool = new Dictionary<string, Queue<GameObject>>();
    void Awake()
    {
        instance = this;
        foreach (Enemy enemy in enemies)
        {
            enemyPool.Add(enemy.enemyPrefab.name, new Queue<GameObject>());
            totalWeight += enemy.enemyWeight;
        }
        StartCoroutine(SpawnEnemyCoroutine());
    }
    IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            int randomWeight = Random.Range(1,totalWeight+1);
            foreach (Enemy enemy in enemies) // Random dựa trên trọng số
            {
                randomWeight -= enemy.enemyWeight;
                if(randomWeight<=0)
                {
                    Vector2 spawnPos = GetRandomSpawnPoint();
                    GetEnemy(enemy.enemyPrefab, spawnPos);
                    break;
                }
            }
            yield return new WaitForSeconds(2); // Lặp lại sau 2 giây
        }
    }
    public void GetEnemy(GameObject newEnemy,Vector2 pos)
    {
        GameObject enemy;
        if (enemyPool[newEnemy.name].Count > 0)
        {
            enemy = enemyPool[newEnemy.name].Dequeue();
            enemy.SetActive(true);
        }
        else
        {
            enemy = Instantiate(newEnemy);
        }
        enemy.transform.position = pos;
        enemy.GetComponent<EnemyBrain>().InitEnemy();
    }
    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool[enemy.name.Replace("(Clone)", "")].Enqueue(enemy);
    }
    public Vector2 GetRandomSpawnPoint() // Nhận 1 điểm để Spawn quái
    {
        SquareSpawn selectedArea = spawnArea[Random.Range(0, spawnArea.Count)];
        float randomX = Random.Range(selectedArea.topLeft.x, selectedArea.bottomRight.x);
        float randomY = Random.Range(selectedArea.topLeft.y, selectedArea.bottomRight.y);
        return new Vector2(randomX, randomY);
    }
    [System.Serializable]
    class SquareSpawn
    {
        public Vector2 topLeft;
        public Vector2 bottomRight;
    }
    [System.Serializable]
    class Enemy
    {
        public int enemyWeight; // Trọng số để random
        public GameObject enemyPrefab;
    }
}