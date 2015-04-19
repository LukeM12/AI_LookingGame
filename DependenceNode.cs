using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Assignment3.Classification
{
    //decisionNode is a feature
    public class DependenceNode
    {
        public static Random rndGen = new Random();

        public string featureName = "";
        public DependenceNode parent;

        public List<DependenceNode> children;

        double p_p1;
        double p_p0;



        public static DependenceNode generateRandomTree(int size, int maxNodeChildren = 3)
        {
            int nameIndex = 0;


            DependenceNode firstNode = new DependenceNode(null, rndGen.NextDouble(), rndGen.NextDouble(), "n" + nameIndex);

            List<DependenceNode> lowestLayer = new List<DependenceNode>();
            List<DependenceNode> newLayer = new List<DependenceNode>();

            lowestLayer.Add(firstNode);
            size--;
            nameIndex++;

            while (size > 0)
            {
                foreach (DependenceNode lowestNode in lowestLayer)
                {
                    int randomNodes = rndGen.Next(0, maxNodeChildren);
                    for (int i = 0; i < randomNodes && size > 0; i++)
                    {
                        newLayer.Add(new DependenceNode(lowestNode, rndGen.NextDouble(), rndGen.NextDouble(), "n" + nameIndex));
                        size--;
                        nameIndex++;
                    }
                }
                if (newLayer.Count > 0)
                {
                    lowestLayer = newLayer;
                    newLayer = new List<DependenceNode>();
                }
            }






            return firstNode;
        }

        public ClassInstance generateClass()
        {
            ClassInstance newClass = new ClassInstance();

            addToClassRecursive(newClass, 0);

            return newClass;
        }

        private void addToClassRecursive(ClassInstance fClass, int parentValue)
        {
            int newValue = createR(parentValue);
            fClass.setFeature(featureName, newValue);

            for (int i = 0; i < children.Count; i++)
                children[i].addToClassRecursive(fClass, newValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="p_p1">probability of value=1 given parent.value=1</param>
        /// <param name="p_p2">probability of value=1 given parent.value=0</param>
        public DependenceNode(DependenceNode parent, double p_p0, double p_p1, String name)
        {
            children = new List<DependenceNode>();
            this.parent = parent;
            if (parent != null)
                parent.addChild(this);
            this.featureName = name;

            this.p_p0 = p_p0;
            this.p_p1 = p_p1;
        }

        public void addChild(DependenceNode dn)
        {
            children.Add(dn);
        }

        public int createR(int parentValue)
        {
            double rnd = rndGen.NextDouble();
            if (parentValue == 0)
            {
                return (rnd <= p_p0) ? 1 : 0;
            }
            else
            {
                return (rnd <= p_p1) ? 1 : 0;
            }
        }

        public override string ToString()
        {
            return toStringInd("");
        }

        private string toStringInd(string indent)
        {
            string s = indent + featureName + "( " + p_p0 + ", " + p_p1 + " )" + "\n";

            for (int i = 0; i < children.Count; i++)
                s += children[i].toStringInd(indent + "\t");

            return s;
        }
    }
}
