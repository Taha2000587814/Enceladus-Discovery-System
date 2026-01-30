using UnityEngine;

[CreateAssetMenu(fileName = "WallMaterialProfile", menuName = "Enceladus/Wall Material Profile")]
public class WallMaterialProfile : ScriptableObject
{
    [Header("Unstable Materials")]
    public Material passableUnstable;
    public Material unpassableUnstable;
}
