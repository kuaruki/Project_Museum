using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingsPickup : MonoBehaviour {
    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private LayerMask PaintingPosition1;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform PickupTarget;
    [SerializeField] private Transform Position1;
    [Space]
    [SerializeField] private float PickupRange;
    private Rigidbody CurrentPainting;
    private Collider CurrentPaintingCollider;
    private bool isHoldingPainting = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (isHoldingPainting) {
                DropPainting();
            }
            else {
                PickUpPainting();
            }
        }

        if (isHoldingPainting && CurrentPainting) {
            MoveToPositionOnE();
        }
    }

    private void PickUpPainting() {
        if (CurrentPainting) {
            CurrentPainting.useGravity = true;
            CurrentPaintingCollider.enabled = true;
            CurrentPainting = null;
            CurrentPaintingCollider = null;
            return;
        }

        Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, PickupRange, PickupMask)) {
            CurrentPainting = hit.rigidbody;
            CurrentPaintingCollider = hit.collider;
            CurrentPainting.useGravity = false;
            CurrentPaintingCollider.enabled = false;
            isHoldingPainting = true;
        }
    }


    private void DropPainting() {
        bool isLookingAtTargetPosition = false;
        Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, PickupRange, PaintingPosition1)) {
            Debug.Log("It is I, PaintingPosition1");
            isLookingAtTargetPosition = true;
        }

        if (CurrentPainting) { //Here the player is looking at the correct position
            if (Input.GetKey(KeyCode.E) && isLookingAtTargetPosition) {
                CurrentPainting.MovePosition(Position1.position);
                CurrentPainting.MoveRotation(Position1.rotation);
                CurrentPainting.velocity = Vector3.zero;
                CurrentPainting.angularVelocity = Vector3.zero;
            }

            CurrentPainting.useGravity = !isLookingAtTargetPosition; // Enable gravity if not looking at target position
            CurrentPaintingCollider.enabled = true;
            CurrentPainting = null;
            CurrentPaintingCollider = null;
            isHoldingPainting = false;
        }
    }


    private void MoveToPositionOnE() {
        Vector3 targetPosition = PickupTarget.position;
        CurrentPainting.MovePosition(targetPosition);
        CurrentPainting.velocity = Vector3.zero;
        CurrentPainting.angularVelocity = Vector3.zero;
    }
}