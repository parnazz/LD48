using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Item _item;

    private Image _slotImage;

    void Start()
    {
        _slotImage = GetComponent<Image>();

        _slotImage.sprite = _item.icon;
    }
}
