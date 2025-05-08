using System.Collections;
using UnityEngine;
public class CoinController : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;
    int value;
    public void SetCoin(int value) // Set Các giá trị cho coin
    {
        trail.Clear();
        StartCoroutine(FollowTargetRoutine(CoinManager.instance.transform, 0.5f));
        this.value = value;
    }
    IEnumerator FollowTargetRoutine(Transform target, float duration)
    {
        float time = 0f;
        Vector3 startPos = transform.position;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            transform.position = Vector3.Lerp(startPos, target.position, t);
            yield return null;
        }

        transform.position = target.position;
        CoinManager.instance.AddCoin(value);
        CoinPool.Instance.ReturnToPool(this);
    }
}
