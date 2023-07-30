using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HintMenu : MonoBehaviour
{
    public void GoToKnightMenu()
    {
        SceneManager.LoadScene("KnightMenu");
    }
        public void GoToArcherMenu()
    {
        SceneManager.LoadScene("ArcherMenu");
    }
        public void GoToBanditMenu()
    {
        SceneManager.LoadScene("BanditMenu");
    }
        public void backToMainMenu()
    {
        SceneManager.LoadScene("MapSelection");
    }
}
