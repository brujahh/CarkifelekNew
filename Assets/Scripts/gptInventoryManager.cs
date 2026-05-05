using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GpInventoryManager : MonoBehaviour
{
    // Player's points
    public int playerPoints = 1000;
    public Text pointsText;

    // Inventory List - stores the purchased items (just their names for simplicity)
    private List<string> inventory = new List<string>();

    // UI buttons for buying items
    public Button buyTVButton;
    public Button buyWashingMachineButton;
    public Button buyDishwasherButton;

    // Reference to the ItemManager to place items on shelves
    public GpItemManager itemManager;

    void Start()
    {
        // Display the initial points
        UpdatePointsUI();

        // Add button listeners for purchasing items
        buyTVButton.onClick.AddListener(() => TryBuyItem("TV"));
        buyWashingMachineButton.onClick.AddListener(() => TryBuyItem("Washing Machine"));
        buyDishwasherButton.onClick.AddListener(() => TryBuyItem("Dishwasher"));
    }

    // Try to buy an item if the player has enough points
    void TryBuyItem(string itemName)
    {
        int itemCost = GetItemCost(itemName);

        if (playerPoints >= itemCost)
        {
            // Subtract the cost and add item to inventory
            playerPoints -= itemCost;
            inventory.Add(itemName);

            // Place the item on a shelf using the ItemManager
            itemManager.PlaceItemInWardrobe(itemName);

            // Update the points UI and inventory
            UpdatePointsUI();
        }
        else
        {
            Debug.Log("Not enough points to buy " + itemName);
        }
    }

    // Get the cost of an item
    int GetItemCost(string itemName)
    {
        switch (itemName)
        {
            case "TV":
                return 500;
            case "Washing Machine":
                return 400;
            case "Dishwasher":
                return 350;
            default:
                return 0;
        }
    }

    // Update the points UI text
    void UpdatePointsUI()
    {
        pointsText.text = "Points: " + playerPoints.ToString();
    }

    // Show the inventory in the console for now (you could make this a UI list in the future)
    public void ShowInventory()
    {
        Debug.Log("Inventory: ");
        foreach (var item in inventory)
        {
            Debug.Log(item);
        }
    }
}
