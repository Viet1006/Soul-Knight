using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public BaseWeapon currentWeapon; // Vũ khí đang sử dụng
    public BaseWeapon secondWeapon;
    public Transform target; // Target để điều hướng súng
    [SerializeField] float findRadius; // Bán kính tìm quái
    [SerializeField] float getItemRadius; // Bán kính nhặt các item
    [SerializeField] PlayerMovement playerMovement;
    GameObject nearestItem; // Item gần nhất
    GameObject selectingItem; // Item đang chọn để tắt object đang chọn của item đấy khi ko chọn nữa
    public GameObject nearestEnemy;
    GameObject selectingEnemy;
    bool isAttack;
    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed) // Khi ấn nút bắn
        {
            if(nearestItem != null)
            {
                
                if(nearestItem.TryGetComponent(out BaseWeapon selectedWeapon))
                {
                    selectedWeapon.PickUp(transform);
                    if(secondWeapon != null)
                    {
                        currentWeapon.Drop();
                    }else if(currentWeapon != null){
                        secondWeapon = currentWeapon;
                        secondWeapon.PutAwayWeapon();
                    }
                    currentWeapon = selectedWeapon;
                }
            }else{ // Nếu ko có item gần đó thì bắn
                isAttack = true;
            }
        }
        if(context.canceled) // Thả nút bắn
        {
            isAttack = false;
            if(currentWeapon != null) currentWeapon.StopAttack();
        }
    }
    public void OnSwitch(InputAction.CallbackContext context)
    {
        if(context.performed && secondWeapon != null && currentWeapon != null)
        {
            (currentWeapon, secondWeapon) = (secondWeapon, currentWeapon);
            secondWeapon.PutAwayWeapon();
            currentWeapon.GetWeapon();
        }
    }
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        nearestEnemy = FindTarget.GetNearestObject(transform.position,findRadius,LayerMask.GetMask("Enemy"));
        nearestItem = FindTarget.GetNearestObject(transform.position,getItemRadius,LayerMask.GetMask("Default"));
        if(nearestEnemy != null)
        {
            target.position = nearestEnemy.transform.position;
        }else{
            target.position = (Vector2)transform.position + playerMovement.moveInputValue; // Ko có target thì quay theo hướng di chuyển player
        }
        if(isAttack && currentWeapon != null) currentWeapon.Attack(target);
        FlipToTarget();
        HandleSelectItem();
        HandleSelectEnemy();
        if(currentWeapon != null)
        {
            currentWeapon.RotateToTarget(target);
        }
    }
    void FlipToTarget() // Lật theo target
    {
        if(target.position.x>transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }else if(target.position.x<transform.position.x){
            transform.rotation = Quaternion.Euler(0,-180,0);
        }
    }
    void HandleSelectItem() // Sử lý việc hiện tên cho item đang chọn
    {
        if(selectingItem != null)
        {
            selectingItem.GetComponent<ICanSelect>()?.ShowSelectObject();
            if(nearestItem == null || nearestItem != selectingItem) // ẩn tên item khi ko có item trong vùng hoặc tìm thấy item gần hơn
            {
                selectingItem.GetComponent<ICanSelect>()?.HideSelectObject();
            }
        }
        selectingItem = nearestItem; // Đặt Item đang chọn là Item gần nhất
    }
    void HandleSelectEnemy()
    {
        if(selectingEnemy != null)
        {
            selectingEnemy.GetComponent<ICanSelect>()?.ShowSelectObject();
            if(nearestEnemy == null || nearestEnemy != selectingEnemy) // ẩn tên Enemy khi ko có Enemy trong vùng hoặc tìm thấy Enemy gần hơn
            {
                selectingEnemy.GetComponent<ICanSelect>()?.HideSelectObject();
            }
        }
        selectingEnemy = nearestEnemy; // Đặt Enemy đang chọn là Enemy gần nhất
    }
}