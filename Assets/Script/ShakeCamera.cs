using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class ShakeCamera
{
    CinemachineVirtualCamera virtualCamera;
    static ShakeCamera instance;
    public static ShakeCamera Instance
    {
       get
       {
            if(instance == null) 
            {
                instance = new ShakeCamera();
                instance.virtualCamera = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
            }
            return instance;
       }
    }
    Tween stopTimer;
    // Rung cam với biên độ Am, tần số Fre, thời gian Time, PosGameObj là vị trí objct gọi hàm
    public void ShakeCam(Vector2 shakePos,float am, float fre, float time , bool checkInCam = true){ // Mặc định là kiểm tra có trong camera không mới rung 
        if(IsInCameraView(shakePos) || !checkInCam){
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = am;
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = fre;
            stopTimer.Kill();
            stopTimer = DOVirtual.DelayedCall(time , StopShakeCam);
        }
    }
    void StopShakeCam()
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
    bool IsInCameraView(Vector2 posCheck) // Kiểm tra vị trí được thêm vào có trong Scene không
    {
        posCheck = Camera.main.WorldToViewportPoint(posCheck); // Chuyển sang ViewPortPoint
        return !(posCheck.x<0 || posCheck.x>1 || posCheck.y<0 || posCheck.y>1);
    }
    
}