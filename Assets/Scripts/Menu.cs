using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("Intro");
    }

    public void Credits(){
        SceneManager.LoadScene("Credits");
    }
    
    public void QuitGame() {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
