using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fruit
{
    public class Layer //层
    {                                                   
        private int numNodesIn, numNodesOut;  //节点输入数量 节点输出数量  //上一层节点数量  本层节点数量
        private double[,] weights;  //节点权重
        private double[] biases;  //节点偏置


        public Layer(int numNodesIn, int numNodesOut)
        {
            this.numNodesIn = numNodesIn;
            this.numNodesOut = numNodesOut;


            weights = new double[numNodesIn, numNodesOut];
            biases = new double[numNodesOut]; // 存在疑问 应该是numNodesIn
        }

        //计算输出节点
        public double[] CalculateOutput(double[] inputs)
        {
            double[] activations = new double[numNodesOut];
            for (int nodeOut = 0; nodeOut < numNodesOut; nodeOut++)
            {
                double weightedInput = biases[nodeOut];
                for (int nodeIn = 0; nodeIn < numNodesIn; nodeIn++)
                {
                    weightedInput += inputs[nodeIn] * weights[nodeIn, nodeOut];
                }

                activations[nodeOut] = ActivetionFunc(weightedInput);
            }

            return activations;
        }

        //标准化(激活)函数
        public double ActivetionFunc(double weightInput)
        {
            return 1 / (1 + Math.Exp(-weightInput)); //使用的是S函数
        }


        public double NodeCast(double outputActivation, double expectedOutput)
        {
            double error = outputActivation - expectedOutput;
            return error * error;
        }
    }
}