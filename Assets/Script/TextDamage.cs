using UnityEngine;

public class TextDamage : MonoBehaviour
{
    [SerializeField] TextMesh textMesh;
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 minSpeed;
    [SerializeField] float acceleration;
    Vector2 initialSpeed;
    [SerializeField] SpriteRenderer spriteRenderer;
    int direction;
    [SerializeField] float timeLife;
    void Update()
    {
        initialSpeed.y -= acceleration * Time.deltaTime;
        transform.position += (Vector3)initialSpeed * Time.deltaTime;
    }
    public void SetText(int damage,Color color , Sprite icon)
    {
        if(Random.Range(-1,1) >= 0)  direction = 1;
        else direction = -1;
        initialSpeed.x = Random.Range(minSpeed.x,maxSpeed.x) * direction;
        initialSpeed.y = Random.Range(minSpeed.y,maxSpeed.y);
        Invoke(nameof(ReturnToPool),0.6f);
        textMesh.text = damage.ToString();
        textMesh.color = color;
        spriteRenderer.sprite = icon;
    }
    void ReturnToPool()
    {
        TextDamePool.Instance.ReturnToPool(this);
    }
}
