using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class YellowButton : MonoBehaviour
{
    [SerializeField] float delayShowTime;
    [SerializeField] float delayHideTime;
    readonly float effectTime = 0.4f;
    [SerializeField] Image imageText;
    Animator anim;
    Material mat; // Mat của text trong button
    private void Start()
    {
        anim = GetComponent<Animator>();
        mat = new Material(ObjectHolder.Instance.revealMaterial); // tạo copy từ material gốc
        imageText.material = mat; // gán bản copy cho mỗi instance của imageText
        gameObject.SetActive(false); // Ẩn nút khi bắt đầu
        UIManageShowAndHide.Instance.OnShowPausePanel += ShowButton;
        UIManageShowAndHide.Instance.OnResumeGame += HideButton;
        mat.SetFloat("_RevealProgress" , 1); // Đặt giá trị ban đầu là 1 để ẩn đi
    }
    void ShowButton()
    {
        DOVirtual.DelayedCall(delayShowTime, () =>
        {
            gameObject.SetActive(true);
            anim.SetTrigger(Parameters.showButton);
            mat.DOFloat(0, "_RevealProgress", effectTime).SetUpdate(true); // Thay đổi ảnh hiện dần trong effectTime
        }).SetUpdate(true);
    }
    void HideButton()
    {
        DOVirtual.DelayedCall(delayHideTime, () =>
        {
            anim.SetTrigger(Parameters.hideButton);
            mat.DOFloat(1, "_RevealProgress", effectTime).OnComplete(()=> gameObject.SetActive(false)).SetUpdate(true); // Thay đổi ảnh đóng dần trong effectTime
        }).SetUpdate(true);
    }
    void OnDestroy()
    {
        UIManageShowAndHide.Instance.OnPauseGame -= ShowButton;
        UIManageShowAndHide.Instance.OnResumeGame -= HideButton;
        Destroy(mat);
    }
}