using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Fruit
{
    public class FruitsNet : MonoBehaviour
    {
        [Range(-3, 3)]
        public double weight_1_1, weight_2_1;
        [Range(-3, 3)]
        public double weight_1_2, weight_2_2;


        public Texture2D texture;
        public UnityEngine.UI.Image image;

        public static Color UnSafeColor = new Color(66 / 255f, 190 / 255f, 202 / 255f, 1);
        public static Color SafeColor   = new Color(121 / 255f, 236 / 255f, 105 / 255f, 1);

        private void Awake()
        {

        }

        private void Start()
        {
            //NeuralNetwork neuralNetwork = new NeuralNetwork(2, 3, 2);

            for (int i = 0; i < 1920; i++)
            {
                for (int j = 0; j < 1920; j++)
                {
                    texture.SetPixel(i, j, SafeColor);
                }
            }
            // 应用更改
            texture.Apply();  
        }

        private void Update()
        {
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    Viusalize(i, j);
                }
            }
        }

        public int Classify(double input1, double input2)
        {
            double OutPut_1 = input1 * weight_1_1 + input2 * weight_2_1;
            double OutPut_2 = input2 * weight_1_2 + input2 * weight_2_2;

            return (OutPut_1 > OutPut_2) ? 0 : 1;
        }

        public void Viusalize(int Graphx, int Graphy)
        {
            int value = Classify(Graphx, Graphy);

            if (value == 1) texture.SetPixel(Graphx, Graphy, UnSafeColor);
            if (value == 0) texture.SetPixel(Graphx, Graphy, SafeColor);

            texture.Apply();

        }
    }
}