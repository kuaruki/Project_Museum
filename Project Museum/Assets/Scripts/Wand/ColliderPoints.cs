using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wand")
        {
            Debug.Log("Collided");
        }
    }
}
