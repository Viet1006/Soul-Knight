using TMPro;
using UnityEngine;

public class TotalScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI kill;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI coin;
    void Start()
    {
        kill.text = TotalStats.Instance.kill.ToString();
        float time = TotalStats.Instance.time;
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        this.time.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        coin.text = TotalStats.Instance.coin.ToString();
    }
    public void Exit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
