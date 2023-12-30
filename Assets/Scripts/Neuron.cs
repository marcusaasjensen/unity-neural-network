using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class Neuron
{
    [SerializeField] private ActivationFunctionLibrary.ActivationFunctionName activationFunction;
    
    [field: SerializeField] public double[] Weights { get; private set; }
    [field: SerializeField] public double[] Inputs { get; set; }
    [field: SerializeField] public double Output { get; set; }
    [field: SerializeField] public double Bias { get; set; }
    [field: SerializeField] public bool IsInputNeuron { get; private set; }
    [field: SerializeField] public int InputIndex { get; private set; }
    
    public Neuron(int inputIndex)
    {
        Inputs = new double[1];
        IsInputNeuron = true;
        InputIndex = inputIndex;
    }

    public Neuron(int numInputs, ActivationFunctionLibrary.ActivationFunctionName activationFunction)
    {
        Weights = new double[numInputs];

        for (var i = 0; i < numInputs; i++)
        {
            Weights[i] = UnityEngine.Random.Range(-.5f, .5f);
        }
        Bias = UnityEngine.Random.Range(-.5f, .5f);
        this.activationFunction = activationFunction;
    }

    public double FeedForward(double[] inputs)
    {
        Inputs = inputs;

        if (IsInputNeuron)
        {
            return Output = inputs[InputIndex];
        }
        
        var sum = Weights.Select((t, i) => inputs[i] * t).Sum();
        sum += Bias;
        
        Output = ActivationFunctionLibrary.GetFunction(activationFunction)(sum);
        return Output;
    }

    public void SetActivationFunction(ActivationFunctionLibrary.ActivationFunctionName activationFunctionName) => activationFunction = activationFunctionName;
    
}