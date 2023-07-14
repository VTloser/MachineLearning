using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    //尽可能多的探索迷宫
    //尽可能多的摸到彩蛋 

    //基因序列 控制转向
    public Dictionary<(bool left, bool forward, bool right), float> gens;

    //记录基因长度 用于交换基因序列
    int dnaLength;

    public DNA()
    {
        gens = new Dictionary<(bool, bool, bool), float>();
        SetRandom();
    }

    public void SetRandom()
    {
        gens.Clear();
        gens.Add((false, false, false), Random.Range(-90, 91));
        gens.Add((false, false, true), Random.Range(-90, 91));
        gens.Add((false, true, true), Random.Range(-90, 91));
        gens.Add((false, true, false), Random.Range(-90, 91));
        gens.Add((true, true, false), Random.Range(-90, 91));
        gens.Add((true, true, true), Random.Range(-90, 91));
        gens.Add((true, false, true), Random.Range(-90, 91));
        gens.Add((true, false, false), Random.Range(-90, 10));
        dnaLength = gens.Count;
    }

    public void Combine(DNA d1, DNA d2)
    {
        int i = 0;
        Dictionary<(bool, bool, bool), float> newGens = new Dictionary<(bool, bool, bool), float>();
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

    public float GerGene((bool left, bool forward, bool right) SeeWall)
    {
        return gens[(SeeWall)];
    }

}
