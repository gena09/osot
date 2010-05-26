using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Text_Model;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules.UnionsAndOther
{
    public class RAZRIV_SOUS : IRule
    {
        public WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os)
        {
            ElementaryProcess ep = (os as ElementaryProcess);
            if ((clausesTree.rels[i].Name == "РАЗРЫВ_СОЮЗ") &&
                (!stats.isMarkedWord(clausesTree.rels[i].TargetItemNo)))
            {
                int saver = -1;
                for (int j = 0; j < clausesTree.rels.Count; j++)
                {
                    if ((clausesTree.rels[j].Name == "РАЗРЫВ_СОЮЗ") && (i != j))
                    {
                        if (clausesTree.rels[j].SourceItemNo == clausesTree.rels[i].SourceItemNo)//условие одной однородности
                        {
                            if (stats.isMarkedWord(clausesTree.rels[j].TargetItemNo))
                                saver = clausesTree.rels[j].TargetItemNo;
                        }
                    }
                }
                if (saver == -1)
                {
                    stats.addLog("Повисшая группа (РАЗРЫВ_СОЮЗ или ОТСОЮЗ)");
                    return null;
                }//переписать свитч, добавить маркер слов и отношений!!!
                LongOperationAttribut attr = null;

                switch (stats.getTypeOfMarked(clausesTree.rels[i].SourceItemNo))
                {
                    case "Action": attr = (ep.action as LongOperationAttribut); break;
                    case "Actor": attr = (ep.actor as LongOperationAttribut); break;
                    case "OFA": attr = (ep.objectForAction as LongOperationAttribut); break;
                    case "CFA": attr = (ep.charsOfAction as LongOperationAttribut); break;
                    case "AOFA": attr = (ep.additionalObjectsForAction as LongOperationAttribut); break;
                }

                attr.addElementaryAttribut(
                            sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr, "",
                            AuxularyMethods.getOperatorByRelationIndex(i, clausesTree, sent),
                            sent.get_Word(clausesTree.rels[i].TargetItemNo)
                );
                stats.markWord(
                    clausesTree.rels[i].TargetItemNo,
                    stats.getTypeOfMarked(clausesTree.rels[i].SourceItemNo),
                    i,
                    SourceTargetEnum.Target
                    );
                return new WordRuleProbability(
                            clausesTree.rels[i].TargetItemNo,
                            sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                            new RAZRIV_SOUS(),
                            0.9,
                            i
                );
                /*
                switch (stats.markedWords[saver])
                {
                    case "Action":
                        (ep.action as LongOperationAttribut).addElementaryAttribut(
                            sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr, "",
                            AuxularyMethods.getOperatorByRelationIndex(i, clausesTree, sent));
                        return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new RAZRIV_SOUS(),
                0.9,
                i);
                        break;
                    case "Actor":
                        (ep.actor as LongOperationAttribut).addElementaryAttribut(
                            sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr, "",
                            AuxularyMethods.getOperatorByRelationIndex(i, clausesTree, sent));
                        return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new RAZRIV_SOUS(),
                0.9,
                i);
                        break;
                    case "OFA":
                        (ep.objectForAction as LongOperationAttribut).addElementaryAttribut(
                            sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr, "",
                            AuxularyMethods.getOperatorByRelationIndex(i, clausesTree, sent));
                        return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new RAZRIV_SOUS(),
                0.9,
                i);
                        break;
                    case "CFA":
                        (ep.charsOfAction as LongOperationAttribut).addElementaryAttribut(
                            sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr, "",
                            AuxularyMethods.getOperatorByRelationIndex(i, clausesTree, sent));
                        return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new RAZRIV_SOUS(),
                0.9,
                i);
                        break;
                    case "AOFA":
                        (ep.additionalObjectsForAction as LongOperationAttribut).addElementaryAttribut(
                            sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr, "",
                            AuxularyMethods.getOperatorByRelationIndex(i, clausesTree, sent));
                        return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new RAZRIV_SOUS(),
                0.9,
                i);
                        break;

                }*/
            }
            return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new RAZRIV_SOUS(),
                0,
                i);
        }
    }
}