using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour , ICanSelect
{
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected GameObject textDamage;
    [SerializeField] protected BaseWeapon currentWeapon;
    [SerializeField] GameObject selectionCircle;
    Vector2 statusPos = new Vector2(-1.5f,52);
    public Vector2 target;
    public void ShowSelectObject()
    {
        selectionCircle.SetActive(true);
    }
    public void HideSelectObject()
    {
        selectionCircle.SetActive(false);
    }
    public virtual void GetHit(float damage,Color colorDamage)
    {
        GameObject newTextDamage =  Instantiate(textDamage,transform.position + new Vector3(0,1f,0),Quaternion.identity);
        newTextDamage.GetComponent<TextDamage>().SetText(damage,colorDamage);
    }
    public virtual void Update()
    {
        target = statusPos;
        transform.position = Vector2.MoveTowards(transform.position,target,5f*Time.deltaTime);
    }
    public void FlipToTarget() // Lật theo target
    {
        if(target.x>transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }else if(target.x<transform.position.x){
            transform.rotation = Quaternion.Euler(0,-180,0);
        }
    }
}
