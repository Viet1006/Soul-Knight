using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;
    int value;
    public static PlayerHandleEffect player;
    public void SetHealth(int value) // Set Các giá trị cho coin
    {
        trail.Clear();
        StartCoroutine(FollowTargetRoutine(0.5f));
        this.value = value;
    }
    IEnumerator FollowTargetRoutine(float duration)
    {
        float time = 0f;
        Vector3 startPos = transform.position;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            transform.position = Vector3.Lerp(startPos, player.transform.position, t);
            yield return null;
        }
        transform.position = player.transform.position;
        HealthBar.instance.AddValue(value);
        player.currentHealth += value;
        if(player.currentHealth > player.heroData.health)
        {
            player.currentHealth = player.heroData.health;
        }
        BulletPool.Instance.ReturnBullet(this);
    }
}
