using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventorySlot : MonoBehaviour
{
    public Item _item;

    public Image _slotImage;

    void Start()
    {
        _slotImage = GetComponent<Image>();
    }
}
