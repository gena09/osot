using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Text_Model;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules.OFA
{
    class INSTR_DOP : IRule
    {
        public WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os)
        {
            LongOperationAttribut OFA = (os as ElementaryProcess).objectForAction as LongOperationAttribut;
            if ((clausesTree.rels[i].Name == "ИНСТР_ДОП") &&
                    !(stats.isMarkedWord(clausesTree.rels[i].TargetItemNo)))
            {
                ElementaryAttribut newAttr = new ElementaryAttribut("", sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr, sent.get_Word(clausesTree.rels[i].TargetItemNo));
                newAttr.teg = "Instrumental";
                OFA.addElementaryAttribut(newAttr);
                stats.markWord(clausesTree.rels[i].TargetItemNo, "OFA", i, SourceTargetEnum.Target);
                return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new INSTR_DOP(),
                0.9,
                i);
            }
            return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new INSTR_DOP(),
                0,
                i);
        }
    }
}
