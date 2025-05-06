using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

class ManageSpawnEnemy : MonoBehaviour
{
    public static ManageSpawnEnemy instance;
    int currentWave = -1; // Wave hiện tại theo index ở list
    Wave[] waves; // Danh sách các wave
    [SerializeField] float timePerWave; // Thời gian cho mỗi wave
    public float timeWaveRemain; // Thời gian còn lại trước khi bắt đầu wave mới
    [SerializeField] TextMeshProUGUI timeText;
    private readonly Dictionary<string, Queue<GameObject>> enemyPool = new();
    int spawnerCount; // Số lượng coroutine đang Spawn
    int enemyRemain; // Số lượng quái trong wave
    void Awake()
    {
        instance = this;
        timeWaveRemain = 0; // Lúc bắt đầu thì cho 10s để chuẩn bị
        waves = Resources.LoadAll<Wave>("Wave Data");
        timeText.gameObject.SetActive(true);
    }
    void Update()
    {
        if(timeWaveRemain <0)
        {
            timeWaveRemain = timePerWave;
            currentWave++; // Chuyển đến wave tiếp theo
            NotificationSystem.instance.ShowNotification("Bắt đầu wave " +(currentWave+1) ,2f); // Thông báo bắt đầu wave mới
            StartCoroutine(SpawnEnemyCoroutine()); // Bắt đầu 1 coroutine để spawn quái
        }else
        {
            timeWaveRemain -= Time.deltaTime;
            timeText.text = "Thời gian còn lại: " + Mathf.Round(timeWaveRemain).ToString();
        }
    }
    IEnumerator SpawnEnemyCoroutine()
    {
        spawnerCount++;
        int costWave = waves[currentWave].cost;
        while (costWave > 0)
        {
            yield return new WaitForSeconds(1/waves[currentWave].spawnRate); // Lặp lại sau 2 giây
            costWave -= SpawnEnemy(waves[currentWave].GetRandomEnemy(), waves[currentWave].GetRandomSpawnArea().GetRandomPosition());
        }
        spawnerCount--;
    }
    int SpawnEnemy(GameObject newEnemy,Vector2 pos) // Lấy quái từ pool 
    {
        if(!enemyPool.ContainsKey(newEnemy.name)) // Kiểm tra quái này đã có queue chưa
        {
            Queue<GameObject> newQueue = new ();
            enemyPool.Add(newEnemy.name , newQueue); // Thêm queue mới cho quái này
        }
        GameObject enemy;
        if (enemyPool[newEnemy.name].Count > 0)
        {
            enemy = enemyPool[newEnemy.name].Dequeue();
        }
        else enemy = Instantiate(newEnemy);
        enemy.transform.position = pos;
        enemyRemain ++;
        enemy.SetActive(true); // Gọi sau khi đặt đúng ví trí tránh bị lỗi
        return enemy.GetComponent<EnemyController>().InitEnemy(); // trả về lượng tiền cần Spawn quái và gọi hàm khởi tạo quái
    }
    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool[enemy.name.Replace("(Clone)", "")].Enqueue(enemy);
        enemyRemain --;
        if(enemyRemain ==0 && spawnerCount == 0) // Nếu không còn quái nào trong wave và không còn coroutine nào đang spawn
        {
            NotificationSystem.instance.ShowNotification("Wave " + (currentWave + 1) + " đã hoàn thành", 2f); // Thông báo hoàn thành wave
        }
    }
    public bool IsFinishWave() => enemyRemain == 0 && spawnerCount == 0;
}