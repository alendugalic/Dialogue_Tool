
using UnityEngine;

namespace DS
{
    using ScriptObject;
    public class DSDialogue : MonoBehaviour
    {
        //Dialogue scriptable OBJ
        [SerializeField] private DSDialogueContainerSO dialogueContainer;
        [SerializeField] private DSDialogueGroupSO dialogueGroup;
        [SerializeField] private DSDialogueSO dialogue;

        //Filters
        [SerializeField] private bool groupedDialogues;
        [SerializeField] private bool startingDialoguesOnly;

        //Indexes
        [SerializeField] private int selectedDialogueGroupIndex;
        [SerializeField] private int selectedDialogueIndex;
    }
}

