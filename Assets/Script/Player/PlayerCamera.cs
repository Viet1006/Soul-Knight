using DG.Tweening;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Transform target;
    public static PlayerCamera instance;
    public Cinemachine.CinemachineVirtualCamera playerCam;
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if(target)
        transform.position = target.position; // Nếu ko có target thì camera được điều chỉnh bên Playerbehaviour
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
        playerCam.Follow = transform;
    }
}