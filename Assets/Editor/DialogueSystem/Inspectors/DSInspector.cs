using System.Collections.Generic;
using UnityEditor;
using System;

namespace DS.Inspector
{

    using ScriptObject;
    using Utilities;


    [CustomEditor(typeof(DSDialogue))]
    public class DSInspector : Editor
    {
        //dialogue scriptable objs
        private SerializedProperty dialogueContainerProperty;
        private SerializedProperty dialogueGroupProperty;
        private SerializedProperty dialogueProperty;
        //filters
        private SerializedProperty groupedDialoguesProperty;
        private SerializedProperty startingDialoguesOnlyProperty;
        //Indexes
        private SerializedProperty selectedDialogueGroupIndexProperty;
        private SerializedProperty selectedDialogueIndexProperty;

        private void OnEnable()
        {
            dialogueContainerProperty = serializedObject.FindProperty("dialogueContainer");
            dialogueGroupProperty = serializedObject.FindProperty("dialogueGroup");
            dialogueProperty = serializedObject.FindProperty("dialogue");
            groupedDialoguesProperty = serializedObject.FindProperty("groupedDialogues");
            startingDialoguesOnlyProperty = serializedObject.FindProperty("startingDialoguesOnly");
            selectedDialogueGroupIndexProperty = serializedObject.FindProperty("selectedDialogueGroupIndex");
            selectedDialogueIndexProperty = serializedObject.FindProperty("selectedDialogueIndex");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDialogueContainerArea();

            DSDialogueContainerSO dialogueContainer = (DSDialogueContainerSO)dialogueContainerProperty.objectReferenceValue;

            if(dialogueContainer == null)
            {
                StopDrawing("Select a Dialogue Container to see the rest of the Inspector");
                return;
            }

            DrawFiltersArea();

            bool currentStartingDialoguesOnlyFilter = startingDialoguesOnlyProperty.boolValue;

            List<string> dialogueNames;
            string dialogueFolderPath = $"Assets/DialogueSystem/Dialogues/{dialogueContainer.FileName}";
            string dialogueInfoMessage;

            if (groupedDialoguesProperty.boolValue)
            {
                List<string> dialogueGroupNames = dialogueContainer.GetDialogueGroupNames();

                if(dialogueGroupNames.Count == 0)
                {
                    StopDrawing("You are missing a Dialogue Group in this Container.");
                    return;
                }

                DrawDialogueGroupArea(dialogueContainer, dialogueGroupNames);
                DSDialogueGroupSO dialogueGroup = (DSDialogueGroupSO) dialogueGroupProperty.objectReferenceValue;
                dialogueNames = dialogueContainer.GetGroupedDialogueNames(dialogueGroup, currentStartingDialoguesOnlyFilter);

                dialogueFolderPath += $"/Groups/{dialogueGroup.GroupName}/Dialogues";

                dialogueInfoMessage = "There are no" + (currentStartingDialoguesOnlyFilter ? "Starting" : "") + "Dialogues in this Group.";
            }
            else
            {
                dialogueNames = dialogueContainer.GetUngroupedDialogueNames(currentStartingDialoguesOnlyFilter);

                dialogueFolderPath += "/Global/Dialogues";

                dialogueInfoMessage = "There are no" + (currentStartingDialoguesOnlyFilter ? "Starting" : "") + " Ungrouped Dialogues in this Container.";
            }
            if(dialogueNames.Count == 0)
            {
                StopDrawing(dialogueInfoMessage);
                return;
            }
            
            DrawDialogueArea(dialogueNames, dialogueFolderPath);

            serializedObject.ApplyModifiedProperties();
        }



