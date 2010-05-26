using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;

namespace Operation_Structures_of_Texts.Classes.Text_Model
{
    /// <summary>
    /// Сделан для хранения последовательности операторных атрибутов, объединённых операциями AND, OR, XOR, NXOR, UNION и т.д.
    /// </summary>
    public class LongOperationAttribut: IOperationStructure
    {
        private ElementaryProcess parentEp;

        /// <summary>
        /// последовательность простых атрибутов, скобок или элементарных процессов составляющих данный атрибут
        /// </summary>
        public List<IOperationStructure> attributSequence;

        /// <summary>
        /// операторы последовательности (первый оператор связывает первый и второй элементы последовательности, второй - связывает второй и третий элементы и т.д.)
        /// </summary>
        public List<string> operators;

        /// <summary>
        /// список атрибутов в процесс исключённых из последовательности для их уточнения. Храняться здесь в базовом виде
        /// </summary>
        public List<IOperationStructure> exqludedAttributsSequence = new List<IOperationStructure>();

        public LongOperationAttribut(ElementaryProcess parentEp)
        {
            attributSequence = new List<IOperationStructure>();
            operators = new List<string>();
            this.parentEp = parentEp;
        }
        
        public void addElementaryAttribut(string word, string notOperation, IWord inWord)
        {
            ElementaryAttribut attribut = new ElementaryAttribut(notOperation, word, inWord);
            attributSequence.Add(attribut);
            parentEp.getString();
        }

        public void addElementaryAttribut(string word, string notOperation, string operetionBefore, IWord inWord)
        {
            ElementaryAttribut attribut = new ElementaryAttribut(notOperation, word, inWord);
            attributSequence.Add(attribut);
            operators.Add(operetionBefore);
            parentEp.getString();
        }
        /// <summary>
        /// Добавление атрибута с предпроверкой добавлять ли его в последовательность атрибутов или же уточнять брэкет
        /// </summary>
        /// <param name="word"></param>
        /// <param name="notOperation"></param>
        /// <param name="operetionBefore"></param>
        /// <param name="wordNomber"></param>
        /// <param name="sent"></param>
        /// <param name="attr"></param>
      
        public void addElementaryAttribut(string word, string notOperation, IWord inWord, string operetionBefore, int saverWordNomber, ISentence sent, LongOperationAttribut attr)
        {
            int brNo = isBrecket(saverWordNomber, sent, attr);
            ElementaryAttribut attribut = new ElementaryAttribut(notOperation, word, inWord);
            if (brNo!= -1)
            {
                //уточнить брэкет
                (attr.attributSequence[brNo] as Brackets).addUnionToAttr2(attribut, operetionBefore);
            }
            else
            {
                //просто добавляем новый элемент в последовательность
                attributSequence.Add(attribut);
                operators.Add(operetionBefore);
                parentEp.getString();
            }
        }

        private int isBrecket(int wordNomber, ISentence sent, LongOperationAttribut attr)
        {
            string word = sent.get_Word(wordNomber).WordStr;
            for (int i = 0; i < attr.attributSequence.Count; i++)
            {
                if (attr.attributSequence[i] is Brackets)
                {
                    Brackets br = attr.attributSequence[i] as Brackets;
                    if ((br.atribut1.word == word) || (br.atribut2.word == word))
                        return i;
                    for (int j = 0; j < br.unionsForAttr2.Count; j++)
                    {
                        if ((br.unionsForAttr2[j] as ElementaryAttribut).word == word)
                            return i;
                    }
                }
            }
            return -1;
        }

        public void addElementaryAttribut(ElementaryAttribut attribut)
        { 
            attributSequence.Add(attribut);
            parentEp.getString();
        }

        public void addBreaksOfAttributs(ElementaryAttribut attribut1, ElementaryAttribut attribut2, string operationBetween)
        {
            Brackets br = new Brackets(attribut1, attribut2, operationBetween);
            attributSequence.Add(br);
            parentEp.getString();
        }

        public void addElementaryProcess(IOperationStructure process)
        {
            attributSequence.Add(process);
            parentEp.getString();
        }

        public ElementaryAttribut exqludeElementaryAtt(string word)
        {
            for (int i = 0; i < attributSequence.Count; i++)
            {
                try{
                if((attributSequence[i] as ElementaryAttribut).word == word)
                {
                    ElementaryAttribut ret = (attributSequence[i] as ElementaryAttribut);
                    exqludedAttributsSequence.Add(attributSequence[i]);
                    attributSequence.RemoveAt(i);
                    return ret;
                }
                }catch(Exception e){}
            }
            return null;
        }

        public bool changeEAttrToBracket(string wordToChange, string newAttrNotString, string newAttrWord, string operationBetween, IWord inWord)
        {
            for (int i = 0; i < attributSequence.Count; i++)
            {
                try
                {
                    if ((attributSequence[i] as ElementaryAttribut).word == wordToChange)
                    {
                        exqludedAttributsSequence.Add(attributSequence[i]);
                        attributSequence[i] = new Brackets(attributSequence[i] as ElementaryAttribut, new ElementaryAttribut(newAttrNotString, newAttrWord, inWord), operationBetween);                        
                        return true;
                    }
                }
                catch (Exception e) { return false; }
            }
            return false;
        }

        public ElementaryAttribut tryToGetMeBaseAttr(string word)
        {
            for (int i = 0; i < attributSequence.Count; i++)
            {
                try
                {
                    if ((attributSequence[i] as ElementaryAttribut).word == word)
                    {
                        return (attributSequence[i] as ElementaryAttribut);
                    }
                }
                catch (Exception e) { }
            }
            for (int i = 0; i < exqludedAttributsSequence.Count; i++)
            {
                try
                {
                    if ((exqludedAttributsSequence[i] as ElementaryAttribut).word == word)
                    {
                        return (exqludedAttributsSequence[i]as ElementaryAttribut);
                    }
                }
                catch (Exception e) { }
            }
            return null;
        }

        public string getString()
        {
            string reprez = "";
            for (int i = 0; i < attributSequence.Count; i++)
            {
                reprez += attributSequence[i].getString();
                try
                {
                    reprez += " " + operators[i] + " ";
                }
                catch (Exception ex) { }
            }
            return reprez;
        }
    }
}
