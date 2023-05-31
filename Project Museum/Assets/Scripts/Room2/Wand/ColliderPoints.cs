using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPoints : MonoBehaviour {
    [SerializeField]
    private GameObject LineRenderer;

    private void OnTriggerEnter(Collider collision) {
        LineRenderer.GetComponent<DrawLines>().CollidedTag = collision.tag;
        LineRenderer.GetComponent<DrawLines>().colliding = true;
        Debug.Log("COLLIDED WITH: " + collision.tag);
    }
}