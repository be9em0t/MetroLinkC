using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;


namespace XMLSettingsIO
{

    public static class SettingsIO
    {
        //Variables
/*    Public TileArray As String(,) 'Declare 2-dimentional array to hold Tile-states
    Public refreshVar As Integer = 0 'TEMPorary solution for running tiles recalculation only once

 */
 
    const string XmlIniFile = "settings.xml"; //const

    public static System.Drawing.Color MetroBlue = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#1ba1e2"));
    public static System.Drawing.Color MetroGreen = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#5e9e1a"));
    public static System.Drawing.Color MetroOrange = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#f09609"));
    public static System.Drawing.Color MetroMagenta = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#fc2061"));
    public static System.Drawing.Color MetroLime = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#89ca07"));
    public static System.Drawing.Color MetroRed = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#f52c2c"));
    public static System.Drawing.Color MetroTeal = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#00c1bc"));
    public static System.Drawing.Color MetroPurple = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#68217a"));
    public static System.Drawing.Color MetroBrown = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#a86100"));
    public static System.Drawing.Color MetroPink = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#e071a3"));
    public static System.Drawing.Color MetroTan = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#dd6022"));
    public static System.Drawing.Color MetroMidGray = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#817e77"));
    public static System.Drawing.Color MetroLight = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#bfbfbf"));
    public static System.Drawing.Color MetroDark = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#303030"));
    public static System.Drawing.Color MetroBorder = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#fdd02a"));
    public static System.Drawing.Color MetroText = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#fafafa"));
    public static System.Drawing.Color MetroHilite = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor("#ffffff")); 

    public static System.Windows.Media.SolidColorBrush MetroBlueBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroGreenBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroOrangeBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroMagentaBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroLimeBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroRedBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroTealBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroPurpleBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroBrownBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroPinkBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroTanBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroMidGrayBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroLightBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroDarkBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroBorderBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroTextBrush = new System.Windows.Media.SolidColorBrush();
    public static System.Windows.Media.SolidColorBrush MetroHiliteBrush = new System.Windows.Media.SolidColorBrush();

    public static int triggerkeyVal = 0x57;
    public static int modKeyVal = 0x2;

 
    public static int tileRows = 5;
    public static int tileCols = 5;
    public static int MarginTop = 10;
    public static int MarginBottom = 10;
    public static int MarginLeft = 10;
    public static int MarginRight = 10;
    public static int OffsetLeft = 0;
    public static int OffsetTop = 0;
    public static int SpacingTiles = 10;
    public static int WideSpacing = 20;
    public static string[] WideColumns = {"0"};
    public static string[] WideRows = {"0"};
    public static  int ActiveBorder = 2;

    public static string ImgSubfolder = "images";
    public static string ImgMissing = "missing.png";
    public static string ImgRunning = "missing.png";
    public static double ImgRunningOpacity = 1;
    public static bool MarkRunning = false;
    public static string MarkRunExeptions = "svchost.exe, lsass.exe, runtimebroker.exe, csrss.exe, services.exe, searchindexer.exe";

    public static int TileHeight = 90;
    public static int TileWidth = 90;
    public static int TileSetupWidth = 90;
    public static string TileSetupImg = "nextPageArrow.png";

    public static int TileTitleTop = 50;
    public static int TileTitleLeft = 5;
    public static int TileFontSize = 13;
    public static string TileFontFamily = "arial";

    public static int TileIconSize = 48;
    public static int TileIconOffset = 0;
    public static int TimerInterval = 25;

    
        public static Dictionary<String, MetroForm.Tile> dictTiles = new Dictionary<string, MetroForm.Tile>(); //Dictionary Tile instances
        public static Dictionary<String, String> dictSettings = new Dictionary<string, string>(); //Dictionary Settings

        

