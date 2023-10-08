using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    public class DNA
    {
        //尽可能多的走的更远
        //基因序列控制转向
        public Dictionary<int, float> gens;

        
        
        //记录基因长度 用于交换基因序列
        int dnaLength;

        public DNA()
        {
            gens = new Dictionary<int, float>();
            SetRandom();
        }

        public void SetRandom()
        {
            gens.Clear();
            for (int i = -10; i <= 10; i += 1)
            {
                Debug.Log(i);
                gens.Add(i, Random.Range(-90, 91));
            }
            
            dnaLength = gens.Count;
        }
        
        public void Combine(DNA d1, DNA d2)
        {
            int i = 0;
            Dictionary<int, float> newGens = new Dictionary<int, float>();
            foreach (var g in gens)
            {
                if (i < dnaLength / 2)
                {
                    newGens.Add(g.Key, d1.gens[g.Key]);
                }
                else
                {
                    newGens.Add(g.Key, d2.gens[g.Key]);
                }

                i++;
            }

            gens = newGens;
        }

        public float GerGene(int SeeWall)
        {
            return gens[(SeeWall)];
        }
    }
}