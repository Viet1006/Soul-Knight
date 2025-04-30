using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TowerLight : MonoBehaviour
{
    public List<Color> colors = new List<Color>();
    public float durationPerColor = 0.5f;
    public Ease easeType = Ease.Linear;
    
    private Light2D towerLight;
    private Sequence colorSequence;

    void Awake()
    {
        towerLight = GetComponent<Light2D>();
        
        if (towerLight == null)
        {
            Debug.LogError("Không tìm thấy component Light2D!");
            return;
        }
        
        if (colors.Count == 0)
        {
            Debug.LogWarning("Danh sách màu trống!");
            colors.Add(Color.white); // Thêm màu mặc định
        }
        
        SetupColorAnimation();
    }

    void SetupColorAnimation()
    {
        colorSequence = DOTween.Sequence();
        
        foreach (Color targetColor in colors)
        {
            // Sử dụng DOTween.To để tween màu
            colorSequence.Append(DOTween.To(
                () => towerLight.color, // Getter
                x => towerLight.color = x, // Setter
                targetColor,
                durationPerColor
            ).SetEase(easeType));
        }
        
        colorSequence.SetLoops(-1, LoopType.Restart);
    }

    void OnDestroy()
    {
        if (colorSequence != null)
            colorSequence.Kill();
    }
}