using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-5)]
public class ObjectHolder : MonoBehaviour
{
    public static ObjectHolder instance;
    public Material flashMaterial;
    public Material defaultMaterial;
    public Material revealMaterial;
    public Material revealVerticalMat;
    public GameObject Fire;
    public GameObject FireZone;
    public List<GameObject> allWeaponPrefab;
    public GameObject bombPrefab;
    void Awake()
    {
        instance = this;
    }
}
