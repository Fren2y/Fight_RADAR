using UnityEngine;

[CreateAssetMenu(fileName = "UData", menuName = "Fight/UnitData", order = 1)]
public class UnitData : ScriptableObject
{
    public float attackDmg = 1;
    public float attackSpeed = 1;
    public float attackRadius = 1;
    public float hp = 10;
}