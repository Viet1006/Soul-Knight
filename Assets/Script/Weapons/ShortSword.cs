using UnityEngine;

public class ShortSword : BaseWeapon
{
    float currentAttack = 1;
    [SerializeField] Animator animator;
    public override BaseBullet CreateBullet(Transform target)
    {
        BaseBullet baseBullet =base.CreateBullet(target);;
        if(currentAttack == 1)
        {
            animator.SetTrigger(Parameters.attack + "1");
            currentAttack += 1;
        }else if(currentAttack == 2)
        {
            animator.SetTrigger(Parameters.attack + "2");
            currentAttack = 1;
            baseBullet.transform.rotation *= Quaternion.Euler(180,0,0);
        }
        return baseBullet;
    }
}