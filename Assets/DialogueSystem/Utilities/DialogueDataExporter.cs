using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DS.Utilities
{
   
    //public static class DialogueDataExporter
    //{
    //    public static void ExportToJSON(List<DSNode> nodes, string filePath)
    //    {
    //        List<Dictionary<string, string>> nodeDataList = new List<Dictionary<string, string>>();

    //        foreach (var node in nodes)
    //        {
    //            Dictionary<string, string> nodeData = new Dictionary<string, string>
    //            {
    //                { "DialogueName", node.DialogueName },
    //                { "Text", node.Text },
    //                { "DialogueType", node.DialogueType.ToString() }
    //            };
    //            nodeDataList.Add(nodeData);
    //        }

    //        string json = JsonUtility.ToJson(new { nodes = nodeDataList });
    //        File.WriteAllText(filePath, json);
    //        Debug.Log("Dialogue data exported successfully!");
    //    }
    //}
}
