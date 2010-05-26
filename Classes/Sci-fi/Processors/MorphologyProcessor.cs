using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using LEMMATIZERLib;
using GRAPHANLib;
using AGRAMTABLib;
using MAPOSTLib;
using Operation_Structures_of_Texts.Classes.Sci_fi;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors
{
    public class MorphologyProcessor : IWordProcessor
    {
        public LemmatizerRussian lemmatizer;
        private RusGramTab rusgram;
        private string[] inWords;
        public string[] outPartOfSpeechs;
        public IParadigmCollection piParadigmCollection;
        public double errProbAfter;
        
        public MorphologyProcessor()
        {
            lemmatizer = new LemmatizerRussianClass();
            rusgram = new RusGramTabClass();
            lemmatizer.LoadDictionariesRegistry();
            rusgram.Load();
        }

        #region IWordProcessor Members


        public void setInitialData(Object inWords)
        {
            if (inWords is string)
                this.inWords = (string[])inWords;
            else throw (new Exception("Морфлогический процессор: Не подходящий тип входных данных"));
        }
        
        /// <summary>
        /// сама функция для работы с текстом, curPage
        /// </summary>
        /// <param name="curPage">массив слов</param>
        /// <returns></returns>
        public void work()
        {
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
                        outPartOfSpeechs[i] = rusgram.GetPartOfSpeechStr(rusgram.GetPartOfSpeech(OneAncode));                       
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
                        
                        outPartOfSpeechs[i] = rusgram.GetPartOfSpeechStr(rusgram.GetPartOfSpeech(OneAncode));                        
                    }
                }
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

        internal void checkErrorsOfMorfology(string[] partsBeforeSyntax, List<string> wordsAfter, List<string> posAfter)
        {
            double errorsNomber = 0;
            int indexAfter = 0;
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
    }
}
