using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Assignment3.Classification.Classification
{
    public abstract class Classification
    {
        public abstract void train(List<ClassInstance> trainingSet);

        public abstract double classify(ClassInstance sample);

        public double test(List<ClassInstance> testingSet)
        {
            double successRate = 0;

            foreach (ClassInstance sample in testingSet)
            {
                double val = classify(sample);
            }


            return successRate;
        }


        public abstract Classification average(Classification[] list);
    }
}
