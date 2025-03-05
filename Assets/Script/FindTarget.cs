using System.Collections.Generic;
using UnityEngine;

public class FindTarget
{
    static public List<GameObject> GetAllObjetInRadius(Vector2 mainPos, float findRadius,int layerMask)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mainPos, findRadius,layerMask);
        List<GameObject> objectsInRadius = new List<GameObject>();
        foreach (Collider2D collider in colliders)
        {
            objectsInRadius.Add(collider.gameObject);
        }
        return objectsInRadius;
    }
    static public GameObject GetNearestObject(Vector2 mainPos,float findRadius,int LayerMask)
    {
        List<GameObject> objectsInRadius = GetAllObjetInRadius(mainPos, findRadius, LayerMask);
        GameObject nearestObject = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject obj in objectsInRadius)
        {
            float distance = Vector2.Distance(mainPos, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestObject = obj;
            }
        }
        return nearestObject;
    }
}
