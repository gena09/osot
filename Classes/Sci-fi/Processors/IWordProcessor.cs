using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors
{
    public interface IWordProcessor
    {      

        void setInitialData(Object data);

        void work();

        double getRealProbabylity();

        double getApriorProbabylity();
    }
}
