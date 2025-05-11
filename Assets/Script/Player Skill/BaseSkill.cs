
using DG.Tweening;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    float skillCoolDown;
    public float skillDuration;
    private bool canActiveSkill = true;
    Tween durationTween;
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
            durationTween= DOVirtual.DelayedCall(skillDuration , () => UnActiveSkill(skillCoolDown),false).OnComplete(() => UnActiveSkill(skillCoolDown));
        }
    }
    protected abstract void PerformSkill();
    protected virtual void UnActiveSkill(float skillCoolDown)
    {
        SkillButton.instance.StartCoolDown(skillCoolDown);
        DOVirtual.DelayedCall(skillCoolDown,()=> canActiveSkill = true,false);
    }
    protected virtual void ResetSKill()
    {
        canActiveSkill = true;
        durationTween.Kill(false);
        UnActiveSkill(0);
    }
}