        #region DrawMethods
        private void StopDrawing(string reason, MessageType messageType = MessageType.Info)
        {
            DSInspectorUtility.DrawHelpBox(reason, messageType);

            DSInspectorUtility.DrawSpaces();

            DSInspectorUtility.DrawHelpBox("You need a Dialogue for this component to work at Runtime!", MessageType.Warning);

            serializedObject.ApplyModifiedProperties();

        }
        private void DrawDialogueContainerArea()
        {
            DSInspectorUtility.DrawHeader("Dialogue Container");
            dialogueContainerProperty.DrawPropertyField();
            DSInspectorUtility.DrawSpaces();
        }
        private void DrawFiltersArea()
        {
            DSInspectorUtility.DrawHeader("Filters");
            groupedDialoguesProperty.DrawPropertyField();
            startingDialoguesOnlyProperty.DrawPropertyField();
            DSInspectorUtility.DrawSpaces();
        }
        private void DrawDialogueGroupArea(DSDialogueContainerSO dialogueContainer, List<string> dialogueGroupNames)
        {
            DSInspectorUtility.DrawHeader("Dialogue Group");

            int oldSelectedDialogueGroupIndex = selectedDialogueGroupIndexProperty.intValue;

            DSDialogueGroupSO oldDialogueGroup = (DSDialogueGroupSO)dialogueGroupProperty.objectReferenceValue;

            bool isOldDialogueGroupNull = oldDialogueGroup == null;
            string oldDialogueGroupName = isOldDialogueGroupNull ? "" : oldDialogueGroup.GroupName;


            UpdateIndexOnNamesListUpdate(dialogueGroupNames, selectedDialogueGroupIndexProperty, oldSelectedDialogueGroupIndex, oldDialogueGroupName, isOldDialogueGroupNull);

            selectedDialogueGroupIndexProperty.intValue = DSInspectorUtility.DrawPopUp("Dialogue Group", selectedDialogueGroupIndexProperty, dialogueGroupNames.ToArray());

            string selectedDialogueGroupName = dialogueGroupNames[selectedDialogueGroupIndexProperty.intValue];

            DSDialogueGroupSO selectedDialogueGroup = DSIOUtility.LoadAsset<DSDialogueGroupSO>
                ($"Assets/DialogueSystem/Dialogues/{dialogueContainer.FileName}/Groups/{selectedDialogueGroupName}", selectedDialogueGroupName);

            dialogueGroupProperty.objectReferenceValue = selectedDialogueGroup;
            DSInspectorUtility.DrawDisabledFields(() => dialogueGroupProperty.DrawPropertyField());
            DSInspectorUtility.DrawSpaces();
        }

        private void DrawDialogueArea(List<string> dialogueNames, string dialogueFolderPath)
        {

            DSInspectorUtility.DrawHeader("Dialogue");
            int oldSelectedDialogueIndex = selectedDialogueIndexProperty.intValue;
            DSDialogueSO oldDialogue = (DSDialogueSO) dialogueProperty.objectReferenceValue;
            bool isOldDialogueNull = oldDialogue == null;
            string oldDialogueName = isOldDialogueNull ? "" : oldDialogue.DialogueName;

            UpdateIndexOnNamesListUpdate(dialogueNames, selectedDialogueIndexProperty, oldSelectedDialogueIndex, oldDialogueName, isOldDialogueNull);

            selectedDialogueIndexProperty.intValue = DSInspectorUtility.DrawPopUp("Dialogue", selectedDialogueIndexProperty, dialogueNames.ToArray());
            string selectedDialogueName = dialogueNames[selectedDialogueIndexProperty.intValue];
            DSDialogueSO selectedDialogue = DSIOUtility.LoadAsset<DSDialogueSO>(dialogueFolderPath, selectedDialogueName);
            dialogueProperty.objectReferenceValue = selectedDialogue;

            DSInspectorUtility.DrawDisabledFields(() => dialogueProperty.DrawPropertyField());
            
        }
        #endregion
        #region Index Method
        private void UpdateIndexOnNamesListUpdate(List<string> optionNames,SerializedProperty indexProperty, int oldSelectedPropertyIndex, string oldPropertyName, bool isOldPropertyNull)
        {
            if (isOldPropertyNull)
            {
                indexProperty.intValue = 0;

                return;
            }

            bool oldIndexIsOutOfBoundsOfNamesListCount = oldSelectedPropertyIndex > optionNames.Count - 1;
            bool oldNameIsDifferentThanSelectedName = oldIndexIsOutOfBoundsOfNamesListCount || oldPropertyName != optionNames[oldSelectedPropertyIndex];
  
            if (oldNameIsDifferentThanSelectedName)
            {
                if (optionNames.Contains(oldPropertyName))
                {
                    indexProperty.intValue = optionNames.IndexOf(oldPropertyName);
                }
                else
                {
                    indexProperty.intValue = 0;
                }
            }
            
        }
        #endregion
    }
}

