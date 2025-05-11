using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

class ManageSpawnEnemy : MonoBehaviour
{
    public static ManageSpawnEnemy instance;
    int currentWave = -1; // Wave hiện tại theo index ở list
    List<Wave> waves = new(); // Danh sách các wave
    readonly float timePerWave = 75; // Thời gian cho mỗi wave
    public float timeWaveRemain; // Thời gian còn lại trước khi bắt đầu wave mới
    [SerializeField] TextMeshProUGUI timeText;
    private readonly Dictionary<string, Queue<GameObject>> enemyPool = new();
    int spawnerCount; // Số lượng coroutine đang Spawn
    int enemyRemain; // Số lượng quái trong wave
    [SerializeField] MainUIAnim MainUIAnim;
    void Awake()
    {
        instance = this;
        timeWaveRemain = 0; // Lúc bắt đầu thì cho 10s để chuẩn bị
        NotificationSystem.Instance.ShowNotification("Hãy cố gắng bảo vệ tháp của bạn khỏi quái vật" , 3f);
        for (int i = 1; i <= 13; i++)
        {
            string waveName = "Wave " + i;
            Wave wavePrefab = Resources.Load<Wave>("Wave Data/" +waveName);
            if (wavePrefab != null)
            {
                waves.Add(wavePrefab);
            }
        }
        timeText.gameObject.SetActive(true);
        MainUIAnim.ShowOpenShopBorder();
    }
    void Update()
    {
        if(timeWaveRemain <0)
        {
            currentWave++;
            if(currentWave == waves.Count) // Đến wave cuối đánh boss
            {
                timeText.gameObject.SetActive(false);
            }
            timeWaveRemain = timePerWave;
             // Chuyển đến wave tiếp theo
            NotificationSystem.Instance.ShowNotification("Bắt đầu wave " +(currentWave+1) ,1f); // Thông báo bắt đầu wave mới
            StartCoroutine(SpawnEnemyCoroutine()); // Bắt đầu 1 coroutine để spawn quái
        }else if(currentWave != waves.Count)
        {
            timeWaveRemain -= Time.deltaTime;
            TotalStats.Instance.time += Time.deltaTime;
            timeText.text = "Thời gian còn lại: " + Mathf.Round(timeWaveRemain).ToString();
        }
    }
    IEnumerator SpawnEnemyCoroutine()
    {
        spawnerCount++;
        int costWave = waves[currentWave].cost;
        while (costWave > 0)
        {
            yield return new WaitForSeconds(1/waves[currentWave].spawnRate); 
            costWave -= SpawnEnemy(waves[currentWave].GetRandomEnemy(), waves[currentWave].GetRandomSpawnArea().GetRandomPosition());
        }
        spawnerCount--;
    }
    public int SpawnEnemy(GameObject newEnemy,Vector2 pos) // Lấy quái từ pool 
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
        TotalStats.Instance.kill++;
        enemyPool[enemy.name.Replace("(Clone)", "")].Enqueue(enemy);
        enemyRemain --;
        if(enemyRemain <=0 && spawnerCount <= 0) // Nếu không còn quái nào trong wave và không còn coroutine nào đang spawn
        {
            enemyRemain = 0;
            if(currentWave == waves.Count - 1) // Nếu đã hoàn thành tất cả các wave
            {
                timeText.gameObject.SetActive(false); // Ẩn text thời gian
                NotificationSystem.Instance.ShowNotification("Hoàn thành tất cả các wave", 2f); // Thông báo hoàn thành tất cả các wave
                UIManageShowAndHide.Instance.PauseGame();
                DOVirtual.DelayedCall(3, () => DOVirtual.DelayedCall(1,() =>
                {
                    DOTween.KillAll(); // Dừng tất cả tween đang chạy
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Total Scene");
                }));
            }
            else
            {
                NotificationSystem.Instance.ShowNotification("Wave " + (currentWave + 1) + " đã hoàn thành", 1f); // Thông báo hoàn thành wave
                timeWaveRemain = 5; // Đặt lại thời gian cho wave mới
            } 
        }
    }
}