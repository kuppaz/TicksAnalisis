using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Ticks_analysis
{
    class help_to_convert
    {
        public static void convert_1()
        {
            string datastring = null;
            string[] dataArray;
            int ii_minus = 0, ii_plus = 0;

            int flag_Ask_avg_F_6 = 0;

            double[] time_sec = new double[20], Diff_seconds = new double[20], Aks = new double[20], Ask_increment = new double[20], Ask_Speed = new double[20], Ask_V_by_3 = new double[20], Ask_V_by_6 = new double[20],
                     Ask_F_by_3 = new double[20], Ask_F_by_6 = new double[20], Ask_V_avg_by_3 = new double[20], Ask_V_avg_by_6 = new double[20], Ask_F_avg_by_3 = new double[20], Ask_F_avg_by_6 = new double[20];

            StreamReader myFile = new StreamReader("D://Ticks//Ticks_Log_2013.03.28.txt");
            StreamWriter OutFile = new StreamWriter("D://Ticks//Ticks_Log_2013.03.28_filt.txt");

            datastring = myFile.ReadLine();
            OutFile.WriteLine("Count	time_sec	Date	Time	Diff_seconds	Aks	Ask_increment	Ask_Speed	Bid	Bid_increment	Bid_Speed	Spread	Ask_V_by_3	Ask_V_by_6	Ask_F_by_3	Ask_F_by_6	Ask_V_avg_by_3	Ask_V_avg_by_6	Ask_F_avg_by_3	Ask_F_avg_by_6");

            for (int i = 0; i < 43140; i++)
            {
                datastring = myFile.ReadLine();
                dataArray = datastring.Split(' ');

                for (int j = 0; j < 19; j++)
                {
                    time_sec[j] = time_sec[j + 1];
                    Diff_seconds[j] = Diff_seconds[j + 1];
                    Aks[j] = Aks[j + 1];
                    Ask_increment[j] = Ask_increment[j + 1];
                    Ask_Speed[j] = Ask_Speed[j + 1];
                }

                time_sec[19] = Convert.ToDouble(dataArray[1]);
                Diff_seconds[19] = Convert.ToDouble(dataArray[4]);
                Aks[19] = Convert.ToDouble(dataArray[6]);
                Ask_increment[19] = Convert.ToDouble(dataArray[7]);
                Ask_Speed[19] = Convert.ToDouble(dataArray[8]);



                //---Индикаторы---

                Ask_V_by_3[19] = (Ask_Speed[19] + Ask_Speed[18] + Ask_Speed[17]) / 3.0;
                Ask_V_by_6[19] = (Ask_Speed[16] + Ask_Speed[15] + Ask_Speed[14] + Ask_V_by_3[19] * 3.0) / 6.0;

                Ask_F_by_3[19] = Ask_V_by_3[19] / Diff_seconds[19];
                Ask_F_by_6[19] = Ask_V_by_6[19] / Diff_seconds[19];


                double summ_dt_3 = Diff_seconds[19] + Diff_seconds[18] + Diff_seconds[17];
                double summ_dt_6 = Diff_seconds[16] + Diff_seconds[15] + Diff_seconds[14] + summ_dt_3;
                Ask_V_avg_by_3[19] = (Ask_increment[19] + Ask_increment[18] + Ask_increment[17]) / summ_dt_3 * 100000;
                Ask_V_avg_by_6[19] = (Ask_increment[19] + Ask_increment[18] + Ask_increment[17] + Ask_increment[16] + Ask_increment[15] + Ask_increment[14]) / summ_dt_6 * 100000;

                Ask_F_avg_by_3[19] = Ask_V_avg_by_3[19] / summ_dt_3;
                Ask_F_avg_by_6[19] = Ask_V_avg_by_6[19] / summ_dt_6;

//Count	            Date	Time	Diff_seconds_dot 3	Diff_seconds	Aks 5	Ask_increment 6	Ask_Speed 7	Bid	Bid_increment	Bid_Speed	Spread 11
//Count	time_sec	Date	Time	Diff_seconds	                    Aks	    Ask_increment	Ask_Speed	Bid	Bid_increment	Bid_Speed	Spread	 
                //Ask_V_by_3	Ask_V_by_6	Ask_F_by_3	Ask_F_by_6	Ask_V_avg_by_3	Ask_V_avg_by_6	Ask_F_avg_by_3	Ask_F_avg_by_6


                //------
//Count	time_sec	Date	Time	Diff_seconds_dot	Diff_seconds	Aks	Ask_increment	Ask_Speed	Bid	Bid_increment	Bid_Speed	Spread	Ask_Speed_by_3	Ask_Speed_by_6	Ask_Speed_by_9
//Count	time_sec	Date	Time	Diff_seconds	                    Aks	Ask_increment	Ask_Speed	Bid	Bid_increment	Bid_Speed	Spread	Ask_V_by_3	Ask_V_by_6	Ask_F_by_3	Ask_F_by_6	Ask_V_avg_by_3	Ask_V_avg_by_6	Ask_F_avg_by_3	Ask_F_avg_by_6	flag_Ask_increment	flag_Ask_avg_F_6

                

                //------

                OutFile.WriteLine(dataArray[0] + " " + time_sec[19] + " " + dataArray[2] + " " + dataArray[3] + " " + dataArray[4] + " " + dataArray[6] + " " + dataArray[7] + " " + dataArray[8]
                            + " " + dataArray[9] + " " + dataArray[10] + " " + dataArray[11] + " " + dataArray[12] + " " + Ask_V_by_3[19].ToString() + " " + Ask_V_by_6[19].ToString()
                            + " " + Ask_F_by_3[19].ToString() + " " + Ask_F_by_6[19].ToString() + " " + Ask_V_avg_by_3[19].ToString() + " " + Ask_V_avg_by_6[19].ToString()
                            + " " + Ask_F_avg_by_3[19].ToString() + " " + Ask_F_avg_by_6[19].ToString());
            }

            OutFile.Close();
        }
    }
}
