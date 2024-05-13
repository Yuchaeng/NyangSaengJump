using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class PlayerChild : PlayerHPBase
{
    private float _currentHp;
    private float _maxHp;
    public override float playerHp { get => _currentHp; set => _currentHp = value; }
    public override float playerHpMax { get => _maxHp; set => _maxHp = value; }

    protected override void Start()
    {
        _currentHp = 45f;
        _maxHp = 45f;
        base.Start();
    }

 }
