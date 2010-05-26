using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Text_Model;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules.UnionsAndOther
{
    class NARECH_GLAGOL:IRule
    {
        #region IRule Members

        public WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os)
        {
            ElementaryProcess ep = (os as ElementaryProcess);
            if (clausesTree.rels[i].Name == "НАРЕЧ_ГЛАГОЛ")
            {
                if (!stats.isMarkedWord(clausesTree.rels[i].SourceItemNo))
                {
                    stats.addLog("Источник наречия не отмечен атрибутом операторной структуры (НАРЕЧ_ГЛАГОЛ)");
                    return null;
                }

                else
                {
                    LongOperationAttribut attr = null;
                    switch (stats.getTypeOfMarked(clausesTree.rels[i].SourceItemNo))
                    {
                        case "Action": attr = (ep.charsOfAction as LongOperationAttribut); break;
                        default: throw new Exception("Наречие с источником не в роли экшна (НАРЕЧ_ГЛАГОЛ)");
                    }
                    attr.addElementaryAttribut(sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr, "", sent.get_Word(clausesTree.rels[i].TargetItemNo));
                    stats.markWord(clausesTree.rels[i].SourceItemNo, "CFA", i, SourceTargetEnum.Target);
                    return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                    sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                    new NARECH_GLAGOL(),
                    1,
                    i);
                }
            }
            return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                    sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                    new NARECH_GLAGOL(),
                    0,
                    i);
        }

        #endregion
    }
}
