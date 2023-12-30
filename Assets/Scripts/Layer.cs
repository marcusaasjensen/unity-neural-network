using System;
using UnityEngine;

[Serializable]
public class Layer
{
    [field: SerializeField] public Neuron[] Neurons { get; private set; }

    public Layer(int numInputs)
    {
        Neurons = new Neuron[numInputs];
        for (var i = 0; i < numInputs; i++)
        {
            Neurons[i] = new Neuron(i);
        }
    }
    
    public Layer(int numNeurons, int numInputsPerNeuron, ActivationFunctionLibrary.ActivationFunctionName activationFunction)
    {
        Neurons = new Neuron[numNeurons];
        for (var i = 0; i < numNeurons; i++)
        {
            Neurons[i] = new Neuron(numInputsPerNeuron, activationFunction);
        }
    }
    
    public double[] FeedForward(double[] inputs)
    {
        var outputs = new double[Neurons.Length];

        for (var i = 0; i < Neurons.Length; i++)
        {
            outputs[i] = Neurons[i].FeedForward(inputs);
        }
        return outputs;
    }

    
    public void SetActivationFunction(ActivationFunctionLibrary.ActivationFunctionName activationFunction)
    {
        foreach (var neuron in Neurons)
        {
            neuron.SetActivationFunction(activationFunction);
        }
    }
}
