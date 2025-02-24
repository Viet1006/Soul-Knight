using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDamage : MonoBehaviour
{
    [SerializeField] TextMesh textMesh;
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 minSpeed;
    [SerializeField] float acceleration;
    Vector2 initialSpeed;
    int direction;
    [SerializeField] float timeLife;
    void Start()
    {
        initialSpeed.x = Random.Range(minSpeed.x,maxSpeed.x);
        initialSpeed.y = Random.Range(minSpeed.y,maxSpeed.y);
        if(Random.Range(-1f,1f) > 0)
        {
            direction = 1;
        }else{
            direction = -1;
        }
        Destroy(gameObject,timeLife);
    }
    void Update()
    {
        initialSpeed.y -= acceleration * Time.deltaTime;
        transform.position += new Vector3(initialSpeed.x * direction, initialSpeed.y, 0) * Time.deltaTime;
        Color color = textMesh.color;
        color.a -= Time.deltaTime / timeLife;
        textMesh.color = color;
    }
    public void SetText(float damage,Color color)
    {
        textMesh.text = damage.ToString();
        textMesh.color = color;
    }
}
