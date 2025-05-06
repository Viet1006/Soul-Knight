using System.Collections.Generic;
using UnityEngine;

public class ChargingBar : MonoBehaviour
{
    static public ChargingBar instance;
    public Transform target;
    List<SpriteRenderer> squares = new();
    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
        for(int i = 0 ; i< 5; i++)
        {
            squares.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
    } 
    public void Update()
    {
        if(target) transform.position = new Vector2(target.position.x,target.position.y + 1.3f);
    }
    public SpriteRenderer GetSquare(int squareID)
    {
        return squares[squareID];
    }
    public void Show(Transform target)
    {
        this.target = target;
        gameObject.SetActive(true);
        for (int i = 0 ; i< 5 ;i++)
        {
            GetSquare(i).color = Color.black;
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
