using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    public Camera Camera;

    public virtual string Name
    {
        get
        {
            return "_base item_";
        }
    }

    public Sprite _Image;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public virtual void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnDrop()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.CompareTag("PaintingsWall"))
            {
                Debug.Log("Raycast HIT");
                gameObject.SetActive(true);
                gameObject.transform.position = hit.point;
                gameObject.transform.eulerAngles = DropRotation;
            }
        }
    }

    public virtual void OnUse()
    {
        transform.localPosition = PickPosition;
        transform.localEulerAngles = PickRotation;
    }

    // Position and rotation of item when is on hand, can be altered on unity editor!!!
    public Vector3 PickPosition;
    public Vector3 PickRotation;

    // Rotation of the object when it is dropped
    public Vector3 DropRotation;
}
