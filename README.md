# Unity Neural Network 🧠
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/6c5ca96a82bc4b18999316bfab824029)](https://app.codacy.com?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=flat&logo=unity)](https://unity3d.com)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Unity based neural network. The represented neural network is a binary classification neural network.

The neural networks can be trained and tested on custom datasets. It works with gate datasets (AND, XOR and so on).

<img width="271" alt="neural_network_cover" src="https://github.com/marcusaasjensen/unity-neural-network/assets/88457743/19395e68-7a58-4503-81a5-793cbe7375e3">

# Instructions 📝

Assuming you have Git installed, clone the project with the following command:

```bash
git clone https://github.com/marcusaasjensen/unity-neural-network.git
```

Open the project in Unity.
>[!IMPORTANT]
>**Unity** 2022.3.0f1

Open the "NeuralNetworkScene" scene.

<img width="227" alt="hierarchy" src="https://github.com/marcusaasjensen/unity-neural-network/assets/88457743/a17711b3-bd76-4401-9dab-407c401ba7db">

Go to the NeuralNetwork Gameobject's inspector.

<img width="324" alt="inspector" src="https://github.com/marcusaasjensen/unity-neural-network/assets/88457743/e1c2f3b3-0fc0-4669-b70f-d9aa621cd433">

Write "NeuralNetwork_XOR" as the name of the neural network.

Right click on the inspector and choose "Neural Network/Load Neural Network from Scriptable Object".

Do the same for the Dataset with the name "XOR" and load from "Dataset/Load Dataset from Scriptable Object".

Focus on the neural network Gameobject in the scene to visualize the Gizmos. You can modify the Gizmos visuals according to your preferences with the Visualizer Properties in the inspector.

<img width="323" alt="neural_network" src="https://github.com/marcusaasjensen/unity-neural-network/assets/88457743/1f8dfe8a-72b6-4901-a738-981f74ad209f">

This neural network has already been trained. Right click on the inspector and choose "Test Neural Network".

The results will appear in the Unity's Console. These are the predictions for both inputs of the XOR Gate.

<img width="200" alt="results" src="https://github.com/marcusaasjensen/unity-neural-network/assets/88457743/ef6e46b1-9d96-4a74-9997-a8fd684fc043">

You can generate your own neural network by using the different functions implemented by right-clicking on the inspector.

When you are satisfied with the results of your neural network, you can save it as a scriptable object: right click on the inspector and choose "Neural Network/Save Neural Network to Scriptable Object." The name of the asset will be the name of the neural network.

You can create your on dataset: right-click on the folder's hirarchy and choose "Create/Scriptable Object/Dataset".

>[!WARNING]
>Make sure to put all of your Datasets inside the folder "Assets/Resources/Datasets", as well for your neural networks inside "Assets/Resources/NeuralNetworks".
>

Enjoy.


