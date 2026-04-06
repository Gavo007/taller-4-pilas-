using System;
using System;
using System.Windows.Forms;

namespace ExpressionEvaluator.UI.Win
{
    public partial class Form1 : Form
    {
        public Form1() { InitializeComponent(); }

        private void btnGenerico_Click(object sender, EventArgs e)
        {
            if (sender is Button boton)
            {
                if (lblPantalla.Text == "Error") lblPantalla.Text = "";
                lblPantalla.Text += boton.Text;
            }
        }
        private void btnIgual_Click(object sender, EventArgs e)
        {
            try
            {
                string expresion = lblPantalla.Text;
                double resultado = ExpressionEvaluator.Core.Evaluator.Evaluate(expresion);
                lblPantalla.Text = resultado.ToString();
            }
            catch { lblPantalla.Text = "Error"; }
        }

   
        private void btnClear_Click(object sender, EventArgs e)
        {
            lblPantalla.Text = "";
        }

        
        private void btnDetale_Click(object sender, EventArgs e)
        {
            if (lblPantalla.Text.Length > 0 && lblPantalla.Text != "Error")
            {
               
                lblPantalla.Text = lblPantalla.Text.Substring(0, lblPantalla.Text.Length - 1);
            }
        }
    }
}
