using System;

public static class ActivationFunctionLibrary
{
    public enum ActivationFunctionName
    {
        Sigmoid,
        Tanh,
        ReLu,
        LeakyReLu,
        Elu,
        SoftPlus,
        Swish,
        Mish,
        BinaryStep,
        Gaussian
    }
    
    public delegate double ActivationFunction(double x);

    private static readonly ActivationFunction[] ActivationFunctions = { Sigmoid, Tanh, ReLu, LeakyReLu, Elu, SoftPlus, Swish, Mish, BinaryStep, Gaussian };
    
    public static ActivationFunction GetFunction(ActivationFunctionName name) => ActivationFunctions[(int)name];

    private static double Sigmoid(double x) => 1 / (1 + Math.Exp(-x));
    private static double Tanh(double x)  => Math.Tanh(x);
    private static double ReLu(double x) => Math.Max(0, x);
    private static double LeakyReLu(double x) => Math.Max(0.01 * x, x);
    private static double Elu(double x) => x >= 0 ? x : 0.01 * (Math.Exp(x) - 1);
    private static double SoftPlus(double x) => Math.Log(1 + Math.Exp(x));
    private static double Swish(double x) => x * Sigmoid(x);
    private static double Mish(double x) => x * Math.Tanh(SoftPlus(x));
    private static double BinaryStep(double x) => x < 0 ? 0 : 1;
    private static double Gaussian(double x) => Math.Exp(-Math.Pow(x, 2));
}