using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{
    public Inventory inventory;
    public Slider playerHpSlider;
    public float PlayerHp { get; set; }
    public float PlayerHpMax { get; set; }

    public PlayerMoveBase playerMove;
    PlayerHPBase myHP;

    public Animator animator;

    PlayerBaby playerBaby;

    WaitForSeconds waitForMatch;
    Collider myCollider;

    public int score;
    public TextMeshProUGUI scoreText;

    public Transform yarnPosition;
    public GameObject yarnPrefab;

    public bool isCatnipMove;
    public int memoryCollect;

    PlayerChild playerChild;
    public ChildCatMovement childMove;
    bool isYarnCorouting;

    private void Start()
    {
        playerMove = GetComponent<PlayerMoveBase>();
        animator = GetComponent<Animator>();
        playerBaby = GetComponent<PlayerBaby>();

        playerChild = GetComponent<PlayerChild>();
        childMove = GetComponent<ChildCatMovement>();

        waitForMatch = new WaitForSeconds(0.3f);

        myCollider = GetComponent<Collider>();

        score = 0;
        scoreText.text = score.ToString();


    }
    private void Update()
    {
        if (inventory.isItemSlotFull)
        {
            playerMove.isMovable = false;
            StageManager.Instance.StopGame();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 우유 획득
        if (other.gameObject.CompareTag("Item_Milk"))
        {
            IItemObject itemInterface = other.gameObject.GetComponent<IItemObject>();
            Item item = itemInterface.getItemInfo();
            other.gameObject.SetActive(false);

            if (!other.gameObject.activeSelf && !inventory.isItemSlotFull)
            {
                inventory.AddItem(item);
                StartCoroutine(Milk_ThreeMatchAndEffect());
            }


        }

        // 캣닢 획득
        if (other.gameObject.CompareTag("Item_Catnip"))
        {
            IItemObject itemInterface = other.gameObject.GetComponent<IItemObject>();
            Item item = itemInterface.getItemInfo();
            other.gameObject.SetActive(false);

            if (!other.gameObject.activeSelf && !inventory.isItemSlotFull)
            {
                inventory.AddItem(item);
                StartCoroutine(Catnip_ThreeMatchAndEffect());

            }

        }

        // 털실 획득
        if (other.gameObject.CompareTag("Item_Yarn"))
        {
            IItemObject itemInterface = other.gameObject.GetComponent<IItemObject>();
            Item item = itemInterface.getItemInfo();
            other.gameObject.SetActive(false);

            if (!other.gameObject.activeSelf && !inventory.isItemSlotFull)
            {
                inventory.AddItem(item);
                StartCoroutine(Yarn_ThreeMatchAndEffect());
            }   
        }

        // 기억조각 획득
        if (other.gameObject.CompareTag("Item_Memory"))
        {
            IItemObject itemInterface = other.gameObject.GetComponent<IItemObject>();
            Item item = itemInterface.getItemInfo();
            other.gameObject.SetActive(false);

            if (!other.gameObject.activeSelf && !inventory.isItemSlotFull)
            {
                inventory.AddItem(item);
                StartCoroutine(Memory_MatchAndEffect(item));

            }
        }

    }


    IEnumerator Milk_ThreeMatchAndEffect()
    {
        
        yield return waitForMatch;

        if (inventory.MatchItem())
        {
            myHP.playerHp += 15;

            if (myHP.playerHp >= myHP.playerHpMax)
            {
                myHP.playerHp = myHP.playerHpMax;
            }
            playerHpSlider.value = myHP.playerHp / myHP.playerHpMax;

            score += 30;
            scoreText.text = score.ToString();
        }
    }

    IEnumerator Catnip_ThreeMatchAndEffect()
    {
        yield return waitForMatch;

        if (inventory.MatchItem())
        {
            isCatnipMove = true;

            myCollider.isTrigger = true;
            myHP.playerHp += 5;

            if (myHP.playerHp >= myHP.playerHpMax)
            {
                myHP.playerHp = myHP.playerHpMax;
            }
            playerHpSlider.value = myHP.playerHp / myHP.playerHpMax;
       
            playerMove.isMovable = false;
            playerMove._myRigid.velocity = Vector2.up * 13;

            score += 30;
            scoreText.text = score.ToString();

            StartCoroutine(ResetSetting());
        }

    }

    IEnumerator Yarn_ThreeMatchAndEffect()
    {
        yield return waitForMatch;

        if (inventory.MatchItem())
        {
            // 체력 조금 오름 + 멈춰서 콜록
            myHP.playerHp += 5;

            if (myHP.playerHp >= myHP.playerHpMax)
            {
                myHP.playerHp = myHP.playerHpMax;
            }
            playerHpSlider.value = myHP.playerHp / myHP.playerHpMax;

            
            score += 30;
            scoreText.text = score.ToString();

            StartCoroutine(StopAndCough());
  
            /*
             * 프레임 기반 업데이트 시스템에서 시간 변화 관찰x, 1프레임내에서 다 반복하려고해서 i를 3번 반복하는 방법은 해도 눈에 안보임
            float currentTime;
            float delayTime = 5f;

            for (currentTime = 0; currentTime < delayTime; currentTime += Time.deltaTime)
            {
                movePlayer.isMovable = false;
                animator.Play("Neko - defensive hiss");
            }
            */
        }

    }

    IEnumerator Memory_MatchAndEffect(Item item)
    {
        yield return waitForMatch;

        if (inventory.MatchItem())
        {
            if (!inventory.isMemorySlotFull)
            {
                inventory.AddMemorySlot(item);
            }
            memoryCollect++;
            score += 30;
            scoreText.text = score.ToString();
        }
    }

    IEnumerator ResetSetting()
    {
        yield return new WaitForSeconds(0.8f);
        myCollider.isTrigger = false;
        playerMove.isMovable = true;
    }

    IEnumerator StopAndCough()
    {
        playerMove.isMovable = false;

        GameObject yarn = Instantiate(yarnPrefab, yarnPosition.position, yarnPrefab.transform.rotation);
        yarn.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        animator.Play("Neko - drink milk");
        
        yield return new WaitForSecondsRealtime(2f);

        playerMove.isMovable = true;

        Destroy(yarn);

        animator.Play(0);
        
    }

    



}
