using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerShopManage : ItemManagement , ICanInteract
{
    public static TowerShopManage instance;
    [SerializeField] Cinemachine.CinemachineVirtualCamera placeTowerCamera;
    public List<TowerData> towers = new(); //  Giữ tham chiếu tới các tower data
    [HideInInspector] public GameObject currentPlatform; //platform hiện tại
    protected void Awake() => instance = this;
    protected override void Start()
    {
        base.Start();
        placeTowerCamera.transform.position = Status.instance.transform.position - new Vector3(0,0,10);
        ListAllTower();
    }
    public void ChoosePlatform(Vector2 mousePos) // Sự kiện chuột trái Click
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        GameObject selectedPlatform = FindTarget.GetNearestObject(mousePos , 0.1f, LayerMask.GetMask("Tower Platform")); // Tìm platform chọn được
        if(! selectedPlatform) // Nếu không chọn trúng platform thì chuyển về vị trí ban đầu
        {
            SetOrigin();
            placeTowerCamera.transform.DOMove(Status.instance.transform.position - new Vector3(0,0,10) , 0.4f) ;
        } else if(selectedPlatform != currentPlatform) // chọn túng thì kiểm tra xem platform vừa chọn được có khác platform đang chọn ko
        {
            currentPlatform = selectedPlatform; // Gán plarForm hiện tại 
            placeTowerCamera.transform.DOMove(selectedPlatform.transform.position - new Vector3(0,0,10),0.4f); // Di chuyển camera đến
            TempTower.instance.HideTempTower(); // Tắt tower tạm đi 
            SetInterractButtons(true);
            BaseTower selectedTower = currentPlatform.GetComponentInChildren<BaseTower>();
            if(!selectedTower) // Nếu platform được chọn không chứa tháp
            {
                appearTween.PlayForward(); // Hiện bảng chọn tháp
            }else // Nếu platform được chọn đang chứa tháp
            {
                appearTween.PlayBackwards(); // Tắt bảng chọn tháp
                TempTower.instance.ShowTowerInfo(selectedTower.towerData,currentPlatform.transform.position);
            }
        }
    }
    void ListAllTower() // List tất cả các tháp ra bảng
    {
        foreach(TowerData tower in towers)
        {
            Instantiate(itemBorder,content).GetComponent<TowerSlot>().SetTowerSlot(tower);
        }
    }
    public void SelectTower(TowerData towerData) // Hàm khi 1 tower trong bảng được chọn
    {
        TempTower.instance.ShowTempTower(towerData, currentPlatform.transform.position); // Hiện tower tạm 
    }
    public void SetOrigin() // Đặt lại trạng thái ban đầu của shop khi mua xong
    {
        SetInterractButtons(true); // bật lại tát cả nút tower trong bảng
        TempTower.instance.HideTempTower(); // tắt tower tạm
        appearTween.PlayBackwards(); // Tắt bảng chọn tháp
        currentPlatform = null;
    }
    public override void Interact()
    {
        CloseTowerShopButotn.instance.ShowButton();
        placeTowerCamera.Priority = 22;
        MouseEvent.instance.OnLeftMousePerformed += ChoosePlatform; // Bắt đầu nhận sự kiện chuột trái khi cần
        UIManageShowAndHide.instance.OpenShop();
    }
    public void CloseShop()
    {
        CloseTowerShopButotn.instance.HideButton();
        placeTowerCamera.Priority = 2;
        MouseEvent.instance.OnLeftMousePerformed -= ChoosePlatform; //hủy sự kiện chuột trái khi đóng Shop
        UIManageShowAndHide.instance.CloseShop();
        SetOrigin();
    }
}