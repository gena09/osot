using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Text_Model;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics
{
    public class SemanticSearchWithProbabilytis
    {
        private ClausesTree clausesTree;
        private ISentence sent;
        private ElementaryProcess ep;
        
        public StatisticsCollector stats;
        
        public SemanticSearchWithProbabilytis(ClausesTree clausesTree, ISentence sent)
        {
            this.clausesTree = clausesTree;
            this.sent = sent;
            ep = new ElementaryProcess(clausesTree);
            stats = StatisticsCollector.Instance;
            stats.initNewPackage();//clearAll();
        }

        public IOperationStructure optimazatorsSearch()
        {
            clausesTree.checkBreaks();
            List<WordRuleProbability> wrpsBase = new List<WordRuleProbability>();            
                checkForThisRule(wrpsBase, "Action.PODL");
                checkForThisRule(wrpsBase, "Action.PRIAM_DOP");
                checkForThisRule(wrpsBase, "Actor.PODL");
                checkForThisRule(wrpsBase, "OFA.PRIAM_DOP");
                checkForThisRule(wrpsBase, "OFA.INSTR_DOP");
                checkForThisRule(wrpsBase, "UnionsAndOther.PG");
            
            ep = buildOptimalOperationStructure(wrpsBase);

            tryGetAction(ep, wrpsBase);
            tryGetActor(ep, wrpsBase);

            List<WordRuleProbability> wrpsUnions = new List<WordRuleProbability>();            
            
                checkForThisRule(wrpsUnions, "Action.PER_GLAG_INF");
                checkForThisRule(wrpsUnions, "UnionsAndOther.PRIDAT_OPR");
                checkForThisRule(wrpsUnions, "UnionsAndOther.ODNORODN_IG");
                checkForThisRule(wrpsUnions, "UnionsAndOther.RAZRIV_SOUS");
                checkForThisRule(wrpsUnions, "UnionsAndOther.OTRICAT_FORM");
                checkForThisRule(wrpsUnions, "Action.ODNORODN_INF");
                checkForThisRule(wrpsUnions, "UnionsAndOther.PRIL_SUSH");
                checkForThisRule(wrpsUnions, "UnionsAndOther.SUSH_CHISL");
                checkForThisRule(wrpsUnions, "UnionsAndOther.GENIT_IG");
                checkForThisRule(wrpsUnions, "UnionsAndOther.FIO");
                checkForThisRule(wrpsUnions, "UnionsAndOther.ANAT_SRAVN");
                checkForThisRule(wrpsUnions, "UnionsAndOther.ELECT_IG");
                checkForThisRule(wrpsUnions, "UnionsAndOther.OTSRAVN");
                checkForThisRule(wrpsUnions, "UnionsAndOther.ODNOR_PRIL");
                checkForThisRule(wrpsUnions, "UnionsAndOther.ODNOR_NAR");
                checkForThisRule(wrpsUnions, "UnionsAndOther.SUSH_OBS_PRIL");            
                checkForThisRule(wrpsUnions, "UnionsAndOther.NARECH_GLAGOL");                
                checkForThisRule(wrpsUnions, "UnionsAndOther.PRICH_SUSH");
                checkForThisRule(wrpsUnions, "UnionsAndOther.NAR_PRIL");
                checkForThisRule(wrpsUnions, "UnionsAndOther.NAR_PREDIK");
                checkForThisRule(wrpsUnions, "UnionsAndOther.MODIF_PRIL");
                checkForThisRule(wrpsUnions, "UnionsAndOther.SRAVN_STEPEN");
                checkForThisRule(wrpsUnions, "UnionsAndOther.NAR_NAR_CHISL");
            //tryGetAOFA();
            ep.statsForThatProcess = stats.getActualPackage();
            return ep;
        }

        private void checkForThisRule(List<WordRuleProbability> wrps, string className)
        {
            for (int i = 0; i < clausesTree.rels.Count; i++)
            {
                try
                {
                    wrps.Add(Factory.get(className).check(clausesTree, i, sent, stats, ep));
                }
                catch (Exception ex)
                {
                    stats.addLog("From " + className + "on rule #" + Convert.ToString(i) + ": "+ ex.Message);
                }
            }
        }

        #region PrognosisMethods

        /// <summary>
        /// Попытка предсказать наименование действия, если оно не было найдено детерминантным методом
        /// </summary>
        /// <param name="ep"></param>        
        private void tryGetAction(ElementaryProcess ep, List<WordRuleProbability> wrps)
        {
            LongOperationAttribut action = ep.action as LongOperationAttribut;            
            if (action.attributSequence.Count == 0)
            {
                Prognoser prognoser = new Prognoser();                
                int actionNo = prognoser.getBestVariantForAction(sent, clausesTree.cloNumber, stats);
                if ((actionNo != -1) && (prognoser.actionWeight > 1))
                {
                    action.addElementaryAttribut(sent.get_Word(actionNo).WordStr, "", sent.get_Word(actionNo));
                    stats.markWord(actionNo, "Action", -1, SourceTargetEnum.Both);
                }
            }
        }

        /// <summary>
        /// Попытка предсказать наименование актора, если оно не было найдено детерминантным методом
        /// </summary>
        /// <param name="ep"></param>
        /// <param name="wrpsBase"></param>
        /// <param name="clausesTree"></param>
        private void tryGetActor(ElementaryProcess ep, List<WordRuleProbability> wrpsBase)
        {
            LongOperationAttribut actor = ep.actor as LongOperationAttribut;            
            if (actor.attributSequence.Count == 0)
            {
                Prognoser prognoser = new Prognoser();
                double actorWeight = 0;
                int actorNo = prognoser.getBestVariantForActor(sent, clausesTree.cloNumber, stats, ep, wrpsBase);
                if ((actorNo != -1) && (prognoser.actorWeight > 1))
                {
                    actor.addElementaryAttribut(sent.get_Word(actorNo).WordStr, "", sent.get_Word(actorNo));
                    stats.markWord(actorNo, "Actor", -1, SourceTargetEnum.Both);
                }
            }
        }

        #endregion

        /// <summary>
        /// Выборка и реализация в операторную структуру наиболее вероятных правил
        /// </summary>
        /// <param name="wrpsBase"></param>
        /// <returns></returns>
        private ElementaryProcess buildOptimalOperationStructure(List<WordRuleProbability> wrpsBase)
        {
            Dictionary<string, List<WordRuleProbability>> wordsNrules = new Dictionary<string, List<WordRuleProbability>>();
            for (int i = 0; i < wrpsBase.Count; i++)
            {
                if (!wordsNrules.ContainsKey(wrpsBase[i].word))
                {
                    wordsNrules.Add(wrpsBase[i].word, new List<WordRuleProbability>());
                    wordsNrules[wrpsBase[i].word].Add(wrpsBase[i]);
                }
                else
                {
                    wordsNrules[wrpsBase[i].word].Add(wrpsBase[i]);
                }
            }
            //снятие семантической омонимии
            //нужно подсчитать сколько её!!!!!!!!!!!
            List<WordRuleProbability> wrpBaseClean = new List<WordRuleProbability>();
            foreach(KeyValuePair<string, List<WordRuleProbability>> pair in wordsNrules)
            {
                WordRuleProbability wrpWithMaxProb = null;
                double maxProb = 0;
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    if (pair.Value[i].probability > maxProb)
                    {
                        maxProb = pair.Value[i].probability;
                        wrpWithMaxProb = pair.Value[i];
                    }
                }
                if (wrpWithMaxProb != null) wrpBaseClean.Add(wrpWithMaxProb);
            }
            
            ElementaryProcess cleanEP = new ElementaryProcess(clausesTree);            
            
            stats.clearAll();
            for (int i = 0; i < wrpBaseClean.Count; i++)
            {
                wrpBaseClean[i].rule.check(clausesTree, wrpBaseClean[i].relationIndex, sent, stats, cleanEP);
            }
            wrpsBase = wrpBaseClean;
            return cleanEP;
        }
    }
}
