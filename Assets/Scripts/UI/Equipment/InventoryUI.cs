using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Transform _inventorySlotsTransform;

    private List<InventorySlot> _inventorySlots;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<LootDropSignal>(OnLootDropped);
    }

    void Start()
    {
        var slots = _inventorySlotsTransform.GetComponentsInChildren<InventorySlot>();
        _inventorySlots = new List<InventorySlot>(slots);
    }

    private void OnLootDropped(LootDropSignal signal)
    {
        foreach (var slot in _inventorySlots)
        {
            if (slot._slotImage.sprite == null)
            {
                slot._item = signal.item;
                slot._slotImage.sprite = signal.item.icon;
                return;
            }
        }
    }
}
