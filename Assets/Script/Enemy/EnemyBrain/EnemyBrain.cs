using System.Collections;
using Pathfinding;
using UnityEngine;
public class EnemyBrain : MonoBehaviour, IGetHit , ICanSelect
{
    public EnemyData enemyData;
    [SerializeField] GameObject selectionCircle;
    [SerializeField] ParticleSystem dieParticle;
    Collider2D colliderEnemy;
    protected Transform target; // Giữ tạm vị trí để phòng khi đang tấn công thì ko thấy player và lỗi khi xuay về phía player
    [SerializeField] Material material;
    float currentHealth;
    Renderer rendererEnemy;
    MaterialPropertyBlock propertyBlock;
    [HideInInspector] public AIPath aIPath;
    public event System.Action OnDie;
    void Start() 
    {
        aIPath = GetComponent<AIPath>();
        colliderEnemy = GetComponent<Collider2D>();
        aIPath.maxSpeed = enemyData.speed;
        rendererEnemy = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
        rendererEnemy.GetPropertyBlock(propertyBlock);
    }
    public virtual int InitEnemy()
    {
        currentHealth = enemyData.health;
        return enemyData.cost;
    }
    public void GetHit(float damage, Color colorDamage)
    {
        GameObject newTextDamage = Instantiate(enemyData.textDamage, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        newTextDamage.GetComponent<TextDamage>().SetText(damage, colorDamage);
        currentHealth -= damage;
        if(currentHealth <=0)
        {
            StartCoroutine(DieIEnum());
            return;
        }
        StartCoroutine(Blink());
    }
    protected virtual IEnumerator DieIEnum()
    {
        colliderEnemy.enabled = false; enabled = false; aIPath.canMove = false; 
        OnDie?.Invoke();
        propertyBlock.SetFloat("_FlashAmount", 1f);  // Làm trắng
        rendererEnemy.SetPropertyBlock(propertyBlock);
        AttackMethodEnemy attackMethod = GetComponent<AttackMethodEnemy>();
        attackMethod.enabled = false;
        attackMethod.StopAllCoroutines();
        for( float timeDie =1f ; timeDie >0 ; timeDie -= Time.deltaTime)
        {
            float shakeAmount = 0.03f;
            transform.position = new Vector2(
                transform.position.x + Random.Range(-shakeAmount, shakeAmount),
                transform.position.y + Random.Range(-shakeAmount, shakeAmount)
            );
            yield return null;
        }
        rendererEnemy.enabled = false; // Tắt hình ảnh
        dieParticle.Play();
        Invoke(nameof(ReturnToPool),1f);
    }
    protected virtual void ReturnToPool()
    {
        rendererEnemy.enabled = true;
        propertyBlock.SetFloat("_FlashAmount", 0f);
        rendererEnemy.SetPropertyBlock(propertyBlock);
        colliderEnemy.enabled = true; enabled = true; aIPath.canMove = true;
        ManageSpawnEnemy.instance.ReturnToPool(gameObject);
    }
    IEnumerator Blink()
    {
        propertyBlock.SetFloat("_FlashAmount", 1f);  // Làm trắng
        rendererEnemy.SetPropertyBlock(propertyBlock);
        yield return new WaitForSeconds(0.1f);
        propertyBlock.SetFloat("_FlashAmount", 0f);  // Trở lại bình thường
        rendererEnemy.SetPropertyBlock(propertyBlock);
    }
    public void ShowSelectObject()
    {
        selectionCircle.SetActive(true);
    }
    public void HideSelectObject()
    {
        selectionCircle.SetActive(false);
    }
}
