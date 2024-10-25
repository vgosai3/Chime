using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryComponent : MonoBehaviour
{
    public int SlotCount = 6;
    private int activeItemIndex = 0;
    private AItem[] items;
    private InventoryGUI inventoryGUI;
    private Player player;

    public void Start()
    {
        items = new AItem[SlotCount];
        inventoryGUI = GameObject.FindObjectOfType<InventoryGUI>();
        player = GameObject.FindObjectOfType<Player>();
    }
    public void UseActiveItemPrimaryAction()
    {
        if (items[activeItemIndex] != null)
        {
            items[activeItemIndex].PrimaryAction();            
        }
        else
        {
            Debug.LogWarning("Cannot use the active item's primary action since there is no item in that slot!");
        }

    }
    private void TryEquip(int itemIndex)
    {
        if (items[itemIndex] != null)
        {
            items[itemIndex].Equip();
        }
    }
    private void TryUnequip(int itemIndex)
    {
        if (items[itemIndex] != null)
        {
            items[itemIndex].Unequip();
        }
    }
    public void SelectNextItem()
    {
        TryUnequip(activeItemIndex);
        activeItemIndex = (activeItemIndex + 1) % SlotCount;
        inventoryGUI.UpdateActiveItemBorder(activeItemIndex);
        TryEquip(activeItemIndex);
    }
    public void SelectPrevItem()
    {
        TryUnequip(activeItemIndex);
        activeItemIndex = (activeItemIndex - 1) % SlotCount;
        inventoryGUI.UpdateActiveItemBorder(activeItemIndex);
        TryEquip(activeItemIndex);
    }
    public void SelectItemByIndex(int itemIndex)
    {
        if (itemIndex < SlotCount && itemIndex >= 0)
        {
            TryUnequip(activeItemIndex);
            activeItemIndex = itemIndex;
            inventoryGUI.UpdateActiveItemBorder(activeItemIndex);
            TryEquip(activeItemIndex);
        }
        else
        {
            Debug.LogError("Requested item index is out of bounds for this inventory!");
        }
    }
    //Returns the index the item is added to
    public int AddItem(AItem item)
    {
        int itemIndex = -1;
        if (items[activeItemIndex] == null)
        {
            itemIndex = activeItemIndex;
        }
        else
        {
            int currIndex = 0;
            while (currIndex < SlotCount)
            {
                if (items[currIndex] == null)
                {
                    itemIndex = currIndex;
                    break;
                }
                currIndex++;
            }
        }
        if (itemIndex >= 0)
        {
            items[itemIndex] = item;
            Debug.Log(item.name + " successfully added to the inventory!");
            inventoryGUI.AddItem(item, itemIndex);
            TryEquip(activeItemIndex);
            return itemIndex;
        }
        else
        { 
            Debug.Log("Could not add the item because the inventory is full!");
            return -1;
        }
    }
    public void RemoveItem(int itemIndex)
    {
        if (items[itemIndex] == null)
        {
            Debug.LogWarning("Attempted to remove an item which did not exist!");
        }
        else
        {
            items[itemIndex] = null;
            inventoryGUI.RemoveItem(itemIndex);
        }
    }
    public void DropItem()
    {
        if (items[activeItemIndex] == null)
        {
            Debug.LogWarning("Attempted to remove an item which did not exist!");
        }
        else
        {
            items[activeItemIndex].Drop(player.transform.position);
            items[activeItemIndex] = null;
            inventoryGUI.RemoveItem(activeItemIndex);
        }
    }

    public AItem[] getItems()
    {
        return items;
    }

    public int[] getItemsSerialized()
    {
        int[] returnArr = new int[SlotCount];
        for (int i = 0; i < SlotCount; i++)
        { 
            returnArr[i] = (items[i] == null) ? (int)Item.None : items[i].getID();
        }
        return returnArr;
    }

    public void updateItemsSerialized(int[] id)
    {
        for (int i = 0; i < SlotCount; i++)
        {
            items[i] = null; //todo - when we have a consistent way to make AItems via storing the prefabs somewhere
            Debug.Log("Item " + i + " is " + id[i]);
        }
    }

}
