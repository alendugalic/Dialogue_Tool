using DS.Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace DS.Elements
{
    using Windows;
    using Enumerations;
    using Utilities;
    using Data.Save;

    public class DSSingleChoiceNode : DSNode
    {
        public override void Draw()
        {
            base.Draw();

            foreach(DSChoiceSaveData choice in Choices)
            {
                Port choicePort = this.CreatePort(choice.Text);
                choicePort.userData = choice;

                outputContainer.Add(choicePort);
            }

            RefreshExpandedState();
        }

        public override void Initialize(string nodeName, DSGraphView dsGraphView, Vector2 position)
        {
            base.Initialize(nodeName, dsGraphView, position);

            DialogueType = DSDialogueType.SingleChoice;
            DSChoiceSaveData choiceSaveData = new DSChoiceSaveData()
            {
                Text = "Next Dialogue"
            };

            Choices.Add(choiceSaveData);
        }
    }
}

