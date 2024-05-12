using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class PlayerBaby : PlayerHPBase
{
    float currentTime = 0;
    float delayTime = 1;
    public SkinnedMeshRenderer targetMaterial;
    bool ratCrashed;

    protected override void Start()
    {
        base.Start();

        playerHp = 30;
        playerHpMax = 30;
        playerHpSlider.value = playerHp / playerHpMax;
        
        targetMaterial = GameObject.Find("cat").GetComponentInChildren<SkinnedMeshRenderer>();

        ratCrashed = false;
        
    }

    protected override void Update()
    {
      
        if (ratCrashed)
        {
            moveBase.isMovable = false;

            currentTime += Time.deltaTime;

            if ((currentTime >= 0.3f && currentTime <= 0.5f) || (currentTime >= 0.7f && currentTime <= 0.9f))
            {
                targetMaterial.materials[0].color = Color.gray;
            }
            else
            {
                targetMaterial.materials[0].color = Color.white;
            }

            if (currentTime > delayTime)
            {
                ratCrashed = false;
                moveBase.isMovable = true;
                currentTime = 0;
            }
        }

    }

   

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("Enemy_Rat"))
        {
            other.gameObject.SetActive(false);
            if (!other.gameObject.activeSelf)
            {
                playerHp -= 10f;
                ratCrashed = true;
            }
        }

    }



   




}
