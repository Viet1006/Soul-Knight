using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : NetworkBehaviour
{
    public BaseWeapon currentWeapon; // Vũ khí đang sử dụng
    public BaseWeapon secondWeapon;
    public Vector2 target; // Target để điều hướng
    [SerializeField] float findRadius; // Bán kính tìm quái
    [SerializeField] float getItemRadius; // Bán kính nhặt các item
    PlayerMovement playerMovement;
    GameObject nearestItem; // Item gần nhất
    GameObject selectingItem; // Item đang chọn để tắt object đang chọn của item đấy khi ko chọn nữa
    GameObject nearestEnemy;
    GameObject selectingEnemy;
    bool isAttack;
    public void OnFire(InputAction.CallbackContext context)
    {
        if( !IsOwner ) return;
        if(context.performed) // Khi ấn nút bắn
        {
            if(nearestItem != null)
            {
                BaseWeapon selectedWeapon = nearestItem.GetComponent<BaseWeapon>();
                if(selectedWeapon !=null)
                {
                    nearestItem.GetComponent<BaseWeapon>().PickUp(transform);
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
        }
    }
    public void OnSwitch(InputAction.CallbackContext context)
    {
        if( !IsOwner ) return;
        if(context.performed && secondWeapon != null && currentWeapon != null)
        {
            (currentWeapon, secondWeapon) = (secondWeapon, currentWeapon);
            secondWeapon.PutAwayWeapon();
            currentWeapon.GetWeapon();
        }
    }
    public override void OnNetworkSpawn()
    {
        if( !IsOwner ) return;
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if( !IsOwner ) return;
        nearestEnemy = FindTarget.GetNearistObject(transform.position,findRadius,LayerMask.GetMask("Enemy"));
        nearestItem = FindTarget.GetNearistObject(transform.position,getItemRadius,LayerMask.GetMask("Default"));
        if(nearestEnemy != null)
        {
            target = nearestEnemy.transform.position;
        }else{
            target = (Vector2)transform.position + playerMovement.moveInputValue; // Ko có target thì quay theo hướng di chuyển player
        }
        if(isAttack && currentWeapon != null)
        {
            currentWeapon.Attack(target);
        }
        FlipToTarget();
        HandleSelectItem();
        HandleSelectEnemy();
        if(currentWeapon != null)
        {
            currentWeapon.target = target;
            currentWeapon.RotateToTarget();
        }
    }
    void FlipToTarget() // Lật theo target
    {
        if(target.x>transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }else if(target.x<transform.position.x){
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
