using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePotion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("WhitePosition")) 
        {
            Debug.Log("WhitePotion");
            //Atualizar variavel no interactor
        }
    }
}
