using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Text_Model;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules
{
    public interface IRule
    {
        WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os);
    }
}
