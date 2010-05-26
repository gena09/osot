using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GRAPHANLib;
using SYNANLib;
using LEMMATIZERLib;
using AGRAMTABLib;
using MAPOSTLib;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors
{
    public class GraphematicProcessor : IWordProcessor
    {
        #region IWordProcessor Members

        public GraphmatFile graphan;
        private string inString;
        public string[] outStrArray;
        

        public GraphematicProcessor()
        {
            graphan = new GraphmatFile();
            graphan.Language = 1;
            graphan.LoadDicts();            
        }

        public void setInitialData(Object str)
        {
            if (str is string)
                this.inString = (string)str;
            else throw (new Exception("Графематический процессор: Не подходящий тип входных данных"));
        }

        public void work()
        {
            graphan.LoadStringToGraphan(inString);            
            string w;            
            outStrArray = new string[graphan.GetLineCount()];
            for (uint i = 0; i < graphan.GetLineCount(); i++)
            {                
                w = graphan.GetWord(i);
                outStrArray[i] = w;
            }
        }

        public double getRealProbabylity()
        {
            throw new NotImplementedException();
        }

        public double getApriorProbabylity()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
