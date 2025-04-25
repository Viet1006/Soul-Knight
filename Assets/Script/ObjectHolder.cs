using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectHolder", menuName = "Object Holder")]
public class ObjectHolder : SerializedScriptableObject
{
    [BoxGroup("Materials")] public Material flashMaterial;
    [BoxGroup("Materials")] public Material defaultMaterial;
    [BoxGroup("Materials")] public Material revealMaterial;
    [BoxGroup("Materials")] public Material revealVerticalMat;
    
    [BoxGroup("Prefabs")] public GameObject Fire;
    [BoxGroup("Prefabs")] public GameObject FireZone;

    [DictionaryDrawerSettings(KeyLabel = "Element Type" , ValueLabel = "Element Icon")]
    [BoxGroup("Sprite")] public Dictionary<BulletElement , Sprite> elementIcon;

    private static ObjectHolder _instance;
    
    public static ObjectHolder Instance
    {
        get
        {
            if (_instance == null)
            {
                // Tìm trong Resources
                _instance = Resources.Load<ObjectHolder>("ObjectHolder");
            }
            return _instance;
        }
    }
}