using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Statistics
{
    public class StatisticsCollector
    {
        #region SingletonRealization
        protected StatisticsCollector() { }

        private sealed class StatisticsCollectorCreator
        {
            private static readonly StatisticsCollector instance = new StatisticsCollector();
            public static StatisticsCollector Instance { get { return instance; } }
        }

        public static StatisticsCollector Instance
        {
            get { return StatisticsCollectorCreator.Instance; }
        }
        #endregion

        #region Stats

        private List<StatPackage> stats;
        private int indexOfActualPackage;
        

        public void initNewPackage()
        {
            if (stats == null)
            {
                stats = new List<StatPackage>();
                indexOfActualPackage = 0;
                stats.Add(new StatPackage());
            }
            else 
            {
                stats.Add(new StatPackage());
                indexOfActualPackage++;
            }
        }

        public void addLog(string logString)
        {
            stats[indexOfActualPackage].log += logString + "\n\r";
        }

        public Dictionary<int, string> getActualMarkedWords()
        {
            return stats[indexOfActualPackage].markedWords;
        }

        public string getTypeOfMarked(int numberOfMarked)
        {
            return stats[indexOfActualPackage].markedWords[numberOfMarked];
        }

        public void markWord(int wordNomber, string atribbuteString, int relationNomberByClouse, SourceTargetEnum en)
        {
            stats[indexOfActualPackage].markedWords.Add(wordNomber, atribbuteString);
            addRelationPart(relationNomberByClouse, en);
        }

        public bool isMarkedWord(int wordNomber)
        {
            return stats[indexOfActualPackage].markedWords.ContainsKey(wordNomber);
        }
        
        private void addRelationPart(int relationNomberByClouse, SourceTargetEnum en)
        {
            if (!stats[indexOfActualPackage].relationUsedInd.ContainsKey(relationNomberByClouse))
            {
                stats[indexOfActualPackage].relationUsedInd.Add(relationNomberByClouse, en);
            }
            else
            {
                if ((stats[indexOfActualPackage].relationUsedInd[relationNomberByClouse] != SourceTargetEnum.Both))
                {
                    if (stats[indexOfActualPackage].relationUsedInd[relationNomberByClouse] != en)
                    {
                        stats[indexOfActualPackage].relationUsedInd[relationNomberByClouse] = SourceTargetEnum.Both;
                    }
                }
            }
        }

        public Dictionary<int, int> getActualWordRelationsDict()
        {
            return stats[indexOfActualPackage].relWordMarkedBy;
        }

        public void clearAll()
        {
            stats[indexOfActualPackage].markedWords = new Dictionary<int, string>();
            stats[indexOfActualPackage].relationUsedInd = new Dictionary<int, SourceTargetEnum>();
            stats[indexOfActualPackage].relWordMarkedBy = new Dictionary<int, int>();
        }

        public StatPackage getActualPackage()
        {
            return stats[indexOfActualPackage];
        }

        #endregion
    }
}