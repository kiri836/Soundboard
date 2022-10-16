using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SoundBoard
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public string[] AllKeys = new string[] { "BACKSPACE", "TAB", "CLEAR", "ENTER", "PAUSE", "CAPS LOCK", "ESC", "SPACEBAR", "PAGE UP", "PAGE DOWN", "END", "HOME", "LEFT ARROW", "UP ARROW", "RIGHT ARROW", "DOWN ARROW", "SELECT", "PRINT", "EXECUTE", "PRINT SCREEN", "INS", "DEL", "HELP", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "NUMPAD 0", "NUMPAD 1", "NUMPAD 2", "NUMPAD 3", "NUMPAD 4", "NUMPAD 5", "NUMPAD 6", "NUMPAD 7", "NUMPAD 8", "NUMPAD 9", "MULTIPLY", "ADD", "SEPERATOR", "SUBTRACT", "DECIMAL", "DIVIDE", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "F13", "F14", "F15", "F16", "F17", "F18", "F19", "F20", "F21", "F22", "F23", "F24" };
        int[] IntKeys = new int[] { 0x08, 0x09, 0x0C, 0x0D, 0x013, 0x014, 0x1B, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A, 0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F, 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0x7B, 0x7C, 0x7D, 0x7E, 0x7F, 0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87 };
        public List<Key> keyMain = new List<Key> { Key.Back, Key.Tab, Key.Clear, Key.Enter, Key.Pause, Key.Capital, Key.Escape, Key.Space, Key.PageUp, Key.PageDown, Key.End, Key.Home, Key.Left, Key.Up, Key.Right, Key.Down, Key.Select, Key.Print, Key.Execute, Key.PrintScreen, Key.Insert, Key.Delete, Key.Help, Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9, Key.A, Key.B, Key.C, Key.C, Key.D, Key.E, Key.F, Key.G, Key.H, Key.I, Key.J, Key.K, Key.L, Key.M, Key.N, Key.O, Key.P, Key.Q, Key.R, Key.S, Key.T, Key.U, Key.V, Key.W, Key.X, Key.Y, Key.Z, Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9, Key.NumPad9, Key.Multiply, Key.Add, Key.Separator, Key.Decimal, Key.Divide, Key.F1, Key.F2, Key.F3, Key.F4, Key.F5, Key.F6, Key.F7, Key.F8, Key.F9, Key.F10, Key.F11, Key.F12, Key.F13, Key.F14, Key.F15, Key.F16, Key.F17, Key.F18, Key.F19, Key.F20, Key.F21, Key.F22, Key.F23, Key.F24 };
        public string textBoxVar;
        public int indexOfArray;
        public Key key;
        public bool ctrl;
        public bool alt;
        public bool shift;

        public Window1()
        {
            InitializeComponent();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
           
            if (Array.IndexOf(AllKeys, textBoxVar) >= 0){
                indexOfArray = Array.IndexOf(AllKeys, textBoxVar);
                MainWindow.shortcut[MainWindow.Number] = AllKeys[indexOfArray] + "-" + ctrl.ToString() + alt.ToString() + shift.ToString();
                if (ctrl == false & alt == false & shift == false)
                {
                    string messageBoxText = "You need atleast one modifier for a shortcut.";
                    string caption = "Notification";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBoxResult result;
                    result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                    return;
                }
                else
                {
                    Close();

                }
            }
            else
            {
                string messageBoxText = "Invalid Key, check capitalization and spelling.";
                string caption = "Notification";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBoxVar = textBox.Text;
        }
        private void Ctrl(object sender, RoutedEventArgs e){ctrl = true;}
        private void UnCtrl(object sender, RoutedEventArgs e){ctrl = false;}
        private void Alt(object sender, RoutedEventArgs e){alt = true;}
        private void UnAlt(object sender, RoutedEventArgs e){alt = false;}
        private void Shift(object sender, RoutedEventArgs e){shift = true;}
        private void UnShift(object sender, RoutedEventArgs e){shift = false;}
    }
}
