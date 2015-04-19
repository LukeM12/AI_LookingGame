using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Assignment3.Classification.Classification
{
    //http://en.wikipedia.org/wiki/Naive_Bayes_classifier


    public class Bayesian_Independent : Classification
    {

        protected Dictionary<String, double> featureProbabilities;

        public Bayesian_Independent()
        {

        }

        public override void train(List<ClassInstance> trainingSet)
        {
            //   Tuple<Name, count
            Dictionary<String, int> featureCounts = new Dictionary<string, int>();

            foreach (ClassInstance sample in trainingSet) //for each sample
            {
                foreach (KeyValuePair<string, int> sampleFeature in sample.features) //each feature
                {
                    // add 1 to featureCount[featureName] if sampleFeature == 1
                    if (sampleFeature.Value == 1)
                    {
                        if (featureCounts.ContainsKey(sampleFeature.Key))
                            featureCounts[sampleFeature.Key]++;
                        else
                            featureCounts.Add(sampleFeature.Key, 1);
                    }
                }
            }

            featureProbabilities = new Dictionary<string, double>();
            foreach (KeyValuePair<string, int> featureN in featureCounts) //each feature
            {
                featureProbabilities.Add(featureN.Key, (double)featureN.Value / (double)trainingSet.Count);
            }
        }

        /// <summary>
        /// calculates the probability the given sample matches this class
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public override double classify(ClassInstance sample)
        {
            double product = 1;

            foreach (KeyValuePair<string, int> feature in sample.features)
            {
                if (feature.Value == 1)
                    product *= featureProbabilities[feature.Key];
                else
                    product *= (1 - featureProbabilities[feature.Key]);

            }

            product /= (double)1 / (double)featureProbabilities.Count;

            return product;
        }


        public override Classification average(Classification[] list)
        {
            Bayesian_Independent[] listN = Array.ConvertAll(list, item => (Bayesian_Independent) item);
            Bayesian_Independent first = listN[0];

            for (int i = 1; i < listN.Length; i++) //skip 0
            {
                foreach (KeyValuePair<string, double> feature in listN[i].featureProbabilities)
                {
                    first.featureProbabilities[feature.Key] += listN[i].featureProbabilities[feature.Key];
                }
            }

            foreach (KeyValuePair<string, double> feature in listN[1].featureProbabilities)
                first.featureProbabilities[feature.Key] /= (double)listN.Length;


            return first;
        }
    }
}
