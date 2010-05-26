using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Text_Model;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules.UnionsAndOther
{
    class PG:IRule
    {
        #region IRule Members

        public WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os)
        {
            ElementaryProcess ep = (os as ElementaryProcess);            
            if (clausesTree.rels[i].Name == "ПГ")
            {
                if (!stats.isMarkedWord(clausesTree.rels[i].SourceItemNo))
                {
                    stats.addLog("Источник предложной группы не отмечен атрибутом операторной структуры (ПГ)");
                }
                else
                {
                    LongOperationAttribut attr = null;

                    switch (stats.getTypeOfMarked(clausesTree.rels[i].SourceItemNo))
                    {
                        case "Action": attr = (ep.action as LongOperationAttribut); break;
                        case "Actor": attr = (ep.actor as LongOperationAttribut); break;
                        case "OFA": attr = (ep.objectForAction as LongOperationAttribut); break;
                        case "CFA": attr = (ep.charsOfAction as LongOperationAttribut); break;
                        case "AOFA": attr = (ep.additionalObjectsForAction as LongOperationAttribut); break;
                    }
                    attr.addBreaksOfAttributs(
                        new ElementaryAttribut("", sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr, sent.get_Word(clausesTree.rels[i].SourceItemNo)),
                        new ElementaryAttribut("", sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr, sent.get_Word(clausesTree.rels[i].TargetItemNo)),
                        "");

                    stats.markWord(clausesTree.rels[i].TargetItemNo,
                        stats.getTypeOfMarked(clausesTree.rels[i].SourceItemNo),
                        i, SourceTargetEnum.Target);
                    return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                    sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                    new PG(),
                    0.75,
                    i);
                }
                ///предполагаем, что предложная группа это обстоятельство
                ///Для будущих разработчиков:здесь самое место для отлавливания тэга, указывающего тип обстоятельства
                ///пример: (в углу) (в час) место и время - этот тэг можно определить по словарю, в котором будет сказано,
                ///что угол - категория места, а час - категория времени
                (ep.additionalObjectsForAction as LongOperationAttribut).addBreaksOfAttributs(
                            new ElementaryAttribut("", sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr, sent.get_Word(clausesTree.rels[i].SourceItemNo)),
                            new ElementaryAttribut("", sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr, sent.get_Word(clausesTree.rels[i].TargetItemNo)),
                            "");
                stats.markWord(clausesTree.rels[i].TargetItemNo,
                            "AOFA",
                            i, SourceTargetEnum.Target);
                stats.markWord(clausesTree.rels[i].SourceItemNo,
                            "AOFA",
                            i, SourceTargetEnum.Source);
                
                return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                        sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                        new PG(),
                        0.8,
                        i);
            }

            return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                    sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                    new PG(),
                    0,
                    i);
        }

        #endregion
    }
}
