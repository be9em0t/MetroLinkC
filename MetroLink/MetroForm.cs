using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Runtime.InteropServices;
using XMLSettingsIO;


namespace MetroForm
{
    public partial class frmMetrolinkHost : Form
    {
        //public double formWidth;    
        //public double formHeight;
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
            SettingsIO.FinalizeIO();
            InitializeComponent();
            RebuildForm();
            CreateWPFHost();    
                                
        }

        public void RebuildForm() //set frmMetrolinkHost defaults
        {
        //GeneralUtils.printOutDictTiles(SettingsIO.dictTiles); //TEMP
        //GeneralUtils.printOutDictSettings(SettingsIO.dictSettings); //TEMP

        if (SettingsIO.tileCols * SettingsIO.tileRows < SettingsIO.dictTiles.Count) //check for correct number of rows and columns
        {
            MessageBox.Show("Number of tiles (" + SettingsIO.dictTiles.Count.ToString() + ") exseeds available space (" + SettingsIO.tileCols * SettingsIO.tileRows + ").\nPlease edit settings.xml", "MetroLink", MessageBoxButtons.OK , MessageBoxIcon.Error);
            EndPogram();
        }

        ///set form size and properties
        this.AllowDrop=false;
        this.KeyPreview=true; //process keyboard events
        this.ControlBox=false;    //Remove the control box so the form will only display client area.
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.WindowState = FormWindowState.Normal;
        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.Manual;

        double formWidth = (SettingsIO.MarginLeft + (SettingsIO.tileCols * (SettingsIO.TileWidth + SettingsIO.SpacingTiles)) + SettingsIO.TileSetupWidth + SettingsIO.MarginRight + (SettingsIO.WideColumns.Length * SettingsIO.WideSpacing)); //replace 1 with SettingsIO.WideColumns.Length 
        double formHeight = ((SettingsIO.MarginTop + ((SettingsIO.tileRows * SettingsIO.TileHeight) + ((SettingsIO.tileRows - 1) * SettingsIO.SpacingTiles)) + SettingsIO.MarginBottom) + ((SettingsIO.WideRows.Length - 1) * SettingsIO.WideSpacing));
        int formLeft = ((Screen.PrimaryScreen.WorkingArea.Width - Convert.ToInt32( formWidth ) ) /2) - SettingsIO.OffsetLeft;
        int formTop = 0 + SettingsIO.OffsetTop;

        this.ClientSize = new System.Drawing.Size(Convert.ToInt32(formWidth), Convert.ToInt32(formHeight));
        this.Location = new Point(formLeft,formTop);
        }

        public void CreateWPFHost()
        {

            /// container that will host our WPF control, we set it using the Child property
            System.Windows.Controls.Grid hostGrid = new System.Windows.Controls.Grid();
            ElementHost WPFhost = new ElementHost()
            {
                AllowDrop=false,
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


            //public static Dictionary<String, MetroForm.Tile> dictTiles = new Dictionary<string, MetroForm.Tile>(); //Dictionary Tile instances

            //Dictionary<String, MetroForm.TileBG> dictTilesBG = new Dictionary<string, MetroForm.TileBG>(); //Dictionary of TilesBG
            //dictTilesBG.Add("zero", new MetroForm.TileBG() { Width = 90, Height = 90 });

            //dictTileBGs.Add("zero", new System.Windows.Controls.Button());

            int leftPos = 95;
            int topPos = 95;
            Dictionary<String, System.Windows.Controls.Button> dictTileBGs = new Dictionary<string, System.Windows.Controls.Button>(); //Dictionary of TilesBG buttons            
            foreach (KeyValuePair<string, Tile> kvp in SettingsIO.dictTiles) //list all
            {
                //Console.WriteLine("add TileBG: {0}", kvp.Key + "bg");
                dictTileBGs.Add(kvp.Key + "bg", new System.Windows.Controls.Button());
                dictTileBGs[kvp.Key + "bg"].Width=SettingsIO.TileWidth;
                dictTileBGs[kvp.Key + "bg"].Height = SettingsIO.TileHeight;
                dictTileBGs[kvp.Key + "bg"].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                dictTileBGs[kvp.Key + "bg"].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                int currentLeftMargin = (kvp.Value.tileColRow()[0] * leftPos) + SettingsIO.MarginLeft;
                int currentTopMargin = (kvp.Value.tileColRow()[1] * topPos) + SettingsIO.MarginTop;
                dictTileBGs[kvp.Key + "bg"].Background = SettingsIO.MetroBlueBrush;

                dictTileBGs[kvp.Key + "bg"].Tag = kvp.Key + "bg";


                dictTileBGs[kvp.Key + "bg"].AllowDrop = true;
                dictTileBGs[kvp.Key + "bg"].PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(TileMouseDown);
                //dictTileBGs[kvp.Key + "bg"].PreviewDragEnter += new System.Windows.DragEventHandler(TileDragOver); //Add effects on DragOver
                dictTileBGs[kvp.Key + "bg"].PreviewDrop += new System.Windows.DragEventHandler(TileDrop);

                dictTileBGs[kvp.Key + "bg"].Margin = new System.Windows.Thickness(currentLeftMargin, currentTopMargin , 0, 0);

                hostGrid.Children.Add(dictTileBGs[kvp.Key + "bg"]);
            }


            /*
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
*/
            
            Controls.Add(WPFhost);
        }

        void TileMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = sender as System.Windows.Controls.Button;
            switch (e.ChangedButton.ToString())
            {
            case "Left":
                    Console.WriteLine("LeftMouseDown on " + item.Tag);
                    break;
            case "Middle":
                    Console.WriteLine("MiddleMouseDown on " + item.Tag);
                    break;
            case "Right":
                    Console.WriteLine("RightMouseDown on " + item.Tag);
                    break;
                default:
                    break;
            }
        }

        void TileDrop(object sender, System.Windows.DragEventArgs e)
        {
            try
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files) Console.WriteLine(file);
            }
            catch (Exception)
            {
                return;
            }
        }

        /* Effects on Drag Over
        void TileDragOver(object sender, System.Windows.DragEventArgs e)
        {
            var item = sender as System.Windows.Controls.Button;
            Console.WriteLine("drag over " + item.Tag);
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = System.Windows.DragDropEffects.Copy;
        }
        */

        private void button2_Click(object sender, EventArgs e)
        {
            frmMetrolinkHost.ActiveForm.Close();
        }

        private void frmMetroForm_Load(object sender, EventArgs e)
        {
            //hotkey3 Register on Form Load 
            //var hotResult = RegisterHotKey((IntPtr)Handle, 1, MOD_CONTROL, (int)Keys.O).ToString();
            var hotResult = RegisterHotKey((IntPtr)Handle, 1, SettingsIO.modKeyVal, SettingsIO.triggerkeyVal);
            if (hotResult==false)
            {
                MessageBox.Show("Hotkey unavailable. \nPlease edit settings.xml and select another hotkey.", "MetroLink", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EndPogram();

                //MessageBox.Show("Hotkey combination already in use. \nPlease edit settings.xml and select another hotkey.");
            }

        }

        private void frmMetroForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //hotkey4 Un-Register on FormClosing
            var hotResult = UnregisterHotKey((IntPtr)Handle, 1).ToString();
            //MessageBox.Show(UnregisterHotKey((IntPtr)Handle, 1).ToString());
            //MessageBox.Show(UnregisterHotKey((IntPtr)Handle, 2).ToString());
        }

        private void EndPogram()
        {
        Environment.Exit(0);
        }

    }
}
