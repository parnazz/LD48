using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatStats", menuName = "Stats")]
public class BaseStats : ScriptableObject
{
    public float _maxHealthPoints;
    public float _defense;
    public float _damage;
}
