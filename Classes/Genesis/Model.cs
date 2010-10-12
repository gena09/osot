using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Sci_fi.Processors;
using Operation_Structures_of_Texts.Classes.Text_Model;

namespace Operation_Structures_of_Texts.Classes.Genesis
{
    public class Model
    {
        public GraphematicProcessor gp;
        public MorphologyProcessor mp;
        public SyntaxProcessor sp;
        public SemanticProcessor semP;

        public List<ElementaryProcess> resultProcesses;

        public Model(string someArgs)
        {
            gp = GraphematicProcessor.Instance;
            mp = MorphologyProcessor.Instance;
            sp = SyntaxProcessor.Instance;
            semP = new SemanticProcessor();
        }
        /// <summary>
        /// Генерирует операторную структуру 
        /// </summary>
        /// <param name="inText">Текст для перевода в операторную структуру</param>
        public void genareteOperationStructureFromPlainText(string inText)
        {
            gp.setInitialData(inText);
            gp.work();
            mp.setInitialData(gp.outStrArray);
            mp.work();
            sp.setInitialData(inText);
            sp.work();
            semP.setInitialData(sp.sentCollection);
            semP.work();

            //TODO: отнести к отображению
            List<string> POSAfter = sp.getPartOfSpeechs();
            string semText = "";
            semText = semP.stringRepresentation;
            resultProcesses = semP.outStruct;
            mp.checkErrorsOfMorfology(mp.outPartOfSpeechs, gp.outStrArray, sp.words, POSAfter);
            //form.setDataFromProcessors(gp.outStrArray, mp.outPartOfSpeechs, POSAfter, mp.errProbAfter, sp.text, semText);

        }
    }
}