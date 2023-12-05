using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel1 : MonoBehaviour
{
    private void Start()
    {
        if(ContinueScript.instance != null)
        {
            Destroy(ContinueScript.instance);
        }
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1 VR");

    }
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");

    }
    public void ExitGame()
    {
        Application.Quit();

    }


}
