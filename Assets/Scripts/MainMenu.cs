using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToMapMenu()
    {
        SceneManager.LoadScene("MapSelection");
    }
    public void GoToDevMenu()
    {
        SceneManager.LoadScene("AboutUs");
    }
    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    public void QuitGame()
	{
		Application.Quit();
	}
}
