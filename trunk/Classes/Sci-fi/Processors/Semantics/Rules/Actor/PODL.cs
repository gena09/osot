using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Text_Model;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules.Actor
{
    public class PODL : IRule
    {
        public WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os)
        {
            LongOperationAttribut actor = (os as ElementaryProcess).actor as LongOperationAttribut;
            if ((clausesTree.rels[i].Name == "ПОДЛ") &&
                    !(stats.isMarkedWord(clausesTree.rels[i].TargetItemNo)))
            {
                actor.addElementaryAttribut(sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr, "", sent.get_Word(clausesTree.rels[i].TargetItemNo));
                stats.markWord(clausesTree.rels[i].TargetItemNo, "Actor", i, SourceTargetEnum.Target);
                return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new PODL(),
                1,
                i);
            }
            return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new PODL(),
                0,
                i);
        }
    }
}
