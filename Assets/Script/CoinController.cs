using System.Collections;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float amplitude = 0.5f; // Height of the oscillation
    public float frequency = 1f;  // Speed of the oscillation
    int value;
    float timeLife;
    public void SetCoin(Vector2 initialSpeed, float acceleration , float dropTime , int value , float timeLife) // Set Các giá trị cho coin
    {
        StartCoroutine(DropEffect(initialSpeed,acceleration,dropTime)); // Bắt đầu hiệu ứng rơi
        GetComponent<Collider2D>().enabled= true; // Bật Collider lên
        this.value = value;
        this.timeLife = timeLife;
    }
    public IEnumerator DropEffect(Vector2 initialSpeed ,float acceleration, float dropTime)
    {
        while (dropTime > 0) // Thời gian rơi
        {
            dropTime -= Time.deltaTime;
            initialSpeed.y -= acceleration * Time.deltaTime; // Giảm vận tốc mô phỏng trọng lực
            transform.position += (Vector3)initialSpeed * Time.deltaTime; // Thay đổi vị trí theo vận tốc
            yield return null;
        }
        Vector2 startPosition = transform.position; // Lấy ví trí ban đầu để float xung quanh bị trí này
        while(timeLife> 0)
        {
            timeLife -= Time.deltaTime;
            transform.position = new Vector2(startPosition.x, startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude); // tạo hiệu ứng float
            yield return null;
        }
        CoinPool.Instance.ReturnToPool(gameObject);
    }
    IEnumerator MoveToCoinText() // Di chuyển đến Ui chứa coin
    {
        while (Vector2.Distance (transform.position,CoinManager.instance.transform.position)> 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position,CoinManager.instance.transform.position , 20 * Time.deltaTime); 
            yield return null;
        }
        CoinManager.instance.AddCoin(value);
        CoinPool.Instance.ReturnToPool(gameObject);
    }
    void OnTriggerEnter2D()
    {
        StopAllCoroutines(); // Dừng tất cả coroutine để bay đến Ui
        GetComponent<Collider2D>().enabled= false;
        StartCoroutine(MoveToCoinText());
    }
}
