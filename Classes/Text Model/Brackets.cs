using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operation_Structures_of_Texts.Classes.Text_Model
{
    public class Brackets : IOperationStructure
    {
        public string type;
        public string key;
        public string teg;

        public ElementaryAttribut atribut1;
        public ElementaryAttribut atribut2;

        public List<IOperationStructure> unionsForAttr2;
        public List<string> operators;
        public string operationBetween;

        public Brackets(ElementaryAttribut atribut1, ElementaryAttribut atribut2, string operationBetween)
        {
            this.atribut1 = atribut1;
            this.atribut2 = atribut2;
            this.operationBetween = operationBetween;
            unionsForAttr2 = new List<IOperationStructure>();
            operators = new List<string>();
        }

        public void addUnionToAttr2(IOperationStructure union, string operationBefore)
        {
            unionsForAttr2.Add(union);
            operators.Add(operationBefore);
        }

        #region IOperationStructure Members

        public string getString()
        {
            string reprez = "(" +
                atribut1.operation + " " +
                atribut1.word + " " +
                operationBetween + " " +
                atribut2.operation + " " +
                atribut2.word;
            for (int i = 0; i < unionsForAttr2.Count; i++)
            {                
                try
                {
                    reprez += " " + operators[i] + " ";
                }
                catch (Exception ex) { }
                reprez += unionsForAttr2[i].getString();
            }
                return reprez + ")";
        }

        #endregion
    }
}
