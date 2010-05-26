using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation_Structures_of_Texts.Classes.Text_Model;

namespace Operation_Structures_of_Texts.Classes.Analysis.Structurs
{
    class Claster
    {
        Layer[] layers;
        List<ElementaryProcess> outStruct;

        public Claster(List<ElementaryProcess> os)
        {
            outStruct = os;
        }

        public void generateLayers()
        {
            layers = new Layer[2];
            layers[0] = generateActorLayer();
            string[] tmpLayerActors = layers[0].generateTmpLayer(outStruct);
            layers[1] = generateActionLayer();
            string[] tmpLayerAction = layers[1].generateTmpLayer(outStruct);
            string[] AllTmpLayers = new string[tmpLayerAction.Length];
            for (int i = 0; i < AllTmpLayers.Length; i++)
                AllTmpLayers[i] = tmpLayerAction[i] + tmpLayerActors[i];
            Skorin form = new Skorin(AllTmpLayers);
            form.Show();
        }

        private Layer generateActorLayer()
        {
            Layer al = new Layer(outStruct.Count, "Actor");
            bool next = true;
            string firstElement = "", lastElement = "", activElement = "";
            
            for (int i = 1; i < outStruct.Count; i++)
            {
                if (next)
                {
                    firstElement = outStruct[i - 1].actor.getString();
                    lastElement = outStruct[i - 1].actor.getString();
                    activElement = outStruct[i].actor.getString();
                }
                else
                {
                    lastElement = outStruct[i - 1].actor.getString();
                    activElement = outStruct[i].actor.getString();
                    next = true;
                }
                next = !comperedTheElements(lastElement, activElement);//точное совпадение эктора
                if (next)
                    next = !comperedTheElements(firstElement, activElement);//точное совпадение эктора с первым эктором группы
                if (next)
                {
                    if (activElement == " она")
                        next = false;
                    //запросы к словарям синонимов и местоимений
                }
                al.layer[i] = next;
            }
            return al;
        }

        private Layer generateActionLayer()
        {
            Layer al = new Layer(outStruct.Count, "Action");
            bool next = true;
            string firstElement = "", lastElement = "", activElement = "";
            for (int i = 1; i < outStruct.Count; i++)
            {
                if (next)
                {
                    firstElement = outStruct[i - 1].action.getString();
                    lastElement = outStruct[i - 1].action.getString();
                    activElement = outStruct[i].action.getString();
                }
                else
                {
                    lastElement = outStruct[i - 1].action.getString();
                    activElement = outStruct[i].action.getString();
                    next = true;
                }
                if (lastElement == activElement)//точное совпадение эктора
                    next = false;
                if (firstElement == activElement)//точное совпадение эктора с первым эктором группы
                    next = false;
                if (activElement == "")//отсутствие эктора, предполагаем пропуск повторяющегося элемента
                    next = false;
                if (next)
                {
                    if (activElement == " она")
                        next = false;
                    //запросы к словарям синонимов и местоимений
                }
                al.layer[i] = next;
            }
            return al;
        }

        private bool comperedTheElements(object first, object second)
        {
            System.Type a = first.GetType();
            if (a.Name == "String")
            {
                string lastElement = (String)first, activElement = (String)second;
                if (lastElement == activElement)//точное совпадение
                    return true;
                if (activElement == "")//отсутствие
                    return true;


            }
            return false;
        }
    }
}
