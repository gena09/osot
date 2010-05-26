using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using LEMMATIZERLib;
using GRAPHANLib;
using AGRAMTABLib;
using MAPOSTLib;
using SEMANLib;
using Operation_Structures_of_Texts.Classes.Sci_fi;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors
{
    public class SyntaxProcessor : IWordProcessor
    {
        private GraphmatFile graphan;
        private ILemmatizer lemmatizer;
        private PLMLineCollection beforeSyntaxPlmLines = new PLMLineCollection();
        private PLMLineCollection afterMorphPlmLines = new PLMLineCollection();
        private IMAPost m_piMAPost;
        private string inString;

        public string text = "";
        public List<string> partOfSpeechs;
        public List<string> words;
        public SentencesCollection sentCollection;
        IGramTab gramTab;

        public SyntaxProcessor()
        {
            graphan = new GraphmatFile();
            graphan.Language = 1;
            graphan.LoadDicts();

            lemmatizer = new LemmatizerRussian();
            lemmatizer.LoadDictionariesRegistry();

            gramTab = new RusGramTab();
            gramTab.Load();

            beforeSyntaxPlmLines = new PLMLineCollection();
            afterMorphPlmLines = new PLMLineCollection();

            m_piMAPost = new MAPostClass();
            m_piMAPost.Init(1, lemmatizer, gramTab);

            //Load syntax module
            sentCollection = new SentencesCollection();
            sentCollection.SyntaxLanguage = 1;
            sentCollection.SetLemmatizer(lemmatizer);
            sentCollection.KillHomonymsMode = 1;

            sentCollection.InitializeProcesser();
            afterMorphPlmLines.AttachLemmatizer(lemmatizer);

        }

        public SyntaxProcessor(GraphmatFile gr, LemmatizerRussian lem)
        {
            /*
            graphan = new GraphmatFile();
            graphan.Language = 1;
            graphan.LoadDicts();

            lemmatizer = new LemmatizerRussian();
            lemmatizer.LoadDictionariesRegistry();*/
            graphan = gr;
            lemmatizer = lem;

            gramTab = new RusGramTab();
            gramTab.Load();

            beforeSyntaxPlmLines = new PLMLineCollection();
            afterMorphPlmLines = new PLMLineCollection();

            m_piMAPost = new MAPostClass();
            m_piMAPost.Init(1, lemmatizer, gramTab);

            //Load syntax module
            sentCollection = new SentencesCollection();
            sentCollection.SyntaxLanguage = 1;
            sentCollection.SetLemmatizer(lemmatizer);
            sentCollection.KillHomonymsMode = 1;

            sentCollection.InitializeProcesser();
            afterMorphPlmLines.AttachLemmatizer(lemmatizer);

        }

        #region Graphan Simulation
        public string[] outStrArray;

        public string[] workGraphan()
        {
            //graphan.LoadStringToGraphan(inString);
            string w;
            outStrArray = new string[graphan.GetLineCount()];
            for (uint i = 0; i < graphan.GetLineCount(); i++)
            {
                w = graphan.GetWord(i);
                outStrArray[i] = w;
            }
            return outStrArray;
        }

        #endregion

        #region Morphology Simulation

        public string[] outPartOfSpeechs;
        public IParadigmCollection piParadigmCollection;
        public double errProbAfter = 0;

        public string[] workLemmatizer()
        {
            string[] inWords = (string[]) outStrArray.Clone();
            outPartOfSpeechs = new string[inWords.Length];
            for (int i = 0; i < inWords.Length; i++)
            {
                piParadigmCollection = lemmatizer.CreateParadigmCollectionFromForm(inWords[i], 0, 0);
                if (piParadigmCollection.Count > 0) // Слова нет в словаре
                {
                    if (piParadigmCollection.Count == 1)
                    {
                        // Получаем нормальную форму слова
                        inWords[i] = piParadigmCollection[0].Norm;
                        //Вырезаем код который содержит часть речи
                        string OneAncode = piParadigmCollection[0].SrcAncode;
                        /*получаем часть речи */
                        outPartOfSpeechs[i] = gramTab.GetPartOfSpeechStr(gramTab.GetPartOfSpeech(OneAncode));
                    }
                    else
                    {
                        /*Несколько словоформ выберем ту что с наибольшим весом,
                        так как однозначно нельзя сказать что это за слово, 
                        просто с большей вероятностью берем чаще используемое слово с большим весом*/
                        int wordWeight = piParadigmCollection[0].WordWeight;
                        int curIdWordMax = 0;
                        for (int kk = 1; kk < piParadigmCollection.Count; kk++)
                        {
                            if (piParadigmCollection[kk].WordWeight > wordWeight)
                            {
                                wordWeight = piParadigmCollection[kk].WordWeight;
                                curIdWordMax = kk;
                            }
                        }
                        inWords[i] = piParadigmCollection[curIdWordMax].Norm;
                        string OneAncode = piParadigmCollection[0].SrcAncode.Substring(0, 2);

                        outPartOfSpeechs[i] = gramTab.GetPartOfSpeechStr(gramTab.GetPartOfSpeech(OneAncode));
                    }
                }
            }
            return outPartOfSpeechs;
        }

        public void checkErrorsOfMorfology(string[] partsBeforeSyntax, List<string> wordsAfter, List<string> posAfter)
        {
            double errorsNomber = 0;
            int indexAfter = 0;
            string[] inWords = (string[])outStrArray.Clone();
            Dictionary<string, string> bef = new Dictionary<string, string>();
            Dictionary<string, string> aft = new Dictionary<string, string>();
            for (int indexBefore = 1; indexBefore < partsBeforeSyntax.Length; indexBefore++)
            {
                if ((inWords[indexBefore] != "\n") && (inWords[indexBefore] != "\"") && (inWords[indexBefore] != " "))
                {
                    if ((partsBeforeSyntax[indexBefore] != posAfter[indexAfter]) &&
                        (posAfter[indexAfter] != null) &&
                        (partsBeforeSyntax[indexBefore] != null))
                    {
                        //bef.Add(inWords[indexBefore], partsBeforeSyntax[indexBefore]);
                        //aft.Add(wordsAfter[indexAfter], posAfter[indexAfter]);
                        errorsNomber++;
                    }
                    indexAfter++;
                }
            }
            errProbAfter = errorsNomber / (partsBeforeSyntax.Length - 1);
        }  
        #endregion        

        #region IWordProcessor Members

        public void setInitialData(Object data)
        {
            if (data is string)
                this.inString = (string)data;
            else throw (new Exception("Синтаксический процессор: Не подходящий тип входных данных"));
        }

        public void work()
        {
            sentCollection.ClearSentences();            
            graphan.LoadStringToGraphan(inString);
            afterMorphPlmLines.ProcessHyphenWords(graphan);
            afterMorphPlmLines.ProcessPlmLines(graphan);
            beforeSyntaxPlmLines = (PLMLineCollection)m_piMAPost.ProcessData(afterMorphPlmLines);
            afterMorphPlmLines.Clear();
            sentCollection.ProcessData(beforeSyntaxPlmLines);

            for (int i = 0; i < sentCollection.SentencesCount; i++)
            {
                ISentence sentence = sentCollection.get_Sentence(i);
                text += "Sent #" + Convert.ToString(i) + "\r\n";
                int[] bestVariantsNombers = getBestVariantsNombers(sentence);                
                IRelationsIterator relationsIterator = sentence.CreateRelationsIterator();
                for (int k = 0; k < sentence.ClausesCount; k++)
                {
                    IClause clo = sentence.get_Clause(k);
                    IClauseVariant cloVar = sentence.get_Clause(k).get_ClauseVariant(bestVariantsNombers[k]);
                    text += "  Clouse #" + Convert.ToString(k) + " " + clo.Description + "RelativeWord:" + clo.RelativeWord + "\r\n";
                    relationsIterator.Reset();
                    relationsIterator.AddClauseNoAndVariantNo(k, bestVariantsNombers[k]);
                    relationsIterator.BuildRelations();
                    for (int j = 0; j < relationsIterator.RelationsCount; j++)
                    {
                        IRelation rel = relationsIterator.get_Relation(j);
                        //relations.Add(rel);
                        text += rel.Name + ": " +
                            sentence.get_Word(rel.SourceItemNo).WordStr + "(" +Convert.ToString(rel.SourceItemNo)+")"
                            + "->" +
                            sentence.get_Word(rel.TargetItemNo).WordStr + "(" + Convert.ToString(rel.TargetItemNo) + ")"
                            + "\r\n";
                    }
                }
                text += "-----";
            }
        }

        public double getRealProbabylity()
        {
            throw new NotImplementedException();
        }

        public double getApriorProbabylity()
        {
            throw new NotImplementedException();
        }

        #endregion

        public List<string> getPartOfSpeechs()
        {
            partOfSpeechs = new List<string>();
            words = new List<string>();
            for (int i = 0; i < sentCollection.SentencesCount; i++)
            {
                ISentence sent = sentCollection.get_Sentence(i);
                for (int j = 0; j < sent.WordsNum; j++)
                {
                    //sent.GetOborotStrByOborotId(j);
                    partOfSpeechs.Add(sent.get_Word(j).get_Homonym(0).POSStr);
                    words.Add(sent.get_Word(j).WordStr);
                }
            }

            return partOfSpeechs;
        }

        public void getSentMembers()
        {
            Dictionary<int, string> sentMembers;
            for (int k = 0; k < sentCollection.SentencesCount; k++)
            {
                ISentence sentence = sentCollection.get_Sentence(k);
            }
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
        /// Выдаёт список отношений для всего предложения
        /// </summary>
        /// <param name="sentence">Предложение</param>
        /// <param name="bestVariantsNombers">варианты предложений</param>
        /// <returns>список отношений для всего предложения</returns>
        private List<IRelation> getRelationsList(ISentence sentence, int[] bestVariantsNombers)
        {
            List<IRelation> relations = new List<IRelation>();
            IRelationsIterator relationsIterator = sentence.CreateRelationsIterator();
            for (int i = 0; i < sentence.ClausesCount; i++)
            {
                relationsIterator.Reset();
                relationsIterator.AddClauseNoAndVariantNo(i, bestVariantsNombers[i]);
                relationsIterator.BuildRelations();
                for (int j = 0; j < relationsIterator.RelationsCount; j++)
                {
                    IRelation rel = relationsIterator.get_Relation(j);
                    relations.Add(rel);
                }
            }
            return relations;
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