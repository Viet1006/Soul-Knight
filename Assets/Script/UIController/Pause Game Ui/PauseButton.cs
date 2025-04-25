using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void PauseGame()
    {
        UIManageShowAndHide.Instance().PauseGame();
    }
}
