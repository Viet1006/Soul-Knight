using UnityEngine;

public class ExplodeEffectController : MonoBehaviour
{
    [SerializeField] Animator anim;
    void OnEnable()
    {
        anim.SetTrigger(Parameters.explode);
    }
}
