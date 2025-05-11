using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectHolder", menuName = "Object Holder")]
public class ObjectHolder : SerializedScriptableObject
{
    [BoxGroup("Materials")] public Material flashMaterial;
    [BoxGroup("Materials")] public Material defaultMaterial;
    [BoxGroup("Materials")] public Material revealMaterial;


    [BoxGroup("Prefabs")] [PreviewField (Height = 70)] public GameObject elementZone;
    [BoxGroup("Prefabs")] [PreviewField (Height = 70)] public GameObject textDamePrefab;
    [BoxGroup("Prefabs")] [PreviewField (Height = 70)] public GameObject iconEffect;
    [BoxGroup("Prefabs")] [PreviewField (Height = 70)] public GameObject coinPrebfab;
    [BoxGroup("Prefabs")] [PreviewField (Height = 70)] public GameObject dieEffect;
    [BoxGroup("Prefabs")] [PreviewField (Height = 70)] public GameObject explodeEffectPrefab;
    [BoxGroup("Prefabs")] [PreviewField (Height = 70)] public GameObject miniExplode;
    [BoxGroup("Prefabs")] [PreviewField (Height = 70)] public GameObject critTextPrefab;


    [DictionaryDrawerSettings(KeyLabel = "Element Type" , ValueLabel = "Element Icon")]
    [BoxGroup("Sprite")] public Dictionary<BulletElement , Sprite> elementIcon;

    [DictionaryDrawerSettings(KeyLabel = "Buff Type",ValueLabel = "Sprite Icon")]
    [BoxGroup("Sprite")] public Dictionary<BuffIconEnum,Sprite> iconSprite;
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