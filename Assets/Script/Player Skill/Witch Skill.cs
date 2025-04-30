using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WitchSkill : BaseSkill
{
    PlayerBehaviour playerBehaviour;
    [SerializeField] GameObject effect;
    [SerializeField] GameObject strike;
    [SerializeField] BulletBuff bulletBuff;
    Animator strikeAnim;
    GameObject target;
    public int damage;
    void Awake()
    {
        strike = Instantiate(strike);
        playerBehaviour = GetComponent<PlayerBehaviour>();
        strikeAnim=strike.GetComponent<Animator>();
        strike.SetActive(false);
        
    }
    public override void UseSkill()
    {
        target = playerBehaviour.nearestEnemy;
        if(target) base.UseSkill();
    }
    protected override void PerformSkill()
    {
        effect.SetActive(true);
        strike.SetActive(true);
        effect.transform.rotation = Quaternion.identity;
        effect.transform.DORotate(new Vector3(0,0,-180), skillDuration)
            .SetEase(Ease.OutCubic)
            .OnComplete(()=> 
                {
                    effect.SetActive(false);
                    strike.SetActive(false);
                });
        strike.transform.position = target.transform.position;
        strikeAnim.SetTrigger("Strike");
        target.GetComponent<EnemyController>().GetHit(damage , BulletElement.Lightning);
        bulletBuff.ApplyBuff(target.GetComponent<Collider2D>() , transform.position);
    }
}
