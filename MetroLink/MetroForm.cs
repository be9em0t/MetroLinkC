﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Runtime.InteropServices;
using XMLSettingsIO;


namespace MetroForm
{
    public partial class frmMetrolinkHost : Form
    {
        public double formWidth;    
        public double formHeight;
        //public double AutoScaleWidthMult; //scaling multiplier according to DPI (96 DPI = no scaling)       
        //public double AutoScaleHeightMult; 

        
        //hotkey1 constants and windows integration        
        const int MOD_ALT = 0x1; 
        const int MOD_CONTROL = 0x2;
        const int MOD_SHIFT = 0x4;
        const int MOD_WIN = 0x8;
        const int WM_HOTKEY = 0x312;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        

        ///hotkey2 WindowMessage        
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY)
            {
                WindowState = FormWindowState.Normal;
                Activate();
                //if (m.WParam.ToInt32() == 2) MessageBox.Show("Hot Key!");
            }
        }

        public frmMetrolinkHost()
        {
            //set frmMetroLinkHost scale factor to account for DPI scaling =====PROBLEM=====
            //AutoScaleWidthMult = (0 + CurrentAutoScaleDimensions.Width) / 96;
            //AutoScaleHeightMult = (0 + CurrentAutoScaleDimensions.Height) / 96;

            SettingsIO.XMLReadElements();
            InitializeComponent();
            MetroForm.RebuildFormClass();
            CreateWPFHost();    
                                
        }



        public void CreateWPFHost()
        {

            /* container that will host our WPF control, we set it using 
             * the Child property */
            System.Windows.Controls.Grid hostGrid = new System.Windows.Controls.Grid();
            ElementHost host = new ElementHost()
            {
                BackColor = Color.Azure,
                Dock = DockStyle.Fill,
                Child = hostGrid,
            };


            //System.Windows.Controls.Button button1 = new System.Windows.Controls.Button();
            //button1.Width = 100;
            //button1.Height = 50;
            //button1.MaxHeight = 150;
            //button1.MaxWidth = 150;

            //Test positioning
            System.Windows.Controls.TextBox txtbx = new System.Windows.Controls.TextBox()
            {
                HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Visible,
            };
            txtbx.Width = 100;
            txtbx.Height = 50;
            txtbx.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            txtbx.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //txtbx.Text = formHeight.ToString() ; //XMLSettingsIO.SettingsIO.dictSettings["imgmissing"];
            int leftPos = 22;
            int topPos = 52;
            txtbx.Margin = new System.Windows.Thickness(leftPos, topPos, 0, 0);


            //play with imgbutton

            System.Windows.Controls.Button imgButt = new System.Windows.Controls.Button();
            imgButt.Width = 100;
            imgButt.Height = 100;
            imgButt.Margin = new System.Windows.Thickness(leftPos + 50, topPos, 0, 0);

            System.Windows.Controls.Image tileImage = new System.Windows.Controls.Image();
            tileImage.Name = "test";
            string imgFile = "c:\\FreePrograms\\MetroLink\\Images\\airtraffic.png";
            tileImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(imgFile));

            hostGrid.Children.Add(tileImage);
            hostGrid.Children.Add(txtbx);


            //host.Child = hostGrid;
            //can.Children.Add(button1);
            //host.Child = txtbx;            

            Controls.Add(host);

        }

        //Events

        private void button2_Click(object sender, EventArgs e)
        {
            frmMetrolinkHost.ActiveForm.Close();
        }

        private void frmMetroForm_Load(object sender, EventArgs e)
        {
            //hotkey3 Register on Form Load 
            var hotResult = RegisterHotKey((IntPtr)Handle, 1, MOD_CONTROL, (int)Keys.O).ToString();

        }

        private void frmMetroForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //hotkey4 Un-Register on FormClosing
            var hotResult = UnregisterHotKey((IntPtr)Handle, 1).ToString();
            //MessageBox.Show(UnregisterHotKey((IntPtr)Handle, 1).ToString());
            //MessageBox.Show(UnregisterHotKey((IntPtr)Handle, 2).ToString());
        }


    }
}
