using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Sci_fi.Processors;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;
using Operation_Structures_of_Texts.Classes.Text_Model;
using Operation_Structures_of_Texts.Classes.Analysis;
using Operation_Structures_of_Texts.Classes.Genesis;

namespace Operation_Structures_of_Texts.Classes
{
    public class Controller
    {
        Model genesisModel;

        #region SingletonRealization

        protected Controller()
        {
            genesisModel = new Model("");
        }

        private sealed class ControllerCreator
        {
            private static readonly Controller instance = new Controller();
            public static Controller Instance { get { return instance; } }
        }

        public static Controller Instance
        {
            get { return ControllerCreator.Instance; }
        }
        #endregion

        public void generateAllFromOneProcessor(string inText, MainForm form, bool morphology, bool postMorphology, bool syntax, bool semantics)
        {
            genesisModel.genareteOperationStructureFromPlainText(inText);
            //Skorin
            form.proj = new Projection(genesisModel.sp, genesisModel.semP);
            //End Skorin
        }
    }
}