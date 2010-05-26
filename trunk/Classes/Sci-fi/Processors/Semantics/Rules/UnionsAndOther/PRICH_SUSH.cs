using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Text_Model;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules.UnionsAndOther
{
    public class PRICH_SUSH : IRule
    {
        public WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os)
        {
            ElementaryProcess ep = (os as ElementaryProcess);
            if (clausesTree.rels[i].Name == "ПРИЧ_СУЩ")
            {
                if (!stats.isMarkedWord(clausesTree.rels[i].SourceItemNo))
                    {
                    stats.addLog("Источник причастия не найден аргумент (ПРИЧ_СУЩ)");
                    return null;
                }
                else
                {
                    switch (stats.getTypeOfMarked(clausesTree.rels[i].SourceItemNo))
                    {
                        case "Action":
                            ep.action = AuxularyMethods.fillUnion(ep.action, i, clausesTree, sent);
                            return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new PRICH_SUSH(),
                0.9,
                i);
                            
                        case "Actor":
                            ep.actor = AuxularyMethods.fillUnion(ep.actor, i, clausesTree, sent);
                            return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new PRICH_SUSH(),
                0.9,
                i);

                        case "OFA":
                            ep.objectForAction = AuxularyMethods.fillUnion(ep.objectForAction, i, clausesTree, sent);
                            return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new PRICH_SUSH(),
                0.9,
                i);
                    }
                }
            }
            return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new PRICH_SUSH(),
                0,
                i);
        }       
    }
}
