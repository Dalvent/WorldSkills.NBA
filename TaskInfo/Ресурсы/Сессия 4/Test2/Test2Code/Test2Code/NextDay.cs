using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test2Code
{
    public partial class NextDay : Form
    {
        Day day = new Day();
        public NextDay()
        {
            InitializeComponent();        
        }
        public void initDate()
        {
            day.Year = int.Parse(this.txt_Year.Text.ToString());
            day.Month = int.Parse(this.txt_Month.Text.ToString());
            day.Date = int.Parse(this.txt_Date.Text.ToString());        
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            this.initDate();

            if (!Validation.ValidDate(day))
            {
                MessageBox.Show("The date you input is invalid!");

                return;
            }

            this.label5.Text = Validation.IncrementDate(day).ToShortDateString();
        }

    }
}
