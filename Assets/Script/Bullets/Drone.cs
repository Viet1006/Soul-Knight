using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Drone : FollowTargetBullet
{
    [SerializeField] GameObject shawdow;
    Material revealMat;
    Vector2 airForcePos ; // Nơi tạo ra Drone
    readonly float acceleration = 10f;
    readonly float rotateSpeed = 240; // Tốc độ quay
    AirForce airForce; // Nơi tạo ra Drone
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject bombPrefab;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        revealMat = new Material(ObjectHolder.instance.revealVerticalMat); // Tạo 1 mat mới
        spriteRenderer.material = revealMat;
        revealMat.SetFloat("_RevealProgress",1f);
    }
    public void SetDrone(float speed , int damage, int critChance,BulletElement element,List<BulletBuff> bulletBuffs, Transform target , AirForce airForce)
    {
        base.SetTargetBullet(speed, damage ,critChance ,element,bulletBuffs, target);
        transform.DOMoveY(transform.position.y + 3 ,0.4f).SetEase(Ease.InSine).OnComplete(() => // 0.4 là thời gian sản xuất mỗi Drone 
        {
            StartCoroutine (MoveToTarget());
            shawdow.SetActive(true);
            airForcePos = transform.position; // Lưu vị trí nơi tạo Drone bắt đầu
        });
        revealMat.DOFloat(0f , "_RevealProgress" , 0.3f).OnComplete(()=> spriteRenderer.material = ObjectHolder.instance.defaultMaterial); // Tạo hiệu ứng reveal
        this.airForce = airForce; // Đặt nơi tạo ta Drone
    }
    System.Collections.IEnumerator MoveToTarget()
    {
        float currentSpeed = 0;
        Vector2 currentDirection = Vector2.zero;
        while ((Vector2.Distance(transform.position - Vector3.up*3 , target.position ) > currentSpeed* Time.deltaTime && !isTargetDie)  // Nếu target chưa chết thì kiểm tra theo transform 
        || (Vector2.Distance(transform.position - Vector3.up*3 , targetPos ) > currentSpeed* Time.deltaTime && isTargetDie))// Nếu chết r thì kiểm tra theo pos
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed , speed , Time.deltaTime * acceleration); // Tăng tốc
            if(! isTargetDie) // Nếu target chưa chết
            {
                //Quay hướng về phía transform
                currentDirection = (target.position - transform.position + Vector3.up*3).normalized;
            } else 
            {
                // Nếu chết rồi quay hướng về nơi chết
                currentDirection = ((Vector3)targetPos - transform.position + Vector3.up*3).normalized;
            }
            Move(currentSpeed , currentDirection , target.position);
            yield return null;
        } // Đã đến đích thả bom và quay về

        DropBomb();
        // Bắt đầu quay về
        Vector2 targetDirection = (airForcePos - (Vector2)transform.position).normalized; // Tính hướng quay về
        float targetAngle = GetAngle(targetDirection); // Tính góc cần quay về
        float currentAngle = GetAngle(currentDirection); // Tính góc hiện tại
        int direction = targetAngle > currentAngle ? 1 : -1; // Xem nên quay về hướng nào gần hơn
        while (Vector2.Distance(transform.position, airForcePos) > 0.2f ) // Bắt đầu di chuyển về air force
        {
            targetDirection = (airForcePos - (Vector2)transform.position).normalized; // Tính Vector cần quay đến
            targetAngle = GetAngle(targetDirection);// Tính góc cần quay đến
            currentAngle = GetAngle(currentDirection); // Tính góc hiện tại
            if(Mathf.Abs (targetAngle - currentAngle) > Time.deltaTime * rotateSpeed) // Nếu góc cần quay lớn thì quay
            {
                currentAngle += direction * Time.deltaTime * rotateSpeed;
                currentDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad) , Mathf.Sin(currentAngle* Mathf.Deg2Rad)).normalized; // ĐỔI góc hiện tại sang vector để di chuyển Drone
                currentSpeed = Mathf.MoveTowards(currentSpeed , 0 , Time.deltaTime * 3); // Thay đổi tốc độ chậm lại khi rotate
            }else{ 
                currentDirection = (airForcePos - (Vector2)transform.position).normalized; // Nếu góc nhỏ quá thì thay đổi luôn thành target
                if(Vector2.Distance(transform.position, airForcePos) > 5)currentSpeed = Mathf.MoveTowards(currentSpeed , speed , Time.deltaTime * acceleration); // Tăng tốc nếu khoảng cách xa
                else currentSpeed = Mathf.MoveTowards(currentSpeed , speed/2 , Time.deltaTime * acceleration);
            }
            Move(currentSpeed , currentDirection , airForcePos);
            yield return null;
        }
        // Đã về vị trí ban đầu
        shawdow.SetActive(false);
        airForce.GetDrone();
        transform.DOMoveY(transform.position.y - 3 ,0.4f).SetEase(Ease.InSine).OnComplete(() =>
        {
            ReturnToPool();
        });
        transform.rotation = Quaternion.identity;
        spriteRenderer.material = revealMat;
        DOVirtual.DelayedCall(0.3f,() => revealMat.DOFloat(1f , "_RevealProgress" , 0.1f));
    }
    void DropBomb()
    {
        BulletPool.instance.GetBullet(bombPrefab , transform.position).GetComponent<Bomb>().SetBomb(0,damage,critChance,element,bulletBuffs);
    }
    void Move(float speed , Vector2 direction , Vector2 target)
    {
        transform.position += speed * Time.deltaTime * (Vector3)direction; // Di chuyển Drone
        if(target.x>transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }else if(target.x<transform.position.x){
            transform.rotation = Quaternion.Euler(0,-180,0);
        }
    }
    public float GetAngle(Vector2 vector) //Đổi vector sang góc từ 0-360 độ
    {
        float angle = Mathf.Atan2(vector.y,vector.x) * Mathf.Rad2Deg;
        if(angle < 0) angle += 360;
        return angle;
    }
}
