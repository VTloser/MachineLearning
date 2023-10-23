using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fruit
{
    public class Layer
    {
        private int numNodesIn, numNodesOut;
        private double[,] weights;
        private double[] biases;


        public Layer(int numNodesIn, int numNodesOut)
        {
            this.numNodesIn = numNodesIn;
            this.numNodesOut = numNodesOut;


            weights = new double[numNodesIn, numNodesOut];
            biases = new double[numNodesOut]; // 存在疑问 应该是numNodesIn
        }

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


        public double ActivetionFunc(double weightInput)
        {
            return 1 / (1 + Math.Exp(-weightInput));
        }


        public double NodeCast(double outputActivation, double expectedOutput)
        {
            double error = outputActivation - expectedOutput;
            return error * error;
        }
    }
}