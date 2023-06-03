using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingsPickup : MonoBehaviour {
    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private LayerMask PaintingPosition1;
    [SerializeField] private LayerMask PaintingPosition2;
    [SerializeField] private LayerMask PaintingPosition3;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform PickupTarget;

    //Paintings Positions
    [SerializeField] private Transform Position1;
    [SerializeField] private Transform Position2;
    [SerializeField] private Transform Position3;

    //Looking at a certain position
    private bool isLookingAtPosition1 = false;
    private bool isLookingAtPosition2 = false;
    private bool isLookingAtPosition3 = false;

    //// Flag indicating if paintings are in their respective positions
    //[SerializeField] private bool isPainting1InPosition = false;
    //[SerializeField] private bool isPainting2InPosition = false;
    //[SerializeField] private bool isPainting3InPosition = false;

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
        Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        isLookingAtPosition1 = Physics.Raycast(ray, out RaycastHit hit1, PickupRange, PaintingPosition1);
        isLookingAtPosition2 = Physics.Raycast(ray, out RaycastHit hit2, PickupRange, PaintingPosition2);
        isLookingAtPosition3 = Physics.Raycast(ray, out RaycastHit hit3, PickupRange, PaintingPosition3);

        if (CurrentPainting) {
            if (Input.GetKey(KeyCode.E)) {
                // Check if the player is looking at any target position
                if (isLookingAtPosition1 || isLookingAtPosition2 || isLookingAtPosition3) {
                    // Move the painting to the corresponding position based on the player's view
                    if (isLookingAtPosition1) {
                        CurrentPainting.MovePosition(Position1.position);
                        CurrentPainting.MoveRotation(Position1.rotation);

                    }
                    else if (isLookingAtPosition2) {
                        CurrentPainting.MovePosition(Position2.position);
                        CurrentPainting.MoveRotation(Position2.rotation);
                    }
                    else if (isLookingAtPosition3) {
                        CurrentPainting.MovePosition(Position3.position);
                        CurrentPainting.MoveRotation(Position3.rotation);
                    }

                    CurrentPainting.velocity = Vector3.zero;
                    CurrentPainting.angularVelocity = Vector3.zero;
                }
                else {
                    // Move the painting to the general pickup target position
                    CurrentPainting.MovePosition(PickupTarget.position);
                    CurrentPainting.MoveRotation(PickupTarget.rotation);
                    CurrentPainting.velocity = Vector3.zero;
                    CurrentPainting.angularVelocity = Vector3.zero;
                }
            }

            CurrentPainting.useGravity = !(isLookingAtPosition1 || isLookingAtPosition2 || isLookingAtPosition3); // Enable gravity if not looking at any target position
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