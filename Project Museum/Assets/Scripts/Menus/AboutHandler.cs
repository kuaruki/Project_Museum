using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AboutHandler : MonoBehaviour
{
    [SerializeField]
    private Button BackButton;


    public void BackFunction() {
        SceneManager.LoadScene("MainMenu");
    }
}
