using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.ScriptObject
{
    public class DSDialogueGroupSO : ScriptableObject
    {
        [field: SerializeField] public string GroupName { get; set; }

        public void Initialize(string groupName)
        {
            GroupName = groupName;
        }
 
    }
}

