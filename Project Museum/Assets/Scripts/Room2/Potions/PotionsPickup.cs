using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PotionsPickup : MonoBehaviour
{

    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform PickupTarget;
    [Space]
    [SerializeField] private float PickupRange;
    private Rigidbody CurrentPotion;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (CurrentPotion) {
                CurrentPotion.useGravity = true;
                CurrentPotion = null;
                return;
            }
            Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out RaycastHit hit, PickupRange, PickupMask)) {
                CurrentPotion = hit.rigidbody;
                CurrentPotion.useGravity = false;
            }
        }

    }
    private void FixedUpdate() {
        if (CurrentPotion) {
            Vector3 DirectionToPoint = PickupTarget.position - CurrentPotion.position;
            float DistanceToPoint = DirectionToPoint.magnitude;

            CurrentPotion.velocity = DirectionToPoint * 12f * DistanceToPoint;
        }
    }
}

//Creds: https://www.youtube.com/watch?v=zgCV26yFAiU