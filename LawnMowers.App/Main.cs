using LawnMowers.Domain.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Windows.Forms;
using LawnMowers.Service;

namespace LawnMowers.App
{
    public partial class Main : Form
    {
        private ILawnLogic _lawnLogic;

        public Main()
        {
            InitializeComponent();
            Setup();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBoxInput.Text = string.Empty;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                _lawnLogic.BuildLawnAndMowers(txtBoxInput.Text);
                MessageBox.Show(_lawnLogic.MoveLawnMowers(), "Mower Finishing Positions");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Uh-oh! Looks like some of your input is incorrect. Please adjust and try again!", "Mower Finishing Positions");
            }
        }

        private void Setup()
        {
            var container = new UnityContainer();
            _lawnLogic = container.Resolve<LawnLogic>();
        }
    }
}
