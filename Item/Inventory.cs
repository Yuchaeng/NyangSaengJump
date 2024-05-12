using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    public Dictionary<Item, int> itemCounts = new Dictionary<Item, int>();
    public List<KeyValuePair<Item, int>> itemCountsList = new List<KeyValuePair<Item, int>>();
    private int totalCount;

    [SerializeField] private Transform itemSlotsParent;
    [SerializeField] private InventorySlot[] itemSlots;
    [SerializeField] private Transform memorySlotsParent;
    [SerializeField] private InventorySlot[] memorySlots;

    public bool isItemSlotFull;

    public int memoryCount;
    public bool isMemorySlotFull;


    // ����Ƽ �����Ϳ��� �ٷ� �۵��ϴ� ����
    private void OnValidate()
    {
        itemSlots = itemSlotsParent.GetComponentsInChildren<InventorySlot>();
        memorySlots = memorySlotsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void Start()
    {
        RefreshItemSlot();

        isItemSlotFull = false;
        isMemorySlotFull = false;
        memoryCount = 0;
    }

    // ������ �����ų� ���� �� slot�� ���� �����ؼ� ȭ�鿡 �����ִ� ���
    // ��ųʸ����� ���� item���� ���޾� ������ ����
    public void RefreshItemSlot()
    {
        for (int j = 0; j < itemSlots.Length; j++)
        {
            itemSlots[j].item = null;
        }

        int i = 0;

        foreach (var item in itemCountsList)
        {
            for (int j = 0; j < item.Value; j++)
            {
                itemSlots[i].item = item.Key;
                i++;
            }
        }

        //foreach (var key in itemCounts.Keys)
        //{
        //    for (int j = 0; j < itemCounts[key]; j++)
        //    {

        //        itemSlots[i].item = key;
        //        i++;
        //    }
        //}

        // ������ �� ������ ���� ��ųʸ� Ȯ���غ����� �� - ���� �Ⱥ���ְ� ��� ����Ἥ �����Ǿ���
    }


    public void AddItem(Item item)
    {
        // item ����Ʈ�� �߰�
        //items.Add(item);

        // item�� ���� ������ �ִ� �񼭳ฮ�� �߰�
        int temp;
        KeyValuePair<Item, int> tempItem;
        if (!itemCountsList.Exists(x => x.Key == item))
        {
            itemCountsList.Add(new KeyValuePair<Item, int>(item, 1));

        }
        else
        {
            if (itemCountsList[0].Key == item)
            {
                temp = itemCountsList[0].Value;
                itemCountsList.RemoveAt(0);
                itemCountsList.Insert(0, new KeyValuePair<Item, int>(item, temp + 1));
            }
            else
            {
                tempItem = itemCountsList.Find(x => x.Key == item);
                temp = tempItem.Value;
                itemCountsList.Remove(tempItem);
                itemCountsList.Insert(0, new KeyValuePair<Item, int>(item, temp + 1));
            }
        }

        for (int i = 0; i < itemCountsList.Count; i++)
        {
            //Debug.Log(itemCountsList[i]);
        }
        /*
        if (!itemCounts.ContainsKey(item))
        {
            itemCounts[item] = 0;
            itemCounts[item]++;
        }
        else
        {
            if (itemCounts.First().Key == item)
            {
                itemCounts[item]++;
            }
            else
            {
                itemCounts[item]++;

                int tmep = itemCounts[item];
                itemCounts.Remove(item);
                
                itemCounts.Reverse();
                
                itemCounts.Add(item, tmep);
                
                itemCounts.Reverse();
                
            }

        }*/

        totalCount++;
 
        RefreshItemSlot();
        //Invoke("MatchItem", 2f);
        //MatchItem();
    }

    public bool MatchItem()
    {
        foreach (var item in itemCountsList)
        {
            if (item.Value >= 3)
            {
                itemCountsList.Remove(item);

                RefreshItemSlot();

                totalCount -= 3;
                Debug.Log(totalCount);

                return true;
            }

            if (totalCount >= itemSlots.Length)
            {
                isItemSlotFull = true;
            }
            else
            {
                isItemSlotFull = false;
            }
        }


        return false;

    }

    public void AddMemorySlot(Item blueItem)
    {

        memorySlots[memoryCount].item = blueItem;
        memoryCount++;

        if (memoryCount >= memorySlots.Length)
        {
            isMemorySlotFull = true;
        }
        
        Debug.Log(isMemorySlotFull);
    }

    // ���� �� ���� ���� ���� -> ��� �ۼ�?

    
}
