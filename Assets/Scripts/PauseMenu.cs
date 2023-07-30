using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Pause_Menu;
	public static bool isPaused;
    void Start () {
		Pause_Menu.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
            if(isPaused)
			{
				ResumeGame();
			}
			else
			{
				PauseGame();
			}
			
		}
		
	}
	public void PauseGame()
	{
		Pause_Menu.SetActive(true);
        Time.timeScale = 0f;
		isPaused = true;
	}
	public void ResumeGame()
	{
		Pause_Menu.SetActive(false);
        Time.timeScale = 1f;
		isPaused = false;
	}

	public void GoToMainMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("MapSelection");
	}
	public void QuitGame()
	{
		Application.Quit();
	}

}
