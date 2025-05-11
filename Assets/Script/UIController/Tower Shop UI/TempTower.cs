using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// Hiện tạm hình ảnh và tầm bắn của tower để xác nhận đặt xuống
public class TempTower : MonoBehaviour
{
    public static TempTower instance;
    SpriteRenderer spriteRenderer;
    [SerializeField] RectTransform confirmButton;
    [SerializeField] RectTransform unConfirmButton;
    [SerializeField] RectTransform upgradeButton;
    Dictionary<RectTransform , Vector2> startPoints = new();
    BaseTower currentTower;
    [SerializeField] Transform indicatorRadius; // Lấy transform để Scale bán kính theo tầm bắn
    [SerializeField] TowerStats towerStats;
    [SerializeField] Sprite confirmIcon;
    BoardShopAnim statsAnim;
    TextMeshProUGUI upgradePriceText;
    [SerializeField] GameObject upgradeEffect;
    [SerializeField] List<GameObject> star;
    Image upgradeImage;
    bool isPress; // Dùng cho các nút cần xác nhận 2 lần
    Sprite upgradeIcon; // Lưu sprite để đổi sang confirm
    void Awake() 
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
        startPoints.Add(confirmButton , confirmButton.anchoredPosition); // lấy các vị trí ban đầu của nút
        startPoints.Add(unConfirmButton , unConfirmButton.anchoredPosition);
        startPoints.Add(upgradeButton , upgradeButton.anchoredPosition);
        statsAnim = towerStats.GetComponent<BoardShopAnim>();
        upgradePriceText = upgradeButton.GetComponentInChildren<TextMeshProUGUI>(true);
        upgradeImage = upgradeButton.GetChild(0).GetComponent<Image>(); // Lấy tham chiếu tới upgrade icon
        upgradeIcon = upgradeImage.sprite; // Lưu tham chiếu trỏ đến upgrde icon
    } 
    public void ShowTempTower(BaseTower baseTower , Vector2 pos) // hiện TempTower
    {
        spriteRenderer.sprite = baseTower.GetComponent<SpriteRenderer>().sprite; // Thay hình ảnh để hiện
        spriteRenderer.enabled = true;
        upgradeButton.gameObject.SetActive(false); unConfirmButton.gameObject.SetActive(false);
        StartTween(confirmButton); StartTween(unConfirmButton); // Bắt đầu anim 2 nút
        SetTempTower(baseTower,pos);
    } 
    public void ShowTowerInfo(BaseTower baseTower , Vector2 pos) // Dùng để hiện thông tin tháp đang chọn (tháp đã được đặt trên paltform)
    {
        confirmButton.gameObject.SetActive(false); unConfirmButton.gameObject.SetActive(false);
        StartTween(upgradeButton); StartTween(unConfirmButton);
        spriteRenderer.enabled = false; // Tắt hình ảnh vì hiển thị theo tower đang chọn   
        SetTempTower(baseTower,pos);
        int upgradePrice = currentTower.towerData.UpgradePrice(baseTower.level);
        if(upgradePrice == -1) upgradeButton.gameObject.SetActive(false);
        else  upgradePriceText.text = upgradePrice.ToString();
    }
    void SetTempTower(BaseTower baseTower , Vector2 pos)
    {
        gameObject.SetActive(true);
        currentTower = baseTower; // Lấy data
        towerStats.SetTowerStats(currentTower.towerData,currentTower.level);
        indicatorRadius.localScale = 2 * currentTower.towerData.RadiusAttack(currentTower.level) * Vector2.one; // Scale tầm bắn theo towerData
        transform.position = pos; // Đặt vị trí tháp tạm
        statsAnim.ShowBoardShop();
        ShowStar(baseTower.level);
    }
    public void OnConfirm() // Khi ấn nút confirm đặt tháp
    {
        if(CoinManager.instance.TryBuy(currentTower.towerData.price)) // Kiểm tra tiền
        {
            Instantiate(currentTower.gameObject , TowerShopManage.instance.currentPlatform.transform).transform.localPosition = Vector2.zero;
        }else{
            NotificationSystem.Instance.ShowNotification("Không đủ tiền" , 2);
        }
        OnUnConfirm();
    }
    public void OnUpgrade()
    {
        if(!isPress)
        {
            upgradeImage.sprite = confirmIcon;
            upgradeImage.sprite = confirmIcon; // Thay đổi upgrade icon thành confirm
            towerStats.SetTowerStats(currentTower.towerData,currentTower.level+1);
            indicatorRadius.localScale = 2 * currentTower.towerData.RadiusAttack(currentTower.level+1) * Vector2.one; // Scale tầm bắn theo towerData
            ShowStar(currentTower.level+1);
            isPress = true; // xác nhận đã được ấn
            return;
        }
        if(CoinManager.instance.TryBuy(currentTower.towerData.UpgradePrice(currentTower.level)))
        {
            currentTower.level += 1;
            Instantiate(upgradeEffect,transform.position,upgradeEffect.transform.rotation);
        }else
        {
            NotificationSystem.Instance.ShowNotification("Không đủ tiền" , 2);
        }
        upgradeImage.sprite = upgradeIcon;
        OnUnConfirm();
    }
    public void OnUnConfirm()
    {
        TowerShopManage.instance.SetOrigin(); // Đặt về trạng thái ban đầu khi xong action
        currentTower = null;
        TowerShopManage.instance.GetMouseEvent(true); // Nhận sự kiện
        gameObject.SetActive(false); // tắt gameobject
        statsAnim.HideBoardShop(); // ẩn stats
        upgradeButton.GetChild(0).GetComponent<Image>().sprite = upgradeIcon;
        isPress = false;
    }
    void StartTween(RectTransform rectTransform) // animation của button
    {
        rectTransform.DOKill();
        rectTransform.gameObject.SetActive(true);
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.DOAnchorPos(startPoints[rectTransform] , 0.5f).SetEase(Ease.OutCubic).SetUpdate(true); // Bật nút lên
    }
    void ShowStar(int level)
    {
        foreach (GameObject starObj in star)
        {
            starObj.SetActive(false);
        }
        for (int i = 0; i < level; i++)
        {
            star[i].SetActive(true);
        }
    }
}