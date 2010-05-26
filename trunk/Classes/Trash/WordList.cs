using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using GRAPHANLib;
using LEMMATIZERLib;
using MAPOSTLib;
using AGRAMTABLib;

namespace Operation_Structures_of_Texts.Classes.Instruments
{
    class WordList
    {
        public List<string> words;

        public WordList(string text)
        {
            GraphmatFile graphan = new GraphmatFile();
            graphan.Language = 1;
            graphan.LoadDicts();

            ILemmatizer lemmatizer = new LemmatizerRussian();
            lemmatizer.LoadDictionariesRegistry();

            IGramTab gramTab = new RusGramTab();
            gramTab.Load();

            PLMLineCollection beforeSyntaxPlmLines = new PLMLineCollection();
            PLMLineCollection afterMorphPlmLines = new PLMLineCollection();

            IMAPost m_piMAPost = new MAPostClass();
            m_piMAPost.Init(1, lemmatizer, gramTab);

            //Load syntax module
            SentencesCollection sentCollection = new SentencesCollection();

            sentCollection.SyntaxLanguage = 1;
            sentCollection.SetLemmatizer(lemmatizer);
            sentCollection.KillHomonymsMode = 1;
            sentCollection.InitializeProcesser();
            //конец инициализации           

            afterMorphPlmLines.AttachLemmatizer(lemmatizer);
            
            sentCollection.ClearSentences();
            graphan.LoadStringToGraphan(text);
            uint c = graphan.GetLineCount();
            words = new List<string>();
            
            for (uint i = 0; i < c; i++)
                words.Add(graphan.GetWord(i));
            //int j = 0;
        }
    }
}