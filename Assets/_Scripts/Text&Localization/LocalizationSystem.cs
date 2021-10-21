using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem
{
    public enum Language
    {
        English,
        Spanish
    }

    public static Language _language = Language.English;

    private static Dictionary<string, string> englishDictionary;
    private static Dictionary<string, string> spanishDictionary;

    public static bool isInit;

    public static void Init()
    {
        CSVLoader _csvLoader = new CSVLoader();
        _csvLoader.LoadCSV();

        englishDictionary = _csvLoader.GetDisctionaryValues("English");
        spanishDictionary = _csvLoader.GetDisctionaryValues("Spanish");

        isInit = true;
    }

    public static string GetLocalizedValue(string key)
    {
        if(!isInit)
        {
            Init();
        }

        string value = key;

        switch(_language)
        {
            case Language.English:
                englishDictionary.TryGetValue(key, out value);
                break;
            case Language.Spanish:
                spanishDictionary.TryGetValue(key, out value);
                break;
        }

        return value;
    }

}
