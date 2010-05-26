using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Sci_fi.Processors;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;
using Operation_Structures_of_Texts.Classes.Text_Model;
using Operation_Structures_of_Texts.Classes.Analysis;

namespace Operation_Structures_of_Texts.Classes
{
    public class Controller
    {
        GraphematicProcessor gp;
        MorphologyProcessor mp;
        SyntaxProcessor sp;
        SemanticProcessor semP;

        public List<ElementaryProcess> resultProcesses;

        public Controller(string someArgs)
        {
            //gp = new GraphematicProcessor();
            //mp = new MorphologyProcessor();
            //sp = new SyntaxProcessor(gp.graphan, mp.lemmatizer);
            sp = new SyntaxProcessor();
            semP = new SemanticProcessor();
        }

        public void genAllFromOneProcessor(string inText, Form1 form, bool morphology, bool syntax, bool semantics)
        {            
            sp = new SyntaxProcessor();
            sp.setInitialData(inText);
            sp.work();
            List<string> POSAfter = sp.getPartOfSpeechs();            
            string semText = "";

            if (semantics)
            {
                semP.setInitialData(sp.sentCollection);
                semP.work();
                semText = semP.stringRepresentation;
            }
            resultProcesses = semP.outStruct;
            form.setDataFromProcessors(sp.workGraphan(), sp.workLemmatizer(), POSAfter, sp.errProbAfter, sp.text, semText);
            //Skorin    
            form.proj = new Projection(sp, semP);

            //End Skorin

        }

        public void genAllFromDifferentProcessors(string inText, Form1 form, bool morphology, bool syntax, bool semantics)
        {
            gp.setInitialData(inText);
            gp.work();

            mp.setInitialData(gp.outStrArray);
            mp.work();

            sp = new SyntaxProcessor();//разобраться, почему его нельзя валидно инициализировать в конструкторе контроллера            
            sp.setInitialData(inText);
            sp.work();
            List<string> POSAfter = sp.getPartOfSpeechs();

            //mp.checkErrorsOfMorfology(mp.outPartOfSpeechs, sp.words,POSAfter );
            string semText = "";

            if (semantics)
            {
                semP.setInitialData(sp.sentCollection);
                semP.work();
                semText = semP.stringRepresentation;
            }

            form.setDataFromProcessors(gp.outStrArray, mp.outPartOfSpeechs, POSAfter, mp.errProbAfter, sp.text, semText);
        }
    }
}