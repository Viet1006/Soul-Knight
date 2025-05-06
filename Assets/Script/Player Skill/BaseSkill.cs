
using DG.Tweening;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    float skillCoolDown;
    public float skillDuration;
    private bool canActiveSkill = true;
    protected virtual void Awake()
    {
        skillCoolDown = GetComponent<PlayerHandleEffect>().heroData.skillCoolDown;
        UIManageShowAndHide.Instance.OnSelectMapComplete += ResetSKill;
    }
    public virtual void UseSkill()
    {
        if(canActiveSkill)
        {
            PerformSkill();
            canActiveSkill = false;
            SkillButton.instance.StartDuration(skillDuration);
            DOVirtual.DelayedCall(skillDuration , () => UnActiveSkill(skillCoolDown)).OnKill(() => UnActiveSkill(skillCoolDown));
        } 
    }
    protected abstract void PerformSkill();
    protected virtual void UnActiveSkill(float skillCoolDown)
    {
        SkillButton.instance.StartCoolDown(skillCoolDown);
        DOVirtual.DelayedCall(skillCoolDown,()=> canActiveSkill = true);
    }
    void ResetSKill()
    {
        canActiveSkill = true;
        SkillButton.instance.ResetButton();
    }
}
