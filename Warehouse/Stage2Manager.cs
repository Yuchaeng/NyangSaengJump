using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Stage2Manager : MonoBehaviour
{
    public ChildCatMovement childCat;
    public GameObject gameOverCanvas;
    public TextMeshProUGUI gameOverText;
    public Inventory inventory;
    PlayerChild baby;
    public GameObject playerCat;
    ChildCatMovement movePlayer;


    private void Start()
    {
        baby = playerCat.GetComponent<PlayerChild>();
        movePlayer = playerCat.GetComponent<ChildCatMovement>();

    }

    public void StopGame()
    {

        if (inventory.isItemSlotFull)
        {
            gameOverText.text = "������ �� á����";
        }
        else if (baby.isDie)
        {
            gameOverText.text = "ü�� ����....";
        }
        else if (movePlayer.isFallAndDie)
        {
            gameOverText.text = "����~";
        }
        else if (childCat.crashMonster)
        {
            gameOverText.text = "���������� ����";
        }
        gameOverCanvas.SetActive(true);

        Time.timeScale = 0f;
    }

}
