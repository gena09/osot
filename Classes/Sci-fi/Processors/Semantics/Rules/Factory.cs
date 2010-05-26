using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules
{
    class Factory
    {
        public static IRule get(string name)
        {
            name = "Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules." + name;
            Type ruleType = Type.GetType(name);

            // If Type.GetType can't find the type, it returns Null
            IRule rule;
            if (ruleType != null)
            {                
                rule = Activator.CreateInstance(ruleType) as IRule;
            }
            else
            {
                throw new Exception("Правило " + name + " не найдено");
            }
            return rule;
        }
    }
}
