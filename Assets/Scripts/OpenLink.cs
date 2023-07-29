using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void OpenChannel()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCypLS_1Km-Q4I3DXFStH54Q");
    }
    public void OpenPage()
    {
        Application.OpenURL("https://www.instagram.com/nekoosoft.company/?igshid=ZGUzMzM3NWJiOQ==");
    }
}
