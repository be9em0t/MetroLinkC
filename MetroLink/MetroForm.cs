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
using WPF = System.Windows;
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
            //SettingsIO.FinalizeIO();
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

        /// Redefine styles, if necessary
        public WPF.Style styleLbl = new WPF.Style(typeof(WPF.Controls.Label));
        public WPF.Style styleButt = new WPF.Style(typeof(WPF.Controls.Button));
        public WPF.Style styleMetroBlu = new WPF.Style(typeof(WPF.Controls.Button));


        public void WPFStylesOff()
        {
            styleButt.Setters.Add(new WPF.Setter
            {
                Property = WPF.Controls.Button.FontSizeProperty,
                Value = 14.0
            });
            styleButt.Setters.Add(new WPF.Setter
            {
                Property = WPF.Controls.Button.FontWeightProperty,
                Value = WPF.FontWeights.Bold    
            });

            WPF.Trigger triggerIsMouseOver =
                new WPF.Trigger { Property = WPF.Controls.Button.IsMouseOverProperty, Value = true };

//            triggerIsMouseOver.Setters.Add(
//              new Setter(Button.ForegroundProperty, Brushes.Black));
//            triggerIsMouseOver.Setters.Add(
//              new Setter(Button.BackgroundProperty, Brushes.AliceBlue));
            triggerIsMouseOver.Setters.Add(new WPF.Setter(WPF.Controls.Button.CursorProperty, System.Windows.Input.Cursors.Hand));

            WPF.Trigger triggerIsPressed = new WPF.Trigger { Property = WPF.Controls.Button.IsPressedProperty, Value = true };
            triggerIsPressed.Setters.Add(new WPF.Setter(WPF.Controls.Button.BackgroundProperty, WPF.Media.Brushes.Orange));

            styleButt.Triggers.Add(triggerIsMouseOver);
            styleButt.Triggers.Add(triggerIsPressed);

        }

        public void WPFStyles()

        {
            ///WPF styles redefinition
            ///

            //styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.OverridesDefaultStyleProperty, false));
            //styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.TemplateProperty, true));

            styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.SnapsToDevicePixelsProperty, true));
            styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.FocusableProperty, false));
            styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.FocusVisualStyleProperty, null));
            styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.AllowDropProperty, true));
            styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.HorizontalAlignmentProperty, WPF.HorizontalAlignment.Left));
            styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.VerticalAlignmentProperty, WPF.VerticalAlignment.Top));
            styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.WidthProperty, Convert.ToDouble( SettingsIO.TileWidth )));
            styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.HeightProperty, Convert.ToDouble( SettingsIO.TileHeight )));
            styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.PaddingProperty, new WPF.Thickness(-4)));
            styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.BorderThicknessProperty, new WPF.Thickness(0)));
            styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.BorderBrushProperty, WPF.Media.Brushes.Transparent));
            //styleButt.Setters.Add(new WPF.Setter(WPF.Controls.Button.BackgroundProperty, WPF.Media.Brushes.Tan));
            styleMetroBlu.Setters.Add(new WPF.Setter(WPF.Controls.Button.BackgroundProperty, WPF.Media.Brushes.Red));
            
            styleButt.BasedOn=styleMetroBlu;

            WPF.Trigger triggerIsPressed = new WPF.Trigger { Property = WPF.Controls.Button.IsPressedProperty, Value = true };
            triggerIsPressed.Setters.Add(new WPF.Setter(WPF.Controls.Button.BackgroundProperty, WPF.Media.Brushes.Orange));
            triggerIsPressed.Setters.Add(new WPF.Setter(WPF.Controls.Button.BorderThicknessProperty, new WPF.Thickness(0)));
            triggerIsPressed.Setters.Add(new WPF.Setter(WPF.Controls.Button.BorderBrushProperty, WPF.Media.Brushes.Transparent));

            styleButt.Triggers.Add(triggerIsPressed);

            styleLbl.Setters.Add(new WPF.Setter(WPF.Controls.Label.HorizontalAlignmentProperty, WPF.HorizontalAlignment.Left));
            styleLbl.Setters.Add(new WPF.Setter(WPF.Controls.Label.VerticalAlignmentProperty, WPF.VerticalAlignment.Top));
            styleLbl.Setters.Add(new WPF.Setter(WPF.Controls.Label.ForegroundProperty, GeneralUtils.ColorToBrush(SettingsIO.MetroText)));
            styleLbl.Setters.Add(new WPF.Setter(WPF.Controls.Label.FontFamilyProperty, new WPF.Media.FontFamily(SettingsIO.TileFontFamily)));
            styleLbl.Setters.Add(new WPF.Setter(WPF.Controls.Label.FontSizeProperty, SettingsIO.TileFontSize));
            styleLbl.Setters.Add(new WPF.Setter(WPF.Controls.Label.IsHitTestVisibleProperty, false));


        }

        public void CreateWPFHost()
        {
            WPFStyles(); // activate redefined styles

            /// container that will host our WPF control, we set it using the Child property
            WPF.Controls.Grid hostGrid = new WPF.Controls.Grid();
            ElementHost WPFhost = new ElementHost()
            {
                AllowDrop=false,
                BackColor = SettingsIO.MetroBackground,
                Dock = DockStyle.Fill,
                Child = hostGrid,                
            };

            //Test create single tile
            /*
    		- add below it the tile color as WPF.Controls.Button DONE
    		- add over it the tile image as WPF.Controls.Image
    		- add the tile title as as WPF.Controls.Label
            */


            //public static Dictionary<String, MetroForm.Tile> dictTiles = new Dictionary<string, MetroForm.Tile>(); //Dictionary Tile instances

            //Dictionary<String, MetroForm.TileBG> dictTilesBG = new Dictionary<string, MetroForm.TileBG>(); //Dictionary of TilesBG
            //dictTilesBG.Add("zero", new MetroForm.TileBG() { Width = 90, Height = 90 });

            //dictTileBGs.Add("zero", new WPF.Controls.Button());

            //int leftPos = Convert.ToDouble( SettingsIO.TileWidth );
            //int topPos = Convert.ToDouble( SettingsIO.TileHeight );

            Dictionary<String, WPF.Controls.Button> dictTileBGs = new Dictionary<string, WPF.Controls.Button>(); //Dictionary of TilesBG buttons            
            Dictionary<String, WPF.Controls.Label> dictTileLBLs = new Dictionary<string, WPF.Controls.Label>(); //Dictionary of Tile labels            


            foreach (KeyValuePair<string, Tile> kvp in SettingsIO.dictTiles) //list all
            {
                ///current TileBG 
                //WPF.Controls.ControlTemplate template = new WPF.Controls.ControlTemplate(typeof(WPF.Controls.Button));                
                //WPF.Trigger trigger = new WPF.Trigger();

                //trigger.Property = WPF.Controls.Button.IsPressedProperty;               
                //trigger.Value = true;
                //template.Triggers.Add(trigger);

                dictTileBGs.Add(kvp.Key + "bg", new WPF.Controls.Button());
                //dictTileBGs[kvp.Key + "bg"].Template = template;
                //dictTileBGs[kvp.Key + "bg"].OverridesDefaultStyle = true;
                dictTileBGs[kvp.Key + "bg"].Style = styleButt;
                //dictTileBGs[kvp.Key + "bg"].Style = styleMetroBlue;

                int currentLeftMargin = (kvp.Value.tileColRow()[0] * (SettingsIO.TileWidth + SettingsIO.SpacingTiles)) + SettingsIO.MarginLeft;
                int currentTopMargin = (kvp.Value.tileColRow()[1] * (SettingsIO.TileHeight + SettingsIO.SpacingTiles)) + SettingsIO.MarginTop;
                dictTileBGs[kvp.Key + "bg"].Margin = new WPF.Thickness(currentLeftMargin, currentTopMargin, 0, 0);
                //dictTileBGs[kvp.Key + "bg"].Background = kvp.Value.MetroSolidBrush();
                
                dictTileBGs[kvp.Key + "bg"].Tag = kvp.Key + "bg";
                dictTileBGs[kvp.Key + "bg"].PreviewMouseDown += new WPF.Input.MouseButtonEventHandler(TileMouseDown);
                //dictTileBGs[kvp.Key + "bg"].PreviewDragEnter += new WPF.DragEventHandler(TileDragOver); //Could add effects on DragOver
                dictTileBGs[kvp.Key + "bg"].PreviewDrop += new WPF.DragEventHandler(TileDrop);
                


                ///current Label 
                dictTileLBLs.Add(kvp.Key + "lbl", new WPF.Controls.Label());
                dictTileLBLs[kvp.Key + "lbl"].Style = styleLbl;  //add custom style                                
                dictTileLBLs[kvp.Key + "lbl"].Margin = new WPF.Thickness(currentLeftMargin + SettingsIO.TileTitleLeft, currentTopMargin + SettingsIO.TileTitleTop, 0, 0);


                dictTileLBLs[kvp.Key + "lbl"].Content = kvp.Value.Title;
                //dictTileLBLs[kvp.Key + "lbl"].Content = kvp.Value.BG;


                //=========Add WPF elements to grid====================
                hostGrid.Children.Add(dictTileBGs[kvp.Key + "bg"]);
                hostGrid.Children.Add(dictTileLBLs[kvp.Key + "lbl"]);

            }


            /*
WPF.Controls.Image TileImg001 = new WPF.Controls.Image();
TileImg001.Width = 90;
TileImg001.Height = 90;
TileImg001.HorizontalAlignment = WPF.HorizontalAlignment.Left;
TileImg001.VerticalAlignment = WPF.VerticalAlignment.Top;
TileImg001.Margin = new WPF.Thickness(leftPos, topPos, 0, 0);
string imgFile = "c:\\FreePrograms\\MetroLink\\Images\\airtraffic.png";
TileImg001.Source = new WPF.Media.Imaging.BitmapImage(new Uri(imgFile));
hostGrid.Children.Add(TileImg001);

WPF.Controls.Label TileLbl001 = new WPF.Controls.Label();
TileLbl001.Content="Label";
TileLbl001.FontSize=8;
TileLbl001.Margin = new WPF.Thickness(leftPos, topPos, 0, 0);
hostGrid.Children.Add(TileLbl001);
*/
            //WPFhost.Invoke += new WPF.Controls.i
            Controls.Add(WPFhost);
        }

        void TileMouseDown(object sender, WPF.Input.MouseButtonEventArgs e)
        {
            var item = sender as WPF.Controls.Button;
            /*
            WPF.Media.Animation.DoubleAnimation da = new WPF.Media.Animation.DoubleAnimation();
            da.From = item.Width-10;
            da.To = item.Width;
            da.Duration = new WPF.Duration(TimeSpan.FromSeconds(1));
            item.BeginAnimation(WPF.Controls.Button.HeightProperty, da);
            */            
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

        void TileDrop(object sender, WPF.DragEventArgs e)
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
        void TileDragOver(object sender, WPF.DragEventArgs e)
        {
            var item = sender as WPF.Controls.Button;
            Console.WriteLine("drag over " + item.Tag);
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = WPF.DragDropEffects.Copy;
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
