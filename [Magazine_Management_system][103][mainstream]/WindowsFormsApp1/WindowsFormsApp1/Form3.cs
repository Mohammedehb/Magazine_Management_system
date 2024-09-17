using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;

namespace WindowsFormsApp1
{
    
    public partial class Form3 : Form
    {
        CrystalReport1 CR;
        public Form3()
        {
            InitializeComponent();
        }

        

        private void Form3_Load(object sender, EventArgs e)
        {
            CR = new CrystalReport1();
            foreach (ParameterDiscreteValue nameM in CR.ParameterFields[0].DefaultValues)
                name_magzine_cmb.Items.Add(nameM.Value);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CR.SetParameterValue(0, name_magzine_cmb.Text);     
            crystalReportViewer1.ReportSource = CR;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
