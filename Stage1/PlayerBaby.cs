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
    private float _currentHp;
    private float _maxHp;
    public override float playerHp { get => _currentHp; set => _currentHp = value; }
    public override float playerHpMax { get => _maxHp; set => _maxHp = value; }

    protected override void Start()
    {
        _currentHp = 30f;
        _maxHp = 30f;

        Debug.Log($"myHp 자식에서 호출 {playerHp}");

        targetMaterial = GameObject.Find("cat").GetComponentInChildren<SkinnedMeshRenderer>();
        ratCrashed = false;

        base.Start();

    }

    protected override void Update()
    {
        base.Update();
        Debug.Log($"myHp 자식에서 호출 {playerHp}");

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