        public static void XMLReadElements()
        {
            XmlDocument xmlini = new XmlDocument();
            try
            {
                xmlini.Load(XmlIniFile);
            }
            catch (XmlException e)
            {             
                System.Windows.Forms.MessageBox.Show(e.Message, "MetroLink - Error in Settings file"); 
                MetroForm.frmMetrolinkHost.ActiveForm.Close();
                return;
            }

            XmlElement root = xmlini.DocumentElement; //Evaluate root nodes - Tiles,Settings/Colors...
            for (int i = 0; i < root.ChildNodes.Count; i++) 
            {
                XmlNode nodeRootChild = root.ChildNodes[i];
                if (nodeRootChild.NodeType == XmlNodeType.Element)
                {

                    switch (nodeRootChild.Name)
                    {
                        case "Tiles":   
                            XmlNode nodeTiles = root.ChildNodes[i]; //Evaluate All Tile nodes 
                            //Console.WriteLine("Tiles ChildNodes to process: " + nodeTiles.ChildNodes.Count.ToString());  //TEMP
                            IEnumerator enumNodeTiles = nodeTiles.ChildNodes.GetEnumerator(); //enum all Tile nodes
                            int tileNumber = 0;
                            while (enumNodeTiles.MoveNext())
                            {
                                XmlNode nodeTileCurrent = (XmlNode)enumNodeTiles.Current; //Evaluate Current Tile

                                if (nodeTileCurrent.NodeType == XmlNodeType.Element)
                                {
                                    IEnumerator enumNodeTileCurrentProperties= nodeTileCurrent.ChildNodes.GetEnumerator(); //enum current Tile's child nodes
                                    //int tileindex = GeneralUtils.IntDecPlaces(tileNumber);
                                    string strIndex = GeneralUtils.StrDecPlaces(tileNumber);                                    
                                    string currentTile = "Tile" + strIndex; //define Tile.key
                                    while (enumNodeTileCurrentProperties.MoveNext())
                                        {
                                            XmlNode nodeProperty = (XmlNode)enumNodeTileCurrentProperties.Current;
                                            if (nodeProperty.NodeType == XmlNodeType.Element)
                                                {
                                                switch (nodeProperty.Name.ToLower().Trim()) //use lowwercase, trimmed version of XML element
	                                                    {
                                                            case "title":
                                                     dictTiles.Add(currentTile, new MetroForm.Tile() { Title = nodeProperty.InnerXml.Trim(), Index = strIndex });
                                                     break;
                                                            case "lmb":
                                                            dictTiles[currentTile].LMB=nodeProperty.InnerXml.Trim();
                                                     break;
                                                            case "lmbopt":
                                                            dictTiles[currentTile].LMBopt = nodeProperty.InnerXml.Trim();
                                                     break;
                                                            case "rmb":
                                                     dictTiles[currentTile].RMB = nodeProperty.InnerXml.Trim();
                                                     break;
                                                            case "rmbopt":
                                                            dictTiles[currentTile].RMBopt = nodeProperty.InnerXml.Trim();
                                                     break;
                                                            case "image":
                                                            dictTiles[currentTile].Image = nodeProperty.InnerXml.Trim();
                                                     break;
                                                            case "visible":
                                                            dictTiles[currentTile].Visible = nodeProperty.InnerXml.Trim();
                                                     break;
                                                            case "bg":
                                                            dictTiles[currentTile].BG = nodeProperty.InnerXml.Trim();
                                                     break;
                                                            case "markrun":
                                                            dictTiles[currentTile].MarkRun = nodeProperty.InnerXml.Trim();
                                                     break;
                                                            case "activate":
                                                            dictTiles[currentTile].Activate = nodeProperty.InnerXml.Trim();
                                                     break;
                                                            default:
                                                     break;
	                                                    }
                                                 }
                                         }
                                    //Console.WriteLine(currentTile + " >> " + dictTiles[currentTile].Title + " \n: LMB" + dictTiles[currentTile].LMB + " \n LMBOpt:" + dictTiles[currentTile].LMBopt + "\n :RMB" + dictTiles[currentTile].RMB + " \n :RMBOpt" + dictTiles[currentTile].RMBopt);
                                    tileNumber = ++tileNumber; //increment Tile Number 
                                  }

                            }
                            break;

                        case "HotkeySettings": //Evaluate child nodes
                            IEnumerator enumNodeHotkeys = nodeRootChild.ChildNodes.GetEnumerator(); //enum all Hotkey nodes
                            while (enumNodeHotkeys.MoveNext())
                            {
                                XmlNode nodeHotkey = (XmlNode)enumNodeHotkeys.Current;
                                if (nodeHotkey.NodeType == XmlNodeType.Element)
                                {
                                    switch (nodeHotkey.Name.ToLower().Trim()) //use lowwercase, trimmed version of XML element
	                                {
                                        case "hotkeycode":
                                            triggerkeyVal = GeneralUtils.FromHex(nodeHotkey.InnerXml.Trim().ToLower());
                                    break;
                                        case "modkeycode":
                                            modKeyVal = GeneralUtils.FromHex(nodeHotkey.InnerXml.Trim().ToLower());
                                    break;
                                        default:
                                    break;
                                    }
                                 }
                             }
                            break;

                        case "MetroColors":  //Evaluate child nodes
                            //XmlNode nodeColors = nodeRootChild;
                            Console.WriteLine("Process MetroColors: " + nodeRootChild.ChildNodes.Count.ToString()); //TEMP   
                            IEnumerator enumNodeColors = nodeRootChild.ChildNodes.GetEnumerator(); //enum all Hotkey nodes
                            while (enumNodeColors.MoveNext())
                            {
                                XmlNode nodeColor = (XmlNode)enumNodeColors.Current;
                                if (nodeColor.NodeType == XmlNodeType.Element)
                                {
                //MetroLight = Color.FromArgb(255, colorRGB(0), colorRGB(1), colorRGB(2))
                //MetroLightBrush.Color = System.Windows.Media.Color.FromArgb(255, CByte(colorRGB(0)), CByte(colorRGB(1)), CByte(colorRGB(2)))
                                    switch (nodeColor.Name.ToLower().Trim()) //use lowwercase, trimmed version of XML element
	                                {
                                        case "hexmetroblue":
                                    MetroBlue = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetrogreen":
                                    MetroGreen = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetroorange":
                                    MetroOrange = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetromagenta":
                                    MetroMagenta = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetrolime":
                                    MetroLime = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetrored":
                                    MetroRed = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetroteal":
                                    MetroTeal = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetropurple":
                                    MetroPurple = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetrobrown":
                                    MetroBrown = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetropink":
                                    MetroPink = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetrotan":
                                    MetroTan = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetromidgray":
                                    MetroMidGray = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetrolight":
                                    MetroLight = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetrodark":
                                    MetroDark = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetroborder":
                                    MetroBorder = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetrotext":
                                    MetroText = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        case "hexmetrohilite":
                                    MetroHilite = System.Drawing.Color.FromArgb(GeneralUtils.HEXtoARGBcolor(nodeColor.InnerXml.Trim().ToLower()));
                                    break;
                                        default:
                                    break;
                                    }
                                 }
                             }
                            break;

                        case "TileSettings":  //Evaluate child nodes
                            XmlNode nodeSettings = nodeRootChild;
                            //Console.WriteLine("Process TileSettings: " + nodeSettings.ChildNodes.Count.ToString()); //TEMP
                            IEnumerator enumNodeSettings = nodeSettings.ChildNodes.GetEnumerator(); //enum all Setting nodes
                            while (enumNodeSettings.MoveNext())
                            {
                                XmlNode nodeSetting = (XmlNode)enumNodeSettings.Current;
                                if (nodeSetting.NodeType == XmlNodeType.Element)
                                {
                                    switch (nodeSetting.Name.ToLower().Trim()) //use lowwercase, trimmed version of XML element
	                                {
                                        case "columns":
                                        tileCols=Convert.ToInt32( nodeSetting.InnerXml.Trim() );
                                        break;
                                        case "rows":
                                        tileRows=Convert.ToInt32( nodeSetting.InnerXml.Trim() );
                                        break;
                                        case "margintop":
                                        MarginTop = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "marginbottom":
                                        MarginBottom = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "marginleft":
                                        MarginLeft = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "marginright":
                                        MarginRight = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "offsetleft":
                                        OffsetLeft = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "offsettop":
                                        OffsetTop = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "spacingtiles":
                                        SpacingTiles = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "widespacing":
                                        WideSpacing = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "widecolumns":
                                        WideColumns = (nodeSetting.InnerXml.Trim()).Split(new Char [] {',', '.', ':', '\t' });                                        
                                        //Console.WriteLine("widecolumns length and first: " + WideColumns.Length.ToString() + " " + WideColumns[0]); //TEMP
                                        break;
                                        case "widerows":
                                        WideRows = (nodeSetting.InnerXml.Trim()).Split(new Char [] {',', '.', ':', '\t' });
                                        //Console.WriteLine("widecoRows length and first: " + WideRows.Length.ToString() + " " + WideRows[0]); //TEMP
                                        break;
                                        case "activeborder":
                                        ActiveBorder = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "imgsubfolder":
                                        ImgSubfolder = nodeSetting.InnerXml.Trim();
                                        //Console.WriteLine("Image subfolder :" + ImgSubfolder + "."); //TEMP
                                        break;
                                        case "imgmissing":
                                        ImgMissing = nodeSetting.InnerXml.Trim();
                                        break;
                                        case "imgrunning":
                                        ImgRunning = nodeSetting.InnerXml.Trim();
                                        break;
                                        case "imgrunningopacity":
                                        ImgRunningOpacity = Convert.ToDouble( nodeSetting.InnerXml.Trim() );
                                        //Console.WriteLine("RunningOpacity: " + ImgRunningOpacity.ToString()); //TEMP
                                        break;
                                        case "markrunning":
                                        MarkRunning = Convert.ToBoolean(nodeSetting.InnerXml.Trim());
                                        //Console.WriteLine("MarkRunning: " + MarkRunning.ToString()); //TEMP
                                        break;
                                        case "markrunexeptions":
                                        MarkRunExeptions = nodeSetting.InnerXml.Trim();
                                        //dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                        //Console.WriteLine("Running Exceptions: " + MarkRunExeptions); //TEMP
                                        break;

                                        case "tileheight":
                                        TileHeight = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "tilewidth":
                                        TileWidth = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;

                                        case "tilesetupwidth":
                                        TileSetupWidth = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "tilesetupimg":
                                        TileSetupImg = nodeSetting.InnerXml.Trim();
                                        break;

                                        case "tiletitletop":
                                        TileTitleTop = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "tiletitleleft":
                                        TileTitleLeft = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "tilefontsize":
                                        TileFontSize = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "tilefontfamily":
                                        TileFontFamily=nodeSetting.InnerXml.Trim();
                                        break;

                                        case "tileiconsize":
                                        TileIconSize = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "tileiconoffset":
                                        TileIconOffset = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                        case "timerinterval":
                                        TimerInterval = Convert.ToInt32(nodeSetting.InnerXml.Trim());
                                        break;
                                    default:
                                        break;
                                    }
                                }
                             }
                            break;
                        default:
                            break;
                        }

                    }
                }

            }

