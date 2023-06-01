using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePotion : MonoBehaviour
{
    [SerializeField]
    private Camera MainCamera;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PurplePosition")) 
        {
            Debug.Log("PurplePotion");
            //Lock the object in place
            gameObject.layer = default;
            //Give the Player some Feedback

            //Atualizar variavel no interactor
            MainCamera.GetComponent<Interactor>().PotionsInPlace += 1;
        }
    }
}
