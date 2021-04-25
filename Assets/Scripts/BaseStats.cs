using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatStats", menuName = "Stats")]
public class BaseStats : ScriptableObject
{
    public float maxHealthPoints;
    public float defense;
    public float damage;
    public float attackSpeed;
}
