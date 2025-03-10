using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerShopManage : MonoBehaviour
{
    public static TowerShopManage instance;
    void Awake()
    {
        instance = this;
        placeTowerCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }
    GameObject currentTower;
    Vector2 mousePos;
    [SerializeField] GameObject buyTowerPanel;
    [SerializeField] List<Vector2> towerPos;
    Cinemachine.CinemachineVirtualCamera placeTowerCamera;
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(currentTower != null && !Physics2D.OverlapCircle(GetNearestTowerPosition(mousePos),0.1f,LayerMask.GetMask("Tower")))
        {
            currentTower.transform.position = GetNearestTowerPosition(mousePos);
        }
        if (Mouse.current.leftButton.wasPressedThisFrame && currentTower != null)
        {
            PutTower();
        }
    }
    Vector2 GetNearestTowerPosition(Vector2 mousePosition)
    {
        Vector2 nearestPos = Vector2.zero;
        float minDistance = Mathf.Infinity;
        foreach (Vector2 towerPosition in towerPos)
        {
            float distance = Vector2.Distance(mousePosition, towerPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPos = towerPosition;
            }
        }
        return nearestPos;
    }
    void PutTower()
    {
        currentTower.GetComponent<Animator>().enabled = true;
        currentTower.GetComponent<BaseTower>().enabled = true;
        currentTower.GetComponent<Collider2D>().enabled = true;
        currentTower = null;
        placeTowerCamera.Priority = 10;
    }
    public void PickUpCard(GameObject tower)
    {
        currentTower = Instantiate(tower);
        buyTowerPanel.SetActive(false);
    }
}
