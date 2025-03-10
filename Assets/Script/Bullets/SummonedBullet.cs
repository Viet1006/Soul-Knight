public class SummonedBullet : BaseBullet
{
    public delegate void endAttackDelegate();
    public endAttackDelegate endAttack;
    public void SummonBullet()
    {
        animator.SetTrigger(Parameters.deploy);
        colliderBullet.enabled = true;
    }
    public void EndAttack() // Kết thúc đòn tấn công
    {
        colliderBullet.enabled = false;
        endAttack?.Invoke();
    }
}