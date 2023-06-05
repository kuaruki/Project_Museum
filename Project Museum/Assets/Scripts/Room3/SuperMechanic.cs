using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMechanic : MonoBehaviour {

    [Header("Components")]
    public Transform target; //our picked up object null == no object present; true = object present

    [Header("Parameters")]
    public LayerMask targetMask;
    public LayerMask ignoreTargetMask;
    public float offsetFactor;

    float originalDistance;
    float originalScale;
    Vector3 targetScale;
    [SerializeField] private RoomBounds roomBounds;

    public float InteractDistance = 6.5f;

    // Update is called once per frame
    void Update() {
        HandleInput();
        ResizeTarget();
    }

    void HandleInput() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (target == null) { //target = null so we don't have an object picked up => we pick up the object
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, InteractDistance, targetMask)) {
                    target = hit.transform;
                    target.GetComponent<Rigidbody>().isKinematic = true;
                    originalDistance = Vector3.Distance(transform.position, target.position); //Distance between the camera and the object the moment the player clicks the mouse button
                    originalScale = target.localScale.x; //This only works for uniform objects in all axis (Vector 3 to store different sized objs)
                    //originalScale = new Vector3(target.localScale.x, target.localScale.y, target.localScale.z); //This only works for uniform objects in all axis (Vector 3 to store different sized objs)
                    new Vector3(1f, 2f, 3f);
                    targetScale = target.localScale;
                }
            }
            else { //this means the player already has a target in his "hand" and the player will drop the item
                target.GetComponent<Rigidbody>().isKinematic = false;
                target = null;
            }
        }
    }


    void ResizeTarget() {
        if (target == null) // No object in hand
            return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignoreTargetMask)) {
            Vector3 newPosition = hit.point - transform.forward * offsetFactor * target.localScale.x;

            // Check if the new position is inside the room bounds
            if (roomBounds.IsInsideRoomBounds(newPosition)) {
                target.position = newPosition;

                float currentDistance = Vector3.Distance(transform.position, target.position); // Current distance from the camera to the object
                float s = currentDistance / originalDistance; // Ratio to scale the object based on the current distance
                targetScale = originalScale * new Vector3 (s,s,s);

                target.localScale = targetScale;
            }
        }
    }

    //void ResizeTarget() {
    //    if (target == null) //no object in hand
    //        return;

    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignoreTargetMask)) {
    //        target.position = hit.point - transform.forward * offsetFactor * targetScale.x;

    //        float currentDistance = Vector3.Distance(transform.position, target.position); //currentDistance from camera to object
    //        float s = currentDistance / originalDistance; //ratio if object is closer it will be reduced; if currDis > OGDis => s > 1 meaning object will have to be incresed
    //        targetScale.x = targetScale.y = targetScale.z = s;

    //        target.localScale = targetScale * originalScale;
    //    }
    //}
}