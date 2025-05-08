using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class ShakeCamera
{
    private static ShakeCamera instance;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    private Tween stopTimer;

    public static ShakeCamera Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ShakeCamera();
                instance.virtualCamera = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
                instance.noise = instance.virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
            return instance;
        }
    }

    public void ShakeCam(Vector2 shakePos, float amplitude, float frequency, float duration, bool checkInCam = true)
    {
        if (!checkInCam || IsInCameraView(shakePos))
        {
            ApplyShake(amplitude, frequency);
            stopTimer?.Kill(); // Dùng null-conditional operator thay vì kiểm tra null
            stopTimer = DOVirtual.DelayedCall(duration, StopShakeCam,false);
        }
    }

    private void ApplyShake(float amplitude, float frequency)
    {
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;
    }

    private void StopShakeCam()
    {
        ApplyShake(0f, 0f); // Reset shake
    }

    private bool IsInCameraView(Vector2 worldPos)
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(worldPos);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
               viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
               viewportPoint.z > 0; // đảm bảo object không nằm phía sau camera
    }
}
