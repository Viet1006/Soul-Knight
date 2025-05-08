using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(-50)]
public class Status : MonoBehaviour
{
    public static Status instance;
    int health = 30;
    [SerializeField] Slider slider;
    public event System.Action OnGetDamage;
    [SerializeField] GameObject textSelect; 
    [SerializeField] GameObject blinkPanel; // Nháy đỏ khi get damage
    Material blinkMaterial;
    [SerializeField] Cinemachine.CinemachineVirtualCamera towerCam;
    void Awake()
    {
        instance = this;
        slider.maxValue = health;
        slider.value = health;
        blinkMaterial = blinkPanel.GetComponent<Image>().material; 
    }
    public void GetDamage(int damage)
    {
        health -= damage;
        slider.value = health;
        if(health <= 0)
        {
            health =0;
            slider.value = health;
            slider.gameObject.SetActive(false);
            towerCam.Priority = 22; // Chuyển sang cam đặt tháp
            towerCam.transform.position = instance.transform.position - new Vector3(0,0,10);
            NotificationSystem.Instance.ShowNotification("Bạn đã thua" , 2f);
            UIManageShowAndHide.Instance.PauseGame();
            DOVirtual.DelayedCall(3, () => DOVirtual.DelayedCall(1,() =>
            {
                DOTween.KillAll(); // Dừng tất cả tween đang chạy
                UnityEngine.SceneManagement.SceneManager.LoadScene("Total Scene");
            }));
        }else 
        {
            OnGetDamage?.Invoke();
            blinkPanel.SetActive(true);
            blinkMaterial.DOFloat(0.7f,"_EdgeSmooth",0.2f).SetLoops(2,LoopType.Yoyo).OnComplete(() => blinkPanel.SetActive(false)); // Nháy đỏ
            ShakeCamera.Instance.ShakeCam(transform.position,1.5f,  1, 1f , false); // Rung camera kể cả nơi tạo rung ko trong camera
        }
        
    }
}
