using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Text_Model;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules.Action
{
    public class PODL : IRule
    {
        public WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os)
        {
            LongOperationAttribut action = (os as ElementaryProcess).action as LongOperationAttribut;
            //стопроцентное правило
            if ((clausesTree.rels[i].Name == "ПОДЛ") &&
                !(stats.isMarkedWord(clausesTree.rels[i].SourceItemNo)))
            {
                action.addElementaryAttribut(sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr, "", sent.get_Word(clausesTree.rels[i].SourceItemNo));
                stats.markWord(clausesTree.rels[i].SourceItemNo, "Action", i, SourceTargetEnum.Source);
                return new WordRuleProbability(clausesTree.rels[i].SourceItemNo,
                sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr,
                new PODL(),
                1,
                i);
            }
            return new WordRuleProbability(clausesTree.rels[i].SourceItemNo,
                sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr,
                new PODL(),
                0,
                i);
        }
    }
}
