using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
public class TowerShopManage : ItemManagement
{
    public static TowerShopManage instance;
    [SerializeField] Cinemachine.CinemachineVirtualCamera placeTowerCamera;
    GameObject[] towers; //  Giữ tham chiếu tới các tower data
    [HideInInspector] public GameObject currentPlatform; //platform hiện tại
    bool isRegistered; // trạng thái có đang đăng ký sự kiện click chuột hay không
    protected void Awake()
    {
        instance = this;
        towers = Resources.LoadAll<GameObject>("Towers");
    }
    protected void Start()
    {
        placeTowerCamera.transform.position = Status.instance.transform.position - new Vector3(0,0,10);
        boardShopAnim = content.transform.parent.GetComponent<BoardShopAnim>();
        for(int i=0;i< towers.Length;i++)
        {
            Instantiate(itemBorder,content).GetComponent<TowerSlot>().SetTowerSlot(towers[i].GetComponentInChildren<BaseTower>());
        }
    }
    public void ChoosePlatform(Vector2 mousePos) // Sự kiện chuột trái Click
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        GameObject selectedPlatform = FindTarget.GetNearestObject(mousePos , 0.1f, LayerMask.GetMask("Water")); // Tìm platform chọn được
        if(!selectedPlatform || !selectedPlatform.CompareTag("Tower Platform")) // Nếu không chọn trúng platform thì chuyển về vị trí ban đầu
        {
            SetOrigin();
            placeTowerCamera.transform.DOMove(Status.instance.transform.position - new Vector3(0,0,10) , 0.4f).SetUpdate(true) ;
        } else if(selectedPlatform != currentPlatform) // chọn trúng thì kiểm tra xem platform vừa chọn được có khác platform đang chọn ko
        {
            currentPlatform = selectedPlatform; // Gán plarForm hiện tại 
            placeTowerCamera.transform.DOMove(selectedPlatform.transform.position - new Vector3(0,0,10),0.4f).SetUpdate(true); // Di chuyển camera đến
            SetInterractButtons(true);
            BaseTower selectedTower = currentPlatform.GetComponentInChildren<BaseTower>();
            if(!selectedTower) // Nếu platform được chọn không chứa tháp
            {
                boardShopAnim.ShowBoardShop(); // Hiện bảng chọn tháp
                CloseTowerShopButton.instance.HideButton();
            }else // Nếu platform được chọn đang chứa tháp
            {
                boardShopAnim.HideBoardShop(); // Tắt bảng chọn tháp
                TempTower.instance.ShowTowerInfo(selectedTower,currentPlatform.transform.position);
                CloseTowerShopButton.instance.HideButton();
                GetMouseEvent(false);
            }
        }
    }
    public void SelectTower(BaseTower baseTower) // khi 1 tower trong content được chọn
    {
        if( !currentPlatform ) return; //currentPlatform null khi bảng đang đóng mà ấn vào tháp
        TempTower.instance.ShowTempTower(baseTower, currentPlatform.transform.position); 
        GetMouseEvent(false);
        boardShopAnim.ShowBoardShop();
    }
    public void SetOrigin() // Đặt lại trạng thái ban đầu của shop khi mua xong
    {
        SetInterractButtons(true); // bật lại tát cả nút tower trong bảng
        boardShopAnim.HideBoardShop(); // Tắt bảng chọn tháp
        CloseTowerShopButton.instance.ShowButton();
        currentPlatform = null;
    }
    public void Interact()
    {
        CloseTowerShopButton.instance.ShowButton();
        placeTowerCamera.Priority = 22;
        GetMouseEvent(true);
        UIManageShowAndHide.Instance.PauseGame();
    }
    public void CloseShop()
    {
        CloseTowerShopButton.instance.HideButton();
        placeTowerCamera.Priority = 2;
        GetMouseEvent(false);
        UIManageShowAndHide.Instance.CloseShop();
        SetOrigin();
    }
    public void GetMouseEvent(bool isGet)
    {
        if(isGet && !isRegistered) // khi muốn đăng ký và đang ở trạng thái không đăng ký
        {
            MouseEvent.instance.OnLeftMousePerformed += ChoosePlatform; // Bắt đầu nhận sự kiện chuột trái khi cần
            isRegistered = true;
        } 
        else if(!isGet && isRegistered)
        {
            MouseEvent.instance.OnLeftMousePerformed -= ChoosePlatform; // Hủy nhận sự kiện chuột trái khi cần
            isRegistered = false;
        } 
    }
}