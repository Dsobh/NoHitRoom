using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextLocalizer : MonoBehaviour
{

    private TMP_Text fieldText;

    public string key;

    // Start is called before the first frame update
    void Start()
    {
        fieldText = GetComponent<TMP_Text>();
        string value = LocalizationSystem.GetLocalizedValue(key);
        fieldText.text = value;
    }
}
