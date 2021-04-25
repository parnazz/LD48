using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot")]
public class LootTable : ScriptableObject
{
    public List<EquipmentList> lootTable;
    public Item healthPotion;
}

[Serializable]
public class EquipmentList
{
    public List<Equipment> equipment;
}