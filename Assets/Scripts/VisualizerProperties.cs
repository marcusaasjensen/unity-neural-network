using System;
using UnityEngine;

[Serializable]
public class VisualizerProperties
{
    [Header("Dimensions")]
    [SerializeField] private float neuronSphereRadius = .05f;
    [SerializeField] private float layerSpacing = 1f;
    [SerializeField] private float neuronSpacing = 1f;
    [Header("Colors")]
    [SerializeField] private Color hiddenNeuronColor = Color.white;
    [SerializeField] private Color connectionColor = Color.white;
    [SerializeField] private Color inputNeuronColor = Color.blue;
    [SerializeField] private Color outputNeuronColor = Color.green;
    
    public float NeuronSphereRadius => neuronSphereRadius;
    public float LayerSpacing => layerSpacing;
    public float NeuronSpacing => neuronSpacing;
    public Color HiddenNeuronColor => hiddenNeuronColor;
    public Color ConnectionColor => connectionColor;
    public Color InputNeuronColor => inputNeuronColor;
    public Color OutputNeuronColor => outputNeuronColor;
}