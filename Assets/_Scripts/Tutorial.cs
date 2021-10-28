using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public string[] dialogLines;
    public string id_room;
    public GameObject Room;
    private RoomBehaviour _roomBehaviour;

    private int dialogLinesCounter = 0;

    public GameObject player;
    public GameObject dialogCanvas;
    public TMP_Text dialogBox;
    public TMP_Text nameTxt;
    
    public string characterName = "King";
    private bool blockDialog = false;
    private bool repeat = true;

    private List<KeyCode> awsd = new List<KeyCode>();

    public float softTime = 1f;
    private float softTimeCounter =0;

    // Start is called before the first frame update
    void Start()
    {
        _roomBehaviour = Room.GetComponent<RoomBehaviour>();
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
            if(dialogLines.Length <= dialogLinesCounter)
            {
                dialogCanvas.SetActive(false);
                dialogLinesCounter++;
            }else
            {
                ChangeText(dialogLines[dialogLinesCounter]);
                dialogLinesCounter++;
            }
            
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

            if(Input.GetKeyDown(KeyCode.A))
            {
                AddKeyCode(KeyCode.A);
            }
            if(Input.GetKeyDown(KeyCode.W))
            {
                AddKeyCode(KeyCode.W);
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                AddKeyCode(KeyCode.S);
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                AddKeyCode(KeyCode.D);
            }

            if(CheckMovement())
            {
                if(softTimeCounter >= softTime)
                {
                    StopGameplay();
                    dialogCanvas.SetActive(true);
                    blockDialog = false;
                }else
                {
                    softTimeCounter += Time.deltaTime;
                }
            }
        }

        if(dialogLinesCounter == 6 && repeat)
        {
                ResumeGameplay();
                _roomBehaviour.ExecutePattern(_roomBehaviour._patternCollection.GetPatternFromRoom(id_room).sequence[1].elements);
                repeat = false;
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
        if(awsd.Count == 4)
        {
            return true;
        }else
        {
            return false;
        }
        
    }

    private void AddKeyCode(KeyCode kcode)
    {
        if(!awsd.Contains(kcode))
        {
            awsd.Add(kcode);
        }
    }
}