            /*
            public static void FinalizeIO()
            {
                MetroBlueBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                //Console.WriteLine("metroblue: " + MetroBlue + " : " + nodeColor.InnerXml.Trim().ToLower() + " : " + MetroBlueBrush); //TEMP   
                MetroGreenBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroOrangeBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroMagentaBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroLimeBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroRedBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroTealBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroPurpleBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroBrownBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroPinkBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroTanBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroMidGrayBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroLightBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroDarkBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroBorderBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroTextBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
                MetroHiliteBrush.Color = System.Windows.Media.Color.FromArgb(MetroBlue.A, MetroBlue.R, MetroBlue.G, MetroBlue.B);
            }
            */
        }

    public static class GeneralUtils
    {
        public static string StringFix(string strIn)
        {
            string result = strIn.ToLower();
            return result;
        }
        public static string StringLowTrim(string strIn)
        {
            string result = strIn.ToLower().Trim();
            return result;
        }
        public static string StrDecPlaces(int arg)
        {
            string decPlaces3 = "000"; //target decimal places
            int sourcePlaces = arg.ToString().Length;
            string result = decPlaces3.Substring(0, (decPlaces3.Length-sourcePlaces)) + arg.ToString();
            return result; //return padded integer
          }

        public static int IntDecPlaces(int arg)
        {
            string decPlaces3 = "000"; //target decimal places
            int sourcePlaces = arg.ToString().Length;
            string result = decPlaces3.Substring(0, (decPlaces3.Length - sourcePlaces)) + arg.ToString();
            return Convert.ToInt32( result ); //return padded integer
        }

        public static string printOutDictTiles(Dictionary<string, MetroForm.Tile> dictInput) //Print out entire dictionary
            {
              foreach (KeyValuePair<string, MetroForm.Tile> kvp in dictInput) //list all
              {
                  //kvp.Value.Title=kvp.Value.Title + " " + kvp.Key.ToString();
                  Console.WriteLine("Tile = {0}, Value = {1}, Index = {2}", kvp.Key, kvp.Value.Title, kvp.Value.Index);
                  //Console.WriteLine(kvp.Value.intTileIndex());
              }

            string result = "";
            return result;
            }

        public static string printOutDictSettings(Dictionary<string, string> dictInput) //Print out entire dictionary
        {
            foreach (KeyValuePair<string, string> kvp in dictInput) //list all
            {
                //kvp.Value.Title=kvp.Value.Title + " " + kvp.Key.ToString();
                Console.WriteLine("Setting: {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            string result = "";
            return result;
        }
        public static int FromHex(string value)
        {
            // strip the leading 0x
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                value = value.Substring(2);
            }
            return Int32.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }
        public static System.Windows.Media.Color HEXtoRGBmediaColor(string HEXcolor)
        {
            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDFD991"); 
            return color;
        }
        public static System.Drawing.Color HEXtoRGBdrawingColor(string HEXcolor)
        {
            System.Drawing.Color color = (System.Drawing.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDFD991");
            return color;
        }
        public static int HEXtoARGBcolor(string HEXcolor)
        {
            //string colorcode = "#FFFFFF00";
            int argb = Int32.Parse(HEXcolor.Replace("#", "ff"), System.Globalization.NumberStyles.HexNumber);
            //Color clr = Color.FromArgb(argb);
            //Console.WriteLine("argb: " + argb);
            return argb;
        }


        public static System.Windows.Media.SolidColorBrush ColorToBrush(System.Drawing.Color inColor)
        {
            System.Windows.Media.SolidColorBrush clrBrush = new System.Windows.Media.SolidColorBrush();
            clrBrush.Color = System.Windows.Media.Color.FromArgb(inColor.A, inColor.R, inColor.G, inColor.B);
            return clrBrush;
        }

        
       }
}
