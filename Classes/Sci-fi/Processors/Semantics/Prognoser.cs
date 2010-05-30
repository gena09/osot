using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;
using Operation_Structures_of_Texts.Classes.Text_Model;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics
{
    public class Prognoser
    {
        public double actionWeight = 0;
        /// <summary>
        /// Метод, дающий прогноз для экшна
        /// </summary>
        /// <param name="sent"></param>
        /// <param name="cloNomber"></param>
        /// <param name="stat"></param>
        /// <returns></returns>
        public int getBestVariantForAction(ISentence sent, int cloNomber, StatisticsCollector stat)
        {
            int bestWordNu = -1;
            Dictionary<int, double> pretendents = new Dictionary<int, double>();
            for (int i = 0; i < sent.WordsNum; i++)
            {
                if ((sent.get_Word(i).ClauseNo == cloNomber) && (!stat.isMarkedWord(i)))
                {
                    pretendents.Add(i, 1);
                    Word word = sent.get_Word(i);
                    //части речи для экшна
                    string pos = word.get_Homonym(0).POSStr;
                    switch (pos)
                    {
                        case "Г": pretendents[i] = pretendents[i] * 8; break;
                        case "ПРИЧАСТИЕ": pretendents[i] = pretendents[i] * 7; break;
                        case "ДЕЕПРИЧАСТИЕ": pretendents[i] = pretendents[i] * 6; break;
                        case "ИНФИНИТИВ": pretendents[i] = pretendents[i] * 7; break;
                        case "КР_ПРИЧАСТИЕ": pretendents[i] = pretendents[i] * 6; break;
                        default: break;
                    }
                    //дополнительные признаки для экшна
                    string description = word.get_Homonym(0).GramDescriptionStr;
                    if (description.Contains("безл"))
                    {
                        pretendents[i] = pretendents[i] * 9;
                    } if (description.Contains("пвл"))
                    {
                        pretendents[i] = pretendents[i] * 8;
                    } if (description.Contains("пе") || description.Contains("нп"))
                    {
                        pretendents[i] = pretendents[i] * 5;
                    }
                }
            }
            double bestWeight = 0;
            foreach (KeyValuePair<int, double> pair in pretendents)
            {
                if (bestWeight < pair.Value)
                {
                    bestWeight = pair.Value;
                    bestWordNu = pair.Key;
                }
            }
            actionWeight = bestWeight;
            return bestWordNu;
        }       


        public double actorWeight = 0;
        /// <summary>
        /// метод, дающий прогноз для эктора
        /// </summary>
        /// <param name="sent"></param>
        /// <param name="cloNomber"></param>
        /// <param name="stat"></param>
        /// <returns></returns>
        public int getBestVariantForActor(ISentence sent, int cloNomber, StatisticsCollector stat,
            ElementaryProcess ep, List<WordRuleProbability> wrp)
        {            
            int actionIndex = -1;
            foreach (KeyValuePair<int, string> pair in stat.getActualMarkedWords() )
            {
                if (pair.Value == "Action")
                    actionIndex = pair.Key;
            }
            string actionDescription = "";
            if (actionIndex != -1)
                actionDescription = sent.get_Word(actionIndex).get_Homonym(0).GramDescriptionStr;
            if (actionDescription.Contains("безл"))
                return -1;

            int bestWordNu = -1;
            Dictionary<int, double> pretendents = new Dictionary<int, double>();
            for (int i = 0; i < sent.WordsNum; i++)
            {
                if ((sent.get_Word(i).ClauseNo == cloNomber) && (!stat.isMarkedWord(i)))
                {
                    pretendents.Add(i,1);
                    Word word = sent.get_Word(i);
                    //части речи для эктора
                    
                    string description = word.get_Homonym(0).GramDescriptionStr;
                    List<string> multiDescr = new List<string>();
                    multiDescr.Add("");
                    int multiCount = 0;
                    for (int j = 0; j < description.Length; j++)
                    {
                        if ((description[j] != ';'))
                            multiDescr[multiCount] += description[j];
                        else if(j<description.Length-2)
                        {
                            multiDescr.Add("");
                            multiCount++;
                        }

                    }
                    for (int j = 0; j < multiDescr.Count; j++)
                    {
                        if (
                            (actionIndex==-1)||(
                            (getFace(actionDescription) == getFace(multiDescr[j]))
                            && (getNumber(actionDescription) == getNumber(multiDescr[j])))
                        )
                        {

                            if (description.Contains("им"))
                            {
                                pretendents[i] = pretendents[i] * 9;
                            }
                            else continue;

                            string pos = word.get_Homonym(0).POSStr;
                            switch (pos)
                            {
                                case "С": pretendents[i] = pretendents[i] * 8; break;
                                case "МС": pretendents[i] = pretendents[i] * 8; break;
                                case "МС-ПРЕДК": pretendents[i] = pretendents[i] * 5; break;
                                case "МС-П": pretendents[i] = pretendents[i] * 5; break;
                                case "КР_ПРИЛ": pretendents[i] = pretendents[i] * 4; break;
                                case "ЧИСЛ": pretendents[i] = pretendents[i] * 2.5; break;
                                case "П": pretendents[i] = pretendents[i] * 2; break;
                                case "ЧИСЛ-П": pretendents[i] = pretendents[i] * 2; break;
                                default: break;
                            }
                            //дополнительные признаки для эктора
                            if (description.Contains("имя"))
                            {
                                pretendents[i] = pretendents[i] * 9;
                            }
                            if (description.Contains("фам"))
                            {
                                pretendents[i] = pretendents[i] * 8;
                            }
                            if (description.Contains("отч"))
                            {
                                pretendents[i] = pretendents[i] * 7;
                            }
                            if (description.Contains("орг"))
                            {
                                pretendents[i] = pretendents[i] * 8;
                            }
                            if (description.Contains("лок"))
                            {
                                pretendents[i] = pretendents[i] * 8;
                            }
                        }
                    }
                }
            }
            double bestWeight = 0;
            foreach (KeyValuePair<int, double> pair in pretendents)
            {
                if (bestWeight < pair.Value)
                {
                    bestWeight = pair.Value;
                    bestWordNu = pair.Key;
                }
            }
            actorWeight = bestWeight;
            return bestWordNu;
        }

        private string getNumber(string description)
        {
            if (description.Contains("ед"))
                return "ед";
            if (description.Contains("мн"))
                return "мн";
            return "";
        }

        private string getFace(string description)
        {
            if (description.Contains("1л"))
                return "1л";
            if (description.Contains("2л"))
                return "2л";
            if (description.Contains("3л"))
                return "3л";
            return "";
        }
    }
}
