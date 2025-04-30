using UnityEngine;
using UnityEngine.InputSystem;
[DefaultExecutionOrder(-100)] // Script này sẽ chạy đầu tiên
public class PlayerBehaviour : MonoBehaviour
{
    [HideInInspector] public BaseWeapon currentWeapon; // Vũ khí đang sử dụng
    public Transform target; // Target để điều hướng súng
    readonly float findRadius = 10; // Bán kính tìm quái
    readonly float getItemRadius = 1.5f; // Bán kính nhặt các item
    PlayerMovement playerMovement;
    GameObject nearestItem; // Item gần nhất
    GameObject selectingItem; // Item đang chọn để tắt object đang chọn của item đấy khi ko chọn nữa
    public GameObject nearestEnemy;
    GameObject selectingEnemy;
    bool isAttacking;
    BaseSkill baseSkill;
    void Start()
    {
        currentWeapon = GetComponentInChildren<BaseWeapon>();
        playerMovement = GetComponent<PlayerMovement>();
        WeaponInventoryManager.instance.usingWeapon = currentWeapon;
        WeaponInventoryManager.instance.OnWeaponEquipped += newWeapon => { // Sự kiện khi thay đổi trang bị
            newWeapon.transform.SetParent(transform); // Đem vũ khí về Player
            newWeapon.transform.SetLocalPositionAndRotation(Vector2.zero, Quaternion.identity);
            currentWeapon = newWeapon.GetComponent<BaseWeapon>();
        };
        baseSkill = GetComponent<BaseSkill>();
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
        if(isAttacking && currentWeapon != null) currentWeapon.Attack(target);
        FlipToTarget();
        HandleSelectItem();
        HandleSelectEnemy();
        if(currentWeapon != null) currentWeapon.RotateToTarget(target);
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
    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed) // Khi ấn nút bắn
        {
            if(nearestItem != null)
            {
                if(nearestItem.TryGetComponent(out ICanInteract iCanInteract))
                {
                    iCanInteract.Interact();
                }
            }else{ // Nếu ko có item gần đó thì bắn
                isAttacking = true;
            }
        }
        if(context.canceled) // Thả nút bắn
        {
            isAttacking = false;
            if(currentWeapon != null) currentWeapon.StopAttack();
        }
    }
    public void OnActiveSkill(InputAction.CallbackContext context)
    {
        if(context.performed) baseSkill.UseSkill();
    }
}