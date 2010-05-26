using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Text_Model;
using Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules;


namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics
{
    public class SemanticSearch
    {        
        private ClausesTree clausesTree;
        private ISentence sent;
        private ElementaryProcess ep;
        
        public SemanticSearchStatistics stats = new SemanticSearchStatistics();
        public string log = "";

        public SemanticSearch(ClausesTree clausesTree, ISentence sent)
        {
            this.clausesTree = clausesTree;
            this.sent = sent;
            ep = new ElementaryProcess();
        }

        public IOperationStructure naivSearch()
        {
            searchAction();
            searchActor();
            searchObjectForAction();
            searchCharsOfAction();
            searchAdditionalObjectsForAction();
            searchUnions();
            return ep;
        }

        /// <summary>
        /// Поиск наименования действия
        /// </summary>
        /// <param name="clausesTree"></param>
        /// <param name="sent"></param>
        /// <returns></returns>
        public void searchAction()
        {
            LongOperationAttribut action = new LongOperationAttribut();
            for (int i = 0; i < clausesTree.rels.Count; i++)
            {
                try
                {
                    // (new Rules.Action.PODL).check(clausesTree, i, sent, stats, action);
                    //(new Rules.Action.PRIAM_DOP).check(clausesTree, i, sent, stats, action);
                }
                catch (Exception ex)
                {
                    log += ex.Message + "\r\n";
                }
                /*
                if (clausesTree.rels[i].Name == "НАРЕЧ_ГЛАГОЛ")
                {
                    action.addElementaryAttribut(sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr, "");
                    markedWords.Add(clausesTree.rels[i].SourceItemNo, "Action");
                }
                if (clausesTree.rels[i].Name == "ПЕР_ГЛАГ_ИНФ")//составной
                {
                    ElementaryAttribut a1 = 
                        new ElementaryAttribut("",sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr);
                    ElementaryAttribut a2 = 
                        new ElementaryAttribut("",sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr);
                    action.addBreaksOfAttributs(a1, a2, "+");                    
                    markedWords.Add(clausesTree.rels[i].SourceItemNo, "Action");
                    markedWords.Add(clausesTree.rels[i].TargetItemNo, "Action");                    
                }
                if (clausesTree.rels[i].Name == "ОДНОР_ИНФ")//составной
                {
                    action.addEAttr(sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr, "AND");
                }*/
            }
            ep.action = action;
        }

        /// <summary>
        /// Поиск наименования объекта действия (актора)
        /// </summary>
        /// <param name="clausesTree"></param>
        /// <param name="sent"></param>
        /// <returns></returns>
        public void searchActor()
        {
            LongOperationAttribut actor = new LongOperationAttribut();
            for (int i = 0; i < clausesTree.rels.Count; i++)
            {
                try
                {
                    //(new Rules.Actor.PODL).check(clausesTree, i, sent, stats, actor);
                }
                catch (Exception ex)
                {
                    log += ex.Message + "\r\n";
                }
                /*
                if (clausesTree.rels[i].Name == "ПРИЛ-СУЩ")//составной
                {
                    actor.addEAttr(sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr + " V " +
                        sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr);
                }
                if (clausesTree.rels[i].Name == "СУЩ-ЧИСЛ")//уточнить, точно ли числительное попадает в оператор
                {
                    actor.addEAttr(sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr,
                        sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr);
                }
                //по Им падежу
                if (clausesTree.rels[i].Name == "ГЕНИТ_ИГ")//составной
                {
                    actor.addEAttr(sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr + " V " +
                        sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr);
                }
                if (clausesTree.rels[i].Name == "ОДНОР_ИГ")
                {
                    actor.addEAttr(sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr, sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr);
                }
                if (clausesTree.rels[i].Name == "ПРИЧ_СУЩ")
                {
                    actor.addEAttr(sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr, sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr);
                }
                if (clausesTree.rels[i].Name == "Р_С_ОДНОР_СУЩ")
                {
                    actor.addEAttr(sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr, sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr);
                }
                if (clausesTree.rels[i].Name == "Р_С_ОДНОР_МС")
                {
                    actor.addEAttr(sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr, sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr);
                }
                if (clausesTree.rels[i].Name == "ПРИЛ_ПОСТПОЗ")
                {
                    actor.addEAttr(sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr, sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr);
                }
                //по Им падежу
                if (clausesTree.rels[i].Name == "СУЩ_ОБС_ПРИЛ")
                {
                    actor.addEAttr(sent.get_Word(clausesTree.rels[i].SourceItemNo).WordStr, sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr);
                }*/
            }
            ep.actor = actor;
        }

        /// <summary>
        /// Поиск объекта, над которым выполняется действие
        /// </summary>
        /// <param name="clausesTree"></param>
        /// <param name="sent"></param>
        /// <returns></returns>
        public void searchObjectForAction()
        {
            LongOperationAttribut OFA = new LongOperationAttribut();
            for (int i = 0; i < clausesTree.rels.Count; i++)
            {
                try
                {
                    //Rules.OFA.PRIAM_DOP.check(clausesTree, i, sent, stats, OFA);
                }
                catch (Exception ex)
                {
                    log += ex.Message + "\r\n";
                }
                /*
                if (clausesTree.rels[i].Name == "НАРЕЧ_ГЛАГОЛ")
                {
                    OFA.addEAttr(sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr);
                }*/
            }
            ep.objectForAction = OFA;
        }

        /// <summary>
        /// поиск характеристик действия
        /// </summary>
        /// <param name="clausesTree"></param>
        /// <param name="sent"></param>
        /// <returns></returns>
        public void searchCharsOfAction()
        {
            /*
            LongOperationAttribut charsOfAction = new LongOperationAttribut();            
            for (int i = 0; i < clausesTree.rels.Count; i++)
            {
                if ((clausesTree.rels[i].Name == "ПГ")&&
                    !(markedWords.ContainsKey(clausesTree.rels[i].TargetItemNo)))
                {
                    charsOfAction.addEAttr(sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr);
                }
                if (clausesTree.rels[i].Name == "ОДНОР_ПРИЛ")
                {
                    charsOfAction.addEAttr(sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr);
                }
            }*/
        }

        /// <summary>
        /// Поиск дополнительных объектов действия
        /// </summary>
        /// <param name="clausesTree"></param>
        /// <param name="sent"></param>
        /// <returns></returns>
        public void searchAdditionalObjectsForAction()
        {

        }

        /// <summary>
        /// Поиск уточнений атрибутов
        /// </summary>
        /// <param name="clausesTree"></param>
        /// <param name="sent"></param>
        /// <returns></returns>
        public void searchUnions()
        {
            for (int i = 0; i < clausesTree.rels.Count; i++)
            {
                try
                {
                    //Rules.UnionsAndOther.PRIDAT_OPR.check(clausesTree, i, sent, stats, ep);
                    //Rules.UnionsAndOther.ODNORODN_IG.check(clausesTree, i, sent, stats, ep);
                    //Rules.UnionsAndOther.RAZRIV_SOUS.check(clausesTree, i, sent, stats, ep);
                    //Rules.UnionsAndOther.OTRICAT_FORM.check(clausesTree, i, sent, stats, ep);
                }
                catch (Exception ex)
                {
                    log += ex.Message + "\r\n";
                }
            }
        }
    }
}
/*
        for (int j = 0; j < clausesTree.rels.Count; j++)
        {
            if (clausesTree.rels[j].Name == "ОТСОЮЗ")
            {
                if (clausesTree.rels[i].SourceItemNo == clausesTree.rels[j].SourceItemNo)
                {
                    glue = j;
                    for (int k = 0; k < clausesTree.rels.Count; k++)
                    {
                        if ((clausesTree.rels[k].Name == "РАЗРЫВ_СОЮЗ") &&
                            (clausesTree.rels[k].SourceItemNo == clausesTree.rels[j].TargetItemNo) &&
                            (k != i))
                        {
                            saver = k;
                        }
                    }
                }
                if (clausesTree.rels[i].SourceItemNo == clausesTree.rels[j].TargetItemNo)
                {
                    glue = j;
                    for (int k = 0; k < clausesTree.rels.Count; k++)
                    {
                        if ((clausesTree.rels[k].Name == "РАЗРЫВ_СОЮЗ") &&
                            (clausesTree.rels[k].SourceItemNo == clausesTree.rels[j].SourceItemNo) &&
                            (k != i))
                        {
                            saver = k;
                        }
                    }
                }
            }
        }
    */