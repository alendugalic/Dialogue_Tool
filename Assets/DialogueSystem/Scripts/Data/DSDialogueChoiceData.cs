using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DS.Data
{
    using ScriptObject;

    [Serializable]
    public class DSDialogueChoiceData : MonoBehaviour
    {

        [field: SerializeField] public string Text { get; set; }
        [field: SerializeField] public DSDialogueSO NextDialogue {  get; set; }
        
    }
}

