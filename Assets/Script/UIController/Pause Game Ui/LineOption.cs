using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LineOption : MonoBehaviour
{
    readonly float delayShowTime = 0.6f;
    readonly float effectTime = 0.7f;
    [SerializeField] Image imageText;
    Material mat;
    private void Start()
    {
        mat = new Material(ObjectHolder.Instance.revealMaterial); // tạo copy từ material gốc
        imageText.material = mat; // gán bản copy cho mỗi instance của imageText
        UIManageShowAndHide.Instance().OnPauseGame += ShowLine;
        UIManageShowAndHide.Instance().OnResumeGame += HideLine;
        mat.SetFloat("_RevealProgress" , 1); // Đặt giá trị ban đầu là 1 để ẩn đi
    }
    void ShowLine()
    {
        DOVirtual.DelayedCall(delayShowTime, () =>
        {
            gameObject.SetActive(true);
            mat.DOFloat(0, "_RevealProgress", effectTime); // Thay đổi ảnh hiện dần ra trong effectTime
        });
    }
    void HideLine()
    {
        mat.DOFloat(1, "_RevealProgress", effectTime); // Thay đổi ảnh đóng dần lại trong effectTime
    }
    void OnDestroy()
    {
        UIManageShowAndHide.Instance().OnPauseGame -= ShowLine;
        UIManageShowAndHide.Instance().OnResumeGame -= HideLine;
        Destroy(mat);
    }
}
