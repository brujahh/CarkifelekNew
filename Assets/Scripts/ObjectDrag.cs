using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
 private Vector3 offset;

 private void OnMouseDown()
 {
     offset = transform.position - ObjectSystem.GetMouseWorldPosition();
 } 

 private void OnMouseDrag()
 {
     Vector3 pos =  ObjectSystem.GetMouseWorldPosition() + offset;
     transform.position = ObjectSystem.current.SnapCoordinatesToGrid(pos);
 }
}