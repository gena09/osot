using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics
{
    public class WordDataTable
    {
        public string word;
        public string infinitiv;
        public string morphologyString;
        public string syntaxPart;
        public string attribut;
        public int sentenceNomber;
        public int clouseNomber;
        public List<IRelation> relations;
    }
}
