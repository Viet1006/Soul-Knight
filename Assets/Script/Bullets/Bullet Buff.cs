using UnityEngine;
[System.Serializable]
public class BulletBuff
{
    public BulletBuffType bulletBuffType;
    public float effectDuration; // Thời gian hiệu ứng cho 1 số hiệu ứng nếu cần
    public float effectAmount; // Mức độ hiệu ứng có thể là damage hoặc là pushback distance
    [Range(0,100)]
    public int chanceApply; // Tỷ lệ xuất hiện buff
    public BulletBuff (BulletBuffType bulletBuffType , float effectDuration , float effectAmount , int chanceApply = 100)
    {
        this.bulletBuffType = bulletBuffType;
        this.effectDuration = effectDuration;
        this.effectAmount = effectAmount;
        this.chanceApply = chanceApply; // Default to 100% chance unless specified otherwise
    }
    public void ApplyBuff(Collider2D collider,Vector2 bulletPos)
    {
        if( !RandomChance.RollChance(chanceApply)) return; // Nếu apply ko được thì return
        switch (bulletBuffType)
        {
            case BulletBuffType.PushBack:
            {
                if(collider && collider.TryGetComponent( out IPushable iPushable))
                    iPushable.StartPush(((Vector2)collider.transform.position-bulletPos).normalized,effectAmount); // Đẩy với dựa trên vị trí đạn và object trúng đạn
                break;
            } 
            case BulletBuffType.Burn:
            {
                if(collider && collider.TryGetComponent( out ICanBurn iCanBurn))
                    iCanBurn.StartBurn((int)effectAmount,effectDuration);
                break;
            } 
            case BulletBuffType.Poison:
            {
                if(collider && collider.TryGetComponent( out ICanPoison iCanPoison))
                    iCanPoison.StartPoison((int)effectAmount,effectDuration);
                break;
            } 
            case BulletBuffType.Stun:
            {
                if(collider && collider.TryGetComponent( out ICanStun iCanStun))
                    iCanStun.StartStun(effectDuration);
                break;
            }
            case BulletBuffType.Frozen:
            {
                if(collider && collider.TryGetComponent( out ICanFrozen iCanFrozen))
                    iCanFrozen.StartFrozen(effectDuration);
                break;
            }
            case BulletBuffType.Vulnerability:
            {
                if(collider && collider.TryGetComponent( out ICanVulnerability iCanVulnerability))
                    iCanVulnerability.StartVulnerability((int)effectAmount,effectDuration);
                break;
            }
            case BulletBuffType.FireZone:
            {
                BulletPool.Instance.GetBullet(ObjectHolder.Instance.elementZone,bulletPos)
                    .GetComponent<ElementZone>()
                    .SetElementZone((int)effectAmount,effectDuration ,BulletElement.Fire);
                break;
            }
            case BulletBuffType.PoisonZone:
            {
                BulletPool.Instance.GetBullet(ObjectHolder.Instance.elementZone,bulletPos)
                    .GetComponent<ElementZone>()
                    .SetElementZone((int)effectAmount,effectDuration ,BulletElement.Poison);
                break;
            } 
            case BulletBuffType.FrozenZone:
            {
                BulletPool.Instance.GetBullet(ObjectHolder.Instance.elementZone,bulletPos)
                    .GetComponent<ElementZone>()
                    .SetElementZone((int)effectAmount,effectDuration ,BulletElement.Frozen);
                break;
            }
            case BulletBuffType.Explode:
            {
                Collider2D[] hittedObjects = Physics2D.OverlapCircleAll(bulletPos,2.5f );
                ExplodeEffectPool.Instance.GetExplodeEffect(bulletPos);
                ShakeCamera.Instance.ShakeCam(bulletPos,1,1,0.5f);
                foreach (Collider2D hittedObject in hittedObjects)
                {
                    if(hittedObject.TryGetComponent(out IGetHit getHit)) getHit.GetHit((int)effectAmount,BulletElement.Fire); 
                }
                break;
            }
        }
    }
}