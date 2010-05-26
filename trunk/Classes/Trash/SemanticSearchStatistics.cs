using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics
{
    public class SemanticSearchStatistics
    {
        public Dictionary<int, string> markedWords = new Dictionary<int, string>();
        private Dictionary<int, string> usedWords = new Dictionary<int, string>();
        private Dictionary<int, SourceTargetEnum> relationUsedInd = new Dictionary<int, SourceTargetEnum>();//номер отношения, 0- отношение не задействовано/1-истьочник задействован/2-приёмник задействован/3-задействованы оба
        public Dictionary<int, int> relWordMarkedBy = new Dictionary<int, int>();

        public void markWord(int wordNomber, string atribbuteString,int relationNomberByClouse, SourceTargetEnum en)
        {
            markedWords.Add(wordNomber, atribbuteString);
            addRelationPart(relationNomberByClouse, en);
        }

        public bool isMarkedWord(int wordNomber)
        {
            return markedWords.ContainsKey(wordNomber);
        }

        public void addUsed(int wordNomber, string usedAsDiscriptor, int relationNomberByClouse, SourceTargetEnum en)
        {
            usedWords.Add(wordNomber, usedAsDiscriptor);
            relationUsedInd.Add(relationNomberByClouse, en);
        }

        private void addRelationPart(int relationNomberByClouse, SourceTargetEnum en)
        {
            if (!relationUsedInd.ContainsKey(relationNomberByClouse))
            {
                relationUsedInd.Add(relationNomberByClouse, en);
            }
            else
            {
                if ((relationUsedInd[relationNomberByClouse] != SourceTargetEnum.Both))
                {
                    if (relationUsedInd[relationNomberByClouse] != en)
                    {
                        relationUsedInd[relationNomberByClouse] = SourceTargetEnum.Both;
                    }
                }
            }
        }
    }
}
