using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuesMg : MonoBehaviour
{
    public Questions que;

    private int[] a = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

    string[] p = new string[4]
    {
        "111111",
        "222222", "333333", "444444"
    };


    void Start()
    {
        var qq = GameObject.Instantiate<Questions>(que, this.transform);
        
        Question T = new Question("Titile", p, answer: 1);
        qq.Init(T);
    }
}