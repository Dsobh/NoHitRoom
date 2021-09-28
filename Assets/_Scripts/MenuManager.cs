using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsPanel;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            ChangeScene(1);
        }
    }

    public void ChangeScene(int sceneIndex)
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(sceneIndex);

    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeInHierarchy);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
