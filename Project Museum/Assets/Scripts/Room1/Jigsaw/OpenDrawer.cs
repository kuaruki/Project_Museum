using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{

    [SerializeField] private GameObject JigsawCamera;
    [SerializeField] private GameObject OpenedDrawerPosition;
    [SerializeField] private GameObject OpenedPaintingPosition;
    [SerializeField] private GameObject JigsawPainting;
    [SerializeField] private Collider JigsawPaintingCollider;
    private Collider Drawercollider;

    private void Start() {
        JigsawPaintingCollider.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (JigsawCamera.GetComponent<DragAndDrop>().PiecesInPlace == 16) {
            //transform.position = OpenedDrawerPosition.transform.position;
            //JigsawPainting.transform.position = OpenedPaintingPosition.transform.position; //Tentativa de por o quadro dentro da gaveta do jigsaw
            //JigsawPaintingCollider.enabled = true;
        }
    }
}
