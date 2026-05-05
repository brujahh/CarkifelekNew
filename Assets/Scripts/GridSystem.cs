using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridSystem : MonoBehaviour
{   
  public GameObject objectToPlace;
  public GameObject fridgeHigh, fridgeMid, fridgeLow, sofaHigh, sofaMid, sofaLow, wmachineHigh, wmachineMid, wmachineLow, tvHigh, tvMid, tvLow, dishwasherHigh, dishwasherMid, dishwasherLow;
  public Button fridgeHighBtn, fridgeMidBtn, fridgeLowBtn, sofaHighBtn, sofaMidBtn, sofaLowBtn, wmachineHighBtn, wmachineMidBtn, wmachineLowBtn, tvHighBtn, tvMidBtn, tvLowBtn, dishwasherHighBtn, dishwasherMidBtn, dishwasherLowBtn;

  public float gridSize = 1f;
  private GameObject ghostObject;
  private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();

  private GameObject objectToRotate;
  Quaternion targetRotation;

        void Start()
        {
            
            CreateGhostObject();
        }
    
        void Update()
        {
            fridgeMidBtn.onClick.AddListener(changeObject);
            updateGhostPosition(); 
    
            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
            CheckRotation();
        }
        
    void changeObject()
    {
       
        objectToPlace = fridgeMid;
    }
    void CheckRotation()
    {
        objectToRotate = ghostObject;
        if (Input.GetKeyDown(KeyCode.R))
        {
            targetRotation = Quaternion.Euler(objectToRotate.transform.eulerAngles.x, objectToRotate.transform.eulerAngles.y + 90, objectToRotate.transform.eulerAngles.z);
            
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            targetRotation = Quaternion.Euler(objectToRotate.transform.eulerAngles.x, objectToRotate.transform.eulerAngles.y - 90, objectToRotate.transform.eulerAngles.z);
            
        }
        objectToRotate.transform.rotation = targetRotation;
    }
        

    void CreateGhostObject()
    {
        ghostObject = Instantiate(objectToPlace);
        ghostObject.GetComponent<Collider>().enabled = false; // Disable collider for ghost object
        Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material mat = renderer.material;
            Color color = mat.color;
            color.a = 0.5f; // Set transparency
            mat.color = color;

            mat.SetFloat("_Mode", 2); // Set material to transparent mode
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
    }

    void updateGhostPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 point = hit.point;

            Vector3 snappedPosition = new Vector3(
                Mathf.Round(point.x / gridSize) * gridSize,
                Mathf.Round(point.y / gridSize) * gridSize,
                Mathf.Round(point.z / gridSize) * gridSize
            );

            
            ghostObject.transform.position = snappedPosition;
            if(occupiedPositions.Contains(snappedPosition))
            {
                SetGhostColor(Color.red);
            }
            else
            {
                SetGhostColor(Color.green);
            }
        }
    }

    void SetGhostColor(Color color)
    {
        Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material mat = renderer.material;
            color.a = 0.5f; // Ensure transparency is maintained
            mat.color = color;
        }
    }

    void PlaceObject()
    {
       Vector3 placementPosition = ghostObject.transform.position;
        if (!occupiedPositions.Contains(placementPosition))
        {
            Instantiate(objectToPlace, placementPosition, targetRotation);
            occupiedPositions.Add(placementPosition);
        }

    }
}
