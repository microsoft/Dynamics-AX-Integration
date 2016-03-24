using System;
using System.Windows.Forms;

namespace RecurringIntegrationApp
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }       

        private void OUTfade_Tick(object sender, EventArgs e)
        {
            if (this.Opacity == 1)
            {
                OUTfade.Enabled = false;
                timer1.Enabled = true;
                return;
            }
            this.Opacity += 0.01;
        }
        int count = 30;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (count == 0)
            {
                timer1.Enabled = false;
                INfade.Enabled = true;
                return;
            }
            count -= 1;
        }

        private void INfade_Tick(object sender, EventArgs e)
        {
            if (this.Opacity == 0)
            {
                INfade.Enabled = false;
                DIXFRecurringJobsProcessor DIXFRecurring = new DIXFRecurringJobsProcessor();
                DIXFRecurring.Show();
                this.Hide();
                return;
            }
            this.Opacity -= 0.01;
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = BackColor;
        }

    }
}
