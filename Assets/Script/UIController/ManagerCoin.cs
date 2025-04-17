using System.Collections;
using TMPro;
using UnityEngine;

public class ManagerCoin : MonoBehaviour
{
    public static ManagerCoin instance;
    int currentValue;
    public int targetValue;
    [SerializeField] TextMeshProUGUI coinText;
    float timer = 1 ; // thời gian để hoàn thành việc thay đổi CointText
    float timerReamain; // thời gian còn lại để hoàn thành
    void Awake() => instance = this;
    public void AddCoin(int changeValue)
    {
        targetValue += changeValue; 
        if(timerReamain <= 0) // nếu ko có coroutine đang thay đổi coinText thì chạy coroutine mới
        { 
            timerReamain = timer; // Khời tạo để chạy coroutine
            StartCoroutine(CountCoinsAnimation());
        } else timerReamain = timer; // Nếu có coroutine đang chạy thì đặt lại timer
    }
    IEnumerator CountCoinsAnimation()
    {
        while (timerReamain > 0 ) 
        {
            timerReamain -= Time.deltaTime;
            float progress = 1-(timerReamain / timer); // Quá trình hoàn thành tăng từ 0-> 1
            // Tính toán giá trị hiển thị (làm tròn để tránh số lẻ)
            currentValue = Mathf.RoundToInt(Mathf.Lerp(currentValue, targetValue, progress)); // distance,progress càng cao thì tốc độ thay đổi càng nhanh
            coinText.text = currentValue.ToString();
            yield return null; // Chờ frame tiếp theo
        }
        // Đảm bảo hiển thị chính xác giá trị cuối cùng
        currentValue  = targetValue;
        coinText.text = currentValue.ToString();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CoinPool.instance.GetCoin(new Vector2(-23,59),9999);
        }
    }
}
