namespace MetroForm
{
    public static class RebuildFormClass
    {
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
    }
}