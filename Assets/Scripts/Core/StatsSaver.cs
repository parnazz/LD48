using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSaver : MonoBehaviour
{
    [SerializeField]
    private BaseStats _baseStats;

    public Character.CharacterStats characterStats;
    public static float maxHealth;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        characterStats = new Character.CharacterStats();

        maxHealth = _baseStats.maxHealthPoints;
        characterStats.currentHealth = _baseStats.maxHealthPoints;
        characterStats.damage = _baseStats.damage;
        characterStats.defense = _baseStats.defense;
        characterStats.attackSpeed = _baseStats.attackSpeed;
    }
}
