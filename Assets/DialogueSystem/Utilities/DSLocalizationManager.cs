using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.Manager
{
    public static class DSLocalizationManager 
    {
        private static Dictionary<string, string> localisationData = new Dictionary<string, string>();

        public static void LoadLocalisationData(string jsonFilePath)
        {
            TextAsset jsonData = Resources.Load<TextAsset>(jsonFilePath);
            if (jsonData != null)
            {
                localisationData = JsonUtility.FromJson<Dictionary<string, string>>(jsonData.text);
            }
            else
            {
                Debug.LogError("Localisation file not found!");
            }
        }

        public static string GetLocalizedString(string key)
        {
            if (localisationData.ContainsKey(key))
            {
                return localisationData[key];
            }
            return key; 
        }
    }
}

