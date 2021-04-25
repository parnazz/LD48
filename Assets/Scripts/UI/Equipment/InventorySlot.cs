using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class InventorySlot : MonoBehaviour, IPointerDownHandler
{
    public Item _item;

    public Image _slotImage;

    private GameController _gameController;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(GameController gameController,
        SignalBus signalBus)
    {
        _gameController = gameController;
        _signalBus = signalBus;
    }

    void Start()
    {
        _slotImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_gameController.GameState != GameState.ExploreState) return;

        if (_item == null) return;
            
        Debug.Log(_item.name);
        _signalBus.Fire(new UseItemSignal { item = _item });
        _item = null;
        _slotImage.sprite = null;
    }
}
