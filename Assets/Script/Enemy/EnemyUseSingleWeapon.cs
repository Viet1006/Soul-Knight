// Sử dụng vũ khí tấn công 1 lần 1 đợt
public class EnemyUseSingleWeapon : EnemyWithWeapon
{
    protected override void StartAttack()
    {
        if(weapon){
            weapon.timeToNextFire = 0 ; // Đảm bảo có thể bắn ngay lập tức 
            weapon.Attack(target);
        } 
        ResetTimeToAttack();
    }
}