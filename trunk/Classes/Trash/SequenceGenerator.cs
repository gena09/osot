using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operation_Structures_of_Texts.Classes.Instruments
{
    class SequenceGenerator
    {
        public string generate(int textLength, int sentenceLength, double dispersion,
            Dictionary<string, int> periods)
        {
            string result = "";
            int actSentLength = sentenceLength;

            Dictionary<string, int> actPeriods = new Dictionary<string, int>(periods); ;
            for (int i = 0; i < textLength; i++)
            {
                bool set = false;
                {
                    for (int j = 0; j < actPeriods.Count; j++)
                    {
                        if (actPeriods[actPeriods.Keys.ToList()[j]] == 0)
                        {
                            result += actPeriods.Keys.ToList()[j] + " ";
                            set = true;
                            actPeriods[actPeriods.Keys.ToList()[j]] = periods[actPeriods.Keys.ToList()[j]] + 1;
                        }
                        actPeriods[actPeriods.Keys.ToList()[j]]--;
                    }
                }
                if (!set)
                {
                    result += "пробел ";
                }
                if (actSentLength == 0) { result += "."; actSentLength = sentenceLength; }
                actSentLength--;
            }
            return result;

        }
    }
}
