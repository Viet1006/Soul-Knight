public class SummonedBullet : BaseBullet
{
    public delegate void endAttackDelegate();
    public endAttackDelegate endAttack;
    public void SummonBullet()
    {
        animator.SetTrigger(Parameters.deploy);
        bulletCollider.enabled = true;
    }
    public void EndAttack() // Kết thúc đòn tấn công
    {
        bulletCollider.enabled = false;
        endAttack?.Invoke();
    }
}