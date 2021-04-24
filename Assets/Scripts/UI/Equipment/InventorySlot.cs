using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventorySlot : MonoBehaviour
{
    private SignalBus _signalBus;
    private Item _item;

    private Image _slotImage;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<LootDropSignal>(OnLootDropped);
    }

    private void OnLootDropped(LootDropSignal signal)
    {
        _item = signal.item;
        _slotImage.sprite = signal.item.icon;
    }

    void Start()
    {
        _slotImage = GetComponent<Image>();
    }
}
