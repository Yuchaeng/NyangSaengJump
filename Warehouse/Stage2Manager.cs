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
            gameOverText.text = "¾ÆÀÌÅÛ ²Ë Ã¡Áö·Õ";
        }
        else if (baby.isDie)
        {
            gameOverText.text = "Ã¼·Â ¼ÒÁø....";
        }
        else if (movePlayer.isFallAndDie)
        {
            gameOverText.text = "³«ÇÏ~";
        }
        else if (childCat.crashMonster)
        {
            gameOverText.text = "¶¼²¬·èÇÑÅ× ¸ÂÀ½";
        }
        gameOverCanvas.SetActive(true);

        Time.timeScale = 0f;
    }

}
