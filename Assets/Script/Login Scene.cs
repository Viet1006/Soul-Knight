
using UnityEngine;
using DG.Tweening;
public class LoginScene : MonoBehaviour
{
    [SerializeField] RectTransform helpBorder;
    void Start()
    {
        helpBorder.anchoredPosition = - helpBorder.anchoredPosition;
    }
    public void NewGameButton()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start Scene");
    }
    public void HelpButton()
    {
        helpBorder.DOAnchorPos(-helpBorder.anchoredPosition, 0.6f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true); // Tạo tween cho helpBorder
    }
    public void CloseHelpButton()
    {
        helpBorder.DOAnchorPos(-helpBorder.anchoredPosition, 0.6f)
            .SetEase(Ease.InBack)
            .SetUpdate(true); // Tạo tween cho helpBorder
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/ucviet.767787/");
    }
    public void GitHub()
    {
        Application.OpenURL("https://github.com/Viet1006/Soul-Knight");
    }
}
