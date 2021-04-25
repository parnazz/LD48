using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class InventorySlot : MonoBehaviour, IPointerDownHandler
{
    public Item item;

    public Image slotImage;

    private GameController _gameController;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(GameController gameController,
        SignalBus signalBus)
    {
        _gameController = gameController;
        _signalBus = signalBus;
    }

    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_gameController.GameState != GameState.ExploreState) return;

        if (item == null) return;
            
        _signalBus.Fire(new UseItemSignal { item = item });
        item = null;
        slotImage.sprite = null;
    }
}
