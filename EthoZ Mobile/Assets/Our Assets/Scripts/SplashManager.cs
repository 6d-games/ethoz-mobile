using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    private void Awake()
    {
        Invoke("TransitionToMainMenu", 3.75f);  
    }

    public void TransitionToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
