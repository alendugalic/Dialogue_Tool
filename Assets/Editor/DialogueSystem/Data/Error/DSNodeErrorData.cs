using DS.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


namespace DS.Data.Error
{
    using Elements;
    public class DSNodeErrorData 
    {
        public DSErrorData ErrorData { get; set; }
        public List<DSNode> Nodes { get; set; }

        public DSNodeErrorData()
        {
            Nodes = new List<DSNode>();
            ErrorData = new DSErrorData();
        }
      
    }
}

