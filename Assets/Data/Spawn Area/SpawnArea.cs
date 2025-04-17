using UnityEngine;
[CreateAssetMenu(fileName = "NewArea", menuName = "NewArea")]
public class SpawnArea : ScriptableObject
{
    public Vector2 topLeft;
    public Vector2 bottomRight;
    public Vector2 GetRandomPosition() => new(Random.Range( topLeft.x, bottomRight.x), Random.Range( topLeft.y, bottomRight.y)); 
}