using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    int currentValue;
    public int targetValue;
    Tween changeValueTween;
    [SerializeField] TextMeshProUGUI coinText;
    void Awake()
    {
        instance = this;
        targetValue = 100000; // Tạo giá trị ban đầu để mua súng
        coinText.text = "100000";
        UIManageShowAndHide.Instance.OnSelectMapComplete += () => 
        {
            targetValue = 0;
            coinText.text = "0";
        };
    }
    public void AddCoin(int changeValue)
    {
        targetValue += changeValue;
        changeValueTween.Kill();
        changeValueTween = DOVirtual.Int(currentValue, targetValue, 0.5f, value =>
        {
            coinText.text = value.ToString();
            currentValue = value;
        });
    }
    public bool TryBuy(int cost)
    {
        if(cost > targetValue) return false;
        AddCoin(-cost);
        return true;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CoinPool.Instance.GetCoin(new Vector2(-23,59),9999);
        }
    }
}