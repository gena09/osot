using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Text_Model;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules.Action
{
    public class PRIAM_DOP:IRule
    {
        //источник может быть акшном а может и не быть
        public WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os)
        {
            LongOperationAttribut action = (os as ElementaryProcess).action as LongOperationAttribut;
            if ((clausesTree.rels[i].Name == "ПРЯМ_ДОП") &&
                    !(stats.isMarkedWord(clausesTree.rels[i].SourceItemNo)))
            {
                action.addElementaryAttribut(sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr, "", sent.get_Word(clausesTree.rels[i].SourceItemNo));
                stats.markWord(clausesTree.rels[i].SourceItemNo, "Action", i, SourceTargetEnum.Source);
                return new WordRuleProbability(clausesTree.rels[i].SourceItemNo,
                sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr,
                new PRIAM_DOP(),
                0.99,
                i);
            }
            return new WordRuleProbability(clausesTree.rels[i].SourceItemNo,
                sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr,
                new PRIAM_DOP(),
                0,
                i);
        }
    }
}
