using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public IInventoryItem Item { get; set;}

    public void OnDrag(PointerEventData eventData)
    {
        //puts the image on the cursor
        transform.position = Input.mousePosition; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //resets the image position to the inventory slot
        transform.localPosition = Vector3.zero;
    }
}
