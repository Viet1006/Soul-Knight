using UnityEngine;

public class RocketFirework : BaseWeapon
{
    SpriteRenderer spriteRenderer;
    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Update()
    {
        timeToNextFire -= Time.deltaTime;
        if(timeToNextFire >= 0)
        {
            spriteRenderer.enabled = false;
        }else{
            spriteRenderer.enabled = true;
        }
    }
}
