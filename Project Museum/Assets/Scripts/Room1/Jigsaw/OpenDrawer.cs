using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{

    [SerializeField]
    private GameObject JigsawCamera;
    [SerializeField]
    private GameObject OpenedDrawerPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (JigsawCamera.GetComponent<DragAndDrop>().PiecesInPlace == 16) {
            transform.position = OpenedDrawerPosition.transform.position;
        }
    }
}
