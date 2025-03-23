using UnityEngine;
public class SummoningTower : BaseTower
{
    public Transform target; // Lấy để tạo hình triệu hồi trên đầu quái thay vì lấy nearestEnemy
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = target.GetComponent<SpriteRenderer>();
    }
    protected override void Attack(Transform target)
    {
    }
    protected override void Update()
    {
        base.Update();
        spriteRenderer.color = new Color(spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b,spriteRenderer.color.a+Time.deltaTime*towerData.fireRate);
        if(nearestEnemy)
        {
            target.position = new Vector2(nearestEnemy.transform.position.x,nearestEnemy.transform.position.y+1.2f);
            target.gameObject.SetActive(true);
        }else{
            target.gameObject.SetActive(false);
        }
    }
    void ResetColor()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b,0);
    }
}
