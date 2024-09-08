using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Is it easy to simply change icon when character inv component has item added to it?
public class InventoryGUI : MonoBehaviour
{
    public PlayerInventoryComponent playerInventory;
    private Image[] itemIcons;
    private Image activeItemBorder;
    public void Start()
    {
        playerInventory = GameObject.FindAnyObjectByType<Player>().GetComponent<PlayerInventoryComponent>();
        itemIcons = new Image[playerInventory.SlotCount];
        Image[] tempImages = GetComponentsInChildren<Image>();
        int imageCount = 0;
        for (int i = 0; i < tempImages.Length; i++)
        {
            if (tempImages[i].name.Contains("ItemIcon"))
            {
                itemIcons[imageCount] = tempImages[i];
                imageCount++;
            }
            if (tempImages[i].name.Contains("ActiveItemBorder"))
            {
                activeItemBorder = tempImages[i];
            }
        }
    }
    public void AddItem(AItem item, int itemIndex)
    {
        itemIcons[itemIndex].sprite = item.ItemIcon;
        itemIcons[itemIndex].enabled = true;
    }
    public void RemoveItem(int itemIndex)
    {
        itemIcons[itemIndex].enabled = false;
    }
    public void UpdateActiveItemBorder(int itemIndex)
    {
        activeItemBorder.rectTransform.position = itemIcons[itemIndex].rectTransform.position;
    }
}
