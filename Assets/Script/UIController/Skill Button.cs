using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public static SkillButton instance;
    Image skilButton;
    Tween durationTween;
    Tween cooldownTween;
    void Awake()
    {
        instance = this;
        skilButton = GetComponent<Image>();
        UIManageShowAndHide.Instance.OnSelectMapComplete += ResetButton;
    }
    public void StartDuration(float skillDuration)
    {
        skilButton.fillMethod = Image.FillMethod.Vertical; // Cooldown từ trên xuống
        skilButton.color = new Color(skilButton.color.r, skilButton.color.g, skilButton.color.b, 0.5f); // Làm mờ
        durationTween.Kill();
        durationTween =skilButton.DOFillAmount(0 , skillDuration)
            .SetEase(Ease.Linear)
            .OnKill(() => skilButton.fillAmount = 1);
    }
    public void StartCoolDown(float skillCoolDown)
    {
        skilButton.fillMethod = Image.FillMethod.Radial360; // Cooldown vòng tròn
        skilButton.fillAmount = 1; // Đặt lại fill amout
        skilButton.fillOrigin = (int)Image.Origin360.Top; // Quay từ 12h và theo (kim đồng hồ mặc định)
        cooldownTween.Kill();
        cooldownTween = skilButton.DOFillAmount(0 , skillCoolDown)
            .SetEase(Ease.Linear)
            .OnKill(()=>{
                    skilButton.color = Color.white; // Làm mờ
                    skilButton.fillAmount = 1;
                });
    }
    void ResetButton()
    {
        durationTween.Kill();
        cooldownTween.Kill();
    }
}
