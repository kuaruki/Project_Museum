using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerg;
    //private Vector3 rot = new Vector3(0,0,0);

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTERED COLLIDER");

        if (other.CompareTag("Player"))
        {
            playerg.SetActive(false);
            player.position = destination.position;
            //player.rotation = Quaternion.Euler(rot);
            playerg.SetActive(true);

            Debug.Log("TELEPORTED");
        }
    }
}
