using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DS.Managers
{

    public class DSDialogueManager : MonoBehaviour
    {
        public static DSDialogueManager Instance { get; private set; }

        public Text dialogueText;
        public Image characterPortrait;
        public Text characterName;

        private Dictionary<string, string> localizedText = new Dictionary<string, string>();
        private string currentLanguage = "en";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadLanguage(string languageCode)
        {
            currentLanguage = languageCode;
            LoadLocalizationData(languageCode);
        }

        private void LoadLocalizationData(string languageCode)
        {
            // Sample data load logic, you can replace this with actual file parsing.
            localizedText.Clear();
            if (languageCode == "en")
            {
                localizedText.Add("DialogueName", "Hello, adventurer!");
                localizedText.Add("NextDialogue", "Proceed to the next step.");
            }
            else if (languageCode == "es")
            {
                localizedText.Add("DialogueName", "¡Hola, aventurero!");
                localizedText.Add("NextDialogue", "Proceda al siguiente paso.");
            }
        }

        public void ShowDialogue(string dialogueName, string text, Sprite portrait, string name)
        {
            characterName.text = name;
            characterPortrait.sprite = portrait;
            dialogueText.text = localizedText.ContainsKey(text) ? localizedText[text] : text;
        }
    }
}
