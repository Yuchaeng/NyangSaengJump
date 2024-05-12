using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image image;

    public Item item
    {
        get
        {
            return _item;
        }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.sprite;
                image.color = UnityEngine.Color.white;
            }
            else
            {
                image.sprite = null;
                UnityEngine.Color color = new Color32(89, 89, 89, 255);
                image.color = color;

            }
        }
    }
    private Item _item;
    
}
