using UnityEngine;
[System.Serializable]
public class BulletBuff
{
    public BulletBuffType bulletBuffType;
    public float effectDuration; // Thời gian hiệu ứng cho 1 số hiệu ứng nếu cần
    public float effectAmount; // Mức độ hiệu ứng có thể là damage hoặc là pushback distance
    [Range(0,100)]
    public int chanceApply; // Tỷ lệ xuất hiện buff
    public void ApplyBuff(Collider2D collider,Vector2 bulletPos)
    {
        if( !RandomChance.RollChance(chanceApply)) return; // Nếu apply ko được thì return
        switch (bulletBuffType)
        {
            case BulletBuffType.PushBack:
            {
                if(collider.TryGetComponent( out IPushable iPushable))
                    iPushable.StartPush(((Vector2)collider.transform.position-bulletPos).normalized,effectAmount); // Đẩy với dựa trên vị trí đạn và object trúng đạn
                break;
            } 
            case BulletBuffType.Burn:
            {
                if(collider.TryGetComponent( out ICanBurn iCanBurn))
                    iCanBurn.StartBurn((int)effectAmount,effectDuration);
                break;
            } 
            case BulletBuffType.Poison:
            {
                if(collider.TryGetComponent( out ICanPoison iCanPoison))
                    iCanPoison.StartPoison((int)effectAmount,effectDuration);
                break;
            } 
            case BulletBuffType.Stun:
            {
                if(collider.TryGetComponent( out ICanStun iCanStun))
                    iCanStun.StartStun(effectDuration);
                break;
            }
            case BulletBuffType.Frozen:
            {
                if(collider.TryGetComponent( out ICanFrozen iCanFrozen))
                    iCanFrozen.StartFrozen(effectDuration);
                break;
            }
            case BulletBuffType.FireZone:
            {
                BulletPool.instance.GetBullet(ObjectHolder.Instance.FireZone,bulletPos);
                break;
            } 
        }
    }
}