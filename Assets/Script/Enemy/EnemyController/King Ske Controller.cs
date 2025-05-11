public class KingSkeController : EnemyController
{
    protected override void Awake()
    {
        BossHealthBar.instance.SetMaxValue(GetComponent<EnemyController>().enemyData.health);
        BossHealthBar.instance.gameObject.SetActive(true);
        base.Awake();
    }
    public override void GetHit(int damage, BulletElement bulletElements, bool isCrit, bool notify = true)
    {
        base.GetHit(damage, bulletElements, isCrit, notify);
        BossHealthBar.instance.SetHealth(currentHealth);
    }
}
