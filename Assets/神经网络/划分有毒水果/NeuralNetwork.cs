using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fruit
{
    public class NeuralNetwork
    {
        private Layer[] layers;

        //创建神经网络

        public NeuralNetwork(params int[] layerSizes)
        {
            layers = new Layer[layerSizes.Length - 1];
            for (int i = 0; i < layerSizes.Length; i++)
            {
                layers[i] = new Layer(layerSizes[i], layerSizes[i + 1]);
            }
        }


        //计算神经网络
        double[] CalCulateOutputs(double[] inputs)
        {
            foreach (var layer in layers)
            {
                inputs = layer.CalculateOutput(inputs);
            }

            return inputs;
        }

        //计算神经网络并返回最大值
        int Classify(double[] inouts)
        {
            double[] Outputs = CalCulateOutputs(inouts);
            return IndexofMaxValue(Outputs);
        }


        //代价函数


        //代价函数
        // double Cost(DataPoint dataPoint)
        // {
        //     double[] outputs = CalCulateOutputs(dataPoint.inpouts);
        //     Layer outputLayer = layers[layers.Length - 1];
        //     double cost = 0;
        //
        //     for (int nodeOut = 0; nodeOut < outputs.Length; nodeOut++)
        //     {
        //         cost += outputLayer.NodeCast(outputs[nodeOut], dataPoint.expectedOutputs[nodeOut]);
        //     }
        //
        //     return cost;
        // }
        //
        // double Cost(DataPoint[] data)
        // {
        //     double totalCost = 0;
        //
        //     foreach (var dataPoint in data)
        //     {
        //         totalCost += Cost(dataPoint);
        //     }
        //
        //     return totalCost / data.Length;
        // }

        int IndexofMaxValue<T>(T[] list) where T : System.IComparable<T>
        {
            T maxvlaue = list[0];
            int maxindex = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if ((list[i].CompareTo(maxvlaue)) > 1)
                {
                    maxvlaue = list[i];
                    maxindex = i;
                }
            }

            return maxindex;
        }
    }
}