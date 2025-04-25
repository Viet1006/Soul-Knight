//cài đặt interface này có để có thể display vũ khí từ weaponSlot vào ô mong muốn 
using UnityEngine;

public interface SetSelectingWeapon
{
    void SetSelectingSlot(GameObject weaponPrefab); // Lấy index trong mảng weaponPrefab để set là được
}
