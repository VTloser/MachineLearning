using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;
using XCharts.Runtime;

namespace  Car
{
    public class CarChars : MonoBehaviour
    {
        public BaseChart maxcharts;
        public BaseChart sumcharts;
        public PopulationManager popMgr;

        private void Awake()
        {
            popMgr.resultshow += AddDatea;
        }

        public void AddDatea(int generation, float max, float sum)
        {
            var t0 = maxcharts.GetSerie(0);
            t0.AddData(generation, max);
            
            var t1 = sumcharts.GetSerie(0);
            t1.AddData(generation, sum);
        }
    }
}
