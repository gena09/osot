using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Sci_fi.Processors;
using Operation_Structures_of_Texts.Classes.Analysis.Structurs;

namespace Operation_Structures_of_Texts.Classes.Analysis
{
    public class Projection
    {
        SyntaxProcessor sym;
        SemanticProcessor sem;
        public Projection(SyntaxProcessor sy, SemanticProcessor se)
        {
            sym = sy;
            sem = se;
        }

        public Projection()
        {
            sym = new SyntaxProcessor();
            sem = new SemanticProcessor();
        }

        public void work()
        {
            Claster claster = new Claster(sem.outStruct);
            claster.generateLayers();
        }

    }
}
