using UnityEngine;

public class Status : MonoBehaviour
{
    public static Status instance;
    void Awake()
    {
        instance = this;
    }
}
