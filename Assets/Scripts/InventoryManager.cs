using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Linear Search Input")]
    public int seekingBananaNumber;

    public List<InventoryItem> inventory = new List<InventoryItem>();

    // generates the inventory list. currently completely ordered. add randomization later.
    private void Awake()
    {
        int inventorySize = Random.Range(50, 100);

        for(int slot = 0; slot < inventorySize; slot++)
        {
            // generates a new InventoryItem with an ID = to its slot #, a banana + slot# name, and a randomized value before adding to the list
            InventoryItem item = new InventoryItem();
            item.itemName = $"banana {slot + 1}";
            item.value = Random.Range(0, 1000);
            inventory.Add(item);
        }
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
        return null;
    }

    private void Start()
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

        // Binary Search Result Display

    }
}
