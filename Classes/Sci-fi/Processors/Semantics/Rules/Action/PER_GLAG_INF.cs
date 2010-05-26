using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Text_Model;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules.Action
{
    class PER_GLAG_INF:IRule
    {
        public WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os)
        {
            //стопроцентное правило
            if ((clausesTree.rels[i].Name == "ПЕР_ГЛАГ_ИНФ"))
            {
                string target = sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr.ToLower();
                if ((target == "и") || (target == "или") || (target == "либо"))
                {
                    stats.markWord(clausesTree.rels[i].TargetItemNo, "Action", i, SourceTargetEnum.Target);
                    stats.getActualWordRelationsDict().Add(clausesTree.rels[i].TargetItemNo, i);
                }
                else
                {                    
                    ((os as ElementaryProcess).action as LongOperationAttribut).addElementaryAttribut(
                        sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                        "",
                        "UNION",
                        sent.get_Word(clausesTree.rels[i].TargetItemNo)
                        );
                    stats.markWord(clausesTree.rels[i].TargetItemNo, "Action", i, SourceTargetEnum.Target);
                    return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                        sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                        new PER_GLAG_INF(),
                        1,
                        i);
                }
            }
            return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new PER_GLAG_INF(),
                0,
                i);
        }
    }
}
