using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Wordle
{
    public partial class Form1 : Form
    {
        string word = "ACUTE";
        int tries = 0;

        List<char> letters = new List<char>{ 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                                                 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public Form1()
        {
            InitializeComponent();

            txtWord1.TextChanged += (sender, e) => txtWord_TextChanged(sender, e, 0);
            txtWord2.TextChanged += (sender, e) => txtWord_TextChanged(sender, e, 1);
            txtWord3.TextChanged += (sender, e) => txtWord_TextChanged(sender, e, 2);
            txtWord4.TextChanged += (sender, e) => txtWord_TextChanged(sender, e, 3);
            txtWord5.TextChanged += (sender, e) => txtWord_TextChanged(sender, e, 4);
            txtWord6.TextChanged += (sender, e) => txtWord_TextChanged(sender, e, 5);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtWord1;
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            TextBox[] textboxes = { txtWord1, txtWord2, txtWord3, txtWord4, txtWord5, txtWord6 };
            Label[,] allLabels = new Label[6, 5] { { lbl1, lbl2, lbl3, lbl4, lbl5 },
                                                { lbl6, lbl7, lbl8, lbl9, lbl10 },
                                                { lbl11, lbl12, lbl13, lbl14, lbl15 },
                                                { lbl16, lbl17, lbl18, lbl19, lbl20 },
                                                { lbl21, lbl22, lbl23, lbl24, lbl25 },
                                                { lbl26, lbl27, lbl28, lbl29, lbl30 } };
            Label[] labels = {allLabels[tries,0], allLabels[tries,1], allLabels[tries,2], allLabels[tries,3], allLabels[tries,4]};

            TextBox currentTb = textboxes[tries];
            string guess = currentTb.Text;

            lblMessage.Text = "";

            if (guess.Length != 5)
            {
                lblMessage.Text = "Warning: Word must be 5 letters!";
                this.ActiveControl = currentTb;
            }
            else
            {
                foreach (char c in guess)
                {
                    letters.Remove(c);
                }
                lblLetters.Text = "Available Letters: ";
                foreach (char c in letters)
                {
                    lblLetters.Text += c + " ";
                }

                tries++;
                for (int i = 0; i < 5; i++)
                {
                    if (word[i] == guess[i])
                    {
                        labels[i].BackColor = Color.LimeGreen;
                    }
                    else if (word.Contains(guess[i]))
                    {
                        labels[i].BackColor = Color.Gold;
                    } else
                    {
                        labels[i].BackColor = Color.DarkGray;
                    }
                    await Task.Delay(350);

                }
                if (guess == word)
                {
                    lblMessage.Text = "Congratulations! You got it in " + tries + " guesses!";
                    btnSubmit.Enabled = false;
                } else if (tries == 6)
                {
                    lblMessage.Text = "Sorry, the word was " + word + ".";
                    btnSubmit.Enabled = false;
                } else
                {
                    currentTb.Enabled = false;
                    currentTb.Visible = false;
                    textboxes[tries].Enabled = true;
                    this.ActiveControl = textboxes[tries];
                }
                
            }    

        }

        private void txtWord_TextChanged(object sender, EventArgs e, int num)
        {
            Label[,] allLabels = new Label[6, 5] { { lbl1, lbl2, lbl3, lbl4, lbl5 },
                                                { lbl6, lbl7, lbl8, lbl9, lbl10 },
                                                { lbl11, lbl12, lbl13, lbl14, lbl15 },
                                                { lbl16, lbl17, lbl18, lbl19, lbl20 },
                                                { lbl21, lbl22, lbl23, lbl24, lbl25 },
                                                { lbl26, lbl27, lbl28, lbl29, lbl30 } };
            Label[] labels = { allLabels[num, 0], allLabels[num, 1], allLabels[num, 2], allLabels[num, 3], allLabels[num, 4] };

            foreach (Label l in labels)
            {
                l.Text = "";
            }

            string guess = (sender as TextBox).Text;
            for (int i = 0; i < guess.Length; i++)
            {
                labels[i].Text = Convert.ToString(guess[i]);
            }
        }
    }
}
