using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour
{
    public void OpenPlayGround1()
    {
        SceneManager.LoadScene(8);
    }
    public void OpenPlayGround2()
    {
        SceneManager.LoadScene(9);
    }
    public void OpenPlayGround3()
    {
        SceneManager.LoadScene(10);
    }
    public void backToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void goToHintMenu()
    {
        SceneManager.LoadScene("HintMenu");
    }

}
