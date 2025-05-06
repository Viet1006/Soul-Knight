using UnityEngine;
using UnityEngine.InputSystem;
[DefaultExecutionOrder(-100)] // Script này sẽ chạy đầu tiên
public class PlayerBehaviour : MonoBehaviour
{
    [HideInInspector] public BaseWeapon currentWeapon; // Vũ khí đang sử dụng
    [SerializeField] Transform target; // Target để điều hướng súng
    readonly float findRadius = 10; // Bán kính tìm quái
    PlayerMovement playerMovement;
    public GameObject nearestEnemy;
    GameObject selectingEnemy;
    bool isAttacking;
    BaseSkill baseSkill;
    void Start()
    {
        currentWeapon = GetComponentInChildren<BaseWeapon>();
        playerMovement = GetComponent<PlayerMovement>();
        baseSkill = GetComponent<BaseSkill>();
    }
    void Update()
    {
        nearestEnemy = FindTarget.GetNearestObject(transform.position,findRadius,LayerMask.GetMask("Enemy"));
        if(nearestEnemy != null)
        {
            target.position = nearestEnemy.transform.position;
        }else{
            if(playerMovement.moveInputValue != Vector2.zero)
            target.position = (Vector2)transform.position + playerMovement.moveInputValue; // Ko có target thì quay theo hướng di chuyển player
        }
        if(isAttacking && currentWeapon != null) currentWeapon.Attack(target);
        FlipToTarget();
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
            isAttacking = true;
        }
        if(context.canceled) // Thả nút bắn
        {
            StopAttack();
        }
    }
    public void StopAttack()
    {
        isAttacking = false;
        if(currentWeapon != null) currentWeapon.StopAttack();
    }
    public void OnActiveSkill(InputAction.CallbackContext context)
    {
        if(context.performed) baseSkill.UseSkill();
    }
    public void OnSelected() // khi hero được chọn
    {
        WeaponInventoryManager.instance.usingWeapon = currentWeapon; // Thêm vũ khí đang dùng cho inventory quản lý
        WeaponShopManager.instance.DeleteSlot(WeaponInventoryManager.instance.usingWeapon); // Xóa vũ khí đang dùng ra khỏi Shop
        WeaponInventoryManager.instance.OnWeaponEquipped += newWeapon => { // Sự kiện khi thay đổi trang bị
            newWeapon.transform.SetParent(transform); // Đem vũ khí về Player
            newWeapon.transform.SetLocalPositionAndRotation(Vector2.zero, Quaternion.identity);
            currentWeapon = newWeapon.GetComponent<BaseWeapon>();
        };
        UIManageShowAndHide.Instance.OnSelectMapComplete += () => transform.position = new Vector2(0,46);
        transform.SetParent(null);
    }
}