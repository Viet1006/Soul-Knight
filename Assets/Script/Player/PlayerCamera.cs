using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerTracker;

    void Start()
    {
        playerTracker = GameObject.Find("PlayerTracker").transform;
    }
    void Update()
    {
        playerTracker.position = transform.position;
    }
}
