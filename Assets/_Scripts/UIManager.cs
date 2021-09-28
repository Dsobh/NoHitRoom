using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private TMP_Text timeText;
    private TMP_Text incrementText;
    public TMP_Text recordText;
    public TMP_Text actualScoreText;
    private Color initialColor;
    [SerializeField]
    private GameObject panelGameOver;


    void Start()
    {
        timeText = GameObject.Find("TimeText").GetComponent<TMP_Text>();
        incrementText = GameObject.Find("ExtraTimeIncrement_Text").GetComponent<TMP_Text>();
        initialColor = incrementText.color;

        
    }

    public void UpdateText(float time)
    {
        timeText.text = time.ToString();
    }
    
    public IEnumerator ShowIncrementTimeText(float time, float value)
    {
        incrementText.text = "+"+ value.ToString();
        for(float i=0f; i<1f; i+= 0.2f)
        {
            Color c = incrementText.color;
            c.a += i;
            incrementText.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        incrementText.color = initialColor;
        UpdateText(time);
    }

    public void ShowGameOverPanel(float record, float actual)
    {
        recordText.text = "Best: " + record.ToString();
        actualScoreText.text = "Last: " + actual.ToString();
        panelGameOver.SetActive(true);
    }
}
