using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SkeletonKing : AttackMethodEnemy
{
    [SerializeField] GameObject skeBulletPrefab;
    readonly float skeBulletSpeed = 6;
    readonly int skeBulletDamage = 4;
    [SerializeField] List<GameObject> skePrefab = new();
    bool isAttack;
    [SerializeField] GameObject duplicateBulletPrefab;
    List<System.Action> skeAction = new();
    [SerializeField] SpawnArea spawnArea;
    [SerializeField] Animator weapon1;
    [SerializeField] Animator weapon2;
    bool isDie;
    protected override void Awake()
    {
        base.Awake();
        skeAction.AddRange(new System.Action[] {UseFollowBullet,SpawnRandomSkeleton , UseDuplicateBullet } );
        StartRandomAction();
    }
    void StartRandomAction()
    {
        if(isDie) return;
        DOVirtual.DelayedCall(Random.Range(2f, 3f), () =>
        {
            skeAction[Random.Range(0, skeAction.Count)].Invoke();
        }, false);
    }
    void UseFollowBullet()
    {
        StartRandomAction();
        if(target == null) return;
        float randomAngle = Random.Range(0,360/5);
        for (int i = 0; i < 5; i++)
        {
            float angle = i * 360f / 5 + randomAngle; // Chia đều góc
            float radians = angle * Mathf.Deg2Rad; // Đổi sang radian
            float radius = 2f; // Bán kính vòng tròn

            Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * radius;
            Vector3 spawnPos = transform.position + offset;
            BulletPool.Instance.GetBullet<SkeletonKingBullet>(skeBulletPrefab, spawnPos,Quaternion.Euler(0,0,angle))
                .SetFollowBullet(skeBulletSpeed , skeBulletDamage , 0 , BulletElement.NoElement ,null, target , 4);
        }
    }
    void SpawnRandomSkeleton()
    {
        weapon2.SetTrigger("SpawnEnemy");
        DOVirtual.DelayedCall(0.4f , () =>
        {
            // Chọn prefab ngẫu nhiên từ danh sách
            GameObject prefabToSpawn = skePrefab[Random.Range(0, skePrefab.Count)];
            Vector2 spawnPos;
            do
            {
                spawnPos = spawnArea.GetRandomPosition();
            }while(Physics2D.OverlapCircle(spawnPos , 6f , LayerMask.GetMask("Default")));
            ManageSpawnEnemy.instance.SpawnEnemy(prefabToSpawn,spawnArea.GetRandomPosition());
        },false).SetLoops(8).OnComplete(StartRandomAction);
    }
    void UseDuplicateBullet()
    {
        StartRandomAction();
        if(target == null) return;
        weapon1.SetTrigger("Duplicate Bullet");
        DOVirtual.DelayedCall(0.9f , ()=>
        {
            BulletPool.Instance.GetBullet<DuplicateBullet>(duplicateBulletPrefab, transform.position, target.position)
                .SetDuplicateBullet(skeBulletSpeed, skeBulletDamage, 4, 1, duplicateBulletPrefab , 4) ;
        });
    }
    protected override void StartAttack()
    {
    }
    public override void StopAttack()
    {
        
    }
    public override void ContinueAttack()
    {
        
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        isDie = true;
    }
}