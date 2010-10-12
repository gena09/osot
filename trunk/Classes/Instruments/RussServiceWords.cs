using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operation_Structures_of_Texts.Classes.Instruments
{
    public class RussServiceWords
    {
        /// <summary>
        /// служебные слова подчинённых! предложений
        /// </summary>
        public List<string> serviceWords = new List<string>(){ "ПОСКОЛЬКУ", "ТАК КАК"};

        public bool isContainsServiceWord(string word)
        {
            for (int i = 0; i < serviceWords.Count; i++)
            {
                if (word.Contains(serviceWords[i])) return true;
            }
            return false;
        }


    }
}
