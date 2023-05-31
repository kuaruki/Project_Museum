using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePotions : MonoBehaviour 
{
    //public GameObject crosshair1;
    //public GameObject crosshair2;

    public Canvas DotCanvas;
    public Canvas HandCanvass;

    public Transform objTransform;
    public Transform cameraTrans;

    public bool interactable;
    public bool pickedup;

    public Rigidbody objRigidbody;
    public float throwAmount;

    void OnTriggerStay(Collider other) {
        if (other.CompareTag("MainCamera"))
        {
            //crosshair1.SetActive(false); My Dot
            DotCanvas.gameObject.SetActive(false);
            //crosshair2.SetActive(true);
            HandCanvass.gameObject.SetActive(true);
            interactable = true;
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("MainCamera")) {
            if (pickedup == false) {

                DotCanvas.gameObject.SetActive(true);
                //crosshair1.SetActive(true);
                HandCanvass.gameObject.SetActive(false);
                //crosshair2.SetActive(false);
                interactable = false;
            }
            if (pickedup == true) {
                objTransform.parent = null;
                objRigidbody.useGravity = true;

                DotCanvas.gameObject.SetActive(true);
                //crosshair1.SetActive(true);
                HandCanvass.gameObject.SetActive(false);
                //crosshair2.SetActive(false);
                interactable = false;
                pickedup = false;
            }
        }
    }
    void Update() {
        if (interactable == true) {
            if (Input.GetMouseButtonDown(0)) {
                objTransform.parent = cameraTrans;
                objRigidbody.useGravity = false;
                pickedup = true;
            }
            if (Input.GetMouseButtonUp(0)) {
                objTransform.parent = null;
                objRigidbody.useGravity = true;
                pickedup = false;
            }
            if (pickedup == true) { //Throw
                if (Input.GetMouseButtonDown(1)) {
                    objTransform.parent = null;
                    objRigidbody.useGravity = true;
                    objRigidbody.velocity = cameraTrans.forward * throwAmount * Time.deltaTime;
                    pickedup = false;
                }
            }
        }
    }
}

//Creds: https://www.youtube.com/watch?v=FnE4aS0dsE4