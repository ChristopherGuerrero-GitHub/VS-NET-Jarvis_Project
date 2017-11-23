using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//added the following api below
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;


namespace Jarvis_Project
{
    public partial class Form1 : Form
    {
        //create objects for speech and synthesizer
        SpeechRecognitionEngine speechRecon = new SpeechRecognitionEngine();
        SpeechSynthesizer jarvis = new SpeechSynthesizer();

        /*
         * Form method initializes the speech recognition event handler, set audio device and 
         * LoadGrammar method which will get the command text file.
         */
        public Form1()
        {
            InitializeComponent();
            speechRecon.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechReconSpeechRecognized);
            LoadGrammar();
            speechRecon.SetInputToDefaultAudioDevice();
            speechRecon.RecognizeAsync(RecognizeMode.Multiple);
                
        }

        /*
         * LoadGrammar will open command text file from local machine and add the words from the text file
         * which match the comments the user will speak and Jarvis will look up for a match into a Choice 
         * object. That object will be added to a Grammar object builder to be passed to the speech recognition class
         * where a LoadGrammar method will be invoked.
         */
        private void LoadGrammar()
        {
            Choices texts = new Choices();
            string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\commands.txt");
            texts.Add(lines);
            Grammar wordLists = new Grammar(new GrammarBuilder(texts));
            speechRecon.LoadGrammar(wordLists);
        }

        /*
         * The SpeechReconSpeechRecognized method contains the user's comments that Jarvis will
         * recognize and respond too.
         */
        private void SpeechReconSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            richTextBox1.Text = e.Result.Text;
            string speech = e.Result.Text;
            if (speech == "hello jarvis")
            {
                jarvis.Speak("hello sir, I am ready to be of service to you");
            }
            if (speech == "how are you")
            {
                jarvis.Speak("I am good sir thank you for asking");

            }
            if (speech == "open youtube")
            {
                jarvis.Speak("Right away sir, initializing Firefox");
                System.Diagnostics.Process.Start("https://www.youtube.com/");
            }
            if (speech == "open weather")
            {
                jarvis.Speak("Of course sir opening browser for weather forecast");
                System.Diagnostics.Process.Start("https://weather.com/");

            }
            if (speech == "good bye jarvis")
            {
                jarvis.Speak("Happy to be of service to you, have a great day sir, shutting systems down");
                Application.Exit();
            }


        }

        
    }
}
