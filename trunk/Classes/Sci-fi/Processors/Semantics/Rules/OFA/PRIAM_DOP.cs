using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Text_Model;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules.OFA
{
    public class PRIAM_DOP : IRule
    {
        //источник может быть акшном а может и не быть
        //приёмник в таком случае не всегда OFA
        public WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os)
        {
            LongOperationAttribut OFA = (os as ElementaryProcess).objectForAction as LongOperationAttribut;
            if ((clausesTree.rels[i].Name == "ПРЯМ_ДОП") &&
                    !(stats.isMarkedWord(clausesTree.rels[i].TargetItemNo)))
            {
                OFA.addElementaryAttribut(
                    sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                    "",
                    sent.get_Word(clausesTree.rels[i].TargetItemNo)
                    );                
                stats.markWord(clausesTree.rels[i].TargetItemNo, "OFA", i, SourceTargetEnum.Target);
                return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new PRIAM_DOP(),
                0.9,
                i);
            }
            return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new PRIAM_DOP(),
                0,
                i);
        }
    }
}
