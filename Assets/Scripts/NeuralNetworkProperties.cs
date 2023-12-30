using System;
using UnityEngine;

[Serializable]
public class NeuralNetworkProperties
{
    public NeuralNetworkProperties() { }
    public NeuralNetworkProperties(NeuralNetworkProperties neuralNetworkProperties)
    {
        numInputs = neuralNetworkProperties.numInputs;
        numHiddenLayers = neuralNetworkProperties.numHiddenLayers;
        numNeuronsPerHiddenLayer = neuralNetworkProperties.numNeuronsPerHiddenLayer;
        numOutputs = neuralNetworkProperties.numOutputs;
        numTrainingEpochs = neuralNetworkProperties.numTrainingEpochs;
        learningRate = neuralNetworkProperties.learningRate;
        defaultActivationFunction = neuralNetworkProperties.defaultActivationFunction;
    }
    
    [SerializeField] private int numInputs = 2;
    [SerializeField] private int numHiddenLayers = 2;
    [SerializeField] private int numNeuronsPerHiddenLayer = 2;
    [SerializeField] private int numOutputs = 1;
    [SerializeField] private int numTrainingEpochs = 10000;
    [SerializeField] private float learningRate = .1f;
    [SerializeField] private ActivationFunctionLibrary.ActivationFunctionName defaultActivationFunction;
    
    public int NumInputs => numInputs;
    public int NumHiddenLayers => numHiddenLayers;
    public int NumNeuronsPerHiddenLayer => numNeuronsPerHiddenLayer;
    public int NumOutputs => numOutputs;
    public int NumTrainingEpochs => numTrainingEpochs;
    public float LearningRate => learningRate;
    public ActivationFunctionLibrary.ActivationFunctionName DefaultActivationFunction => defaultActivationFunction;
}