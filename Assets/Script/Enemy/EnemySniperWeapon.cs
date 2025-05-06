using DG.Tweening;
using UnityEngine;

public class EnemySniperWeapon : EnemyWithWeapon
{
    MoveToStatus moveToStatus;
    readonly float aimTime = 1.5f; // Thời gian ngắmn
    readonly float prepareTime = 0.5f; // Delay trước khi bắn
    public LineRenderer aimLine;
    Vector2 endPos;
    public bool canRotate = true;
    Tween attackTween;
    Tween prepareTween;
    void Start()
    {
        moveToStatus= GetComponent<MoveToStatus>();
    }
    protected override void StartAttack()
    {
        aimLine.enabled = true;
        canRotate = true;
        moveToStatus.StopMove();
        attackTween =DOVirtual.Float(0,aimTime ,aimTime ,value => 
        {
            Aiming(); // Cập nhật đường ngắm 
        }).OnComplete(PrepareFire);
    }
    
    void PrepareFire()
    {
        canRotate = false; // KO cho vũ khí rotate
        prepareTween= DOVirtual.Float(0,prepareTime ,prepareTime ,value => 
        {
            Aiming();// Cập nhật đường ngắm phòng bị đẩy
        }).OnComplete(() =>
        {
            CreateBullet();
            ResetTimeToAttack();
            aimLine.enabled = false; // tắt đường ngắm
            moveToStatus.ContinueMove();
            canRotate = true;
        });
    }
    public override void FlipToTarget()
    {
        if(canRotate) base.FlipToTarget();
    }
    protected override void RotateWeapon()
    {
        if(canRotate) base.RotateWeapon();
    }
    public void Aiming() // Ngắm 
    {
        endPos = Physics2D.Raycast(spawnBulletPos.position,weapon.right , LayerMask.GetMask("Wall")).point;
        aimLine.SetPosition(0,spawnBulletPos.position);
        aimLine.SetPosition(1,endPos);
    }
    public override void OnDisable() // Được gọi khi mới được tạo
    {
        base.OnDisable();
        canRotate = true;
        aimLine.enabled = false;
        attackTween.Kill(false);
        prepareTween.Kill(false);
    }
    public override void StopAttack()
    {
        base.StopAttack();
        attackTween.Kill(false);
        prepareTween.Kill(false);
        aimLine.enabled = false;
        canRotate = true;
    }
}
