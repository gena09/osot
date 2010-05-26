using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics
{
    public class WordRuleProbability
    {
        public int wordNomber;
        public string word;
        public IRule rule;
        public double probability;
        public int relationIndex;
        

        public WordRuleProbability(int wordNomber, string word, IRule rule, double probability, int relationIndex)
        {
            this.wordNomber = wordNomber;
            this.word = word;
            this.rule = rule;
            this.probability = probability;
            this.relationIndex = relationIndex;
        }
    }
}
