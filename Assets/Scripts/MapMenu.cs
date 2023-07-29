using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour
{
    public void OpenPlayGround1()
    {
        SceneManager.LoadScene("PlayGround1");
    }
    public void OpenPlayGround2()
    {
        SceneManager.LoadScene("PlayGround2");
    }
    public void OpenPlayGround3()
    {
        SceneManager.LoadScene("PlayGround3");
    }
    public void backToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
