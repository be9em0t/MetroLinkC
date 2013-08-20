using System;
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
            RebuildForm();
            CreateWPFHost();    
                                
        }

        public void RebuildForm() //set frmMetrolinkHost defaults
        {
        GeneralUtils.printOutDictTiles(SettingsIO.dictTiles); //TEMP
        GeneralUtils.printOutDictSettings(SettingsIO.dictSettings); //TEMP

        //Console.WriteLine("HEX" + SettingsIO.triggerkeyVal.ToString());  //TEMP
/*                '--Check all tiles fit on form--
        If tileCols * tileRows < TileArray.GetLength(0) Then
            MsgBox("Number of tiles exseeds available space." + vbCrLf + "Some tiles will not display." + vbCrLf + "Please correct settings.xml file.", MsgBoxStyle.Exclamation)
        End If
*/

        //this.AllowDrop=false;
        this.KeyPreview=true; //process keyboard events
        this.ControlBox=false;    //Remove the control box so the form will only display client area.
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.WindowState = FormWindowState.Normal;
        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.Manual;

        //SettingsIO.MarginLeft = Convert.ToInt32(XMLSettingsIO.SettingsIO.dictSettings["marginleft"]);        
        //int tileCols  = Convert.ToInt32(XMLSettingsIO.SettingsIO.dictSettings["tilecols"]);
        //int TileWidth  = Convert.ToInt32(XMLSettingsIO.SettingsIO.dictSettings["tilewidth"]);
        //int SpacingTiles = Convert.ToInt32(XMLSettingsIO.SettingsIO.dictSettings["spacingtiles"]);
        //int TileSetupWidth = Convert.ToInt32(XMLSettingsIO.SettingsIO.dictSettings["tilesetupwidth"]);
        //int MarginRight = Convert.ToInt32(XMLSettingsIO.SettingsIO.dictSettings["marginright"]);
        //WideColumns.Length = Convert.ToInt32(XMLSettingsIO.SettingsIO.dictSettings["marginleft"]);
        //int WideSpacing = Convert.ToInt32(XMLSettingsIO.SettingsIO.dictSettings["widespacing"]);

        //formWidth = CInt(formWidth * frmMetrolinkHost.AutoScaleWidthMult) 'Take care of windows DPI scale
        //formHeight = CInt(formHeight * frmMetrolinkHost.AutoScaleHeightMult)

        formWidth = (SettingsIO.MarginLeft + (SettingsIO.tileCols * (SettingsIO.TileWidth + SettingsIO.SpacingTiles)) + SettingsIO.TileSetupWidth + SettingsIO.MarginRight + (SettingsIO.WideColumns.Length * SettingsIO.WideSpacing)); //replace 1 with SettingsIO.WideColumns.Length 
        formHeight = ((SettingsIO.MarginTop + ((SettingsIO.tileRows * SettingsIO.TileHeight) + ((SettingsIO.tileRows - 1) * SettingsIO.SpacingTiles)) + SettingsIO.MarginBottom) + ((SettingsIO.WideRows.Length - 1) * SettingsIO.WideSpacing));
        int formLeft = ((Screen.PrimaryScreen.WorkingArea.Width - Convert.ToInt32( formWidth ) ) /2) - SettingsIO.OffsetLeft;
        int formTop = 0 + SettingsIO.OffsetTop;

        this.ClientSize= new System.Drawing.Size(Convert.ToInt32(formWidth), Convert.ToInt32(formHeight));
        this.Location=new Point(formLeft,formTop);
        }

        public void CreateWPFHost()
        {

            /* container that will host our WPF control, we set it using 
             * the Child property */
            System.Windows.Controls.Grid hostGrid = new System.Windows.Controls.Grid();
            ElementHost WPFhost = new ElementHost()
            {
                BackColor = Color.Gray,
                Dock = DockStyle.Fill,
                Child = hostGrid,
            };

            //Test create single tile
            /*
    		- add below it the tile color as System.Windows.Controls.Button
    		- add over it the tile image as System.Windows.Controls.Image
    		- add the tile title as as System.Windows.Controls.Label
            */

            int leftPos = 22;
            int topPos = 52;

            System.Windows.Controls.Button TileBG001 = new System.Windows.Controls.Button();
            TileBG001.Width = 90;
            TileBG001.Height = 90;
            TileBG001.HorizontalAlignment=System.Windows.HorizontalAlignment.Left;
            TileBG001.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            TileBG001.Margin = new System.Windows.Thickness(leftPos, topPos, 0, 0);
            hostGrid.Children.Add(TileBG001);

            System.Windows.Controls.Image TileImg001 = new System.Windows.Controls.Image();
            TileImg001.Width = 90;
            TileImg001.Height = 90;
            TileImg001.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            TileImg001.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            TileImg001.Margin = new System.Windows.Thickness(leftPos, topPos, 0, 0);
            string imgFile = "c:\\FreePrograms\\MetroLink\\Images\\airtraffic.png";
            TileImg001.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(imgFile));
            hostGrid.Children.Add(TileImg001);

            System.Windows.Controls.Label TileLbl001 = new System.Windows.Controls.Label();
            TileLbl001.Content="Label";
            TileLbl001.FontSize=8;
            TileLbl001.Margin = new System.Windows.Thickness(leftPos, topPos, 0, 0);
            hostGrid.Children.Add(TileLbl001);


            Controls.Add(WPFhost);
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
