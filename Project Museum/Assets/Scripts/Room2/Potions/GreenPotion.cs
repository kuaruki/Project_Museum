using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPotion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("GreenPosition")) 
        {
            Debug.Log("GreenPotion");
            //Atualizar variavel no interactor
        }
    }
}
