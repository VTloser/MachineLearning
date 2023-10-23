using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fruit
{
    public class FruitsNet : MonoBehaviour
    {
        private void Awake()
        {
            NeuralNetwork neuralNetwork = new NeuralNetwork(2, 3, 2);
        }
    }
}