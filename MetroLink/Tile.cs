using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLSettingsIO;
using WPF = System.Windows;

namespace MetroForm
{
    public class Tile
    {
        /*public string Index { get; set; }
        public string Title { get; set; }
        public string LMB { get; set; }
        public string LMBopt { get; set; }
        public string RMB { get; set; }
        public string RMBopt { get; set; }
        public string Image { get; set; }
        public string Visible { get; set; }
        public string BG { get; set; }
        public string MarkRun { get; set; }
        public string Activate { get; set; }
        */
        public string Index;
        public string Title;
        public string LMB;
        public string LMBopt;
        public string RMB;
        public string RMBopt;
        public string Image;
        public string Visible;
        public string BG;
        public string MarkRun;
        public string Activate;

        public int intTileIndex ()
        {
        return Convert.ToInt32(Index);
        }

        public int posHoriz()
        {
            return Convert.ToInt32(Index);
        }

        public int posVert()
        {
            return Convert.ToInt32(Index);
        }

        public int[] tileColRow()
        {
        int[] colRow = {1,1};
        colRow[1] = Convert.ToInt32(Index) / SettingsIO.tileCols;
        colRow[0] = Convert.ToInt32(Index) - (SettingsIO.tileRows * colRow[1]); 
        return colRow;
        }

        public System.Drawing.Color MetroColor()
        {
            System.Drawing.Color metroClr = new System.Drawing.Color();
            switch (BG.ToLower().Trim()) //use lowwercase, trimmed version of XML element
            {
                case "metroblue":
                    metroClr= SettingsIO.MetroBlue;
                    break;
                case "metrogreen":
                    metroClr = SettingsIO.MetroGreen;
                    break;
                case "metroorange":
                    metroClr = SettingsIO.MetroOrange;
                    break;
                case "metromagenta":
                    metroClr = SettingsIO.MetroMagenta;
                    break;
                case "metrolime":
                    metroClr = SettingsIO.MetroLime;
                    break;
                case "metrored":
                    metroClr = SettingsIO.MetroRed;
                    break;
                case "metroteal":
                    metroClr = SettingsIO.MetroTeal;
                    break;
                case "metropurple":
                    metroClr = SettingsIO.MetroPurple;
                    break;
                case "metrobrown":
                    metroClr = SettingsIO.MetroBrown;
                    break;
                case "metropink":
                    metroClr = SettingsIO.MetroPink;
                    break;
                case "metrotan":
                    metroClr = SettingsIO.MetroTan;
                    break;
                case "metromidgray":
                    metroClr = SettingsIO.MetroMidGray;
                    break;
                case "metrolight":
                    metroClr = SettingsIO.MetroLight;
                    break;
                case "metrodark":
                    metroClr = SettingsIO.MetroDark;
                    break;
                case "metroborder":
                    metroClr = SettingsIO.MetroBorder;
                    break;
                case "metrotext":
                    metroClr = SettingsIO.MetroText;
                    break;
                case "metrohilite":
                    metroClr = SettingsIO.MetroHilite;
                    break;
                default:
                    break;
            }
            return metroClr;
        }

        public WPF.Media.SolidColorBrush MetroSolidBrush()
        {
            System.Drawing.Color inColor = MetroColor();
            WPF.Media.SolidColorBrush metroSolidBr = new WPF.Media.SolidColorBrush();
            metroSolidBr.Color = WPF.Media.Color.FromArgb(inColor.A, inColor.R, inColor.G, inColor.B);
            return metroSolidBr;
        }

        /*
        public WPF.Style styleTest = new WPF.Style
        {
            TargetType = typeof(WPF.Controls.Control)            
        };

        style.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.Green));
        myControl.Style = style;
        */
    }

    public class TileBG
    {
        public int Width { get; set; }
        public int Height { get; set; }
//            TileBG001.HorizontalAlignment=WPF.HorizontalAlignment.Left;
//            TileBG001.VerticalAlignment = WPF.VerticalAlignment.Top;
//            TileBG001.Margin = new WPF.Thickness(leftPos, topPos, 0, 0);
/*
        public string Title { get; set; }
        public string LMB { get; set; }
        public string LMBopt { get; set; }
        public string RMB { get; set; }
        public string RMBopt { get; set; }
        public string Image { get; set; }
        public string Visible { get; set; }
        public string BG { get; set; }
        public string MarkRun { get; set; }
        public string Activate { get; set; }
   */ 
   }
    
}
