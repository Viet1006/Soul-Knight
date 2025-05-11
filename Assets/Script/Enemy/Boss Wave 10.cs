using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossWave10 : MonoBehaviour
{
    MoveToStatus moveToStatus;
    [SerializeField] GameObject bouncePrefab;
    readonly float bounceSpeed = 15f;
    readonly float bounceCooldown = 4f;
    readonly int bounceDamage = 3;
    [SerializeField] GameObject spawnPrefab;
    readonly float spawnSpeed = 10f;
    readonly float spawnCooldown = 6f;
    readonly int spawnDamage = 3;
    [SerializeField] List<LineRenderer> lineRenderers ;
    [SerializeField] GameObject lineHolder;
    Tween bounceTween;
    Tween spawnTween;
    bool isAttack;
    void Awake()
    {
        bounceTween = DOVirtual.DelayedCall(bounceCooldown  ,() => StartCoroutine(UseBounceBullet()) , false).SetLoops(-1);
        moveToStatus = GetComponent<MoveToStatus>();
        spawnTween = DOVirtual.DelayedCall(spawnCooldown  , () => StartCoroutine(UseSpawnBullet()) , false).SetLoops(-1);
    }
    IEnumerator UseBounceBullet()
    {
        yield return new WaitUntil(() => !isAttack);
        float randomAngle = Random.Range(0,360/6);
        for (int i = 0; i < 6; i++)
        {
            float angle = i * 360f / 6 + randomAngle; // Chia đều góc
            float radians = angle * Mathf.Deg2Rad; // Đổi sang radian
            float radius = 1.5f; // Bán kính vòng tròn

            Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * radius;
            Vector3 spawnPos = transform.position + offset;
            BulletPool.Instance.GetBullet<BulletPierceAndBounce>(bouncePrefab, spawnPos,Quaternion.Euler(0,0,angle))
                .SetBullet(bounceSpeed , bounceDamage , 0 , BulletElement.NoElement , null , 15);
        }
    }
    IEnumerator UseSpawnBullet()
    {
        yield return new WaitUntil(() => !isAttack);
        isAttack = true;
        lineHolder.SetActive(true);
        moveToStatus.StopMove(); // Dừng lại
        float rollTime = Random.Range(1.5f ,2); // Thời gian quay
        while(rollTime > 0)
        {
            rollTime -= Time.deltaTime;
            lineHolder.transform.Rotate(Vector3.forward, 180 * Time.deltaTime );
            for (int i = 0; i < lineRenderers.Count; i++)
            {
                lineRenderers[i].SetPosition(0, lineRenderers[i].transform.position);
                lineRenderers[i].SetPosition(1, Physics2D.Raycast(lineRenderers[i].transform.position,lineRenderers[i].transform.right , float.MaxValue,  LayerMask.GetMask("Wall")).point);
            }
            yield return null;
        }
        DOVirtual.DelayedCall(1 , () => 
            {
                for (int i = 0; i < lineRenderers.Count; i++)
                {
                    BulletPool.Instance.GetBullet<BulletPierceAndBounce>(spawnPrefab, lineRenderers[i].transform.position,lineRenderers[i].transform.rotation)
                        .SetBullet(spawnSpeed , spawnDamage , 0 , BulletElement.NoElement , null , 15);
                }
                lineHolder.SetActive(false);
                moveToStatus.ContinueMove();
                isAttack = false;
            },false);
    }
    void OnDisable()
    {
        bounceTween.Kill();
        spawnTween.Kill();
        lineHolder.SetActive(false);
    }
}
