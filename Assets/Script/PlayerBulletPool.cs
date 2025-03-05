using System.Collections.Generic;
using UnityEngine;
public class PlayerBulletPool : MonoBehaviour
{
    public int initialSize; // Số đạn ban đầu khởi tạo sẵn
    [SerializeField] List<GameObject> bulletPrefabs;
    public List<Queue<GameObject>> pool;
    void Awake()
    {
        for(int i = 0 ; i < bulletPrefabs.Count; i++)
        {
            for (int j = 0 ; j< initialSize ; j++)
            {
                Debug.Log("i");
                GameObject newBullet = Instantiate(bulletPrefabs[i]);
                newBullet.SetActive(false);
                pool[i].Enqueue(newBullet);
            }
        }
    }
    private void PreloadBullets()
    {
        
    }
}
