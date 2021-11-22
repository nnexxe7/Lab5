using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();



        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "txt files (*.txt)|.txt|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = textBox2.Text;
            richTextBox1.Text = File.ReadAllText(path);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ofd.Filter = "txt files (*.txt)|.txt|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(ofd.FileName) == ".txt")
                {
                    richTextBox1.SaveFile(ofd.FileName, RichTextBoxStreamType.PlainText);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            textBox2.Text = "";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Unchecked & checkBox2.CheckState == CheckState.Unchecked)
            {
                //nic sie nie robi
            }
            if (checkBox1.CheckState == CheckState.Checked & checkBox2.CheckState == CheckState.Unchecked)
            {
                // robi sie kompresja
                string path = textBox2.Text;
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    using (Stream s = File.Create(path+".rar"))
                    {
                        using (Stream ds = new DeflateStream(s, CompressionMode.Compress))
                        {
                            for (byte i = 0; i > 100; i++)
                            {
                                ds.WriteByte(i);
                            }

                        }
                    }
                }
            }

            if (checkBox1.CheckState == CheckState.Unchecked & checkBox2.CheckState == CheckState.Checked)
            {
                // robi sie szyfrowanie
                if (checkBox2.CheckState == CheckState.Checked)
                {
                    File.Encrypt(textBox2.Text);
                }
                if (checkBox2.CheckState == CheckState.Unchecked)
                {
                    File.Decrypt(textBox2.Text);
                }
            }
            if (checkBox1.CheckState == CheckState.Checked & checkBox2.CheckState == CheckState.Checked)
            {
                //robi sie to i to
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
