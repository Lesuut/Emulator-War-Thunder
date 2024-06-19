using UnityEngine;

[CreateAssetMenu(fileName = "Ammunition", menuName = "CustomData/AmmunitionInfo", order = 1)]
public class ProjectileInfo : ScriptableObject
{
    public ProjectileType projectileType = ProjectileType.Absent;
    public GameObject bodyPrefab;
    public GameObject sleevePrefab;
}
public enum ProjectileType
{
    Absent = 0,
    BB = 1,
    OF = 2,
}