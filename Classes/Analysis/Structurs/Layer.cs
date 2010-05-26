using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Text_Model;

namespace Operation_Structures_of_Texts.Classes.Analysis.Structurs
{
    class Layer
    {
        public string name = "";
        public bool[] layer;
        private string[] tmpLayer;

        public Layer(int caunt, string n)
        {
            layer = new bool[caunt];
            if (caunt > 0)
                layer[0] = true;
            name = n;
        }

        public string[] generateTmpLayer(List<ElementaryProcess> outStruct)
        {
            if (layer == null)
                return null;
            if (layer.Length == 0)
                return null;
            tmpLayer = new string[outStruct.Count];
            string firstElement = "";
            for (int i = 0; i < outStruct.Count; i++)
            {
                if (layer[i])
                    if (name == "Actor")
                        firstElement = outStruct[i].actor.getString();
                    else
                        if (name == "Action")
                            firstElement = outStruct[i].action.getString();
                tmpLayer[i] = firstElement;
            }
            return tmpLayer;
        }
    }
}
