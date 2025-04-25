using DG.Tweening;
using UnityEngine;

public class UpgradeEffect : MonoBehaviour
{
    public float timeLife;
    public float height;
    public void Awake()
    {
        transform.DOMoveY(transform.position.y + height , timeLife)
            .OnComplete(()=> Destroy(gameObject));
    }
}