using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Linear Search Input")]
    public int seekingBananaNumber;

    public List<InventoryItem> inventory = new List<InventoryItem>();
        
    public int maxIDValue = 9999;

    // generates the inventory list. currently completely ordered. add randomization later.
    private void Awake()
    {
        int inventorySize = Random.Range(50, 100);

        for(int slot = 0; slot < inventorySize; slot++)
        {
            // generates a new InventoryItem with an ID = to its slot #, a banana + slot# name, and a randomized value before adding to the list
            InventoryItem item = new InventoryItem();
            item.id = Random.Range(0, maxIDValue);
            item.itemName = $"banana {slot + 1}";
            item.value = Random.Range(0, 1000);
            inventory.Add(item);
        }

        // Sort inventory by ID once at initialization
        inventory.Sort((a, b) => a.id.CompareTo(b.id));

    }

    public InventoryItem LinearSearchByName(string itemName)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == itemName)
            {
                return inventory[i];
            }
        }
        return null;
    }

    public InventoryItem BinarySearchByID(int itemID)
    {
        int left = 0;
        int right = inventory.Count - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            int midID = inventory[mid].id;

            if (midID == itemID)
            {
                return inventory[mid];
            }
            else if (midID < itemID)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return null;
    }

    public void QuickSortByValue()
    {
        QuickSort(inventory, 0, inventory.Count - 1);
    }

    private void QuickSort(List<InventoryItem> list, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(list, low, high);
            QuickSort(list, low, pivotIndex - 1);
            QuickSort(list, pivotIndex + 1, high);
        }
    }

    private int Partition(List<InventoryItem> list, int low, int high)
    {
        int pivotValue = list[high].value;
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (list[j].value <= pivotValue)
            {
                i++;
                Swap(list, i, j);
            }
        }

        Swap(list, i + 1, high);
        return i + 1;
    }

    private void Swap(List<InventoryItem> list, int a, int b)
    {
        InventoryItem temp = list[a];
        list[a] = list[b];
        list[b] = temp;
    }


    private void Start()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        {
            // Linear Search Result Display
            string soughtItemValue = $"banana {seekingBananaNumber}";
            InventoryItem found = LinearSearchByName(soughtItemValue);
            if (found != null)
            {
                Debug.Log($"Linear Search Result: {found.itemName} located.");
            }
            else
            {
                Debug.Log("Linear Search Result: null.");
            }
        }
        //if (Input.GetKeyDown(KeyCode.S))
        {
            // Binary Search Result Display
            int randomIDToSearch = inventory[Random.Range(0, inventory.Count)].id;
            InventoryItem foundByID = BinarySearchByID(randomIDToSearch);
            if (foundByID != null)
            {
                Debug.Log($"Binary Search Result: Item with ID {randomIDToSearch} is {foundByID.itemName}.");
            }
            else
            {
                Debug.Log($"Binary Search Result: Item with ID {randomIDToSearch} not found.");
            }

        }
        //if (Input.GetKeyDown(KeyCode.D))
        {
            // QuickSort Result Display
            QuickSortByValue();
            Debug.Log("Inventory sorted by Value using QuickSort.");
        }
    }
}
