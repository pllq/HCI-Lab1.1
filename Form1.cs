using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Lab1._1
{
    public partial class Form1 : Form
    {


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void Form1_Load(object sender, EventArgs e){}

        //Buttons

        private sbyte user_has_diff_with_writing_text;

        private void show_message_button_Click(object sender, EventArgs e)
        {
            string message_box_show = "Please, write text, which you want to output, into the text box.";

            switch (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                case false:
                    user_has_diff_with_writing_text = 0;
                    MessageBox.Show(textBox1.Text, "Show Message");
                    break;

                case true:
                    user_has_diff_with_writing_text++;

                    if (user_has_diff_with_writing_text >= 3 && user_has_diff_with_writing_text < 10)
                    {
                        MessageBox.Show(message_box_show, "Text box is empty");
                    }

                    if (user_has_diff_with_writing_text >= 10)
                    {
                        user_has_diff_with_writing_text = 3;
                        string very_rare_message = "You either stupid of have nothing to do. I will repeat one more time:\n" + message_box_show.ToUpper();
                        MessageBox.Show(very_rare_message, "Text box is empty");
                    }

                    break;
            }
        }

        private void default_message1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is my default message.";
        }

        private void default_message2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is another default message.";
        }

        private void execute_button_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    textBox1.Text = "";
                    break;

                case 1:
                    if (String.IsNullOrWhiteSpace(textBox1.Text)) 
                    {
                        MessageBox.Show("In order to copy text, first of all you have to write it.", "Text box is empty");
                    }
                    else 
                    {
                        Clipboard.SetDataObject(textBox1.Text);
                    }
                    break;

                case 2:
                    textBox1.Text = Clipboard.GetText();
                    break;
            }
        }

        private void check_blocks_button_Click(object sender, EventArgs e)
        {
            CheckBox[] no_checked = new CheckBox[] { show_message_actions_checkBox, show_program_actions_checkBox, 
                                             enable_message_actions_checkBox, enable_program_actions_checkBox };
            int amount_of_checked = 0;
            int[] my_array = new int[no_checked.Length];

            for (int i = 0; i < no_checked.Length; i++)
            {
                if (no_checked[i].Checked == true)
                {
                    my_array[amount_of_checked] = i;
                    amount_of_checked++;
                }
            }

            if(amount_of_checked == 0) 
            {
                MessageBox.Show("Non of the checkboxes are checked.", "Checkboxes aren't checked");
                return;
            }

            if (!(my_array.Length == amount_of_checked)) 
            {
                Array.Resize(ref my_array, amount_of_checked);
            }

            CheckBox[] new_checked = new CheckBox[amount_of_checked];

            for (int i = 0; i < new_checked.Length; i++) 
            {
                new_checked[i] = no_checked[my_array[i]];                
            }

            string[] my_string = new string[new_checked.Length];

            for (int i = 0; i < my_string.Length; i++)
            {
                my_string[i] = new_checked[i].Text;
            }

            var result = new StringBuilder();
            foreach (string all_checked in my_string)
            {
                result.Append($"{all_checked} checkbox is checked\n");
            }
            MessageBox.Show(result.ToString(), "Checkboxes that are checked");
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        //

        private void textBox1_TextChanged(object sender, EventArgs e){}

        //

        private void label1_Click(object sender, EventArgs e){}

        //

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){}

        //Checkboxes

        private void show_message_actions_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            switch (show_message_actions_checkBox.Checked) 
            { 

                case true:
                    show_message_button.Enabled = true;
                    default_message1.Enabled = true;
                    default_message2.Enabled = true;
                    break;

                case false:
                    show_message_button.Enabled = false;
                    default_message1.Enabled = false;
                    default_message2.Enabled = false;
                    break;

            }
        }

        private void show_program_actions_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            switch (show_program_actions_checkBox.Checked)
            {
                case true:
                    execute_button.Enabled = true;
                    break;

                case false:
                    execute_button.Enabled = false;
                    break;

            }
        }

        private void enable_message_actions_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            switch (enable_message_actions_checkBox.Checked)
            {

                case true:
                    textBox1.Enabled = true;
                    break;

                case false:
                    textBox1.Enabled = false;
                    break;

            }
        }

        private void enable_program_actions_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            switch (enable_program_actions_checkBox.Checked)
            {
                case true:
                    comboBox1.Enabled = true;
                    break;

                case false:
                    comboBox1.Enabled = false;
                    break;

            }
        }

        //For custom panel (border) (functinality to move)

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]

        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);

        private void panel1_Paint(object sender, PaintEventArgs e){}

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        //
    }
}