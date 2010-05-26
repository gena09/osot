using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Statistics
{
    public class StatPackage
    {
        /// <summary>
        /// Статистика использования слов в операторной структуре
        /// </summary>
        public Dictionary<int, string> markedWords = new Dictionary<int, string>();
        /// <summary>
        /// Статистика использования отношений в операторной структуре
        /// </summary>
        public Dictionary<int, SourceTargetEnum> relationUsedInd = new Dictionary<int, SourceTargetEnum>();
        /// <summary>
        /// Словарь слово - отношение, от которого данное слово унаследовало оператор
        /// </summary>
        public Dictionary<int, int> relWordMarkedBy = new Dictionary<int, int>();
        /// <summary>
        /// Лог ошибок и предупреждений процесса построения данной операторной структуры
        /// </summary>
        public string log = "";
    }
}
