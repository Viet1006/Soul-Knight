using DG.Tweening;
using UnityEngine;
public class WitchSkill : BaseSkill
{
    PlayerBehaviour playerBehaviour;
    [SerializeField] GameObject skillEffect;
    [SerializeReference] BulletBuff strikeBuff;
    Animator strikeAnim;
    [SerializeField] GameObject strikePrefab;
    GameObject target;
    public int damage;
    protected override void Awake()
    {
        base.Awake();
        playerBehaviour = GetComponent<PlayerBehaviour>();
        strikeAnim = Instantiate(strikePrefab).GetComponent<Animator>();
        strikeAnim.gameObject.SetActive(false);
    }
    public override void UseSkill()
    {
        target = playerBehaviour.nearestEnemy;
        if(target) base.UseSkill();
    }
    protected override void PerformSkill()
    {
        skillEffect.SetActive(true);
        strikeAnim.gameObject.SetActive(true);
        strikeAnim.transform.position = target.transform.position;
        strikeAnim.SetTrigger("Strike");
        skillEffect.transform.rotation = Quaternion.identity;
        skillEffect.transform.DORotate(new Vector3(0,0,-180), skillDuration)
            .SetEase(Ease.OutCubic)
            .OnComplete(()=> 
                {
                    skillEffect.SetActive(false);
                    strikeAnim.gameObject.SetActive(false);
                });
        target.GetComponent<EnemyController>().GetHit(damage , BulletElement.Lightning);
        strikeBuff.TryHandleOnObject(target.GetComponent<Collider2D>() , transform.position);
    }
}
