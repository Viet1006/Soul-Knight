using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "NewWave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    public List<Enemy> enemies = new();
    public List<SpawnArea> spawnAreas = new();
    public int cost; // Lượng tiền để spawn wave này
    public float spawnRate;
    public SpawnArea GetRandomSpawnArea() => spawnAreas[Random.Range(0,spawnAreas.Count)];
    public GameObject GetRandomEnemy()
    {
        int randomWeight = Random.Range(1, enemies.Sum(enemy => enemy.enemyWeight) + 1); // Random từ 1 - tổng trọng số 
        foreach (Enemy enemy in enemies) // Random dựa trên trọng số
        {
            randomWeight -= enemy.enemyWeight;
            if(randomWeight<=0)
            {
                return enemy.enemyPrefab;
            }
        }
        return null;
    }
}