using UnityEngine;
[CreateAssetMenu(fileName = "NewHero", menuName = "HeroData")]
public class HeroData : ScriptableObject
{
    public int health;
    public float speed;
    public const float acceleration = 20f;
}