using DS.Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace DS.Elements
{
    using Enumerations;
    using Utilities;

    public class DSSingleChoiceNode : DSNode
    {
        public override void Draw()
        {
            base.Draw();

            foreach(string choice in Choices)
            {
                Port choicePort = this.CreatePort(choice);

                choicePort.portName = choice;

                outputContainer.Add(choicePort);
            }

            RefreshExpandedState();
        }

        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);

            DialogueType = DSDialogueType.SingleChoice;

            Choices.Add("Next Dialogue");
        }
    }
}

