﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Xml.Serialization;
using Windows.Media.Playback;
using NAudio.Wave;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SoundBoard
{
    public partial class MainWindow : Window
    {
        static string[] path = new string[18];
        static string[] name = new string[18];
        public static string[] shortcut = new string[18];
        static bool[] onoff = new bool[18];
        static int[] volume = new int[18];
        static WaveOutEvent[] audioPlayers = new WaveOutEvent[18];
        public static int Number = 0;
        static int CheckBoxNum = 0;
        public static Window1 win1 = new Window1();
        static int[] playingCheck = new int[18];
        IntPtr mainHandle = Process.GetCurrentProcess().MainWindowHandle;

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        const int MYACTION_HOTKEY_ID = 0;

        public MainWindow()
        {

            for (int i = 0; i < 18; i++)
            {
                shortcut[i] = "Shortcut Sequence";
                volume[i] = 1;
                name[i] = "Title";
                WaveOutEvent audioplayer = new WaveOutEvent();
                audioPlayers[i] = audioplayer;
                audioPlayers[i].NumberOfBuffers = 0;
                UnregisterHotKey(mainHandle, i);
            }
            InitializeComponent();
            DeserializeObject();
            Update();
            for (int i = 0; i < 18; i++)
            {
                Number = i;
                if (shortcut[i] != "Shortcut Sequence")
                {
                    ShortcutFactory();
                }
            }
        }

        [Serializable]
        public class Configuration
        {
            public string[] FilePath { get; set; }
            public string[] FileName { get; set; }
            public string[] Shortcut { get; set; }
            public bool[] OnOff { get; set; }
            public int[] Volume { get; set; }
        }
        
        void Save(object sender, RoutedEventArgs e)
        {
            Configuration MainConfig = new Configuration();
            MainConfig.FilePath = path;
            MainConfig.FileName = name;
            //MainConfig.Shortcut = shortcut;
            MainConfig.OnOff = onoff;
            MainConfig.Volume = volume;
            Serialize(MainConfig);
            string messageBoxText = "Everything has been saved";
            string caption = "Notification";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result;
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }
        void File(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int TextBoxNum = Int32.Parse(button.Name.Substring(4));
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "Clip"; // Default file name
                dlg.DefaultExt = ".mp3"; // Default file extension
                dlg.Filter = "Audio Files(*.mp3;*.mp4;)|*.mp3;*.mp4"; // Filter files by extension

                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    path[TextBoxNum] = dlg.FileName;
                    var x = (TextBox)this.FindName("Title" + TextBoxNum);
                    name[TextBoxNum] = dlg.SafeFileName;
                    x.Text = name[TextBoxNum];

                }
            }

        }
        public void ShortcutEnter(object sender, EventArgs e)
        {
            /*Button button = (Button)sender;
            Number = Int32.Parse(button.Name.Substring(5));
            Window1 win1 = new Window1();
            win1.Show();*/
        }
        
        private void ShortcutFactory()
        {
            string[] splitShortcut = shortcut[Number].Split('-');
            var key = win1.keyMain[win1.indexOfArray];
            switch (splitShortcut[1])
            {
                case "truefalsefalse":
                    RegisterHotKey(mainHandle, Number, 2, (int)win1.key);
                    break;
                case "falsetruefalse":
                    RegisterHotKey(mainHandle, Number, 1, (int)win1.key);
                    break;
                case "falsefalsetrue":
                    RegisterHotKey(mainHandle, Number, 4, (int)win1.key);
                    break;
                case "truetruefalse":
                    RegisterHotKey(mainHandle, Number, 3, (int)win1.key);
                    break;
                case "truefalsetrue":
                    RegisterHotKey(mainHandle, Number, 6, (int)win1.key);
                    break;
                case "flasetruetrue":
                    RegisterHotKey(mainHandle, Number, 5, (int)win1.key);
                    break;
                case "truetruetrue":
                    RegisterHotKey(mainHandle, Number, 7, (int)win1.key);
                    break;
            }
        }

        private void PlayPause(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.ToString() == "Play")
            {
                Number = Int32.Parse(button.Name.Substring(9));
                button.Content = "Pause";
                InitializeSound();
            }
            else
            {
                Number = Int32.Parse(button.Name.Substring(9));
                button.Content = "Play";
                var OPD = audioPlayers[Number];
                InitializeSound();
            }

        }
        private void VolumeChanged(object sender, EventArgs e)
        {
            Slider slider = (Slider)sender;
            audioPlayers[Number].Volume = Convert.ToSingle(slider.Value);
        }


        void CheckedBox(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            CheckBoxNum = Int32.Parse(checkbox.Name.Substring(8));

            onoff[CheckBoxNum] = true;
        }
        void UnCheckedBox(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            CheckBoxNum = Int32.Parse(checkbox.Name.Substring(8));

            onoff[CheckBoxNum] = false;
        }

        static void InitializeSound()
        {
            if (onoff[CheckBoxNum] == false | name[Number] == "Title")
            {
                return;
            }
            AudioFileReader audioFile = new AudioFileReader(path[Number]);
            if (playingCheck[Number] == 1)
            {
                audioPlayers[Number].Stop();
                audioPlayers[Number].Dispose();
                playingCheck[Number] = 0;
            } else
            {
                audioPlayers[Number].Init(audioFile);
                audioPlayers[Number].Play();
                playingCheck[Number] = 1;
            }
        }
        private void DeserializeObject(string filename = "C:\\Users\\kiril\\Documents\\Configuration.xml")
        {
            Configuration g;

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
                g = (Configuration)serializer.Deserialize(fs);

            }
            path = g.FilePath;
            name = g.FileName;
            shortcut = g.Shortcut;
            onoff = g.OnOff;
            volume = g.Volume;
        }
        void Serialize(object MainConfig)
        {
            using (FileStream fs = new FileStream("C:\\Users\\kiril\\Documents\\Configuration.xml", FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
                serializer.Serialize(fs, MainConfig);
            }
        }
        void Update()
        {
            for (int i = 0; i < 18; i++)
            {
                var t = (TextBox)this.FindName("Title" + i);
                var c = (CheckBox)this.FindName("Checkbox" + i);
                var v = (Slider)this.FindName("Volume" + i);
                t.Text = name[i];
                c.IsChecked = onoff[i];
                v.Value = volume[i];
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ShortcutSequence1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void unregisterAll(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < 18; i++)
            {
                UnregisterHotKey(mainHandle, i);
                shortcut[i] = "Shortcut Sequence";
            }
        }
    }
}
