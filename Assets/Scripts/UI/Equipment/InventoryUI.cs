using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Transform _inventorySlotsTransform;

    [SerializeField]
    private Transform _equipmentSlotsTransform;

    private List<InventorySlot> _inventorySlots;
    private List<EquipmentSlot> _equipmentSlots;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<LootDropSignal>(OnLootDropped);
        _signalBus.Subscribe<UseItemSignal>(OnUseItem);
        _signalBus.Subscribe<HealingSignal>(OnUseHealthPotion);
    }

    void Start()
    {
        AddInventorySlots();
        AddEquipmentSlots();
    }

    private void AddEquipmentSlots()
    {
        var slots = _equipmentSlotsTransform.GetComponentsInChildren<EquipmentSlot>();
        _equipmentSlots = new List<EquipmentSlot>(slots);
    }

    private void AddInventorySlots()
    {
        var slots = _inventorySlotsTransform.GetComponentsInChildren<InventorySlot>();
        _inventorySlots = new List<InventorySlot>(slots);
    }

    private void OnLootDropped(LootDropSignal signal)
    {
        foreach (var slot in _inventorySlots)
        {
            if (slot.item == null)
            {
                slot.item = signal.item;
                slot.slotImage.sprite = signal.item.icon;
                return;
            }
        }
    }

    private void OnUseItem(UseItemSignal signal)
    {
        if (signal.item.itemTag != "Consumable")
        {
            foreach (var slot in _equipmentSlots)
            {
                if (slot.slotTag == signal.item.itemTag)
                {
                    slot.item = (Equipment)signal.item;
                    slot.slotImage.sprite = signal.item.icon;
                    _signalBus.Fire(new ItemEquipedSignal { item = slot.item });
                    return;
                }
            }
        }

        _signalBus.Fire(new UseHealthPotionSignal { item = (HealthPotion)signal.item });
    }

    private void OnUseHealthPotion(HealingSignal signal)
    {
        foreach (var slot in _inventorySlots)
        {
            if (slot.item != null && slot.item.itemTag == "Consumable")
            {
                UsePotion(slot);
                return;
            }
        }
    }

    private void UsePotion(InventorySlot slot)
    {
        _signalBus.Fire(new UseHealthPotionSignal { item = (HealthPotion)slot.item });
        slot.item = null;
        slot.slotImage.sprite = null;
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<LootDropSignal>(OnLootDropped);
        _signalBus.Unsubscribe<UseItemSignal>(OnUseItem);
        _signalBus.Unsubscribe<HealingSignal>(OnUseHealthPotion);
    }
}
