using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour 
{
    [SerializeField]
    private Button Resume;
    [SerializeField]
    private Button MainMenu;
    [SerializeField]
    private Button Quit;
    [SerializeField]
    private GameObject PlayerObject;

    public void ResumeFunction() {
        PlayerObject.GetComponent<PlayerMovement>().pause();
    } 
    public void MMFunction() {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitFunction() {
        Application.Quit();
    }
}
