using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   public void ToChooseYourMode()
    {
        SceneManager.LoadScene("ChooseYourMode");
    }

    public void ToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ToOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }

}
