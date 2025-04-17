using DG.Tweening;
using UnityEngine;
// Hiện tạm hình ảnh và tầm bắn của tower để xác nhận đặt xuống
public class TempTower : MonoBehaviour
{
    public static TempTower instance;
    SpriteRenderer spriteRenderer;
    [SerializeField] RectTransform confirmButton;
    [SerializeField] RectTransform unConfirmButton;
    Vector2 confirmPos; // Vị trí ban đầu của confirmButoon
    Vector2 unConfirmPos;
    public TowerData currentTower;
    [SerializeField] Transform indicatorRadius; // Lấy transform để Scale bán kính theo tầm bắn
    void Awake() 
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        confirmPos = confirmButton.anchoredPosition;
        unConfirmPos = unConfirmButton.anchoredPosition;
        gameObject.SetActive(false);
    } 
    public void ShowTempTower(TowerData towerData , Vector2 pos) // hiện TempTower
    {
        gameObject.SetActive(true); // Bật gamobject
        currentTower = towerData; // Lấy data
        spriteRenderer.sprite = towerData.towerPrefab.GetComponent<SpriteRenderer>().sprite; // Thay hình ảnh để hiện
        transform.position = pos; // Đặt vị trí tháp tạm
        StartTween(confirmButton , confirmPos); // Bắt đầu animtion Button
        StartTween(unConfirmButton , unConfirmPos);
        indicatorRadius.localScale = 2 * towerData.radiusAttack * Vector2.one; // Scale tầm bắn theo towerData
    } 
    public void ShowTowerInfo(TowerData towerData , Vector2 pos) // Dùng để hiện thông tin tháp đang chọn (tháp đã được đặt trên paltform)
    {
        gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(false);
        unConfirmButton.gameObject.SetActive(false);
        spriteRenderer.enabled = false;
        indicatorRadius.localScale = 2 * towerData.radiusAttack * Vector2.one; // Scale tầm bắn theo towerData
        transform.position = pos;
    }
    void SetTempTower(TowerData towerData , Vector2 pos)
    {
        transform.position = pos;
        indicatorRadius.localScale = 2 * towerData.radiusAttack * Vector2.one;
        gameObject.SetActive(true);
    }
    public void HideTempTower() // Ẩn tháp
    {
        gameObject.SetActive(false);
        currentTower = null;
    }
    public void OnConfirm() // Khi ấn nút confirm đặt tháp
    {
        if(currentTower.price > ManagerCoin.instance.targetValue) // Kiểm tra tiền
        {
            NotificationSystem.instance.ShowNotification("Không đủ tiền" , 2);
        }else{
            Instantiate(currentTower.towerPrefab , TowerShopManage.instance.currentPlatform.transform).transform.localPosition = Vector2.zero;
        }
        currentTower = null;
        TowerShopManage.instance.SetOrigin(); // Đặt về trạng thái ban đầu khi xong action
    }
    public void OnUnConfirm()
    {
        TowerShopManage.instance.SetOrigin(); // Đặt về trạng thái ban đầu khi xong action
        currentTower = null;
    }
    void StartTween(RectTransform rectTransform , Vector2 startPos) // animation của button
    {
        rectTransform.DOKill();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.DOAnchorPos(startPos , 0.5f).SetEase(Ease.OutCubic);
    }
}