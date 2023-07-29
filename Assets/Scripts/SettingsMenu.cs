using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
	
	
	public void setFullScreen(bool isFullscreen)
	{
        Screen.fullScreen = isFullscreen;
	}

    public void SetQuality (int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}
    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
