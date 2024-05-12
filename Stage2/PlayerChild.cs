using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class PlayerChild : PlayerHPBase
{


    protected override void Start()
    {
        base.Start();

        playerHp = 60;
        playerHpMax = 60;
        playerHpSlider.value = playerHp / playerHpMax;


        //Stage2Manager.Instance.StopGame();

    }

 }
