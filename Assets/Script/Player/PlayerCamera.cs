using Unity.Netcode;
using Cinemachine;
using UnityEngine;

public class PlayerCamera : NetworkBehaviour
{
    public Transform playerTracker;

    public override void OnNetworkSpawn()
    {
        if (IsOwner) // Chỉ áp dụng camera cho player của chính mình
        {
            playerTracker = GameObject.Find("PlayerTracker").transform;
        }
    }
    void Update()
    {
        if(!IsOwner) return;
        playerTracker.transform.position = transform.position;
    }
}
