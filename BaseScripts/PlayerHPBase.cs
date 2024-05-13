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
            moveBase.fallButLive = false;  //ü�� �ѹ��� ������ �ٷ� ���ƾ���
            Debug.Log("���� �� ü��" + playerHp);
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
