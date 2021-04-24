using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatStats", menuName = "Stats")]
public class CombatStats : ScriptableObject
{
    public float _healthPoints;
    public float _defense;
    public float _damage;
}
