using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics
{
    /// <summary>
    /// Элемент дерева клауз
    /// </summary>
    public class ClausesTree
    {
        /// <summary>
        /// Номер клаузы в ISentence, из которого был сформирован этот элемент дерева
        /// </summary>
        public int cloNumber;
        /// <summary>
        /// Уровень вложенности данного элемента в структуру предложения
        /// </summary>
        public int level;
        /// <summary>
        /// Список отношений данного элемента дерева
        /// </summary>
        public List<IRelation> rels;
        /// <summary>
        /// номер клаузы в ISentence, которая является родителем (подчиняющая часть предложения) данного элемента
        /// </summary>
        public int parent;
        /// <summary>
        /// список подчинённых клауз данной клаузе
        /// </summary>
        public List<ClausesTree> childrens;
        /// <summary>
        /// Номер отношения, по которому подчинено данная клауза
        /// </summary>
        public int bridgeRelationNumber;

        public ISentence sentance;
        public List<Word> wordsOutOfGroups;        
        Dictionary<int, List<IRelation>> parts = new Dictionary<int, List<IRelation>>();

        public ClausesTree(int cloNomber, int level, List<IRelation> rels, ISentence sentance)
        {
            this.cloNumber = cloNomber;
            if (level == 0) parent = -1;
            this.level = level;
            childrens = new List<ClausesTree>();
            this.rels = rels;
            wordsOutOfGroups = new List<Word>();
            this.sentance = sentance;
            checkWordsOutOfGroups();
        }

        public ClausesTree(int cloNomber, int level, List<IRelation> rels, int bridgRelationNo, ISentence sentance)
        {
            this.cloNumber = cloNomber;
            if (level == 0) parent = -1;
            this.level = level;
            childrens = new List<ClausesTree>();
            this.rels = rels;
            this.bridgeRelationNumber = bridgRelationNo;
            wordsOutOfGroups = new List<Word>();
            this.sentance = sentance;
            checkWordsOutOfGroups();
        }

        public ClausesTree()
        {
            wordsOutOfGroups = new List<Word>();
        }

        /// <summary>
        /// Поиск потомков данной клаузы
        /// </summary>
        /// <param name="dot"></param>
        /// <param name="sent"></param>
        /// <param name="relDict"></param>
        /// <param name="downConnect"></param>
        /// <param name="upConnect"></param>
        /// <param name="rootToDel"></param>
        public void backToTheRoots(ClausesTree dot, ISentence sent, Dictionary<int,
            List<IRelation>> relDict, bool downConnect, bool upConnect, List<int> rootToDel)
        {
            //присоединение предложения через relation у child'а (присоединение "вниз")
            for (int i = 0; i < sent.ClausesCount; i++)
            {
                if ((downConnect) && (i != dot.parent))
                {
                    int brig = clauseIsChildrenOfDot(i, dot, sent, relDict);
                    if (brig != -1)
                    {
                        rootToDel.Add(i);
                        ClausesTree newDot = new ClausesTree(i, dot.level + 1, relDict[i], brig, sentance);
                        dot.childrens.Add(newDot);
                        backToTheRoots(newDot, sent, relDict, true, false, rootToDel);
                    }
                }
            }
            if (upConnect)
            {
                //присоединение предложения через relation у dot'a
                for (int i = 0; i < relDict[dot.cloNumber].Count; i++)
                {
                    if (relDict[dot.cloNumber][i].SourceClauseNo != dot.cloNumber)
                    {
                        ClausesTree newDot = new ClausesTree(relDict[dot.cloNumber][i].SourceClauseNo, dot.level + 1,
                            relDict[relDict[dot.cloNumber][i].SourceClauseNo], sentance);
                        rootToDel.Add(relDict[dot.cloNumber][i].SourceClauseNo);
                        newDot.parent = dot.cloNumber;
                        dot.childrens.Add(newDot);
                        backToTheRoots(newDot, sent, relDict, true, false, rootToDel);
                    }
                    if (relDict[dot.cloNumber][i].TargetClauseNo != dot.cloNumber)
                    {
                        ClausesTree newDot = new ClausesTree(relDict[dot.cloNumber][i].TargetClauseNo, dot.level + 1,
                            relDict[relDict[dot.cloNumber][i].TargetClauseNo], sentance);
                        rootToDel.Add(relDict[dot.cloNumber][i].TargetClauseNo);
                        newDot.parent = dot.cloNumber;
                        dot.childrens.Add(newDot);
                        backToTheRoots(newDot, sent, relDict, true, false, rootToDel);
                    }
                }
            }
        }

        private int clauseIsChildrenOfDot(int i, ClausesTree dot, ISentence sent, Dictionary<int, List<IRelation>> relDict)
        {
            IClause clo = sent.get_Clause(i);
            //if ((clo.RelativeWord == -1) || (i == dot.cloNomber)) return -1;
            if ((i == dot.cloNumber)) return -1;
            else
            {
                for (int n = 0; n < relDict[i].Count; n++)
                {
                    if ((relDict[i][n].SourceClauseNo == dot.cloNumber)
                        //|| (relDict[i][n].TargetClauseNo == dot.cloNomber)
                        )
                    {
                        return n;
                    }
                }
            }
            return -1;
        }

        public void checkWordsOutOfGroups()
        {
            for (int i = 0; i < sentance.WordsNum; i++)
            {
                string s = sentance.get_Word(i).WordStr;
                if (sentance.get_Word(i).ClauseNo == cloNumber)
                {
                    bool isInGroup = false;
                    for (int j = 0; j < rels.Count; j++)
                    {
                        if ((i == rels[j].SourceItemNo) || (i == rels[j].TargetItemNo))
                        {
                            isInGroup = true;
                            break;
                        }
                    }
                    if (!isInGroup)
                    {
                        wordsOutOfGroups.Add(sentance.get_Word(i));
                    }
                }
            }
        }        

        public void checkBreaks()
        {
            List<int> relNombersToCheck = new List<int>(rels.Count);
            for (int i = 0; i < rels.Count; i++)
            {
                relNombersToCheck.Add(i);
                parts.Add(i,new List<IRelation>());
                parts[i].Add(rels[i]);
            }
            int maxCount = rels.Count;
            bool goOn = false;
            do
            {
                goOn = false;
                for (int i = 0; i < maxCount; i++)
                {
                    if(parts.ContainsKey(i))
                    for (int j = 0; j < maxCount; j++)
                    {
                        if (parts.ContainsKey(j))
                        if (i != j)
                            goOn = goOn || checkParts(parts[i], i, parts[j], j, parts);                            
                    }
                }
            } while (goOn);
        }

        

        private bool checkParts(List<IRelation> part1, int p1No, List<IRelation> part2, int p2No,
            Dictionary<int, List<IRelation>> parts)
        {
            bool goOn = false;
            for (int i = 0; i < part1.Count; i++)
            {
                for (int j = 0; j < part2.Count; j++)
                {                   
                    if ((part1[i].SourceItemNo == part2[j].SourceItemNo) ||
                        (part1[i].TargetItemNo == part2[j].TargetItemNo) ||
                        (part1[i].SourceItemNo == part2[j].TargetItemNo) ||
                        (part1[i].TargetItemNo == part2[j].SourceItemNo))
                    {
                        for (int k = 0; k < part2.Count; k++ )
                            part1.Add(part2[k]);
                        parts.Remove(p2No);
                        return true;
                    }
                }
            }
            return goOn;
        }
    }
}
