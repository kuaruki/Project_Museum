using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField]
    private Button Button_SG;
    [SerializeField]
    private Button Button_About;
    [SerializeField]
    private Button Button_Quit;

    public void StartFunction() {
        SceneManager.LoadScene("Museum");
    }

    public void AboutFunction() {
        SceneManager.LoadScene("About");
    }

    [SerializeField]
    public void QuitFunction() {
        Application.Quit();
    }
}
