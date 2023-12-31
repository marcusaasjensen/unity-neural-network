using System;
using UnityEngine;

[Serializable]
public class Data
{
    [field: SerializeField] public double[] Input { get; set; }
    [field: SerializeField] public double[] Target { get; set; }
}