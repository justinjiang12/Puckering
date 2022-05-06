using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace RouteButler_Yaskawa
{
    [Serializable]
    public class RouteBook_Yaskawa
    {
        public int CommandCount { get; set; }
        public int PointCount { get; set; }
        public int DoutCount { get; set; }
        public int RobotCommandCount { get; set; }
        public int WeldingCount { get; set; }  //Welding

        public string FileName;
        public int PointNumber { get; set; }
        public int DoutNumber { get; set; }
        public int RobotCommandNumber { get; set; }
        public int WeldingNumber { get; set; } //Welding


        public int[] MovingMode { get; set; }//1 linear ; 2 spline;
        public int[] DOutMode { get; set; }//+ ON ; - OFF
        public string[] RobotCommand { get; set; }//+ ON ; - OFF
        public int[] ProcessQueue { get; set; }//1:move ; 2:digital output ; 3:direct command
        public double[] Override { get; set; }//10~1500
        public int[] Accerlerate { get; set; }//20~90
        public int[] Decerlerate { get; set; }//20~90
        public double[] X { get; set; }
        public double[] Y { get; set; }
        public double[] Z { get; set; }
        public double[] A { get; set; }
        public double[] B { get; set; }
        public double[] C { get; set; }
        public int Workspace { get; set; }//0 OFF ; 1~23
        public int FlipMode { get; set; }//0 flip ; 1 non-flip
        public int[] Tool { get; set; }//0 OFF ; 1~23



        public RouteBook_Yaskawa() { }

        public RouteBook_Yaskawa(string _fileName = "None", int _pointNumber = 1, int _doutNumber = 0, int _robotCommandNumber = 0, int _WeldingNumber = 0)
        {
            CommandCount = 0;
            PointCount = 0;
            DoutCount = 0;
            RobotCommandCount = 0;

            FileName = _fileName;
            PointNumber = _pointNumber;
            DoutNumber = _doutNumber;
            RobotCommandNumber = _robotCommandNumber;
            WeldingNumber = _WeldingNumber;

            ProcessQueue = new int[_pointNumber + _doutNumber + _robotCommandNumber+ _WeldingNumber];
            MovingMode = new int[_pointNumber];
            DOutMode = new int[_doutNumber];
            RobotCommand = new string[_robotCommandNumber];



            Override = new double[_pointNumber];
            Accerlerate = new int[_pointNumber];
            Decerlerate = new int[_pointNumber];

            X = new double[_pointNumber];
            Y = new double[_pointNumber];
            Z = new double[_pointNumber];
            A = new double[_pointNumber];
            B = new double[_pointNumber];
            C = new double[_pointNumber];

            Workspace = 0;
            FlipMode = 0;

            Tool = new int[_pointNumber];
        }
    }

    public class Librarian_Yaskawa
    {
        public void SaveFile(RouteBook_Yaskawa _routeBook, string _filepath, string _filename)
        {
            string path = _filepath + "\\" + "YASKAWA_" + _filename + ".txt";
            using (FileStream oFileStream = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(oFileStream, _routeBook);
                oFileStream.Flush();
                oFileStream.Close();
                oFileStream.Dispose();
            };
        }

        public RouteBook_Yaskawa LoadFile(string _filepath, string _filename)
        {
            string path = _filepath + "\\" + "YASKAWA_" + _filename + ".txt";
            using (FileStream oFileStream = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                RouteBook_Yaskawa pathStructure = (RouteBook_Yaskawa)binaryFormatter.Deserialize(oFileStream);
                oFileStream.Flush();
                oFileStream.Close();
                oFileStream.Dispose();
                return pathStructure;
            }
        }
    }
}
