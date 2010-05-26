using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Text_Model;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules.UnionsAndOther
{
    public class OTRICAT_FORM : IRule
    {
        public WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os)
        {
            ElementaryProcess ep = (os as ElementaryProcess);
            if (clausesTree.rels[i].Name == "ОТР_ФОРМА")
            {
                if (!stats.isMarkedWord(clausesTree.rels[i].SourceItemNo))
                    {
                    stats.addLog("Для отрицания не найден аргумент (ОТР_ФОРМА)");
                    return null;
                }                    
                else
                {
                    switch (stats.getTypeOfMarked(clausesTree.rels[i].SourceItemNo))
                    {
                        case "Action":
                            ep.action = AuxularyMethods.fillNOT(ep.action, i, clausesTree, sent);
                            return new WordRuleProbability(clausesTree.rels[i].SourceItemNo,
                sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr,
                new OTRICAT_FORM(),
                1,
                i);
                            break;
                        case "Actor":
                            ep.actor = AuxularyMethods.fillNOT(ep.actor, i, clausesTree, sent);
                            return new WordRuleProbability(clausesTree.rels[i].SourceItemNo,
                sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr,
                new OTRICAT_FORM(),
                1,
                i);
                            break;
                        case "OFA":
                            ep.objectForAction = AuxularyMethods.fillNOT(ep.objectForAction, i, clausesTree, sent);
                            return new WordRuleProbability(clausesTree.rels[i].SourceItemNo,
                sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr,
                new OTRICAT_FORM(),
                1,
                i);
                            break;
                        case "CFA":
                            ep.charsOfAction = AuxularyMethods.fillNOT(ep.charsOfAction, i, clausesTree, sent);
                            return new WordRuleProbability(clausesTree.rels[i].SourceItemNo,
                sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr,
                new OTRICAT_FORM(),
                1,
                i);
                            break;
                        case "AOFA":
                            ep.additionalObjectsForAction = AuxularyMethods.fillNOT(ep.additionalObjectsForAction, i, clausesTree, sent);
                            return new WordRuleProbability(clausesTree.rels[i].SourceItemNo,
                sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr,
                new OTRICAT_FORM(),
                1,
                i);
                            break;
                    }
                }
            }
            return new WordRuleProbability(clausesTree.rels[i].SourceItemNo,
                sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr,
                new OTRICAT_FORM(),
                0,
                i);
        }
    }
}
