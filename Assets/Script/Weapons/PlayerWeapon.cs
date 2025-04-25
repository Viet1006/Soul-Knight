using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    BaseWeapon baseWeapon;
    void Awake()
    {
        baseWeapon = GetComponent<BaseWeapon>();
    }
    public void Upgrade()
    {
        baseWeapon.level += 1;
    }
}
