using DG.Tweening;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public Cinemachine.CinemachineVirtualCamera playerCam;
    void Awake()
    {
        instance = this;
    }
    public void SetTarget()
    {
        playerCam.Follow = transform;
    }
}