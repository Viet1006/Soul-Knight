using UnityEngine;

public class ChargingBar : MonoBehaviour
{
    public Transform target;
    public void Update()
    {
        if(target) transform.position = new Vector2(target.position.x,target.position.y + 1.3f);
    }
}
