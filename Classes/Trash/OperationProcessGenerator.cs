using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Text_Model;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors
{
    public class OperationProcessGenerator
    {
        public List<ElementaryProcess> genFromSent(Sentence sentence)
        {
            int[] bestVariantsNombers = getBestVariantsNombers(sentence);            
            Dictionary<int, List<IRelation>> rDict = getRelationsDict(sentence, bestVariantsNombers);             
            List<ClausesTree> tree = genTree(sentence, bestVariantsNombers, rDict);//с деревом всё ещё могут быть проблемы
            List<ElementaryProcess> os = naivGenOSFromClousesTree(tree, sentence);
            return os;
        }

        public List<ElementaryProcess> naivGenOSFromClousesTree(List<ClausesTree> tree, ISentence sent)
        {
            List<ElementaryProcess> textAsProcess = new List<ElementaryProcess>();            
            for (int i = 0; i < tree.Count; i++)
            {
                ElementaryProcess ep = new ElementaryProcess(tree[i]);                
                SemanticSearchWithProbabilytis search = new SemanticSearchWithProbabilytis(tree[i], sent,i);
                ep = search.optimazatorsSearch() as ElementaryProcess;
                textAsProcess.Add(ep);
            }
            return textAsProcess;
        }
        
        private List<ClausesTree> genTree(ISentence sentence, int[] bestVariantsNombers, Dictionary<int, List<IRelation>> rDict)
        {            
            List<ClausesTree> treeRoots = new List<ClausesTree>();            
            //находим корни синтаксического дерева
            for (int i = 0; i < sentence.ClausesCount; i++)
            {
                Clause clo = sentence.get_Clause(i);
                if (clo.RelativeWord == -1)//не всегда это признак корня синтаксического дерева!!!
                {
                    treeRoots.Add(new ClausesTree(i, 0, rDict[i],sentence));
                }
            }
            //к корням присоединяем остальные клаузы
            List<int> roorToDel = new List<int>();
            for (int i = 0; i < treeRoots.Count; i++)
                treeRoots[i].backToTheRoots(treeRoots[i], sentence, rDict, true, true, roorToDel);
            for (int i = 0; i < treeRoots.Count; i++)
                if (roorToDel.Contains(treeRoots[i].cloNumber)) treeRoots.RemoveAt(i);
            return treeRoots;
        }

        /// <summary>
        /// Строит массив в котором содержатся номера лучших вариантов 
        /// клауз данного предложения (что считается лучшим - спросить у Сокирко)
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private int[] getBestVariantsNombers(ISentence sentence)
        {
            int[] bestVariantsNombers = new int[sentence.ClausesCount];
            for (int i = 0; i < sentence.ClausesCount; i++)
            {
                IClause clo = sentence.get_Clause(i);
                int bestWeight = 0;//Int32.MaxValue;//0;//!!!
                for (int j = 0; j < clo.VariantsCount; j++)
                {
                    ClauseVariant cloVar = clo.get_ClauseVariant(j);
                    if (bestWeight < cloVar.VariantWeight)
                    {
                        bestWeight = cloVar.VariantWeight;
                        bestVariantsNombers[i] = j;
                    }
                }
            }
            return bestVariantsNombers;
        }        

        /// <summary>
        /// Выдаёт словарь клауз и их отношений для всего предложения
        /// </summary>
        /// <param name="sentence">Предложение</param>
        /// <param name="bestVariantsNombers">варианты предложений</param>
        /// <returns>список отношений для всего предложения</returns>
        private Dictionary<int, List<IRelation>> getRelationsDict(ISentence sentence, int[] bestVariantsNombers)
        {
            Dictionary<int, List<IRelation>> relations = new Dictionary<int, List<IRelation>>();
            IRelationsIterator relationsIterator = sentence.CreateRelationsIterator();
            for (int i = 0; i < sentence.ClausesCount; i++)
            {
                relationsIterator.Reset();
                relationsIterator.AddClauseNoAndVariantNo(i, bestVariantsNombers[i]);
                relationsIterator.BuildRelations();
                relations.Add(i, new List<IRelation>());
                for (int j = 0; j < relationsIterator.RelationsCount; j++)
                {
                    IRelation rel = relationsIterator.get_Relation(j);
                    relations[i].Add(rel);
                }
            }
            return relations;
        }

    }
}