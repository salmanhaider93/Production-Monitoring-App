using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Production_monitoring_tool
{
    public partial class Form2 : Form
    {

        public string peregrineGoal { get; private set; }
        public string petrel3Goal { get; private set; }
        public string perdix2Goal { get; private set; }
        public string swiftGoal { get; private set; }
        public string tericGoal { get; private set; }
        public string nerd2Goal { get; private set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public static string SetValueForText1 = "";
        private void send_button_Click(object sender, EventArgs e)
        {

            peregrineGoal = peregrine_goal_label_textBox.Text;
            petrel3Goal = petrel3_goal_label_textBox.Text;
            perdix2Goal = perdix2_goal_label_textBox.Text;
            swiftGoal = swift_goal_label_textBox.Text;
            tericGoal = teric_goal_label_textBox.Text;
            nerd2Goal = nerd2_goal_label_textBox.Text;

            this.Close();

        }

        

    }
}
