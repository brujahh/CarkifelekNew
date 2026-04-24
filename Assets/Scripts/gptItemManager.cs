using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    // Reference to the UI buttons and shelf locations
    public Button buyTVButton;
    public Button buyWashingMachineButton;
    public Button buyDishwasherButton;

    // Prefabs for the items
    public GameObject tvPrefab;
    public GameObject washingMachinePrefab;
    public GameObject dishwasherPrefab;

    // List of shelves in the wardrobe (each is a Transform where we can place an item)
    public Transform[] shelfLocations;

    // Keep track of the items placed on shelves
    private GameObject[] placedItems;

    // Variables for rotating and scaling items
    private GameObject selectedItem;
    private float rotationSpeed = 100f;
    private float scaleSpeed = 0.05f;

    void Start()
    {
        // Initialize the array to track placed items
        placedItems = new GameObject[shelfLocations.Length];
    }

    void Update()
    {
        if (selectedItem != null)
        {
            HandleItemRotationAndScaling();
        }
    }

    // Method to place an item in the wardrobe
    public void PlaceItemInWardrobe(string itemName)
    {
        GameObject itemPrefab = GetItemPrefab(itemName);

        // Try placing the item on a shelf
        for (int i = 0; i < shelfLocations.Length; i++)
        {
            if (placedItems[i] == null) // Check if the shelf is empty
            {
                // Place the item on the first empty shelf
                GameObject item = Instantiate(itemPrefab, shelfLocations[i].position, Quaternion.identity);
                item.transform.SetParent(shelfLocations[i]); // Make the item a child of the shelf
                placedItems[i] = item; // Track the item as placed
                selectedItem = item; // Select the item for interaction
                break;
            }
        }
    }

    // Get the prefab for the selected item
    GameObject GetItemPrefab(string itemName)
    {
        switch (itemName)
        {
            case "TV":
                return tvPrefab;
            case "Washing Machine":
                return washingMachinePrefab;
            case "Dishwasher":
                return dishwasherPrefab;
            default:
                return null;
        }
    }

    // Handle rotating and scaling the selected item
    void HandleItemRotationAndScaling()
    {
        if (Input.GetKey(KeyCode.R)) // Rotate item with "R" key
        {
            selectedItem.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.T)) // Scale item with "T" key
        {
            selectedItem.transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Y)) // De-scale item with "Y" key
        {
            selectedItem.transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
        }
    }
}
