
using DG.Tweening;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public float skillCoolDown;
    public float skillDuration;
    private bool canActiveSkill = true;
    public virtual void UseSkill()
    {
        if(canActiveSkill)
        {
            PerformSkill();
            canActiveSkill = false;
            SkillButton.instance.StartDuration(skillDuration);
            DOVirtual.DelayedCall(skillDuration , () => UnActiveSkill(skillCoolDown));
        } 
    }
    protected abstract void PerformSkill();
    protected virtual void UnActiveSkill(float skillCoolDown)
    {
        SkillButton.instance.StartCoolDown(skillCoolDown);
        DOVirtual.DelayedCall(skillCoolDown,()=> canActiveSkill = true);
    }
}
