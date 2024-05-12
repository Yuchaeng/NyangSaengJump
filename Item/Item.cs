using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        OnlyHp,
        Buff,
        Debuff,
        NoneEffect,
        Photo,
    }

    public ItemType Type => _type;
    public string Name => _name;
    public string Caption => _caption;
    public Sprite sprite => _sprite;
    public GameObject ItemPrefab => _itemPrefab;

    [SerializeField] ItemType _type;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _caption;
    [SerializeField] private GameObject _itemPrefab;
}
