using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePotion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PurplePotion")) 
        {
            Debug.Log("PurplePotion");
            //Atualizar variavel no interactor
        }
    }
}
