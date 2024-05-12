using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class StageManager : MonoBehaviour
{

    public Inventory inventory;
    public TextMeshProUGUI gameOverText;
    public GameObject playerCat;
    PlayerHPBase playerHp;
    PlayerMoveBase playerMove;
    ItemPickUp itemPickUp;

    public GameObject gameOverCanvas;
    public GameObject clearCanvas;

    public Image[] resultMemory = new Image[3];
    public Image[] myImage = new Image[3];
    public TextMeshProUGUI score;


    private static StageManager _instance;

    public static StageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }
            return _instance;
        }

    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(gameOverCanvas);
            //DontDestroyOnLoad(clearCanvas);
            gameOverCanvas.SetActive(false);
            clearCanvas.SetActive(false);

        }
        else
        {
            Destroy(gameObject);
        }

        Screen.orientation = ScreenOrientation.Portrait;

    }

    private void Start()
    {
        playerHp = playerCat.GetComponent<PlayerHPBase>();
        playerMove = playerCat.GetComponent<PlayerMoveBase>();
        itemPickUp = playerCat.GetComponent<ItemPickUp>();

    }

    public void StopGame()
    {
        gameOverCanvas.SetActive(true);
        if (inventory.isItemSlotFull)
        {
            gameOverText.text = "¾ÆÀÌÅÛ ²Ë Ã¡Áö·Õ";
        }
        else if (playerHp.isDie)
        {
            gameOverText.text = "Ã¼·Â ¼ÒÁø....";
        }
        else if (playerMove.isFallAndDie)
        {
            gameOverText.text = "³«ÇÏ~";
        }
        //else if (childCat.crashMonster)
        //{
        //    gameOverText.text = "¶¼²¬·èÇÑÅ× ¸ÂÀ½";
        //}

        Time.timeScale = 0f;

    }

    public void ClearGame()
    {
        Invoke("Clear", 1f);
    }

    public void Clear()
    {
        score.text = itemPickUp.score.ToString();
        for (int i = 0; i < myImage.Length; i++)
        {
            myImage[i].sprite = resultMemory[i].sprite;
        }
        clearCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
