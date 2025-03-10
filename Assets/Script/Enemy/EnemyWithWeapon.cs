using UnityEngine;

public class EnemyWithWeapon : BaseEnemy
{
    [SerializeField] BaseWeapon weapon;
    float timeToAttack; // Thời gian đến đợt tấn công kế tiếp 
    public float attackTime; // Thời gian tấn công trong 1 đợt
    float attackTimeRemain; // Thời gian tấn công còn lại trong đợt đấy
    void Start()
    {
        weapon = GetComponentInChildren<BaseWeapon>();
    }
    protected override void Attack(Vector2 Target)
    {
        if(timeToAttack >= 0 )
        {
            timeToAttack -= Time.deltaTime;
            return;
        }
        if(weapon!= null && attackTimeRemain > 0)
        {
            weapon.Attack(nearestPlayer.transform);
            attackTimeRemain -= Time.deltaTime;
        } else if(attackTimeRemain <=0)
        {
            attackTimeRemain = attackTime;
            timeToAttack = enemyData.AttackRate;
            weapon.StopAttack();
        }
    }
    protected virtual void Update()
    {
        nearestPlayer = FindTarget.GetNearestObject(transform.position,enemyData.radiusFindPlayer,LayerMask.GetMask("Player"));
        if(nearestPlayer != null)
        {
            Attack(nearestPlayer.transform.position);
            target = nearestPlayer.transform ;
        }else{
            target = null;
        }
        if (nearestPlayer != null && weapon != null)
        {
            weapon.target = nearestPlayer.transform;
            weapon.RotateToTarget();
        }else if(weapon!=null) {
            weapon.transform.localRotation = Quaternion.identity;
        }
        FlipToTarget();
    }
    public void GetWeapon(GameObject weapon)
    {
        weapon.transform.SetParent(transform);
        this.weapon = weapon.GetComponent<BaseWeapon>();
    }
}
