using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public string[] dialogLines;

    private int dialogLinesCounter = 0;

    public GameObject player;
    public GameObject dialogCanvas;
    public TMP_Text dialogBox;
    public TMP_Text nameTxt;
    
    public string characterName = "King";
    private bool blockDialog = false;


    // Start is called before the first frame update
    void Start()
    {
        StopGameplay();
        dialogCanvas.SetActive(true);
        nameTxt.text = "???";
        ChangeText(dialogLines[dialogLinesCounter]);
        dialogLinesCounter++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && !blockDialog)
        {
            ChangeText(dialogLines[dialogLinesCounter]);
            dialogLinesCounter++;
        }

        if(dialogLinesCounter == 2)
        {
            nameTxt.text = characterName;
        }

        if(dialogLinesCounter == 5)
        {
            dialogCanvas.SetActive(false);
            ResumeGameplay();
            blockDialog = true;
            if(CheckMovement())
            {
                //TODO:Reactivar canvas
            }
        }
    }

    private void ChangeText(string key)
    {
        string value = LocalizationSystem.GetLocalizedValue(key);
        dialogBox.text = value;
    }

    private void StopGameplay()
    {
        Time.timeScale = 0;
        player.GetComponent<Movement>().enabled = false;
    }

    private void ResumeGameplay()
    {
        Time.timeScale = 1;
        player.GetComponent<Movement>().enabled = true;
    }

    private bool CheckMovement()
    {
        //TODO: comprobar que ha pulsado AWSD
        return false;
    }
}
