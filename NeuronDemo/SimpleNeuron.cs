using System;
using System.Collections.Generic;
using System.Text;

namespace NeuronDemo;

public class SimpleNeuron
{
    public double Weight = 0.5;
    public double Bias = 0.1;
    public double LearningRate = 0.5;

    // Sigmoid Activation: f(x) = 1 / (1 + e^-x)
    private double Sigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));

    // Derivative of Sigmoid for Backpropagation
    private double SigmoidDerivative(double output) => output * (1.0 - output);

    public double Forward(double input)
    {
        double z = (input * Weight) + Bias;
        return Sigmoid(z);
    }

    public void Train(double input, double target)
    {
        // 1. Forward Pass
        double output = Forward(input);

        // 2. Calculate Error (Mean Squared Error derivative)
        double error = output - target;

        // 3. Backward Pass (Chain Rule)
        double delta = error * SigmoidDerivative(output);

        // 4. Update Parameters
        Weight -= LearningRate * delta * input;
        Bias -= LearningRate * delta;
    }
}
