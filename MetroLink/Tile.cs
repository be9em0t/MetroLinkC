using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLSettingsIO;

namespace MetroForm
{
    public class Tile
    {
        public string Index { get; set; }
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

    }

    public class TileBG
    {
        public int Width { get; set; }
        public int Height { get; set; }
//            TileBG001.HorizontalAlignment=System.Windows.HorizontalAlignment.Left;
//            TileBG001.VerticalAlignment = System.Windows.VerticalAlignment.Top;
//            TileBG001.Margin = new System.Windows.Thickness(leftPos, topPos, 0, 0);
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
