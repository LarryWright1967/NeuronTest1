using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NeuronDemo;

public class TrainingLoop
{
    private Form1 _mainForm;

    public TrainingLoop(Form1 form)
    {
        _mainForm = form;
    }
    public void Run()
    {
        SimpleNeuron neuron = new SimpleNeuron();
        double input = 2.0;
        double target = 0.0;

        var tb = _mainForm.textBox1;

        tb.Text = tb.Text + ("Iteration  |  Input    |  Target  | Output |    Error" + Environment.NewLine);
        //tb.Text = tb.Text + ("--------------------------" + Environment.NewLine);

        for (int i = 0; i <= 10000000; i++)
        {
            double output = neuron.Forward(input);
            double error = Math.Pow(output - target, 2); // Mean Squared Error

            if (i % 50000 == 0) // Print every 10th iteration
            {
                tb.Text = tb.Text + ($"{i.ToString("00000000")} | {input:F5} | {target:F5} | {output:F5} | {error:F9}" + Environment.NewLine);
            }

            neuron.Train(input, target);
        }
    }
}
