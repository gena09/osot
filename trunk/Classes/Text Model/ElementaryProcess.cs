using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Text_Model
{
    public class ElementaryProcess : IOperationStructure
    {
        public string type;
        public string key;
        public string teg;

        public IOperationStructure actor;//t
        public IOperationStructure action;//g
        public IOperationStructure objectForAction; //d
        public IOperationStructure additionalObjectsForAction;//e
        public IOperationStructure charsOfAction;//w

        public string stringReprezentation = "";

        #region StatsAndAdditionInfo
        /// <summary>
        /// Элемент дерева клауз, породивший данный процесс
        /// </summary>
        public ClausesTree inTreeElement;
        /// <summary>
        /// "Пакет" статистики для данного процесса
        /// </summary>
        public StatPackage statsForThatProcess;

        public string statsString;

        #endregion

        public ElementaryProcess(ClausesTree inTreeElement)
        {
            actor = new LongOperationAttribut(this);
            action = new LongOperationAttribut(this);
            additionalObjectsForAction = new LongOperationAttribut(this);
            charsOfAction = new LongOperationAttribut(this);
            objectForAction = new LongOperationAttribut(this);
                        
            this.inTreeElement = inTreeElement;
        }

        public string getStatString()
        {
            statsString = "";
            //----------слова вне графа
            bool thereIsWordsOutOfGroups = false;
            if ((inTreeElement.wordsOutOfGroups != null) | (inTreeElement.wordsOutOfGroups.Count != 0))
            {                
                for (int i = 0; i < inTreeElement.wordsOutOfGroups.Count; i++)
                {
                    if (inTreeElement.wordsOutOfGroups[i].WordStr != ".")//дописать остальные знаки препинания
                        thereIsWordsOutOfGroups = true;
                }
            }
            if (thereIsWordsOutOfGroups)
                statsString += "@ + ";
            else
                statsString += "@ - ";
            //----------разрывы графа
            if (inTreeElement.parts.Count > 1)
                statsString += "@ + ";
            else
                statsString += "@ - ";
            //----------повисшие синтаксические группы
            bool notFull = false;
            bool hangingGroup = false;
            for(int i = 0; i < inTreeElement.rels.Count; i++)
            {
                if (statsForThatProcess.relationUsedInd.ContainsKey(i))
                {
                    //не полностью обработанные группы
                    if (statsForThatProcess.relationUsedInd[i] != SourceTargetEnum.Both)
                    {
                        notFull = true;
                    }
                }
                else
                {
                    //повисшая группа
                    hangingGroup = true;
                }
            }
            if (notFull)
                statsString += "@ + ";
            else
                statsString += "@ - ";

            if (hangingGroup)
                statsString += "@ + ";
            else
                statsString += "@ - ";
            
            
            return statsString;
        }

        public string getString()
        {
            stringReprezentation = "";
            stringReprezentation += "{";
            genStringFromAttr(actor);
            stringReprezentation += " | ";
            genStringFromAttr(action);
            stringReprezentation += " | ";
            genStringFromAttr(objectForAction);
            stringReprezentation += " | ";
            genStringFromAttr(charsOfAction);
            stringReprezentation += " | ";
            genStringFromAttr(additionalObjectsForAction);

            stringReprezentation += "}";
            getStatString();
            return stringReprezentation;
        }

        private void genStringFromAttr(IOperationStructure attr)
        {
            if (attr != null) 
                if ((attr as LongOperationAttribut).attributSequence.Count != 0)
                stringReprezentation += (attr as LongOperationAttribut).getString();
                else
                    stringReprezentation += " e ";
            else
                stringReprezentation += " e ";
        }
    }
}
