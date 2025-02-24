using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/Enemies Data", order = 3)]
public class EnemyData : ScriptableObject
{
    public float health;
    public float speed;
    public GameObject textDamage;
}
