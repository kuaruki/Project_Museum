using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePotion : MonoBehaviour
{
    [SerializeField]
    private Camera MainCamera;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("WhitePosition")) 
        {
            Debug.Log("WhitePotion");
            //Lock the object in place
            gameObject.layer = default;
            //Give the Player some Feedback

            //Atualizar variavel no interactor
            MainCamera.GetComponent<Interactor>().PotionsInPlace += 1;
        }
    }
}
