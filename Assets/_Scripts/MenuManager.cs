using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsPanel;
    [SerializeField]
    private GameObject settingsPanel;
    public Dropdown _languageMenu;


    void Start()
    {
        //Add listener for when the value of the Dropdown changes, to take action
        _languageMenu.onValueChanged.AddListener(delegate {
            DropdownValueChanged(_languageMenu);
        });
    }

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

    public void ShowSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ApplyChanges()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


    void DropdownValueChanged(Dropdown change)
    {
        switch(change.value)
        {
            case 0:
                LocalizationSystem._language = LocalizationSystem.Language.English;
                break;
            case 1:
                LocalizationSystem._language = LocalizationSystem.Language.Spanish;
                break;
        }
    }

}
