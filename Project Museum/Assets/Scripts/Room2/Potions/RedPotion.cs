using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPotion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("RedPosition")) 
        {
            Debug.Log("RedPotion");
            //Atualizar variavel no interactor
        }
    }
}
