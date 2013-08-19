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

    public static System.Drawing.Color MetroMagenta ;
    public static System.Drawing.Color MetroPurple ;
    public static System.Drawing.Color MetroTeal ;
    public static System.Drawing.Color MetroLime ;
    public static System.Drawing.Color MetroBrown ;
    public static System.Drawing.Color MetroPink ;
    public static System.Drawing.Color MetroOrange ;
    public static System.Drawing.Color MetroBlue ;
    public static System.Drawing.Color MetroRed ;
    public static System.Drawing.Color MetroGreen ;

    public static System.Drawing.Color MetroLight ;
    public static System.Drawing.Color MetroDark ;
    public static System.Drawing.Color MetroBorder ;
    public static System.Drawing.Color MetroText ;
    public static System.Drawing.Color MetroHilite ;

    public static System.Windows.Media.SolidColorBrush MetroLightBrush ;
    public static System.Windows.Media.SolidColorBrush MetroDarkBrush ;
    public static System.Windows.Media.SolidColorBrush MetroMagentaBrush ;
    public static System.Windows.Media.SolidColorBrush MetroPurpleBrush ;
    public static System.Windows.Media.SolidColorBrush MetroTealBrush ;
    public static System.Windows.Media.SolidColorBrush MetroLimeBrush ;
    public static System.Windows.Media.SolidColorBrush MetroBrownBrush ;
    public static System.Windows.Media.SolidColorBrush MetroPinkBrush ;
    public static System.Windows.Media.SolidColorBrush MetroOrangeBrush ;
    public static System.Windows.Media.SolidColorBrush MetroBlueBrush ;
    public static System.Windows.Media.SolidColorBrush MetroRedBrush ;
    public static System.Windows.Media.SolidColorBrush MetroGreenBrush ;
    public static System.Windows.Media.SolidColorBrush MetroTextBrush ;
    public static System.Windows.Media.SolidColorBrush MetroBorderBrush ;
    public static System.Windows.Media.SolidColorBrush MetroHiliteBrush ;

    public static int triggerkeyVal = 0x57;
    public static int modKeyVal = 8;

 
    public static int tileRows = 5;
    public static  int tileCols = 5;
    public static  int TileHeight = 90;
    public static  int TileWidth = 90;
    public static  int TileSetupWidth = 90;
    public static  string TileSetupImg = "nextPageArrow.png";
    public static  int MarginTop  = 10;
    public static  int MarginBottom  = 10;
    public static  int MarginLeft  = 10;
    public static  int MarginRight  = 10;
    public static  int OffsetTop  = 0;
    public static  int OffsetLeft  = 0;
    public static  int SpacingTiles = 10;
    public static  int WideSpacing = 20;
    public static  string[] WideColumns = {"0"};
    public static  string[] WideRows = {"0"};
    //public static  int ActiveBorder = 2;
    public static  string ImgSubfolder = "images";
    public static  string ImgMissing = "missing.png";
    public static  string ImgRunning = "missing.png";
    public static  double ImgRunningOpacity = 1;
    public static  bool MarkRunning = false;
    public static  string MarkRunExeptions = "svchost.exe, lsass.exe, runtimebroker.exe, csrss.exe, services.exe, searchindexer.exe";
    public static  int TileTitleTop = 50;
    public static  int TileTitleLeft = 5;
    public static  int TileFontSize = 13;
    public static  string TileFontFamily = "arial";
    public static  int TileIconSize = 48;
    public static  int TileIconOffset = 0;
    public static  int TimerInterval = 25;


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
                            Console.WriteLine("Tiles ChildNodes to process: " + nodeTiles.ChildNodes.Count.ToString());  //TEMP

                            //XmlNodeList elemListNode1 = nodeRootChild.ChildNodes;
                            //IEnumerator elemEnumNode1 = elemListNode1.GetEnumerator();

                            IEnumerator enumNodeTiles = nodeTiles.ChildNodes.GetEnumerator(); //enum all Tile nodes

                            int tileNumber = 0;
                            while (enumNodeTiles.MoveNext())
                            {
                                XmlNode nodeTileCurrent = (XmlNode)enumNodeTiles.Current; //Evaluate Current Tile

                                if (nodeTileCurrent.NodeType == XmlNodeType.Element)
                                {
                                    IEnumerator enumNodeTileCurrentProperties= nodeTileCurrent.ChildNodes.GetEnumerator(); //enum current Tile's child nodes                                   
                                    string currentTile = "Tile" + GeneralUtils.IntDecPlaces(tileNumber); //define Tile.key
                                    while (enumNodeTileCurrentProperties.MoveNext())
                                        {
                                            XmlNode nodeProperty = (XmlNode)enumNodeTileCurrentProperties.Current;
                                            if (nodeProperty.NodeType == XmlNodeType.Element)
                                                {
                                                switch (nodeProperty.Name.ToLower().Trim()) //use lowwercase, trimmed version of XML element
	                                                    {
                                                            case "title":
                                                     dictTiles.Add(currentTile, new MetroForm.Tile() { Title = nodeProperty.InnerXml.Trim() });
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
                            XmlNode nodeHotkeys = nodeRootChild;
                                    Console.WriteLine("Process Hotkeys: " + nodeHotkeys.ChildNodes.Count.ToString());    //TEMP
                        //Console.WriteLine("HotkeySettings: " + node.ChildNodes.Count.ToString());
                            break;
                        case "MetroColors":  //Evaluate child nodes
                            XmlNode nodeColors = nodeRootChild;
                                    Console.WriteLine("Process MetroColors: " + nodeColors.ChildNodes.Count.ToString()); //TEMP   
                            //Console.WriteLine("MetroColors: " + node.ChildNodes.Count.ToString());
                            break;

                        case "TileSettings":  //Evaluate child nodes
                            XmlNode nodeSettings = nodeRootChild;
                            Console.WriteLine("Process TileSettings: " + nodeSettings.ChildNodes.Count.ToString()); //TEMP
                            IEnumerator enumNodeSettings = nodeSettings.ChildNodes.GetEnumerator(); //enum all Setting nodes
                            //int tileNumber = 0;
                            while (enumNodeSettings.MoveNext())
                            {
                                XmlNode nodeSetting = (XmlNode)enumNodeSettings.Current;
                                if (nodeSetting.NodeType == XmlNodeType.Element)
                                {
                                    switch (nodeSetting.Name.ToLower().Trim()) //use lowwercase, trimmed version of XML element
	                                {
                                        case "columns":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "rows":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "margintop":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "marginbottom":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "marginleft":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "marginright":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "offsetleft":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "offsettop":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "spacingtiles":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "widespacing":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "widecolumns":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "widerows":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "activeborder":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "imgsubfolder":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "imgmissing":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "imgrunning":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "imgrunningopacity":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "markrunning":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "markrunexeptions":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "tileheight":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "tilewidth":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "tiletitletop":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "tiletitleleft":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "tilefontsize":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "tilefontfamily":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "tilesetupwidth":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "tilesetupimg":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "tileiconsize":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "tileiconoffset":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
                                        break;
                                        case "timerinterval":
                                        dictSettings.Add(nodeSetting.Name.ToLower().Trim(), nodeSetting.InnerXml.Trim());
                                    //Console.WriteLine(nodeSetting.Name.ToLower().Trim() + " : " + dictSettings[nodeSetting.Name.ToLower().Trim()]); //TEMP
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
        public static string IntDecPlaces(int arg)
        {
            string decPlaces3 = "000"; //target decimal places
            int sourcePlaces = arg.ToString().Length;
            string result = decPlaces3.Substring(0, (decPlaces3.Length-sourcePlaces)) + arg.ToString();
            return result; //return padded integer
          }

        public static string printOutDict(Dictionary<string, MetroForm.Tile> dictInput) //Print out entire dictionary
            {
              foreach (KeyValuePair<string, MetroForm.Tile> kvp in dictInput) //list all
              {
                  //kvp.Value.Title=kvp.Value.Title + " " + kvp.Key.ToString();
                  Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value.Title);
              }

            string result = "";
            return result;
            }      
          }
}
