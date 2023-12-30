using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class NeuralNetwork
{
    [field: SerializeField] public Layer[] Layers { get; private set;  }
    private Layer InputLayer => Layers[0];
    private Layer OutputLayer => Layers[^1];

    public NeuralNetwork(int numInputs, int numHiddenLayers, int numNeuronsPerHiddenLayer, int numOutputs, ActivationFunctionLibrary.ActivationFunctionName defaultActivationFunction)
    {
        Layers = new Layer[numHiddenLayers + 2];

        Layers[0] = new Layer(numInputs);

        for (var i = 1; i <= numHiddenLayers; i++)
        {
            Layers[i] = new Layer(numNeuronsPerHiddenLayer, numInputs, defaultActivationFunction);
        }

        Layers[^1] = new Layer(numOutputs, numNeuronsPerHiddenLayer, defaultActivationFunction);
    }

    private double[] FeedForward(double[] inputs)
    {
        var outputs = InputLayer.FeedForward(inputs);

        for (var i = 1; i < Layers.Length; i++)
        {
            outputs = Layers[i].FeedForward(outputs);
        }
        return outputs;
    }
    
    public void Train(List<Data> trainingData, int epochs, float learningRate)
    {
        var epoch = 0;
        for (; epoch < epochs; epoch++)
        {
            var shuffledData = trainingData.OrderBy(x => Guid.NewGuid()).ToList();

            foreach (var example in shuffledData)
            {
                var inputs = example.Input;
                var targets = example.Target;

                var predictions = Predict(inputs);

                BackPropagate(targets, predictions, learningRate);
            }
        }
    }
    
    public void Test(List<Data> testData)
    {
        foreach (var example in testData)
        {
            var inputs = example.Input;
            var targets = example.Target;

            var predictions = Predict(inputs);

            Debug.Log($"Input: {string.Join(", ", inputs)}\nPrediction: {string.Join(", ", predictions)}\nTarget: {string.Join(", ", targets)}\n");
        }
    }

    private void BackPropagate([NotNull] double[] targets, [NotNull] double[] predictions, double learningRate)
    {
        if (targets == null)
        {
            throw new ArgumentNullException(nameof(targets));
        }

        if (predictions == null)
        {
            throw new ArgumentNullException(nameof(predictions));
        }

        var outputLayer = OutputLayer;
        var outputErrors = new double[outputLayer.Neurons.Length];
        for (var i = 0; i < outputLayer.Neurons.Length; i++)
        {
            var neuron = outputLayer.Neurons[i];
            outputErrors[i] = targets[i] - predictions[i];
            var gradient = predictions[i] * (1 - predictions[i]);
            var delta = outputErrors[i] * gradient;
            for (var j = 0; j < neuron.Weights.Length; j++)
            {
                var input = neuron.Inputs[j];
                var weightDelta = input * delta * learningRate;
                neuron.Weights[j] += weightDelta;
            }
            neuron.Bias += delta * learningRate;
        }

        for (var i = Layers.Length - 2; i > 0; i--)
        {
            var hiddenLayer = Layers[i];
            var hiddenErrors = new double[hiddenLayer.Neurons.Length];
            for (var j = 0; j < hiddenLayer.Neurons.Length; j++)
            {
                var neuron = hiddenLayer.Neurons[j];
                var error = outputLayer.Neurons.Select((outputNeuron, k) => outputErrors[k] * outputNeuron.Weights[j]).Sum();
                hiddenErrors[j] = error;
                var gradient = neuron.Output * (1 - neuron.Output);
                var delta = error * gradient;
                for (var k = 0; k < neuron.Weights.Length; k++)
                {
                    var input = neuron.Inputs[k];
                    var weightDelta = input * delta * learningRate;
                    neuron.Weights[k] += weightDelta;
                }
                neuron.Bias += delta * learningRate;
            }
            outputErrors = hiddenErrors;
        }
    }

    public void SetActivationFunction(int index, ActivationFunctionLibrary.ActivationFunctionName activationFunction) => Layers[index].SetActivationFunction(activationFunction);

    private double[] Predict(double[] inputs) => FeedForward(inputs);
}