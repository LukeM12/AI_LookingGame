using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Assignment3.Classification
{

    //an evaluated Dependece Tree (Dependence Node)
    public class ClassInstance
    {
        public Dictionary<String, int> features;

        public ClassInstance()
        {
            features = new Dictionary<string, int>();
        }

        public void setFeature(String featureName, int value)
        {
            if (features.ContainsKey(featureName))
                features[featureName] = value;
            else
                features.Add(featureName, value);
        }

        public string ToStringFlat()
        {
            String s = "";

            foreach (KeyValuePair<string, int> entry in features)
            {
                s += entry.Key + ":" + entry.Value + " ";
            }

            return s;
        }

        public override string ToString()
        {
            String s = "";

            foreach(KeyValuePair<string, int> entry in features){
                s += entry.Key + ": " + entry.Value + "\n";
            }

            return s;
        }
    }
}
