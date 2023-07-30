using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour
{
    public void OpenPlayGround1()
    {
        SceneManager.LoadScene("1");
    }
    public void OpenPlayGround2()
    {
        SceneManager.LoadScene("11");
    }
    public void OpenPlayGround3()
    {
        SceneManager.LoadScene("21");
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
