using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Dataset")]
public class Dataset : ScriptableObject
{
    [field: SerializeField] public List<Data> TrainingData { get; set;  } = new();
}
