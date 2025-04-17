using UnityEngine;

public class ChargingBar : MonoBehaviour
{
    static public ChargingBar instance;
    public Transform target;
    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    } 
    public void Update()
    {
        if(target) transform.position = new Vector2(target.position.x,target.position.y + 1.3f);
    }
    public SpriteRenderer GetSquare(int squareID)
    {
        return transform.GetChild(squareID).GetComponent<SpriteRenderer>();
    }
}
