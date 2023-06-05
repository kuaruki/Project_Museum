using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPotion : MonoBehaviour
{
    [SerializeField]
    private Camera MainCamera;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("RedPosition")) 
        {
            Debug.Log("RedPotion");
            //Lock the object in place
            gameObject.layer = default;
            //Give the Player some Feedback
            MainCamera.GetComponent<Interactor>().Correct.Play();

            //Atualizar variavel no interactor
            MainCamera.GetComponent<Interactor>().PotionsInPlace += 1;
        }
    }
}
