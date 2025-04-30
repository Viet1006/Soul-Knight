using System.Collections.Generic;
using UnityEngine;

public class KnightSkill : BaseSkill
{
    PlayerBehaviour playerBehaviour;
    [SerializeField] GameObject skillEffect;
    public List<BulletBuff> randomBuffs = new();
    System.Action<List<BulletBuff>> _buffAction;
    void Awake()
    {
        playerBehaviour = GetComponent<PlayerBehaviour>();
        _buffAction = RandomBuffs;
    }
    protected override void PerformSkill()
    {
        playerBehaviour.currentWeapon.OnAttack += _buffAction;
        skillEffect.SetActive(true);
    }
    protected override void UnActiveSkill(float skillCoolDown)
    {
        base.UnActiveSkill(skillCoolDown);
        skillEffect.SetActive(false);
        playerBehaviour.currentWeapon.OnAttack -= _buffAction;
    }
    void RandomBuffs(List<BulletBuff> addedBuffs)
    {
        // Chọn ngẫu nhiên 1 Buff từ danh sách
        BulletBuff selectedBuff = randomBuffs[Random.Range(0, randomBuffs.Count)];

        // Thêm Buff vào danh sách đạn
        addedBuffs.Add(selectedBuff);
    }
}
