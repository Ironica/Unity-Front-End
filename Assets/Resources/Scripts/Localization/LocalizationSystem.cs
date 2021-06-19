using System;
using System.Collections;
using System.Collections.Generic;
using Resources.Scripts.Utils;
using UnityEngine;

public class LocalizationSystem
{
    public enum Language
    {
        English, French, Chinese, Japanese
    }

    public static Language language = Language.English;

    private static Dictionary<string, string> localizedEN;
    private static Dictionary<string, string> localizedFR;
    private static Dictionary<string, string> localizedZH;
    private static Dictionary<string, string> localizedJP;

    public static bool isInit;

    public static CSVLoader csvLoader;

    public static void Init()
    {
        csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        UpdateDictionaries();

        isInit = true;
    }

    public static void UpdateDictionaries()
    {
        localizedEN = csvLoader.GetDictionaryValues("en");
        localizedFR = csvLoader.GetDictionaryValues("fr");
        localizedZH = csvLoader.GetDictionaryValues("zh");
        localizedJP = csvLoader.GetDictionaryValues("jp");
    }

    public static Dictionary<string, string> GetDictionaryForEditor()
    {
        if (!isInit) { Init(); }

        return localizedEN;
    }

    public static string GetLocalizedValue(string key)
    {
        if (!isInit) { Init(); }

        return language switch
        {
            Language.English => localizedEN.GetOrDefault(key),
            Language.French => localizedFR.GetOrDefault(key),
            Language.Chinese => localizedZH.GetOrDefault(key),
            Language.Japanese => localizedJP.GetOrDefault(key),
            _ => throw new Exception(),
        };
    }

#if UNITY_EDITOR
    public static void Add(string key, string value)
    {
        if (value.Contains("\""))
        {
            value = value.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }
        csvLoader.LoadCSV();
        csvLoader.Add(key, value);
        csvLoader.LoadCSV();
        UpdateDictionaries();
    }

    public static void Replace(string key, string value)
    {
        if (value.Contains("\""))
        {
            value = value.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }
        csvLoader.LoadCSV();
        csvLoader.Edit(key, value);
        csvLoader.LoadCSV();
        UpdateDictionaries();
    }

    public static void Remove(string key)
    {
        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }
        csvLoader.LoadCSV();
        csvLoader.Remove(key);
        csvLoader.LoadCSV();
        UpdateDictionaries();
    }
#endif
}
