using System;
using System.IO;
using System.Linq;
using UnityEditor;
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
    
    public int numInputs = 2;
    public int numHiddenLayers = 2;
    public int numNeuronsPerHiddenLayer = 2;
    public int numOutputs = 1;
    public int numTrainingEpochs = 10000;
    public float learningRate = .1f;
    public ActivationFunctionLibrary.ActivationFunctionName defaultActivationFunction;
}

[CreateAssetMenu(menuName = "Neural Network/Neural Network Scriptable Object")]
public class NeuralNetworkScriptableObject : ScriptableObject
{
    public NeuralNetworkProperties neuralNetworkProperties;
    public NeuralNetwork neuralNetwork;
}

[Serializable]
internal class VisualizeProperties
{
    [Header("Dimensions")]
    public float neuronSphereRadius = .05f;
    public float layerSpacing = 1f;
    public float neuronSpacing = 1f;
    [Header("Colors")]
    public Color hiddenNeuronColor = Color.white;
    public Color connectionColor = Color.white;
    public Color inputNeuronColor = Color.blue;
    public Color outputNeuronColor = Color.green;
}

public class NeuralNetworkVisualizer : MonoBehaviour
{
    [Header("Neural Network")]
    [SerializeField] private string neuralNetworkName = "NeuralNetworkScriptableObject";
    [SerializeField] private NeuralNetworkProperties neuralNetworkProperties;
    [Header("Gizmos")]
    [SerializeField] private VisualizeProperties visualizerProperties;
    
    private NeuralNetwork _neuralNetwork;
    
    private double[][] _trainingData =
    {
        new[] {0d, 1d},
        new[] {0d, 0d},
        new[] {1d, 0d},
        new[] {1d, 1d}
    };
        
    private double[][] _targetData =
    {
        new[] {1d},
        new[] {0d},
        new[] {1d},
        new[] {0d}
    };

    private void OnDrawGizmos()
    {
        if (_neuralNetwork?.Layers == null || _neuralNetwork.Layers.Length == 0) return;

        var numLayers = _neuralNetwork.Layers.Length;
        var layerWidth = visualizerProperties.layerSpacing / numLayers;

        for (var i = 0; i < numLayers; i++)
        {
            var layer = _neuralNetwork.Layers[i];
            var layerX = i * layerWidth + layerWidth / 2;

            var numNeurons = layer.Neurons?.Length ?? 0;
            var isInputLayer = i == 0;
            var isOutputLayer = i == numLayers - 1;

            for (var j = 0; j < numNeurons; j++)
            {
                var neuronWidth = visualizerProperties.neuronSpacing / numNeurons;
                var neuronY = j * neuronWidth + neuronWidth / 2;
                
                var neuronColor = isInputLayer ? 
                    visualizerProperties.inputNeuronColor 
                    : (isOutputLayer ? 
                        visualizerProperties.outputNeuronColor 
                        : visualizerProperties.hiddenNeuronColor);
                
                Gizmos.color = neuronColor;
                Gizmos.DrawSphere(new Vector3(layerX, neuronY), visualizerProperties.neuronSphereRadius);
                
                if (i >= numLayers - 1) continue;
                
                var nextLayer = _neuralNetwork.Layers[i + 1];
                var nextLayerX = (i + 1) * layerWidth + layerWidth / 2;
                var nextNeuronWidth = visualizerProperties.neuronSpacing / nextLayer.Neurons.Length;

                for (var k = 0; k < nextLayer.Neurons?.Length; k++)
                {
                    var nextNeuronY = k * nextNeuronWidth + nextNeuronWidth / 2;

                    Gizmos.color = visualizerProperties.connectionColor;
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
        _neuralNetwork = new NeuralNetwork(neuralNetworkProperties.numInputs, neuralNetworkProperties.numHiddenLayers, neuralNetworkProperties.numNeuronsPerHiddenLayer, neuralNetworkProperties.numOutputs, neuralNetworkProperties.defaultActivationFunction);
    }
    
    [ContextMenu("Train Neural Network")]
    public void TrainNeuralNetwork()
    {
        if (_neuralNetwork == null)
        {
            throw new Exception("Neural Network is null. Create a neural network first.");
        }
        
        _neuralNetwork.Train(_trainingData, _targetData, neuralNetworkProperties.numTrainingEpochs, neuralNetworkProperties.learningRate);
    }
    
    [ContextMenu("Test Neural Network")]
    public void TestNeuralNetwork()
    {
        if (_neuralNetwork == null)
        {
            throw new Exception("Neural Network is null. Create a neural network first.");
        }
        
        //shuffle data
        var shuffledData = _trainingData.Zip(_targetData, (data, target) => new { Data = data, Target = target })
            .OrderBy(x => Guid.NewGuid())
            .ToList();

        _trainingData = shuffledData.Select(x => x.Data).ToArray();
        _targetData = shuffledData.Select(x => x.Target).ToArray();

        _neuralNetwork.Test(_trainingData, _targetData);
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
        scriptableObject.neuralNetworkProperties = neuralNetworkProperties;
        scriptableObject.neuralNetwork = _neuralNetwork;
        
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
            neuralNetworkProperties = new NeuralNetworkProperties(scriptableObject.neuralNetworkProperties);
            _neuralNetwork = scriptableObject.neuralNetwork;
            Debug.Log("Neural Network Properties loaded from: " + filePath);
        }
        else
        {
            Debug.LogError("Neural Network Properties file not found: " + filePath);
        }
    }
    
}