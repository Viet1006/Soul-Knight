using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChooseHeroController : MonoBehaviour
{
    public static ChooseHeroController instance; 
    [SerializeField] CinemachineVirtualCamera overCam;
    [SerializeField] GameObject playerTracker;
    [SerializeField] TextMeshProUGUI skillDescription;
    [SerializeField] TextMeshProUGUI skillCoolDown;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] Image initWeapon;
    [SerializeField] Image skillIcon;
    [SerializeField] Image skillButton;
    GameObject selectedHero;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        MouseEvent.instance.OnLeftMousePerformed += CheckChooseHero;
    }
    void CheckChooseHero(Vector2 mousePos)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        selectedHero = FindTarget.GetNearestObject(mousePos , 0.1f, LayerMask.GetMask("Player")); // Tìm platform chọn được
        if(selectedHero)
        {
            MouseEvent.instance.OnLeftMousePerformed -= CheckChooseHero;
            playerTracker.transform.position = selectedHero.transform.position;  
            UIManageShowAndHide.Instance.ChooseHero();
            SetHeroStats(selectedHero.GetComponent<PlayerHandleEffect>().heroData);
            overCam.Priority = 0; // Chuyển sang chiếu trên player cam
            PlayerCamera.instance.playerCam.transform.position = selectedHero.transform.position + Vector3.back * 10; // đưa play cam về nhân vật chọn
        }
    }
    void ChangeOverCam()
    {
        MouseEvent.instance.OnLeftMousePerformed += CheckChooseHero;
        overCam.Priority = 10;
    }
    void SetHeroStats(HeroData heroData)
    {
        skillDescription.text = heroData.skillDescription;
        skillCoolDown.text = heroData.skillCoolDown.ToString();
        health.text = heroData.health.ToString();
        speed.text = heroData.speed.ToString();
        skillIcon.sprite = heroData.skillIcon;
        skillButton.sprite = heroData.skillIcon;
        initWeapon.sprite = heroData.initWeapon.GetComponent<SpriteRenderer>().sprite;
    }
    public void OnStartButton()
    {
        selectedHero.GetComponent<PlayerInput>().enabled = true;
        UIManageShowAndHide.Instance.CompleteChoose();
        HealthBar.instance.SetMaxValue(selectedHero.GetComponent<PlayerHandleEffect>().heroData.health);
        PlayerCamera.instance.SetTarget(selectedHero.transform);
        selectedHero.GetComponent<PlayerBehaviour>().OnSelected();
    }
    public void OnBack()
    {
        UIManageShowAndHide.Instance.CloseChooseHero();
        instance.ChangeOverCam();
    }
}
