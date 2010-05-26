using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Text_Model;
using SYNANLib;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules
{
    public static class AuxularyMethods
    {
        public static string getOperatorByRelationIndex(int relationIndex, ClausesTree clausesTree, ISentence sent)
        {
            if (clausesTree.rels[relationIndex].Name == "ОТСОЮЗ")
            {
                if (sent.get_Word(clausesTree.rels[relationIndex].SourceItemNo).WordStr ==
                    sent.get_Word(clausesTree.rels[relationIndex].TargetItemNo).WordStr)
                {
                    switch (sent.get_Word(clausesTree.rels[relationIndex].SourceItemNo).WordStr.ToUpper())
                    {
                        case "И": return "AND";
                            break;
                        case "ИЛИ": return "XOR";
                            break;
                        case "ЛИБО": return "XOR";
                            break;
                        case "НИ": return "NXOR";
                            break;
                        default: throw new Exception("Не известный союз в operatorByRelationIndex()");
                    }
                }
                else throw new Exception("В отношении ОТСОЮЗ союзы бывают разного типа (прецендентная информация)");
            }
            if (clausesTree.rels[relationIndex].Name == "РАЗРЫВ_СОЮЗ")
            {
                switch (sent.get_Word(clausesTree.rels[relationIndex].SourceItemNo).WordStr.ToUpper())
                {
                    case "И": return "AND";
                        break;
                    case "ИЛИ": return "XOR";
                        break;
                    case "ЛИБО": return "XOR";
                        break;
                    case "НИ": return "NXOR";
                        break;
                    case "КАК": return "AND";
                        break;
                    default: throw new Exception("Не известный союз в operatorByRelationIndex()");
                }
            }
            if (clausesTree.rels[relationIndex].Name == "ОДНОР_ИНФ")
            {
                switch (sent.get_Word(clausesTree.rels[relationIndex].SourceItemNo).WordStr.ToUpper())
                {
                    case "И": return "AND";
                        break;
                    case "ИЛИ": return "XOR";
                        break;
                    case "ЛИБО": return "XOR";
                        break;
                    case "НИ": return "NXOR";
                        break;
                    case "КАК": return "AND";
                        break;
                    default: throw new Exception("Не известный союз в operatorByRelationIndex()");
                }
            }
            throw new Exception("Неверное использование функции operatorByRelationIndex()");
            return "Error";
        }

        public static IOperationStructure fillNOT(IOperationStructure attr, int relationIndex, ClausesTree clausesTree, ISentence sent)
        {
            LongOperationAttribut target = attr as LongOperationAttribut;
            for (int j = 0; j < target.attributSequence.Count; j++)
            {
                if (target.attributSequence[j] is ElementaryAttribut)
                {
                    if ((target.attributSequence[j] as ElementaryAttribut).word ==
                        sent.get_Word(clausesTree.rels[relationIndex].SourceItemNo).WordStr)
                        (target.attributSequence[j] as ElementaryAttribut).operation = "NOT";
                }
                if (target.attributSequence[j] is Brackets) { }
            }
            return target;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static IOperationStructure fillUnion(IOperationStructure attr, int i, ClausesTree clausesTree, ISentence sent)
        {
            LongOperationAttribut target = attr as LongOperationAttribut;
            if (weNeedNewElementaryProcess(i ,clausesTree, sent))
            {
                //выбрать чаилда и сгенерить из него процесс
                //SemanticSearch s = new SemanticSearch(chooseMeChild(i,clausesTree, sent), sent);
                //target.addElementaryProcess(s.naivSearch());
                SemanticSearchWithProbabilytis s = new SemanticSearchWithProbabilytis(chooseMeChild(i, clausesTree, sent), sent);
                target.addElementaryProcess(s.optimazatorsSearch());
                target.operators.Add("UNION");
            }
            else
            {
                target.addElementaryAttribut(sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr, "", sent.get_Word(clausesTree.rels[i].TargetItemNo));
                target.operators.Add("UNION");
            }
            return target;
        }

        public static bool weNeedNewElementaryProcess(int relNomber, ClausesTree clausesTree, ISentence sent)
        {
            if (clausesTree.rels[relNomber].SourceClauseNo != clausesTree.rels[relNomber].TargetClauseNo) return true;
            return false;
        }

        public static ClausesTree chooseMeChild(int relIndex, ClausesTree clausesTree, ISentence sent)
        {
            int s = clausesTree.rels[relIndex].SourceClauseNo;
            int t = clausesTree.rels[relIndex].TargetClauseNo;
            if (s == clausesTree.cloNumber)
                for (int i = 0; i < clausesTree.childrens.Count; i++)
                    if (clausesTree.childrens[i].cloNumber == t) return clausesTree.childrens[i];

            if (t == clausesTree.cloNumber)
                for (int i = 0; i < clausesTree.childrens.Count; i++)
                    if (clausesTree.childrens[i].cloNumber == s) return clausesTree.childrens[i];

            throw new Exception("Не найден дочерний элемент синтаксического дерева предложения, для заданного отношения. Ошибка синтаксического дерева либо неверное отношение.");
            return null;
        }        
    }
}
