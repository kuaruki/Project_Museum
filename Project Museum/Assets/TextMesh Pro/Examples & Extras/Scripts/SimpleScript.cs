using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    
    public class SimpleScript : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision) {
            //if(collision.transform.CompareTag("DrawPoint")) {

            //}
            
            Debug.Log("Colidiu com os pontos");
        }

    }
}
