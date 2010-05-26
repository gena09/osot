using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Text_Model;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules.Action
{
    class ODNORODN_INF : IRule
    {
        public WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os)
        {
            ElementaryProcess ep = (os as ElementaryProcess);
            if ((clausesTree.rels[i].Name == "ОДНОР_ИНФ") &&
                    (!stats.isMarkedWord(clausesTree.rels[i].TargetItemNo)))
            {
                int saver = -1;
                bool isPartOfOther = false;
                IRelation saverRelation = null;
                if (stats.isMarkedWord(clausesTree.rels[i].SourceItemNo))
                    saver = clausesTree.rels[i].SourceItemNo;
                if (stats.getActualWordRelationsDict().ContainsKey(clausesTree.rels[i].SourceItemNo))
                {
                    if (clausesTree.rels[stats.getActualWordRelationsDict()[clausesTree.rels[i].SourceItemNo]].Name == "ПЕР_ГЛАГ_ИНФ")
                    {
                        saver = clausesTree.rels[stats.getActualWordRelationsDict()[clausesTree.rels[i].SourceItemNo]].SourceItemNo;
                        isPartOfOther = true;
                        saverRelation = clausesTree.rels[stats.getActualWordRelationsDict()[clausesTree.rels[i].SourceItemNo]];
                    }
                }
                if (saver == -1)
                {
                    stats.addLog("Повисшая группа однородных инфинитивов (ОДНОР_ИНФ)");
                    return null;
                }                
                if (sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr == ",")
                {
                    stats.markWord(
                        clausesTree.rels[i].TargetItemNo,
                        stats.getTypeOfMarked(saver),
                        i,
                        SourceTargetEnum.Target
                        );
                    return new WordRuleProbability(
                        clausesTree.rels[i].TargetItemNo,
                        sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                        new ODNORODN_INF(),
                        0.9,
                        i
                        );
                }
                LongOperationAttribut attr = null;
                switch (stats.getTypeOfMarked(saver))
                {
                    case "Action": attr = (ep.action as LongOperationAttribut); break;
                    case "Actor": attr = (ep.actor as LongOperationAttribut); break;
                    case "OFA": attr = (ep.objectForAction as LongOperationAttribut);  break;
                    case "CFA": attr = (ep.charsOfAction as LongOperationAttribut); break;
                    case "AOFA": attr = (ep.additionalObjectsForAction as LongOperationAttribut); break;
                }                
                return getReturnValueAndFillOther(
                    attr,
                    stats,
                    sent,
                    clausesTree,
                    saverRelation,
                    sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                    "",
                    "+",
                    clausesTree.rels[i].TargetItemNo, stats.getTypeOfMarked(saver),
                    i,
                    SourceTargetEnum.Target,
                    clausesTree.rels[i].TargetItemNo,
                    sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                    new ODNORODN_INF(),
                    0.9,
                    i,
                    isPartOfOther,
                    "",
                    "+"//не уверен что в этом брекете "+" как операция между ними(там ПЕР_ГЛАГ_ИНФ определяет)
                    );
            }
            return new WordRuleProbability(
                clausesTree.rels[i].TargetItemNo,
                sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                new ODNORODN_INF(),
                0,
                i
                );
        }

        public WordRuleProbability getReturnValueAndFillOther(LongOperationAttribut attr, StatisticsCollector stats, ISentence sent, ClausesTree cl, IRelation saverRelation,
            string attrWord, string notString, string opBefore,
            int markWordIndex, string mark, int relIndexToMark, SourceTargetEnum relEnumToMark,
            int wNoToWRPReturn, string wStrToWRPReturn, IRule ruleToWRPReturn, double prob, int relIndexToWRPReturn,
            bool isPartOf, string first, string operationBetween)
        {
            if (isPartOf)
            {
                if (!attr.changeEAttrToBracket(sent.get_Word(saverRelation.SourceItemNo).WordStr, notString, attrWord, operationBetween, sent.get_Word(saverRelation.SourceItemNo)))
                {                    
                    ElementaryAttribut exqludedAttr = attr.tryToGetMeBaseAttr(sent.get_Word(saverRelation.SourceItemNo).WordStr);
                    if (exqludedAttr != null)
                    {
                        Brackets br = new Brackets(exqludedAttr, new ElementaryAttribut(notString, attrWord, sent.get_Word(markWordIndex)), operationBetween);
                        attr.attributSequence.Add(br);
                        attr.operators.Add(AuxularyMethods.getOperatorByRelationIndex(relIndexToMark,cl,sent));
                    }
                }
                stats.markWord(markWordIndex, mark, relIndexToMark, relEnumToMark);
                return new WordRuleProbability(wNoToWRPReturn,
                    wStrToWRPReturn,
                    ruleToWRPReturn,
                    prob,
                    relIndexToWRPReturn);
            }
            else
            {
                attr.addElementaryAttribut(attrWord, notString, opBefore, sent.get_Word(markWordIndex));
                stats.markWord(markWordIndex, mark, relIndexToMark, relEnumToMark);
                return new WordRuleProbability(wNoToWRPReturn,
                    wStrToWRPReturn,
                    ruleToWRPReturn,
                    prob,
                    relIndexToWRPReturn);
            }
            return new WordRuleProbability(wNoToWRPReturn,
                    wStrToWRPReturn,
                    ruleToWRPReturn,
                    0,
                    relIndexToWRPReturn);
        }
    }
}