using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace exchange
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public class people
    {
        public String ad { get; set; }
        public String data0 { get; set; }
        public String data1 { get; set; }
        public String data2 { get; set; }
        public String data3 { get; set; }
        public String data4 { get; set; }
        public String data5 { get; set; }
        public String data6 { get; set; }
        public String data7 { get; set; }
        public String data8 { get; set; }
        public String data9 { get; set; }
        public String data10 { get; set; }
        public String data11 { get; set; }
        public String data12 { get; set; }
        public String data13 { get; set; }
        public String data14 { get; set; }
        public String data15 { get; set; }

    }
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Hello!");
            foreach (MenuItem item in FileMenuItem.Items)
            {
                FileMenuItem.IsSubmenuOpen = true;
               // item.IsEnabled = true;//启用菜单项  

            }
        }
        public int ConvertToHex(String c)
        {
            int temp = 0;
            foreach (char str in c)
            {
                int hextemp = 0;
                switch ((int)str)
                {
                    case 0x30: hextemp = 0;break;
                    case 0x31: hextemp = 1; break;
                    case 0x32: hextemp = 2; break;
                    case 0x33: hextemp = 3; break;
                    case 0x34: hextemp = 4; break;
                    case 0x35: hextemp = 5; break;
                    case 0x36: hextemp = 6; break;
                    case 0x37: hextemp = 7; break;
                    case 0x38: hextemp = 8; break;
                    case 0x39: hextemp = 9; break;
                    case 0x41: hextemp = 10; break;
                    case 0x42: hextemp = 11; break;
                    case 0x43: hextemp = 12; break;
                    case 0x44: hextemp = 13; break;
                    case 0x45: hextemp = 14; break;
                    case 0x46: hextemp = 15; break;
                }
                    temp = (temp <<4) + hextemp;
               
            }
            return temp;
        }
        public String ConvertToString(int data)
        {
            String temp = "";
            int[] datatemp = new int[8];

            datatemp[0] = ((data >> 28) & 0x0f) % 16;
            datatemp[1] = ((data >> 24) & 0x0f) % 16;
            datatemp[2] = ((data >> 20) & 0x0f) % 16;
            datatemp[3] = ((data >> 16) & 0x0f) % 16;
            datatemp[4] = ((data >> 12) & 0x0f) % 16;
            datatemp[5] = ((data >> 8) & 0x0f) % 16;
            datatemp[6] = ((data >> 4) & 0x0f) % 16;
            datatemp[7] = (data  & 0x0f) % 16;
            
            for (int i = 0; i < 8; i++)
            {
                switch (datatemp[i])
                {
                    case 0:temp += "0";break;
                    case 1: temp += "1"; break;
                    case 2: temp += "2"; break;
                    case 3: temp += "3"; break;
                    case 4: temp += "4"; break;
                    case 5: temp += "5"; break;
                    case 6: temp += "6"; break;
                    case 7: temp += "7"; break;
                    case 8: temp += "8"; break;
                    case 9: temp += "9"; break;
                    case 10: temp += "A"; break;
                    case 11: temp += "B"; break;
                    case 12: temp += "C"; break;
                    case 13: temp += "D"; break;
                    case 14: temp += "E"; break;
                    case 15: temp += "F"; break;
                }
            }

            return temp;

        }
        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<people> peopleList = new ObservableCollection<people>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Filter = "Data 文件|*.s19;*.hex;*.bin;*.srec|s19文件|*.s19|hex文件|*.hex";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "*hex";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    if ((openFileDialog.OpenFile()) != null)
                    {
                        String szLine;
                        int StartAdd = 0,IncAdd=0;
                        SubWindow1 ow = new SubWindow1();
                        ow.Show();
                        ow.Title = openFileDialog.FileName;
                        StreamReader HexReader = new StreamReader(openFileDialog.FileName);
                        szLine = HexReader.ReadLine(); //读取一行数据
                        if (szLine.Substring(0,1)==":")
                        {
                            int StartAddSize = ConvertToHex(szLine.Substring(1, 2));
                            if ((StartAddSize < 0x10) && (StartAddSize > 0))
                            {
                                if (Convert.ToInt32(szLine.Substring(7, 2)) == 4)
                                    StartAdd = (ConvertToHex(szLine.Substring(9, 4))<<16);
                            }
                            else if ((StartAddSize >=0x10) && (Convert.ToInt32(szLine.Substring(7, 2)) == 0))
                            {
                                if (StartAddSize > 0x10)
                                {
                                    peopleList.Add(new people()
                                    {
                                        ad = ConvertToString(StartAdd + IncAdd),
                                        data0 = szLine.Substring(9, 2),
                                        data1 = szLine.Substring(11, 2),
                                        data2 = szLine.Substring(13, 2),
                                        data3 = szLine.Substring(15, 2),
                                        data4 = szLine.Substring(17, 2),
                                        data5 = szLine.Substring(19, 2),
                                        data6 = szLine.Substring(21, 2),
                                        data7 = szLine.Substring(23, 2),
                                        data8 = szLine.Substring(25, 2),
                                        data9 = szLine.Substring(27, 2),
                                        data10 = szLine.Substring(29, 2),
                                        data11 = szLine.Substring(31, 2),
                                        data12 = szLine.Substring(33, 2),
                                        data13 = szLine.Substring(35, 2),
                                        data14 = szLine.Substring(37, 2),
                                        data15 = szLine.Substring(39, 2),

                                    });
                                    switch (StartAddSize - 0x10)
                                    {
                                        case 0:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    //data0 = szLine.Substring(41, 2),
                                                    //data1 = szLine.Substring(43, 2),
                                                    //data2 = szLine.Substring(45, 2),
                                                    //data3 = szLine.Substring(47, 2),
                                                    //data4 = szLine.Substring(49, 2),
                                                    //data5 = szLine.Substring(51, 2),
                                                    //data6 = szLine.Substring(53, 2),
                                                    //data7 = szLine.Substring(55, 2),
                                                    //data8 = szLine.Substring(57, 2),
                                                    //data9 = szLine.Substring(59, 2),
                                                    //data10 = szLine.Substring(61, 2),
                                                    //data11 = szLine.Substring(63, 2),
                                                    //data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 1:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    //data1 = szLine.Substring(43, 2),
                                                    //data2 = szLine.Substring(45, 2),
                                                    //data3 = szLine.Substring(47, 2),
                                                    //data4 = szLine.Substring(49, 2),
                                                    //data5 = szLine.Substring(51, 2),
                                                    //data6 = szLine.Substring(53, 2),
                                                    //data7 = szLine.Substring(55, 2),
                                                    //data8 = szLine.Substring(57, 2),
                                                    //data9 = szLine.Substring(59, 2),
                                                    //data10 = szLine.Substring(61, 2),
                                                    //data11 = szLine.Substring(63, 2),
                                                    //data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 2:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    //data2 = szLine.Substring(45, 2),
                                                    //data3 = szLine.Substring(47, 2),
                                                    //data4 = szLine.Substring(49, 2),
                                                    //data5 = szLine.Substring(51, 2),
                                                    //data6 = szLine.Substring(53, 2),
                                                    //data7 = szLine.Substring(55, 2),
                                                    //data8 = szLine.Substring(57, 2),
                                                    //data9 = szLine.Substring(59, 2),
                                                    //data10 = szLine.Substring(61, 2),
                                                    //data11 = szLine.Substring(63, 2),
                                                    //data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 3:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    //data3 = szLine.Substring(47, 2),
                                                    //data4 = szLine.Substring(49, 2),
                                                    //data5 = szLine.Substring(51, 2),
                                                    //data6 = szLine.Substring(53, 2),
                                                    //data7 = szLine.Substring(55, 2),
                                                    //data8 = szLine.Substring(57, 2),
                                                    //data9 = szLine.Substring(59, 2),
                                                    //data10 = szLine.Substring(61, 2),
                                                    //data11 = szLine.Substring(63, 2),
                                                    //data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 4:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    data3 = szLine.Substring(47, 2),
                                                    //data4 = szLine.Substring(49, 2),
                                                    //data5 = szLine.Substring(51, 2),
                                                    //data6 = szLine.Substring(53, 2),
                                                    //data7 = szLine.Substring(55, 2),
                                                    //data8 = szLine.Substring(57, 2),
                                                    //data9 = szLine.Substring(59, 2),
                                                    //data10 = szLine.Substring(61, 2),
                                                    //data11 = szLine.Substring(63, 2),
                                                    //data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 5:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    data3 = szLine.Substring(47, 2),
                                                    data4 = szLine.Substring(49, 2),
                                                    //data5 = szLine.Substring(51, 2),
                                                    //data6 = szLine.Substring(53, 2),
                                                    //data7 = szLine.Substring(55, 2),
                                                    //data8 = szLine.Substring(57, 2),
                                                    //data9 = szLine.Substring(59, 2),
                                                    //data10 = szLine.Substring(61, 2),
                                                    //data11 = szLine.Substring(63, 2),
                                                    //data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 6:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    data3 = szLine.Substring(47, 2),
                                                    data4 = szLine.Substring(49, 2),
                                                    data5 = szLine.Substring(51, 2),
                                                    //data6 = szLine.Substring(53, 2),
                                                    //data7 = szLine.Substring(55, 2),
                                                    //data8 = szLine.Substring(57, 2),
                                                    //data9 = szLine.Substring(59, 2),
                                                    //data10 = szLine.Substring(61, 2),
                                                    //data11 = szLine.Substring(63, 2),
                                                    //data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 7:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    data3 = szLine.Substring(47, 2),
                                                    data4 = szLine.Substring(49, 2),
                                                    data5 = szLine.Substring(51, 2),
                                                    data6 = szLine.Substring(53, 2),
                                                    //data7 = szLine.Substring(55, 2),
                                                    //data8 = szLine.Substring(57, 2),
                                                    //data9 = szLine.Substring(59, 2),
                                                    //data10 = szLine.Substring(61, 2),
                                                    //data11 = szLine.Substring(63, 2),
                                                    //data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 8:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    data3 = szLine.Substring(47, 2),
                                                    data4 = szLine.Substring(49, 2),
                                                    data5 = szLine.Substring(51, 2),
                                                    data6 = szLine.Substring(53, 2),
                                                    data7 = szLine.Substring(55, 2),
                                                    //data8 = szLine.Substring(57, 2),
                                                    //data9 = szLine.Substring(59, 2),
                                                    //data10 = szLine.Substring(61, 2),
                                                    //data11 = szLine.Substring(63, 2),
                                                    //data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 9:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    data3 = szLine.Substring(47, 2),
                                                    data4 = szLine.Substring(49, 2),
                                                    data5 = szLine.Substring(51, 2),
                                                    data6 = szLine.Substring(53, 2),
                                                    data7 = szLine.Substring(55, 2),
                                                    data8 = szLine.Substring(57, 2),
                                                    //data9 = szLine.Substring(59, 2),
                                                    //data10 = szLine.Substring(61, 2),
                                                    //data11 = szLine.Substring(63, 2),
                                                    //data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 10:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    data3 = szLine.Substring(47, 2),
                                                    data4 = szLine.Substring(49, 2),
                                                    data5 = szLine.Substring(51, 2),
                                                    data6 = szLine.Substring(53, 2),
                                                    data7 = szLine.Substring(55, 2),
                                                    data8 = szLine.Substring(57, 2),
                                                    data9 = szLine.Substring(59, 2),
                                                    //data10 = szLine.Substring(61, 2),
                                                    //data11 = szLine.Substring(63, 2),
                                                    //data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 11:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    data3 = szLine.Substring(47, 2),
                                                    data4 = szLine.Substring(49, 2),
                                                    data5 = szLine.Substring(51, 2),
                                                    data6 = szLine.Substring(53, 2),
                                                    data7 = szLine.Substring(55, 2),
                                                    data8 = szLine.Substring(57, 2),
                                                    data9 = szLine.Substring(59, 2),
                                                    data10 = szLine.Substring(61, 2),
                                                    //data11 = szLine.Substring(63, 2),
                                                    //data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 12:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    data3 = szLine.Substring(47, 2),
                                                    data4 = szLine.Substring(49, 2),
                                                    data5 = szLine.Substring(51, 2),
                                                    data6 = szLine.Substring(53, 2),
                                                    data7 = szLine.Substring(55, 2),
                                                    data8 = szLine.Substring(57, 2),
                                                    data9 = szLine.Substring(59, 2),
                                                    data10 = szLine.Substring(61, 2),
                                                    data11 = szLine.Substring(63, 2),
                                                    //data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 13:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    data3 = szLine.Substring(47, 2),
                                                    data4 = szLine.Substring(49, 2),
                                                    data5 = szLine.Substring(51, 2),
                                                    data6 = szLine.Substring(53, 2),
                                                    data7 = szLine.Substring(55, 2),
                                                    data8 = szLine.Substring(57, 2),
                                                    data9 = szLine.Substring(59, 2),
                                                    data10 = szLine.Substring(61, 2),
                                                    data11 = szLine.Substring(63, 2),
                                                    data12 = szLine.Substring(65, 2),
                                                    //data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 14:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    data3 = szLine.Substring(47, 2),
                                                    data4 = szLine.Substring(49, 2),
                                                    data5 = szLine.Substring(51, 2),
                                                    data6 = szLine.Substring(53, 2),
                                                    data7 = szLine.Substring(55, 2),
                                                    data8 = szLine.Substring(57, 2),
                                                    data9 = szLine.Substring(59, 2),
                                                    data10 = szLine.Substring(61, 2),
                                                    data11 = szLine.Substring(63, 2),
                                                    data12 = szLine.Substring(65, 2),
                                                    data13 = szLine.Substring(67, 2),
                                                    //data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 15:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    data3 = szLine.Substring(47, 2),
                                                    data4 = szLine.Substring(49, 2),
                                                    data5 = szLine.Substring(51, 2),
                                                    data6 = szLine.Substring(53, 2),
                                                    data7 = szLine.Substring(55, 2),
                                                    data8 = szLine.Substring(57, 2),
                                                    data9 = szLine.Substring(59, 2),
                                                    data10 = szLine.Substring(61, 2),
                                                    data11 = szLine.Substring(63, 2),
                                                    data12 = szLine.Substring(65, 2),
                                                    data13 = szLine.Substring(67, 2),
                                                    data14 = szLine.Substring(69, 2),
                                                    //data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;
                                        case 16:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd + 16),
                                                    data0 = szLine.Substring(41, 2),
                                                    data1 = szLine.Substring(43, 2),
                                                    data2 = szLine.Substring(45, 2),
                                                    data3 = szLine.Substring(47, 2),
                                                    data4 = szLine.Substring(49, 2),
                                                    data5 = szLine.Substring(51, 2),
                                                    data6 = szLine.Substring(53, 2),
                                                    data7 = szLine.Substring(55, 2),
                                                    data8 = szLine.Substring(57, 2),
                                                    data9 = szLine.Substring(59, 2),
                                                    data10 = szLine.Substring(61, 2),
                                                    data11 = szLine.Substring(63, 2),
                                                    data12 = szLine.Substring(65, 2),
                                                    data13 = szLine.Substring(67, 2),
                                                    data14 = szLine.Substring(69, 2),
                                                    data15 = szLine.Substring(71, 2),

                                                });
                                            }
                                            break;

                                    }
                                }
                                else
                                {
                                    switch (StartAddSize)
                                    {
                                        case 0:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    //data0 = szLine.Substring(9, 2),
                                                    //data1 = szLine.Substring(11, 2),
                                                    //data2 = szLine.Substring(13, 2),
                                                    //data3 = szLine.Substring(15, 2),
                                                    //data4 = szLine.Substring(17, 2),
                                                    //data5 = szLine.Substring(19, 2),
                                                    //data6 = szLine.Substring(21, 2),
                                                    //data7 = szLine.Substring(23, 2),
                                                    //data8 = szLine.Substring(25, 2),
                                                    //data9 = szLine.Substring(27, 2),
                                                    //data10 = szLine.Substring(29, 2),
                                                    //data11 = szLine.Substring(31, 2),
                                                    //data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 1:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    //data1 = szLine.Substring(11, 2),
                                                    //data2 = szLine.Substring(13, 2),
                                                    //data3 = szLine.Substring(15, 2),
                                                    //data4 = szLine.Substring(17, 2),
                                                    //data5 = szLine.Substring(19, 2),
                                                    //data6 = szLine.Substring(21, 2),
                                                    //data7 = szLine.Substring(23, 2),
                                                    //data8 = szLine.Substring(25, 2),
                                                    //data9 = szLine.Substring(27, 2),
                                                    //data10 = szLine.Substring(29, 2),
                                                    //data11 = szLine.Substring(31, 2),
                                                    //data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 2:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    //data2 = szLine.Substring(13, 2),
                                                    //data3 = szLine.Substring(15, 2),
                                                    //data4 = szLine.Substring(17, 2),
                                                    //data5 = szLine.Substring(19, 2),
                                                    //data6 = szLine.Substring(21, 2),
                                                    //data7 = szLine.Substring(23, 2),
                                                    //data8 = szLine.Substring(25, 2),
                                                    //data9 = szLine.Substring(27, 2),
                                                    //data10 = szLine.Substring(29, 2),
                                                    //data11 = szLine.Substring(31, 2),
                                                    //data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 3:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    //data3 = szLine.Substring(15, 2),
                                                    //data4 = szLine.Substring(17, 2),
                                                    //data5 = szLine.Substring(19, 2),
                                                    //data6 = szLine.Substring(21, 2),
                                                    //data7 = szLine.Substring(23, 2),
                                                    //data8 = szLine.Substring(25, 2),
                                                    //data9 = szLine.Substring(27, 2),
                                                    //data10 = szLine.Substring(29, 2),
                                                    //data11 = szLine.Substring(31, 2),
                                                    //data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 4:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    data3 = szLine.Substring(15, 2),
                                                    //data4 = szLine.Substring(17, 2),
                                                    //data5 = szLine.Substring(19, 2),
                                                    //data6 = szLine.Substring(21, 2),
                                                    //data7 = szLine.Substring(23, 2),
                                                    //data8 = szLine.Substring(25, 2),
                                                    //data9 = szLine.Substring(27, 2),
                                                    //data10 = szLine.Substring(29, 2),
                                                    //data11 = szLine.Substring(31, 2),
                                                    //data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 5:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    data3 = szLine.Substring(15, 2),
                                                    data4 = szLine.Substring(17, 2),
                                                    //data5 = szLine.Substring(19, 2),
                                                    //data6 = szLine.Substring(21, 2),
                                                    //data7 = szLine.Substring(23, 2),
                                                    //data8 = szLine.Substring(25, 2),
                                                    //data9 = szLine.Substring(27, 2),
                                                    //data10 = szLine.Substring(29, 2),
                                                    //data11 = szLine.Substring(31, 2),
                                                    //data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 6:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    data3 = szLine.Substring(15, 2),
                                                    data4 = szLine.Substring(17, 2),
                                                    data5 = szLine.Substring(19, 2),
                                                    //data6 = szLine.Substring(21, 2),
                                                    //data7 = szLine.Substring(23, 2),
                                                    //data8 = szLine.Substring(25, 2),
                                                    //data9 = szLine.Substring(27, 2),
                                                    //data10 = szLine.Substring(29, 2),
                                                    //data11 = szLine.Substring(31, 2),
                                                    //data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 7:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    data3 = szLine.Substring(15, 2),
                                                    data4 = szLine.Substring(17, 2),
                                                    data5 = szLine.Substring(19, 2),
                                                    data6 = szLine.Substring(21, 2),
                                                    //data7 = szLine.Substring(23, 2),
                                                    //data8 = szLine.Substring(25, 2),
                                                    //data9 = szLine.Substring(27, 2),
                                                    //data10 = szLine.Substring(29, 2),
                                                    //data11 = szLine.Substring(31, 2),
                                                    //data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 8:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    data3 = szLine.Substring(15, 2),
                                                    data4 = szLine.Substring(17, 2),
                                                    data5 = szLine.Substring(19, 2),
                                                    data6 = szLine.Substring(21, 2),
                                                    data7 = szLine.Substring(23, 2),
                                                    //data8 = szLine.Substring(25, 2),
                                                    //data9 = szLine.Substring(27, 2),
                                                    //data10 = szLine.Substring(29, 2),
                                                    //data11 = szLine.Substring(31, 2),
                                                    //data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 9:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    data3 = szLine.Substring(15, 2),
                                                    data4 = szLine.Substring(17, 2),
                                                    data5 = szLine.Substring(19, 2),
                                                    data6 = szLine.Substring(21, 2),
                                                    data7 = szLine.Substring(23, 2),
                                                    data8 = szLine.Substring(25, 2),
                                                    //data9 = szLine.Substring(27, 2),
                                                    //data10 = szLine.Substring(29, 2),
                                                    //data11 = szLine.Substring(31, 2),
                                                    //data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 10:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    data3 = szLine.Substring(15, 2),
                                                    data4 = szLine.Substring(17, 2),
                                                    data5 = szLine.Substring(19, 2),
                                                    data6 = szLine.Substring(21, 2),
                                                    data7 = szLine.Substring(23, 2),
                                                    data8 = szLine.Substring(25, 2),
                                                    data9 = szLine.Substring(27, 2),
                                                    //data10 = szLine.Substring(29, 2),
                                                    //data11 = szLine.Substring(31, 2),
                                                    //data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 11:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    data3 = szLine.Substring(15, 2),
                                                    data4 = szLine.Substring(17, 2),
                                                    data5 = szLine.Substring(19, 2),
                                                    data6 = szLine.Substring(21, 2),
                                                    data7 = szLine.Substring(23, 2),
                                                    data8 = szLine.Substring(25, 2),
                                                    data9 = szLine.Substring(27, 2),
                                                    data10 = szLine.Substring(29, 2),
                                                    //data11 = szLine.Substring(31, 2),
                                                    //data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 12:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    data3 = szLine.Substring(15, 2),
                                                    data4 = szLine.Substring(17, 2),
                                                    data5 = szLine.Substring(19, 2),
                                                    data6 = szLine.Substring(21, 2),
                                                    data7 = szLine.Substring(23, 2),
                                                    data8 = szLine.Substring(25, 2),
                                                    data9 = szLine.Substring(27, 2),
                                                    data10 = szLine.Substring(29, 2),
                                                    data11 = szLine.Substring(31, 2),
                                                    //data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 13:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    data3 = szLine.Substring(15, 2),
                                                    data4 = szLine.Substring(17, 2),
                                                    data5 = szLine.Substring(19, 2),
                                                    data6 = szLine.Substring(21, 2),
                                                    data7 = szLine.Substring(23, 2),
                                                    data8 = szLine.Substring(25, 2),
                                                    data9 = szLine.Substring(27, 2),
                                                    data10 = szLine.Substring(29, 2),
                                                    data11 = szLine.Substring(31, 2),
                                                    data12 = szLine.Substring(33, 2),
                                                    //data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 14:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    data3 = szLine.Substring(15, 2),
                                                    data4 = szLine.Substring(17, 2),
                                                    data5 = szLine.Substring(19, 2),
                                                    data6 = szLine.Substring(21, 2),
                                                    data7 = szLine.Substring(23, 2),
                                                    data8 = szLine.Substring(25, 2),
                                                    data9 = szLine.Substring(27, 2),
                                                    data10 = szLine.Substring(29, 2),
                                                    data11 = szLine.Substring(31, 2),
                                                    data12 = szLine.Substring(33, 2),
                                                    data13 = szLine.Substring(35, 2),
                                                    //data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 15:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    data3 = szLine.Substring(15, 2),
                                                    data4 = szLine.Substring(17, 2),
                                                    data5 = szLine.Substring(19, 2),
                                                    data6 = szLine.Substring(21, 2),
                                                    data7 = szLine.Substring(23, 2),
                                                    data8 = szLine.Substring(25, 2),
                                                    data9 = szLine.Substring(27, 2),
                                                    data10 = szLine.Substring(29, 2),
                                                    data11 = szLine.Substring(31, 2),
                                                    data12 = szLine.Substring(33, 2),
                                                    data13 = szLine.Substring(35, 2),
                                                    data14 = szLine.Substring(37, 2),
                                                    //data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;
                                        case 16:
                                            {
                                                peopleList.Add(new people()
                                                {
                                                    ad = ConvertToString(StartAdd + IncAdd),
                                                    data0 = szLine.Substring(9, 2),
                                                    data1 = szLine.Substring(11, 2),
                                                    data2 = szLine.Substring(13, 2),
                                                    data3 = szLine.Substring(15, 2),
                                                    data4 = szLine.Substring(17, 2),
                                                    data5 = szLine.Substring(19, 2),
                                                    data6 = szLine.Substring(21, 2),
                                                    data7 = szLine.Substring(23, 2),
                                                    data8 = szLine.Substring(25, 2),
                                                    data9 = szLine.Substring(27, 2),
                                                    data10 = szLine.Substring(29, 2),
                                                    data11 = szLine.Substring(31, 2),
                                                    data12 = szLine.Substring(33, 2),
                                                    data13 = szLine.Substring(35, 2),
                                                    data14 = szLine.Substring(37, 2),
                                                    data15 = szLine.Substring(39, 2),

                                                });
                                            }
                                            break;

                                    }
                                }
                               
                            }
                        }
                        do
                        {
                            szLine = HexReader.ReadLine();
                            if (szLine.Substring(0, 1) == ":")
                            {
                                int DataSize = ConvertToHex(szLine.Substring(1, 2));
                                if (ConvertToHex(szLine.Substring(7, 2)) == 0)
                                {
                                    IncAdd = ConvertToHex(szLine.Substring(3, 4));
                                    
                                    
                                    if (DataSize > 0x10)
                                    {
                                        peopleList.Add(new people()
                                        {
                                            ad = ConvertToString(StartAdd + IncAdd),
                                            data0 = szLine.Substring(9, 2),
                                            data1 = szLine.Substring(11, 2),
                                            data2 = szLine.Substring(13, 2),
                                            data3 = szLine.Substring(15, 2),
                                            data4 = szLine.Substring(17, 2),
                                            data5 = szLine.Substring(19, 2),
                                            data6 = szLine.Substring(21, 2),
                                            data7 = szLine.Substring(23, 2),
                                            data8 = szLine.Substring(25, 2),
                                            data9 = szLine.Substring(27, 2),
                                            data10 = szLine.Substring(29, 2),
                                            data11 = szLine.Substring(31, 2),
                                            data12 = szLine.Substring(33, 2),
                                            data13 = szLine.Substring(35, 2),
                                            data14 = szLine.Substring(37, 2),
                                            data15 = szLine.Substring(39, 2),
                                           
                                        });
                                        switch (DataSize-0x10)
                                        {
                                            case 0:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        //data0 = szLine.Substring(41, 2),
                                                        //data1 = szLine.Substring(43, 2),
                                                        //data2 = szLine.Substring(45, 2),
                                                        //data3 = szLine.Substring(47, 2),
                                                        //data4 = szLine.Substring(49, 2),
                                                        //data5 = szLine.Substring(51, 2),
                                                        //data6 = szLine.Substring(53, 2),
                                                        //data7 = szLine.Substring(55, 2),
                                                        //data8 = szLine.Substring(57, 2),
                                                        //data9 = szLine.Substring(59, 2),
                                                        //data10 = szLine.Substring(61, 2),
                                                        //data11 = szLine.Substring(63, 2),
                                                        //data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 1:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        //data1 = szLine.Substring(43, 2),
                                                        //data2 = szLine.Substring(45, 2),
                                                        //data3 = szLine.Substring(47, 2),
                                                        //data4 = szLine.Substring(49, 2),
                                                        //data5 = szLine.Substring(51, 2),
                                                        //data6 = szLine.Substring(53, 2),
                                                        //data7 = szLine.Substring(55, 2),
                                                        //data8 = szLine.Substring(57, 2),
                                                        //data9 = szLine.Substring(59, 2),
                                                        //data10 = szLine.Substring(61, 2),
                                                        //data11 = szLine.Substring(63, 2),
                                                        //data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 2:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        //data2 = szLine.Substring(45, 2),
                                                        //data3 = szLine.Substring(47, 2),
                                                        //data4 = szLine.Substring(49, 2),
                                                        //data5 = szLine.Substring(51, 2),
                                                        //data6 = szLine.Substring(53, 2),
                                                        //data7 = szLine.Substring(55, 2),
                                                        //data8 = szLine.Substring(57, 2),
                                                        //data9 = szLine.Substring(59, 2),
                                                        //data10 = szLine.Substring(61, 2),
                                                        //data11 = szLine.Substring(63, 2),
                                                        //data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 3:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        //data3 = szLine.Substring(47, 2),
                                                        //data4 = szLine.Substring(49, 2),
                                                        //data5 = szLine.Substring(51, 2),
                                                        //data6 = szLine.Substring(53, 2),
                                                        //data7 = szLine.Substring(55, 2),
                                                        //data8 = szLine.Substring(57, 2),
                                                        //data9 = szLine.Substring(59, 2),
                                                        //data10 = szLine.Substring(61, 2),
                                                        //data11 = szLine.Substring(63, 2),
                                                        //data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 4:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        data3 = szLine.Substring(47, 2),
                                                        //data4 = szLine.Substring(49, 2),
                                                        //data5 = szLine.Substring(51, 2),
                                                        //data6 = szLine.Substring(53, 2),
                                                        //data7 = szLine.Substring(55, 2),
                                                        //data8 = szLine.Substring(57, 2),
                                                        //data9 = szLine.Substring(59, 2),
                                                        //data10 = szLine.Substring(61, 2),
                                                        //data11 = szLine.Substring(63, 2),
                                                        //data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 5:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        data3 = szLine.Substring(47, 2),
                                                        data4 = szLine.Substring(49, 2),
                                                        //data5 = szLine.Substring(51, 2),
                                                        //data6 = szLine.Substring(53, 2),
                                                        //data7 = szLine.Substring(55, 2),
                                                        //data8 = szLine.Substring(57, 2),
                                                        //data9 = szLine.Substring(59, 2),
                                                        //data10 = szLine.Substring(61, 2),
                                                        //data11 = szLine.Substring(63, 2),
                                                        //data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 6:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        data3 = szLine.Substring(47, 2),
                                                        data4 = szLine.Substring(49, 2),
                                                        data5 = szLine.Substring(51, 2),
                                                        //data6 = szLine.Substring(53, 2),
                                                        //data7 = szLine.Substring(55, 2),
                                                        //data8 = szLine.Substring(57, 2),
                                                        //data9 = szLine.Substring(59, 2),
                                                        //data10 = szLine.Substring(61, 2),
                                                        //data11 = szLine.Substring(63, 2),
                                                        //data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 7:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        data3 = szLine.Substring(47, 2),
                                                        data4 = szLine.Substring(49, 2),
                                                        data5 = szLine.Substring(51, 2),
                                                        data6 = szLine.Substring(53, 2),
                                                        //data7 = szLine.Substring(55, 2),
                                                        //data8 = szLine.Substring(57, 2),
                                                        //data9 = szLine.Substring(59, 2),
                                                        //data10 = szLine.Substring(61, 2),
                                                        //data11 = szLine.Substring(63, 2),
                                                        //data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 8:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        data3 = szLine.Substring(47, 2),
                                                        data4 = szLine.Substring(49, 2),
                                                        data5 = szLine.Substring(51, 2),
                                                        data6 = szLine.Substring(53, 2),
                                                        data7 = szLine.Substring(55, 2),
                                                        //data8 = szLine.Substring(57, 2),
                                                        //data9 = szLine.Substring(59, 2),
                                                        //data10 = szLine.Substring(61, 2),
                                                        //data11 = szLine.Substring(63, 2),
                                                        //data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 9:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        data3 = szLine.Substring(47, 2),
                                                        data4 = szLine.Substring(49, 2),
                                                        data5 = szLine.Substring(51, 2),
                                                        data6 = szLine.Substring(53, 2),
                                                        data7 = szLine.Substring(55, 2),
                                                        data8 = szLine.Substring(57, 2),
                                                        //data9 = szLine.Substring(59, 2),
                                                        //data10 = szLine.Substring(61, 2),
                                                        //data11 = szLine.Substring(63, 2),
                                                        //data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 10:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        data3 = szLine.Substring(47, 2),
                                                        data4 = szLine.Substring(49, 2),
                                                        data5 = szLine.Substring(51, 2),
                                                        data6 = szLine.Substring(53, 2),
                                                        data7 = szLine.Substring(55, 2),
                                                        data8 = szLine.Substring(57, 2),
                                                        data9 = szLine.Substring(59, 2),
                                                        //data10 = szLine.Substring(61, 2),
                                                        //data11 = szLine.Substring(63, 2),
                                                        //data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 11:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        data3 = szLine.Substring(47, 2),
                                                        data4 = szLine.Substring(49, 2),
                                                        data5 = szLine.Substring(51, 2),
                                                        data6 = szLine.Substring(53, 2),
                                                        data7 = szLine.Substring(55, 2),
                                                        data8 = szLine.Substring(57, 2),
                                                        data9 = szLine.Substring(59, 2),
                                                        data10 = szLine.Substring(61, 2),
                                                        //data11 = szLine.Substring(63, 2),
                                                        //data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 12:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        data3 = szLine.Substring(47, 2),
                                                        data4 = szLine.Substring(49, 2),
                                                        data5 = szLine.Substring(51, 2),
                                                        data6 = szLine.Substring(53, 2),
                                                        data7 = szLine.Substring(55, 2),
                                                        data8 = szLine.Substring(57, 2),
                                                        data9 = szLine.Substring(59, 2),
                                                        data10 = szLine.Substring(61, 2),
                                                        data11 = szLine.Substring(63, 2),
                                                        //data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 13:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        data3 = szLine.Substring(47, 2),
                                                        data4 = szLine.Substring(49, 2),
                                                        data5 = szLine.Substring(51, 2),
                                                        data6 = szLine.Substring(53, 2),
                                                        data7 = szLine.Substring(55, 2),
                                                        data8 = szLine.Substring(57, 2),
                                                        data9 = szLine.Substring(59, 2),
                                                        data10 = szLine.Substring(61, 2),
                                                        data11 = szLine.Substring(63, 2),
                                                        data12 = szLine.Substring(65, 2),
                                                        //data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 14:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        data3 = szLine.Substring(47, 2),
                                                        data4 = szLine.Substring(49, 2),
                                                        data5 = szLine.Substring(51, 2),
                                                        data6 = szLine.Substring(53, 2),
                                                        data7 = szLine.Substring(55, 2),
                                                        data8 = szLine.Substring(57, 2),
                                                        data9 = szLine.Substring(59, 2),
                                                        data10 = szLine.Substring(61, 2),
                                                        data11 = szLine.Substring(63, 2),
                                                        data12 = szLine.Substring(65, 2),
                                                        data13 = szLine.Substring(67, 2),
                                                        //data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 15:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        data3 = szLine.Substring(47, 2),
                                                        data4 = szLine.Substring(49, 2),
                                                        data5 = szLine.Substring(51, 2),
                                                        data6 = szLine.Substring(53, 2),
                                                        data7 = szLine.Substring(55, 2),
                                                        data8 = szLine.Substring(57, 2),
                                                        data9 = szLine.Substring(59, 2),
                                                        data10 = szLine.Substring(61, 2),
                                                        data11 = szLine.Substring(63, 2),
                                                        data12 = szLine.Substring(65, 2),
                                                        data13 = szLine.Substring(67, 2),
                                                        data14 = szLine.Substring(69, 2),
                                                        //data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;
                                            case 16:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd + 16),
                                                        data0 = szLine.Substring(41, 2),
                                                        data1 = szLine.Substring(43, 2),
                                                        data2 = szLine.Substring(45, 2),
                                                        data3 = szLine.Substring(47, 2),
                                                        data4 = szLine.Substring(49, 2),
                                                        data5 = szLine.Substring(51, 2),
                                                        data6 = szLine.Substring(53, 2),
                                                        data7 = szLine.Substring(55, 2),
                                                        data8 = szLine.Substring(57, 2),
                                                        data9 = szLine.Substring(59, 2),
                                                        data10 = szLine.Substring(61, 2),
                                                        data11 = szLine.Substring(63, 2),
                                                        data12 = szLine.Substring(65, 2),
                                                        data13 = szLine.Substring(67, 2),
                                                        data14 = szLine.Substring(69, 2),
                                                        data15 = szLine.Substring(71, 2),

                                                    });
                                                }
                                                break;

                                        }
                                        //peopleList.Add(new people()
                                        //{
                                        //    ad = ConvertToString(StartAdd+IncAdd+16),
                                        //    data0 = szLine.Substring(41, 2),
                                        //    //data1 = szLine.Substring(43, 2),
                                        //    //data2 = szLine.Substring(45, 2),
                                        //    //data3 = szLine.Substring(47, 2),
                                        //    //data4 = szLine.Substring(49, 2),
                                        //    //data5 = szLine.Substring(51, 2),
                                        //    //data6 = szLine.Substring(53, 2),
                                        //    //data7 = szLine.Substring(55, 2),
                                        //    //data8 = szLine.Substring(57, 2),
                                        //    //data9 = szLine.Substring(59, 2),
                                        //    //data10 = szLine.Substring(61, 2),
                                        //    //data11 = szLine.Substring(63, 2),
                                        //    //data12 = szLine.Substring(65, 2),
                                        //    //data13 = szLine.Substring(67, 2),
                                        //    //data14 = szLine.Substring(69, 2),
                                        //    //data15 = szLine.Substring(71, 2),

                                        //});
                                        //ow.textBlock.Text += szLine.Substring(9, 32);
                                        //ow.textBlock.Text += "\r\n";
                                        //ow.textBlock.Text += szLine.Substring(41, DataSize * 2 - 32);
                                        //ow.textBlock.Text += "\r\n";


                                    }
                                    else if (DataSize > 0)
                                    {
                                        switch (DataSize)
                                        {
                                            case 0:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        //data0 = szLine.Substring(9, 2),
                                                        //data1 = szLine.Substring(11, 2),
                                                        //data2 = szLine.Substring(13, 2),
                                                        //data3 = szLine.Substring(15, 2),
                                                        //data4 = szLine.Substring(17, 2),
                                                        //data5 = szLine.Substring(19, 2),
                                                        //data6 = szLine.Substring(21, 2),
                                                        //data7 = szLine.Substring(23, 2),
                                                        //data8 = szLine.Substring(25, 2),
                                                        //data9 = szLine.Substring(27, 2),
                                                        //data10 = szLine.Substring(29, 2),
                                                        //data11 = szLine.Substring(31, 2),
                                                        //data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 1:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        //data1 = szLine.Substring(11, 2),
                                                        //data2 = szLine.Substring(13, 2),
                                                        //data3 = szLine.Substring(15, 2),
                                                        //data4 = szLine.Substring(17, 2),
                                                        //data5 = szLine.Substring(19, 2),
                                                        //data6 = szLine.Substring(21, 2),
                                                        //data7 = szLine.Substring(23, 2),
                                                        //data8 = szLine.Substring(25, 2),
                                                        //data9 = szLine.Substring(27, 2),
                                                        //data10 = szLine.Substring(29, 2),
                                                        //data11 = szLine.Substring(31, 2),
                                                        //data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 2:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        //data2 = szLine.Substring(13, 2),
                                                        //data3 = szLine.Substring(15, 2),
                                                        //data4 = szLine.Substring(17, 2),
                                                        //data5 = szLine.Substring(19, 2),
                                                        //data6 = szLine.Substring(21, 2),
                                                        //data7 = szLine.Substring(23, 2),
                                                        //data8 = szLine.Substring(25, 2),
                                                        //data9 = szLine.Substring(27, 2),
                                                        //data10 = szLine.Substring(29, 2),
                                                        //data11 = szLine.Substring(31, 2),
                                                        //data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 3:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        //data3 = szLine.Substring(15, 2),
                                                        //data4 = szLine.Substring(17, 2),
                                                        //data5 = szLine.Substring(19, 2),
                                                        //data6 = szLine.Substring(21, 2),
                                                        //data7 = szLine.Substring(23, 2),
                                                        //data8 = szLine.Substring(25, 2),
                                                        //data9 = szLine.Substring(27, 2),
                                                        //data10 = szLine.Substring(29, 2),
                                                        //data11 = szLine.Substring(31, 2),
                                                        //data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 4:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        data3 = szLine.Substring(15, 2),
                                                        //data4 = szLine.Substring(17, 2),
                                                        //data5 = szLine.Substring(19, 2),
                                                        //data6 = szLine.Substring(21, 2),
                                                        //data7 = szLine.Substring(23, 2),
                                                        //data8 = szLine.Substring(25, 2),
                                                        //data9 = szLine.Substring(27, 2),
                                                        //data10 = szLine.Substring(29, 2),
                                                        //data11 = szLine.Substring(31, 2),
                                                        //data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 5:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        data3 = szLine.Substring(15, 2),
                                                        data4 = szLine.Substring(17, 2),
                                                        //data5 = szLine.Substring(19, 2),
                                                        //data6 = szLine.Substring(21, 2),
                                                        //data7 = szLine.Substring(23, 2),
                                                        //data8 = szLine.Substring(25, 2),
                                                        //data9 = szLine.Substring(27, 2),
                                                        //data10 = szLine.Substring(29, 2),
                                                        //data11 = szLine.Substring(31, 2),
                                                        //data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 6:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        data3 = szLine.Substring(15, 2),
                                                        data4 = szLine.Substring(17, 2),
                                                        data5 = szLine.Substring(19, 2),
                                                        //data6 = szLine.Substring(21, 2),
                                                        //data7 = szLine.Substring(23, 2),
                                                        //data8 = szLine.Substring(25, 2),
                                                        //data9 = szLine.Substring(27, 2),
                                                        //data10 = szLine.Substring(29, 2),
                                                        //data11 = szLine.Substring(31, 2),
                                                        //data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 7:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        data3 = szLine.Substring(15, 2),
                                                        data4 = szLine.Substring(17, 2),
                                                        data5 = szLine.Substring(19, 2),
                                                        data6 = szLine.Substring(21, 2),
                                                        //data7 = szLine.Substring(23, 2),
                                                        //data8 = szLine.Substring(25, 2),
                                                        //data9 = szLine.Substring(27, 2),
                                                        //data10 = szLine.Substring(29, 2),
                                                        //data11 = szLine.Substring(31, 2),
                                                        //data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 8:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        data3 = szLine.Substring(15, 2),
                                                        data4 = szLine.Substring(17, 2),
                                                        data5 = szLine.Substring(19, 2),
                                                        data6 = szLine.Substring(21, 2),
                                                        data7 = szLine.Substring(23, 2),
                                                        //data8 = szLine.Substring(25, 2),
                                                        //data9 = szLine.Substring(27, 2),
                                                        //data10 = szLine.Substring(29, 2),
                                                        //data11 = szLine.Substring(31, 2),
                                                        //data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 9:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        data3 = szLine.Substring(15, 2),
                                                        data4 = szLine.Substring(17, 2),
                                                        data5 = szLine.Substring(19, 2),
                                                        data6 = szLine.Substring(21, 2),
                                                        data7 = szLine.Substring(23, 2),
                                                        data8 = szLine.Substring(25, 2),
                                                        //data9 = szLine.Substring(27, 2),
                                                        //data10 = szLine.Substring(29, 2),
                                                        //data11 = szLine.Substring(31, 2),
                                                        //data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 10:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        data3 = szLine.Substring(15, 2),
                                                        data4 = szLine.Substring(17, 2),
                                                        data5 = szLine.Substring(19, 2),
                                                        data6 = szLine.Substring(21, 2),
                                                        data7 = szLine.Substring(23, 2),
                                                        data8 = szLine.Substring(25, 2),
                                                        data9 = szLine.Substring(27, 2),
                                                        //data10 = szLine.Substring(29, 2),
                                                        //data11 = szLine.Substring(31, 2),
                                                        //data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 11:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        data3 = szLine.Substring(15, 2),
                                                        data4 = szLine.Substring(17, 2),
                                                        data5 = szLine.Substring(19, 2),
                                                        data6 = szLine.Substring(21, 2),
                                                        data7 = szLine.Substring(23, 2),
                                                        data8 = szLine.Substring(25, 2),
                                                        data9 = szLine.Substring(27, 2),
                                                        data10 = szLine.Substring(29, 2),
                                                        //data11 = szLine.Substring(31, 2),
                                                        //data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 12:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        data3 = szLine.Substring(15, 2),
                                                        data4 = szLine.Substring(17, 2),
                                                        data5 = szLine.Substring(19, 2),
                                                        data6 = szLine.Substring(21, 2),
                                                        data7 = szLine.Substring(23, 2),
                                                        data8 = szLine.Substring(25, 2),
                                                        data9 = szLine.Substring(27, 2),
                                                        data10 = szLine.Substring(29, 2),
                                                        data11 = szLine.Substring(31, 2),
                                                        //data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 13:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        data3 = szLine.Substring(15, 2),
                                                        data4 = szLine.Substring(17, 2),
                                                        data5 = szLine.Substring(19, 2),
                                                        data6 = szLine.Substring(21, 2),
                                                        data7 = szLine.Substring(23, 2),
                                                        data8 = szLine.Substring(25, 2),
                                                        data9 = szLine.Substring(27, 2),
                                                        data10 = szLine.Substring(29, 2),
                                                        data11 = szLine.Substring(31, 2),
                                                        data12 = szLine.Substring(33, 2),
                                                        //data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 14:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        data3 = szLine.Substring(15, 2),
                                                        data4 = szLine.Substring(17, 2),
                                                        data5 = szLine.Substring(19, 2),
                                                        data6 = szLine.Substring(21, 2),
                                                        data7 = szLine.Substring(23, 2),
                                                        data8 = szLine.Substring(25, 2),
                                                        data9 = szLine.Substring(27, 2),
                                                        data10 = szLine.Substring(29, 2),
                                                        data11 = szLine.Substring(31, 2),
                                                        data12 = szLine.Substring(33, 2),
                                                        data13 = szLine.Substring(35, 2),
                                                        //data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 15:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        data3 = szLine.Substring(15, 2),
                                                        data4 = szLine.Substring(17, 2),
                                                        data5 = szLine.Substring(19, 2),
                                                        data6 = szLine.Substring(21, 2),
                                                        data7 = szLine.Substring(23, 2),
                                                        data8 = szLine.Substring(25, 2),
                                                        data9 = szLine.Substring(27, 2),
                                                        data10 = szLine.Substring(29, 2),
                                                        data11 = szLine.Substring(31, 2),
                                                        data12 = szLine.Substring(33, 2),
                                                        data13 = szLine.Substring(35, 2),
                                                        data14 = szLine.Substring(37, 2),
                                                        //data15 = szLine.Substring(39, 2),

                                                    });
                                                }
                                                break;
                                            case 16:
                                                {
                                                    peopleList.Add(new people()
                                                    {
                                                        ad = ConvertToString(StartAdd + IncAdd),
                                                        data0 = szLine.Substring(9, 2),
                                                        data1 = szLine.Substring(11, 2),
                                                        data2 = szLine.Substring(13, 2),
                                                        data3 = szLine.Substring(15, 2),
                                                        data4 = szLine.Substring(17, 2),
                                                        data5 = szLine.Substring(19, 2),
                                                        data6 = szLine.Substring(21, 2),
                                                        data7 = szLine.Substring(23, 2),
                                                        data8 = szLine.Substring(25, 2),
                                                        data9 = szLine.Substring(27, 2),
                                                        data10 = szLine.Substring(29, 2),
                                                        data11 = szLine.Substring(31, 2),
                                                        data12 = szLine.Substring(33, 2),
                                                        data13 = szLine.Substring(35, 2),
                                                        data14 = szLine.Substring(37, 2),
                                                        data15 = szLine.Substring(39, 2),

                                                    });
                                                    
                                                }
                                                break;

                                        }
                                    }
                                   
                                }
                                else if (ConvertToHex(szLine.Substring(7, 2)) == 1)
                                {
                                    break;
                                }
                                else if (ConvertToHex(szLine.Substring(7, 2)) == 5)
                                {
                                   
                                }
                            }
                        }
                        while (szLine != null);
                        ow.dataGrid1.ItemsSource = peopleList;
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
               
            }
            else
            {
                
            }
            
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("SAVE!");
        }

        private void CloseMenuItem_Click(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show("CLOSE!");
        }
    }
    public class MainCommands

    {

        private static RoutedUICommand requery;

        static MainCommands()

        {

            InputGestureCollection inputs = new InputGestureCollection();

            inputs.Add(new KeyGesture(Key.F, ModifierKeys.Control,"Ctrl+F"));

            requery = new RoutedUICommand("Requery", "Requery", typeof(MainCommands), inputs);

        }
        public static RoutedUICommand Requery

        {
            get { return requery; }
        }

    }

    public class OpenCommands

    {

        private static RoutedUICommand requery;

        static OpenCommands()

        {

            InputGestureCollection inputs = new InputGestureCollection();

            inputs.Add(new KeyGesture(Key.O, ModifierKeys.Control, "Ctrl+O"));

            requery = new RoutedUICommand(
         "Requery", "Requery", typeof(OpenCommands), inputs);

        }
        public static RoutedUICommand Requery

        {
            get { return requery; }
        }

    }
    public class SaveCommands

    {

        private static RoutedUICommand requery;

        static SaveCommands()

        {

            InputGestureCollection inputs = new InputGestureCollection();

            inputs.Add(new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl+S"));

            requery = new RoutedUICommand(
         "Requery", "Requery", typeof(SaveCommands), inputs);

        }
        public static RoutedUICommand Requery

        {
            get { return requery; }
        }

    }
    public class CloseCommands

    {

        private static RoutedUICommand requery;

        static CloseCommands()

        {

            InputGestureCollection inputs = new InputGestureCollection();

            inputs.Add(new KeyGesture(Key.F4, ModifierKeys.Control, "Alt+F4"));

            requery = new RoutedUICommand(
         "Requery", "Requery", typeof(CloseCommands), inputs);

        }
        public static RoutedUICommand Requery

        {
            get { return requery; }
        }

    }

}