using Unity.Netcode;
using UnityEngine;

public class NetworkManagerScipt : MonoBehaviour
{
    public void Client()
    {
        NetworkManager.Singleton.StartClient();
    }
    public void Host()
    {
        NetworkManager.Singleton.StartHost();
    }
    public void Disconnect()
    {
        NetworkManager.Singleton.Shutdown();
    }
}
