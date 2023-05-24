using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Chest : MonoBehaviour
{

    [SerializeField]
    private GameObject CipherThingy;
    [SerializeField]
    private GameObject Wand;

    // Start is called before the first frame update
    void Start()
    {
        Wand.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(CipherThingy.GetComponent<CipherScript>().CipherCorrect == true) { // if the cipher was solved
            //Open the lid
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(-60f, 180f, 0f));
            Wand.GetComponent<Collider>().enabled = true;
        }
    }
}
