using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class NeuralNetworkVisualizer : MonoBehaviour
{
    [Header("Neural Network")]
    [SerializeField] private string neuralNetworkName = "NeuralNetworkScriptableObject";
    [SerializeField] private NeuralNetworkProperties neuralNetworkProperties;
    [Header("Gizmos")]
    [SerializeField] private VisualizerProperties visualizerProperties;
    
    private NeuralNetwork _neuralNetwork;
    
    private readonly List<Data> _trainingData = new()
    {
        new Data { Input = new[] {0d, 1d}, Target = new[] {1d} },
        new Data { Input = new[] {0d, 0d}, Target = new[] {0d} },
        new Data { Input = new[] {1d, 0d}, Target = new[] {1d} },
        new Data { Input = new[] {1d, 1d}, Target = new[] {0d} }
    };

    private void OnDrawGizmos()
    {
        if (_neuralNetwork?.Layers == null || _neuralNetwork.Layers.Length == 0)
        {
            return;
        }

        var numLayers = _neuralNetwork.Layers.Length;
        var layerWidth = visualizerProperties.LayerSpacing / numLayers;

        for (var i = 0; i < numLayers; i++)
        {
            var layer = _neuralNetwork.Layers[i];
            var layerX = i * layerWidth + layerWidth / 2;

            var numNeurons = layer.Neurons?.Length ?? 0;
            var isInputLayer = i == 0;
            var isOutputLayer = i == numLayers - 1;

            for (var j = 0; j < numNeurons; j++)
            {
                var neuronWidth = visualizerProperties.NeuronSpacing / numNeurons;
                var neuronY = j * neuronWidth + neuronWidth / 2;
                
                var neuronColor = isInputLayer ? 
                    visualizerProperties.InputNeuronColor 
                    : (isOutputLayer ? 
                        visualizerProperties.OutputNeuronColor 
                        : visualizerProperties.HiddenNeuronColor);
                
                Gizmos.color = neuronColor;
                Gizmos.DrawSphere(new Vector3(layerX, neuronY), visualizerProperties.NeuronSphereRadius);

                if (i >= numLayers - 1)
                {
                    continue;
                }
                
                var nextLayer = _neuralNetwork.Layers[i + 1];
                var nextLayerX = (i + 1) * layerWidth + layerWidth / 2;
                var nextNeuronWidth = visualizerProperties.NeuronSpacing / nextLayer.Neurons.Length;

                for (var k = 0; k < nextLayer.Neurons?.Length; k++)
                {
                    var nextNeuronY = k * nextNeuronWidth + nextNeuronWidth / 2;

                    Gizmos.color = visualizerProperties.ConnectionColor;
                    Gizmos.DrawLine(new Vector3(layerX, neuronY), new Vector3(nextLayerX, nextNeuronY));
                }
            }
        }
    }

    
    [ContextMenu("Clear Neural Network")]
    public void ClearNeuralNetwork() => _neuralNetwork = null;
    
    [ContextMenu("Create Neural Network")]
    public void CreateNeuralNetwork()
    {
        ClearNeuralNetwork();
        _neuralNetwork = new NeuralNetwork(neuralNetworkProperties.NumInputs, neuralNetworkProperties.NumHiddenLayers, neuralNetworkProperties.NumNeuronsPerHiddenLayer, neuralNetworkProperties.NumOutputs, neuralNetworkProperties.DefaultActivationFunction);
    }
    
    [ContextMenu("Train Neural Network")]
    public void TrainNeuralNetwork()
    {
        if (_neuralNetwork == null)
        {
            Debug.LogError("Neural Network is null. Create a neural network first.");
            return;
        }
        
        _neuralNetwork.Train(_trainingData, neuralNetworkProperties.NumTrainingEpochs, neuralNetworkProperties.LearningRate);
    }
    
    [ContextMenu("Test Neural Network")]
    public void TestNeuralNetwork()
    {
        if (_neuralNetwork == null)
        {
            Debug.LogError("Neural Network is null. Create a neural network first.");
            return;
        }
        
        var shuffledData = _trainingData.OrderBy(x => Guid.NewGuid()).ToList();
        
        _neuralNetwork.Test(shuffledData);
    }
    
    [ContextMenu("Generate Neural Network (Create, Train, Test)")]
    public void CreateTrainTestNeuralNetwork()
    {
        CreateNeuralNetwork();
        TrainNeuralNetwork();
        TestNeuralNetwork();
    }
    
    [ContextMenu("Save Neural Network to Scriptable Object")]
    public void SaveNeuralNetworkPropertiesToScriptableObject()
    {
        const string folderPath = "Assets/Resources/";
        
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        
        var filePath = Path.Combine(folderPath, neuralNetworkName + ".asset");
        var scriptableObject = ScriptableObject.CreateInstance<NeuralNetworkScriptableObject>();
        scriptableObject.NeuralNetworkProperties = neuralNetworkProperties;
        scriptableObject.NeuralNetwork = _neuralNetwork;
        
        AssetDatabase.CreateAsset(scriptableObject, filePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        Debug.Log("Neural Network saved to: " + filePath);
    }
    
    [ContextMenu("Load Neural Network from Scriptable Object")]
    public void LoadNeuralNetworkPropertiesFromScriptableObject()
    {
        const string folderPath = "Assets/Resources/";
        var filePath = Path.Combine(folderPath, neuralNetworkName + ".asset");
        
        if (File.Exists(filePath))
        {
            var scriptableObject = AssetDatabase.LoadAssetAtPath<NeuralNetworkScriptableObject>(filePath);
            neuralNetworkProperties = new NeuralNetworkProperties(scriptableObject.NeuralNetworkProperties);
            _neuralNetwork = scriptableObject.NeuralNetwork;
            Debug.Log("Neural Network Properties loaded from: " + filePath);
        }
        else
        {
            Debug.LogError("Neural Network Properties file not found: " + filePath);
        }
    }
    
}