//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Painting2 : MonoBehaviour {
//    [SerializeField] private Camera MainCamera;
//    private void OnTriggerEnter(Collider other) {
//        if (other.gameObject.CompareTag("PaintingPosition2")) {
//            Debug.Log("Painting 2 in place");
//            //Lock the object in place
//            gameObject.layer = default;
//            //Give the Player some Feedback

//            //Atualizar variavel no interactor
//            MainCamera.GetComponent<Interactor>().PaintingsInPlace += 1;
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting2 : MonoBehaviour {
    [SerializeField] private Camera MainCamera;
    private Rigidbody rb;
    private Collider coll;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PaintingPosition2")) {
            Debug.Log("Painting 2 in place");

            // Lock the object in place by disabling the Rigidbody and Collider
            rb.isKinematic = true;
            coll.enabled = false;

            // Give the Player some Feedback

            // Atualizar variavel no interactor
            MainCamera.GetComponent<Interactor>().PaintingsInPlace += 1;
        }
    }
}