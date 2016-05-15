using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace SpeedTyping {
    public partial class PlayForm : Form {
        public String[] words { get; set; }
        string newWord;
        int counter, points, seconds;
        Label[] letters;
        SoundPlayer sound, sound1;
        Queue<char> wordsStack;
        public PlayForm() {
            words = new String[] { "tools", "hello", "televison", "invitation", "suggestion", "notice", "anchor", "thief", "share", "automobile", "birthday", "world", "player", "person", "happiness", "encyclopedia" };
            counter = 0;
            points = 0;
            seconds = 60;
            //sound = new SoundPlayer(@"C:\Users\denni\Documents\Visual Studio 2015\Projects\SpeedTyping\SpeedTyping\bin\Debug\beep1.wav");
            //sound1 = new SoundPlayer(@"C:\Users\denni\Documents\Visual Studio 2015\Projects\SpeedTyping\SpeedTyping\bin\Debug\beep2.wav");
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e) {

        }
        public void getNewWord() {
            Random ra = new Random();
            newWord = words[ra.Next(0, words.Count())];
        }

        private void button1_Click(object sender, EventArgs e) {
            getNewWord();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
            timer1.Start();
            timer1.Interval = 1000;
            char currentChar = wordsStack.Dequeue();
            if (e.KeyChar == currentChar) {
                letters[counter].BackColor = Color.Green;
                points++;
                label6.Text = points.ToString();
            }
            else {
                letters[counter].BackColor = Color.Crimson;
                //  sound1.Play();
                points--;
                label6.Text = points.ToString();
            }
            counter++;
            if (wordsStack.Count == 0) {
                //sound.Play();
                loadStack();
                e.Handled = true;
            }
        }

        public void loadStack() {
            getNewWord();
            for (int i = 0; i < newWord.Length; i++) {
                wordsStack.Enqueue(newWord[i]);
            }
            fillingLabels();
            textBox1.Text = "";
        }

        private void PlayForm_Load(object sender, EventArgs e) {
            letter1.Text = newWord;
            wordsStack = new Queue<char>();
            letters = new Label[] { letter1, letter2, letter3, letter4, letter5, letter6, letter7, letter8, letter9, letter10, letter11, letter12 };
            loadStack();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e) {

        }

        private void label4_Click(object sender, EventArgs e) {

        }

        public void fillingLabels() {
            int halfOfArray = 6;
            int starter = halfOfArray - (newWord.Length / 2);
            if (starter > 0) {
                starter++;
            }
            counter = starter;
            for (int i = 0; i < 12; i++) {
                letters[i].BackColor = Color.Transparent;
                letters[i].Text = "";
            }
            for (int i = 0; i < newWord.Length; i++) {
                letters[starter].Text = newWord[i] + "";
                starter++;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        private void PlayForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if ( seconds != 0) {
                if (seconds > 0) {
                    if (seconds > 9) {
                        label5.Text = seconds.ToString();
                        seconds--;
                    }
                    else {
                        label5.Text = "0" + seconds;
                        seconds--;
                    }
                }
            }
            else {
                timer1.Stop();
                DialogResult result = MessageBox.Show("Your Score is " + points + "\nPlay again?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) {
                    this.Close();
                    PlayForm form = new PlayForm();
                    form.Show();
                }
                else {
                    Application.Exit();
                }
            }
        }
    }
}
