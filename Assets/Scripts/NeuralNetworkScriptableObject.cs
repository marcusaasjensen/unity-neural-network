﻿using UnityEngine;

[CreateAssetMenu(menuName = "Neural Network/Neural Network Scriptable Object")]
public class NeuralNetworkScriptableObject : ScriptableObject
{
    [SerializeField] private NeuralNetworkProperties neuralNetworkProperties;
    [SerializeField] private NeuralNetwork neuralNetwork;
    
    public NeuralNetworkProperties NeuralNetworkProperties
    {
        get => neuralNetworkProperties;
        set => neuralNetworkProperties = value;
    }

    public NeuralNetwork NeuralNetwork
    {
        get => neuralNetwork;
        set => neuralNetwork = value;
    }
}