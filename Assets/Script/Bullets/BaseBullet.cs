using UnityEngine;
// Lớp cơ sở các loại đạn
public class BaseBullet : MonoBehaviour
{
    [SerializeField] protected BulletData bulletData;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected Animator animator;
    [SerializeField] ParticleSystem explodeEffect;
    [SerializeField] protected float damage;
    bool isExplode;
    public virtual void SpawnBullet(Vector3 initialPos,Quaternion initialQuaternion,Vector2 target,float damage)
    {
        Instantiate(bullet,initialPos,initialQuaternion);
        this.damage = damage;
    }
    void Update()
    {
        if(!isExplode)
        {
            transform.position += Time.deltaTime * bulletData.speed * transform.right;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
    public virtual void HandleCollision(Collider2D collision)
    {
        isExplode = true;
        animator.SetTrigger("Explode");
        explodeEffect.Play();
        BaseEnemy hittedEnemy = collision.gameObject.GetComponent<BaseEnemy>();
        if(hittedEnemy)
        {
            hittedEnemy.GetHit(damage,bulletData.colorDamage);
        }
    }
}