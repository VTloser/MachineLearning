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
                gens.Add(i, Random.Range(-45f, 45f));
                
                // if (i > 0)
                // {
                //     //gens.Add(i, Random.Range(-45f, 0f));
                //     
                // }
                // else if (i == 0)
                // {
                //     //gens.Add(i, 0);
                //     
                // }
                // else if (i < 0)
                // {
                //     //gens.Add(i, Random.Range(0f, 45f));
                //
                // }
            }
            
            dnaLength = gens.Count;
        }
        
        public void Combine(DNA d1, DNA d2)
        {
            int i = 0;
            Dictionary<int, float> newGens = new Dictionary<int, float>();
            bool reversal = false;
            foreach (var g in gens)
            {
                if (Random.Range(0, 11) > 5) reversal = !reversal;
                if ((i % 2 == 0) == reversal) newGens.Add(g.Key, d1.gens[g.Key]);
                else newGens.Add(g.Key, d2.gens[g.Key]);
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