using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBase : MonoBehaviour
{
    public Slider playerHpSlider;
    [SerializeField] private float _decreaseSpeed;

    protected PlayerMoveBase moveBase;

    public float playerHp { get; set; }
    public float playerHpMax { get; set; }

    private Animator animator;

    public bool isDie;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("baseHp");
    }

    protected virtual void Update()
    {
        playerHp -= _decreaseSpeed * Time.deltaTime;
        playerHpSlider.value = playerHp / playerHpMax;

        if (moveBase.fallButLive)
        {
            playerHp -= 5f;
            moveBase.fallButLive = false;  //체력 한번만 내리고 바로 막아야함
            Debug.Log("감소 후 체력" + playerHp);
        }

        if (playerHp <= 0)
        {
            playerHp = 0;
            moveBase.isMovable = false;
            isDie = true;
            Time.timeScale = 0;
            animator.Play("Neko - loaf");
            StageManager.Instance.StopGame();
        }

    }
}
