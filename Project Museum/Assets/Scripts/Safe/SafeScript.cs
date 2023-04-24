using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SafeScript : MonoBehaviour
{

    //[SerializeField] private GameObject pausePanel;

    /*private void Awake() {
        Time.timeScale = 1; //isto faz com que o jogo comece assim que a cena e carregada,
                            //o que impede o jogo de ficar parado no inicio do nivel assim
                            //que se muda de mapa
    }*/

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        /*
        //Pause mechanic
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Time.timeScale == 1) {
                pause();
            }
            else {
                resume();
            }
        }*/
    }


    // Is Dead?
    /*
    void Dead() {
        animator.SetBool("Alive", false);
    }

    IEnumerator ExampleCoroutine() {
        Dead();
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
        deathPanel.SetActive(true);
        Time.timeScale = 0; //Stops the game
    }
    */

    /*
    //PAUSE
    public void pause() {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    //RESUME
    public void resume() {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    */
}