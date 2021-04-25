using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Equipment item;

    public Image slotImage;

    public string slotTag;

    void Start()
    {
        slotImage = GetComponent<Image>();
    }
}
