

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NeuronDemo
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var trainingLoop = new TrainingLoop(this);
            trainingLoop.Run();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}

namespace NeuronDemo
{
    public class NeuronLayer
    {
        public List<SimpleNeuron> Neurons = new List<SimpleNeuron>();

        public NeuronLayer(int neuronCount)
        {
            for (int i = 0; i < neuronCount; i++)
                Neurons.Add(new SimpleNeuron());
        }

        // Forward pass for the whole layer
        public double[] Forward(double input)
        {
            double[] outputs = new double[Neurons.Count];

            for (int i = 0; i < Neurons.Count; i++)
                outputs[i] = Neurons[i].Forward(input);

            return outputs;
        }

        // Train each neuron independently
        public void Train(double input, double[] targets)
        {
            for (int i = 0; i < Neurons.Count; i++)
                Neurons[i].Train(input, targets[i]);
        }
    }
}


namespace NeuronDemo
{

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
}

namespace NeuronDemo
{

    public class TrainingLoop
    {
        private ScottPlot.Coordinates[] ToPoints(double[] xs, double[] ys)
        {
            var pts = new ScottPlot.Coordinates[xs.Length];
            for (int i = 0; i < xs.Length; i++)
                pts[i] = new ScottPlot.Coordinates(xs[i], ys[i]);
            return pts;
        }


        private Form1 _mainForm;

        public TrainingLoop(Form1 form)
        {
            _mainForm = form;
        }
        public void Run()
        {
            var layer = new NeuronLayer(2);

            double input = 2.0;
            double[] targets = { 0.0, 1.0 };

            var tb = _mainForm.textBox1;

            List<double> out1History = new List<double>();
            List<double> out2History = new List<double>();
            List<double> iterationHistory = new List<double>();

            for (int i = 0; i < 100000; i++)
            {
                var outputs = layer.Forward(input);

                out1History.Add(outputs[0]);
                out2History.Add(outputs[1]);
                iterationHistory.Add(i);

                // Print occasionally
                if (i % 10000 == 0)
                {
                    tb.AppendText($"{i} | Out1={outputs[0]:F5} | Out2={outputs[1]:F5}" + Environment.NewLine);
                }

                layer.Train(input, targets);
            }

            tb.AppendText($"Point Count = {iterationHistory.Count}" + Environment.NewLine);

            var plt = _mainForm.formsPlot1.Plot;

            plt.Clear();

            var points1 = ToPoints(iterationHistory.ToArray(), out1History.ToArray());
            var points2 = ToPoints(iterationHistory.ToArray(), out2History.ToArray());

            plt.Add.Scatter(points1, ScottPlot.Color.FromSDColor(Color.Brown));
            plt.Add.Scatter(points2, ScottPlot.Color.FromSDColor(Color.DarkCyan));

            plt.Legend.IsVisible = true;
            plt.Title("Neuron Outputs Over Training");
            plt.XLabel("Iteration");
            plt.YLabel("Output Value");
            plt.Axes.AutoScale();

            _mainForm.formsPlot1.Refresh();

        }
    }
}

namespace NeuronDemo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            textBox1 = new TextBox();
            button2 = new Button();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(0, 0);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(413, 38);
            button1.TabIndex = 0;
            button1.Text = "Run";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            textBox1.Location = new Point(0, 48);
            textBox1.Margin = new Padding(4, 5, 4, 5);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(646, 692);
            textBox1.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(421, 0);
            button2.Margin = new Padding(4, 5, 4, 5);
            button2.Name = "button2";
            button2.Size = new Size(413, 38);
            button2.TabIndex = 2;
            button2.Text = "Clear";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // formsPlot1
            // 
            formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            formsPlot1.DisplayScale = 1.5F;
            formsPlot1.Location = new Point(653, 48);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(958, 702);
            formsPlot1.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1613, 748);
            Controls.Add(formsPlot1);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        public TextBox textBox1;
        private Button button2;
        public ScottPlot.WinForms.FormsPlot formsPlot1;
    }
}
