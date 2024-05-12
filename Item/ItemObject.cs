using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IItemObject
{
    [Header("Item")]
    public Item item;
    [Header("Item Image")]
    public Sprite itemImage;

    public Item getItemInfo()
    {
        return item;
    }

    private void Start()
    {
        itemImage = item.sprite;
    }


}
