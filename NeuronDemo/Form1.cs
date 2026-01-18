namespace NeuronDemo;

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
