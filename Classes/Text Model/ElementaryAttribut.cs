using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;

namespace Operation_Structures_of_Texts.Classes.Text_Model
{
    public class ElementaryAttribut : IOperationStructure
    {
        public string operation;// только для NOT
        public string word;

        public string type;
        public string key;
        public string teg;
        public IWord inWord;

        public ElementaryAttribut(string operation, string word, IWord inWord)
        {
            this.operation = operation;
            this.word = word;
            this.inWord = inWord;
        }

        /*public ElementaryAttribut()
        {
            operation = "";
            word = "";
        }
        */
        #region IOperationStructure Members

        public string getString()
        {
            return operation + " " + word;
        }

        #endregion
    }
}