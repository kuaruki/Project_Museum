using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting3 : MonoBehaviour {
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Transform ReturnPoint;
    private Rigidbody rb;
    private Collider coll;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PaintingPosition3")) {
            Debug.Log("Painting 3 in place");

            // Lock the object in place by disabling the Rigidbody and Collider
            rb.isKinematic = true;
            coll.enabled = false;

            // Give the Player some Feedback
            MainCamera.GetComponent<Interactor>().Correct.Play();

            // Atualizar variavel no interactor
            MainCamera.GetComponent<Interactor>().PaintingsInPlace += 1;
        }
        if (other.gameObject.CompareTag("PaintingCatcher")) {
            gameObject.transform.position = ReturnPoint.position;
        }
    }
}