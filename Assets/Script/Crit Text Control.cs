
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CritTextControl : MonoBehaviour
{
    TextMesh textMeshPro;
    readonly float effectDuration = 0.5f;
    void Awake()
    {
        textMeshPro = GetComponent<TextMesh>();
    }
    public void SetCritText(Vector2 pos)
    {
        gameObject.SetActive(true);
        transform.position = pos;
        textMeshPro.color = new Color(1, 1, 1, 1);
        transform.DOMoveY(pos.y + 1.5f, effectDuration);
        transform.DOScale(Vector2.one * 1.5f, effectDuration);
        StartCoroutine(FadeOut());
    }
    
    IEnumerator FadeOut()
    {
        float elapsed = 0f;
        while (elapsed < effectDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / effectDuration);
            textMeshPro.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        TextDamePool.Instance.ReturnCritText(this);
    }
}
