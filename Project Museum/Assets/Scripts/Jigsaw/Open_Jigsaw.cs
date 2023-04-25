using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Open_Jigsaw : MonoBehaviour, IInteractable
{
    public void Interact() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Jigsaw2D");
    }
}
