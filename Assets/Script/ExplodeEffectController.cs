using UnityEngine;

public class ExplodeEffectController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem particle;
    void OnEnable()
    {
        anim.SetTrigger(Parameters.explode);
        particle.Play();
    }
}
