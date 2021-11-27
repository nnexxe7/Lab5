using NPOI.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
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

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = textBox2.Text;
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                while (fs.Read(b, 0, b.Length) > 0)
                {
                    richTextBox1.Text = (temp.GetString(b));
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = textBox2.Text;
            File.WriteAllText(path, richTextBox1.Text);

            ///////////////////////////////////////////////////////////////////////
            if (checkBox1.CheckState == CheckState.Unchecked & checkBox2.CheckState == CheckState.Unchecked)
            {
                //nic sie nie robi
            }
            if (checkBox1.CheckState == CheckState.Checked & checkBox2.CheckState == CheckState.Unchecked)
            {
                // robi sie kompresja
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    using (Stream s = File.Create(path))
                    {
                        using (Stream ds = new DeflateStream(s, CompressionMode.Compress))
                        {
                            for (byte i = 0; i < 100; i++)
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
                    FileStream stream = new FileStream(path ,
                    FileMode.OpenOrCreate, FileAccess.Write);

                    DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

                    cryptic.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
                    cryptic.IV = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");

                    CryptoStream crStream = new CryptoStream(stream,
                    cryptic.CreateEncryptor(), CryptoStreamMode.Write);


                    byte[] data = ASCIIEncoding.ASCII.GetBytes("1234567890");

                    crStream.Write(data, 0, data.Length);
                    crStream.Close();
                    stream.Close();
                }
            }
            if (checkBox1.CheckState == CheckState.Checked & checkBox2.CheckState == CheckState.Checked)
            {
                //robi sie to i to
            }

            ////////////////////////////////////////////////////////////////////////////
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
