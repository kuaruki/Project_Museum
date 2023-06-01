using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PotionsPickup : MonoBehaviour {
    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform PickupTarget;
    [Space]
    [SerializeField] private float PickupRange;
    private Rigidbody CurrentPotion;
    private Collider CurrentPotionCollider;
    public Collider GreenPotionCollider;
    public Collider RedPotionCollider;
    public Collider WhitePotionCollider;
    public Collider PurplePotionCollider;


    private void Start() {
        GreenPotionCollider.enabled = false;
        RedPotionCollider.enabled = false;
        WhitePotionCollider.enabled = false;
        PurplePotionCollider.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) { //Picks the object up
            if (CurrentPotion) {
                CurrentPotion.useGravity = true;
                CurrentPotionCollider.enabled = true; //Disables collider so it doesn't register if the player passes the object throught the goal
                CurrentPotion = null;
                CurrentPotionCollider = null;
                return;
            }
            Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out RaycastHit hit, PickupRange, PickupMask)) {
                CurrentPotion = hit.rigidbody;
                CurrentPotionCollider = hit.collider;
                CurrentPotion.useGravity = false;
                CurrentPotionCollider.enabled = false;
            }
        }

        if (!CurrentPotion) {
            if (Input.GetKeyDown(KeyCode.E)) { //Drops the object
                if (CurrentPotion) {
                    CurrentPotion.useGravity = true;
                    CurrentPotionCollider.enabled = true; //Enables the collider so that the object can collide with the goal
                    CurrentPotion = null;
                    CurrentPotionCollider = null;
                }
            }
        }
    }

    private void FixedUpdate() {
        if (CurrentPotion) {
            CurrentPotion.MovePosition(PickupTarget.position);
            CurrentPotion.velocity = Vector3.zero;
            CurrentPotion.angularVelocity = Vector3.zero;
        }
    }

    public void SetPositionsTrue() {
        GreenPotionCollider.enabled = true;
        RedPotionCollider.enabled = true;
        WhitePotionCollider.enabled = true;
        PurplePotionCollider.enabled = true;
    }
}