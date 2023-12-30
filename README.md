# unity-neural-network
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/6c5ca96a82bc4b18999316bfab824029)](https://app.codacy.com?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)

Unity based neural network.

The neural networks can be trained and tested on the XOR Gate dataset.

# Instructions

Assuming you have Git installed, clone the project with the following command:

```git clone https://github.com/marcusaasjensen/unity-neural-network.git```

Open the project in Unity.
>[!IMPORTANT]
>**Unity** 2022.3.0f1

Open the "NeuralNetworkScene" scene.

<img width="227" alt="hierarchy" src="https://github.com/marcusaasjensen/unity-neural-network/assets/88457743/a17711b3-bd76-4401-9dab-407c401ba7db">

Go to the NeuralNetwork Gameobject's inspector.

<img width="332" alt="inspector" src="https://github.com/marcusaasjensen/unity-neural-network/assets/88457743/b7c06cac-558a-4284-885a-e22d86575e49">

Write "NeuralNetworkScriptableObject" as the name of the neural network.

Right click on the inspector and choose "Load Neural Network from Scriptable Object".

Focus on the neural network Gameobject in the scene to visualize the Gizmos. You can modify the Gizmos visuals according to your preferences with the Visualizer Properties in the inspector.

<img width="323" alt="neural_network" src="https://github.com/marcusaasjensen/unity-neural-network/assets/88457743/1f8dfe8a-72b6-4901-a738-981f74ad209f">


This neural network has already been trained. Right click on the inspector and choose "Test Neural Network".

The results will appear in the Unity's Console. These are the predictions for both inputs of a XOR Gate.

<img width="193" alt="results" src="https://github.com/marcusaasjensen/unity-neural-network/assets/88457743/303b4d17-50b1-4e0d-8133-e15b92aaf8b2">

You can generate your own neural network by using the different functions implemented by right-clicking on the inspector.

When you are satisfied with the results of your neural network, you can save it as a scriptable object: right click on the inspector and choose "Save Neural Network to Scriptable Object." The name of the asset will be the name of the neural network.

Enjoy.


