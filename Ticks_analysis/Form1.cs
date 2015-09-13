using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//using System.IO.Compression;
using Ticks_analysis;

namespace Ticks_analysis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int BaseCount, BaseCountToSkip;
        public string[] CurTime, Date_on_time;
        public double[] timeStep, Ask, Bid, AskIncrement, BidIncrement;
        public double[] StM5_733_0, StM5_733_1, StM5_733_2, StM5_733_3, StM5_733_4;

        private void ReadInMemory_Click(object sender, EventArgs e)
        {
            string datastring = null;
            string[] dataArray;

            if (this.With_indicators.Checked == false) WorkClasses.base_file_name = "//Ticks_base.txt";
            if (this.With_indicators.Checked == true) WorkClasses.base_file_name = "//Ticks_base_indicator.txt";


            BaseCount = 0;
            StreamReader baseFile_0 = new StreamReader(WorkClasses.dir_str + WorkClasses.base_file_name);
            for (int i = 0; ; i++)
            {
                datastring = baseFile_0.ReadLine();
                if (baseFile_0.EndOfStream == true) break;
                BaseCount++;
            }
            baseFile_0.Close();

            int j;
            DateTime start_time1 = DateTime.Now;
            StreamReader baseFile_1 = new StreamReader(WorkClasses.dir_str + WorkClasses.base_file_name);
            datastring = baseFile_1.ReadLine();





            if (this.interval_201304_201308.Checked == true)
            {
                int left = 1, 
                    right = 4400000;
                BaseCount = right - left;
                for (j = 0; j < left; j++)
                    datastring = baseFile_1.ReadLine();
            }
            else if (this.interval_201309_201311.Checked == true)
            {
                int left = 4400001,
                    right = 7100000;
                BaseCount = right - left;
                for (j = 0; j < left; j++)
                    datastring = baseFile_1.ReadLine();
            }
            else if (this.interval_201312_201402.Checked == true)
            {
                int left = 7100001,
                    right = 9400000;
                BaseCount = right - left;
                for (j = 0; j < left; j++)
                    datastring = baseFile_1.ReadLine();
            }
            else if (this.interval_ECN_since_22_07_14.Checked == true)
            {
                int left = 14390211;
                BaseCount = BaseCount - left;
                for (j = 0; j < left; j++)
                    datastring = baseFile_1.ReadLine();
            }
            else
            {
                BaseCountToSkip = 4000000 - BaseCount + 10400000;
                for (j = 0; j < BaseCount - 10400000; j++)
                {
                    datastring = baseFile_1.ReadLine();
                }
                BaseCount = BaseCount - j - 10;
            }



            CurTime = new string[BaseCount]; 
            Date_on_time = new string[BaseCount];
            timeStep = new double[BaseCount]; 
            Ask = new double[BaseCount]; 
            Bid = new double[BaseCount];
            AskIncrement = new double[BaseCount]; 
            BidIncrement = new double[BaseCount];

            StM5_733_0 = new double[BaseCount];
            StM5_733_1 = new double[BaseCount];
            StM5_733_2 = new double[BaseCount];
            StM5_733_3 = new double[BaseCount];
            StM5_733_4 = new double[BaseCount];


            for (int i = 0; i < BaseCount; i++)
            {
                datastring = baseFile_1.ReadLine();
                if (baseFile_1.EndOfStream == true) break;

                dataArray = datastring.Split(' ');
                Date_on_time[i] = dataArray[0];
                CurTime[i] = dataArray[1];
                timeStep[i] = Convert.ToDouble(dataArray[2]);
                Ask[i] = Convert.ToDouble(dataArray[3]);
                Bid[i] = Convert.ToDouble(dataArray[5]);
                AskIncrement[i] = Convert.ToDouble(dataArray[4]);
                BidIncrement[i] = Convert.ToDouble(dataArray[6]);

                if (this.With_indicators.Checked)
                {
                    StM5_733_0[i] = Convert.ToDouble(dataArray[7]);

                    if (i > 0)
                    {
                        StM5_733_4[i] = StM5_733_4[i - 1];
                        StM5_733_3[i] = StM5_733_3[i - 1];
                        StM5_733_2[i] = StM5_733_2[i - 1];
                        StM5_733_1[i] = StM5_733_1[i - 1];

                        if (StM5_733_1[i] != Convert.ToDouble(dataArray[8]))
                        {
                            StM5_733_4[i] = StM5_733_3[i - 1];
                            StM5_733_3[i] = StM5_733_2[i - 1];
                            StM5_733_2[i] = StM5_733_1[i - 1];
                            StM5_733_1[i] = Convert.ToDouble(dataArray[8]);
                        }
                    }
                }
            }
            baseFile_1.Close();
            DateTime start_end1 = DateTime.Now;

            Console.WriteLine(" ");
            Console.WriteLine((start_end1 - start_time1).ToString());
            Console.WriteLine(" ");
        }



        private void StartAnalyse_Click(object sender, EventArgs e)
        {
            string datastring = null;
            

            if (With_indicators.Checked == false) WorkClasses.base_file_name = "//Ticks_base.txt";
            if (With_indicators.Checked == true)
            {
                WorkClasses.base_file_name = "//Ticks_base_indicator.txt";

                if (this.UseShortFile.Checked == true)
                    WorkClasses.base_file_name = "//Ticks_base_indicator_Short.txt";

                //if (Ind_Local_file.Checked == true)
                //    WorkClasses.base_file_name = "//Ticks_base_ind_local.txt";
            }

            StreamWriter temp_help = new StreamWriter(WorkClasses.dir_str + "//_temp_help.txt");

            StreamReader baseFile = new StreamReader(WorkClasses.dir_str + WorkClasses.base_file_name);
            StreamWriter OutFile = new StreamWriter(WorkClasses.dir_str + "//_Ticks_all_processed.txt");
            StreamWriter ResultFile = new StreamWriter(WorkClasses.dir_str + "//_Agregate_by_Ind.txt");
            StreamWriter DetailResultFile = new StreamWriter(WorkClasses.dir_str + "//_Agregate_Detail_by_Ind.txt");

            //StreamReader stream = new StreamReader("D://Ticks//archive//_Bases//2013.11.01_Ticks_base.txt");
            //StreamReader stream = new StreamReader("D://Ticks//_Agregate_by_Cycle.txt");
            //string allstr = baseFile.ReadToEnd();
            //string[] ArrayOfAllString = allstr.Split('\n');
            //allstr.Remove(0);
            



            DetailResultFile.WriteLine("count Date CurTime AskIncrement flg_F_avg_6 Bid Ask ticks_activ_cnt ticks_active_time profit_step AskSpeed Ask_V_3 Ask_V_6 Ask_V_avg_3 Ask_V_avg_6 Ask_F_avg_3 Ask_F_avg_6");

            StreamWriter helpFile = new StreamWriter(WorkClasses.dir_str + "//_help_out.txt");
            StreamWriter helpFile_byDays = new StreamWriter(WorkClasses.dir_str + "//_help_out_by_days.txt");
            StreamWriter helpFile_byMonth = new StreamWriter(WorkClasses.dir_str + "//_help_out_by_month.txt");

            WorkClasses.Ticks_In TicksIn = new WorkClasses.Ticks_In();
            WorkClasses.Ticks_Out TicksOut = new WorkClasses.Ticks_Out();
            WorkClasses.Stat_Info StatInfo = new WorkClasses.Stat_Info();

            if (this.Only_BUY.Checked) TicksIn.Only_BUY_flg = true;
            if (this.Only_SELL.Checked) TicksIn.Only_SELL_flg = true;
            if (this.With_indicators.Checked) TicksIn.With_indicators = true;

            int dim = TicksIn.dim - 1;
            for (int i = 0; i < dim+1; i++) TicksIn.timeStep[i] = 1;

            int flg_F_avg_6 = 0;
            double ticks_sum = 0;

            int cnt_closed_orders = 0, cnt_negative_avg_Last_N_profit = 0;
            int[] Counts_On_Close = new int[50000], Counts_On_Open = new int[50000];
            double ticks_active_time = 0.0;
            string order_open_time = "";

            double[] Last_N_profit = new double[20];

            datastring = baseFile.ReadLine();
            OutFile.WriteLine("Count CurTime TimeInc Ask Bid AskIncrement flg_F ticks_activ_cnt ticks_active_time Bid-OpenASK OpenBID-Bid AskSpeed Ask_F_avg_3 Ask_F_avg_6 StM5_733_0 StM5_733_1 StM5_733_2 StM5_733_3");
            helpFile.WriteLine("OpenN CloseN Cur_Date flag_F MinLocal MaxLocal OpenSpread CloseSpread CurPointProfit AbsPointProfit Balance BalanceDay MaxFall MaxFallDay CntClosed BigMassNega BigMassPosi AskSpeed Ask_F_avg_3");

            DateTime start_time = DateTime.Now;
            double summ_dt_3;

            for (int i = 0; ; i++)
            {
                if (baseFile.EndOfStream == true || i >= BaseCount) break;

                if (this.only_full_part.Checked == true && i == 0)
                {
                    for (int j = 0; j < BaseCountToSkip; j++)
                    {
                        datastring = baseFile.ReadLine();
                        TicksIn.Count = TicksIn.Count + 1;
                        i++;
                    }
                }

                
                //datastring = baseFile.ReadLine();
                ////TicksIn.dataArray = datastring.Split(new Char[] {' '}, 10);
                //TicksIn.dataArray = datastring.Split(' ');

                TicksIn.Count = TicksIn.Count + 1;
                //TicksIn.Date = TicksIn.dataArray[0];
                //TicksIn.CurTime = TicksIn.dataArray[1];
                TicksIn.CurTime = CurTime[i];
                TicksIn.Date = Date_on_time[i];



                for (int j = 0; j < dim; j++)
                {
                    TicksIn.timeStep[j] = TicksIn.timeStep[j + 1];
                    TicksIn.Ask[j] = TicksIn.Ask[j + 1];
                    TicksIn.Bid[j] = TicksIn.Bid[j + 1];
                    TicksIn.AskIncrement[j] = TicksIn.AskIncrement[j + 1];
                    TicksIn.BidIncrement[j] = TicksIn.BidIncrement[j + 1];
                    TicksIn.Spread[j] = TicksIn.Spread[j + 1];
                    TicksOut.AskSpeed[j] = TicksOut.AskSpeed[j + 1];
                    TicksOut.Ask_F_avg_6[j] = TicksOut.Ask_F_avg_6[j + 1];
                }

                //TicksIn.timeStep[dim] = Convert.ToDouble(TicksIn.dataArray[2]);
                //TicksIn.Ask[dim] = Convert.ToDouble(TicksIn.dataArray[3]);
                //TicksIn.Bid[dim] = Convert.ToDouble(TicksIn.dataArray[5]);
                //TicksIn.AskIncrement[dim] = Convert.ToDouble(TicksIn.dataArray[4]);
                //TicksIn.BidIncrement[dim] = Convert.ToDouble(TicksIn.dataArray[6]);     
                //TicksIn.Spread[dim] = Math.Abs(TicksIn.Ask[dim] - TicksIn.Bid[dim]) * 100000.0;

                TicksIn.timeStep[dim] = timeStep[i];
                TicksIn.Ask[dim] = Ask[i];
                TicksIn.Bid[dim] = Bid[i];
                TicksIn.AskIncrement[dim] = AskIncrement[i];
                TicksIn.BidIncrement[dim] = BidIncrement[i];
                TicksIn.Spread[dim] = Math.Abs(TicksIn.Ask[dim] - TicksIn.Bid[dim]) * 100000.0;




                if (With_indicators.Checked == true)
                {
                    //for (int j = TicksIn.StM1_733_0.Length - 1; j > 0; j--)
                    //{
                    //    TicksIn.StM1_733_0[j] = TicksIn.StM1_733_0[j - 1];
                    //}
                    //TicksIn.StM1_733_0[0] = Convert.ToDouble(TicksIn.dataArray[11]);

                    //TicksIn.StM5_733_0 = Convert.ToDouble(TicksIn.dataArray[7]);
                    //TicksIn.StM5_532_0 = Convert.ToDouble(TicksIn.dataArray[9]);
                    //TicksIn.DifOpenClose_0 = Convert.ToInt32(TicksIn.dataArray[13]);
                    //TicksIn.StdDevM5_40_0 = Convert.ToDouble(TicksIn.dataArray[15]);
                    //if (TicksIn.StM1_733_1 != Convert.ToDouble(TicksIn.dataArray[12]))
                    //{
                    //    TicksIn.StM1_733_4 = TicksIn.StM1_733_3;
                    //    TicksIn.StM1_733_3 = TicksIn.StM1_733_2;
                    //    TicksIn.StM1_733_2 = TicksIn.StM1_733_1;
                    //    TicksIn.StM1_733_1 = Convert.ToDouble(TicksIn.dataArray[12]);
                    //}
                    //if (TicksIn.StM5_733_1 != Convert.ToDouble(TicksIn.dataArray[8]))
                    //{
                    //    TicksIn.StM5_733_4 = TicksIn.StM5_733_3;
                    //    TicksIn.StM5_733_3 = TicksIn.StM5_733_2;
                    //    TicksIn.StM5_733_2 = TicksIn.StM5_733_1;
                    //    TicksIn.StM5_733_1 = Convert.ToDouble(TicksIn.dataArray[8]);
                    //    TicksIn.StM5_532_4 = TicksIn.StM5_532_3;
                    //    TicksIn.StM5_532_3 = TicksIn.StM5_532_2;
                    //    TicksIn.StM5_532_2 = TicksIn.StM5_532_1;
                    //    TicksIn.StM5_532_1 = Convert.ToDouble(TicksIn.dataArray[10]);
                    //    TicksIn.DifOpenClose_4 = TicksIn.DifOpenClose_3;
                    //    TicksIn.DifOpenClose_3 = TicksIn.DifOpenClose_2;
                    //    TicksIn.DifOpenClose_2 = TicksIn.DifOpenClose_1;
                    //    TicksIn.DifOpenClose_1 = Convert.ToInt32(TicksIn.dataArray[14]);
                    //    TicksIn.StdDevM5_40_4 = TicksIn.StdDevM5_40_3;
                    //    TicksIn.StdDevM5_40_3 = TicksIn.StdDevM5_40_2;
                    //    TicksIn.StdDevM5_40_2 = TicksIn.StdDevM5_40_1;
                    //    TicksIn.StdDevM5_40_1 = Convert.ToDouble(TicksIn.dataArray[16]);
                    //    TicksIn.iBullsM5_7_4 = TicksIn.iBullsM5_7_3;
                    //    TicksIn.iBullsM5_7_3 = TicksIn.iBullsM5_7_2;
                    //    TicksIn.iBullsM5_7_2 = TicksIn.iBullsM5_7_1;
                    //    TicksIn.iBullsM5_7_1 = Convert.ToDouble(TicksIn.dataArray[17]);
                    //    TicksIn.iBearM5_7_4 = TicksIn.iBearM5_7_3;
                    //    TicksIn.iBearM5_7_3 = TicksIn.iBearM5_7_2;
                    //    TicksIn.iBearM5_7_2 = TicksIn.iBearM5_7_1;
                    //    TicksIn.iBearM5_7_1 = Convert.ToDouble(TicksIn.dataArray[18]);
                    //}

                    TicksIn.StM5_733_0 = StM5_733_0[i];
                    TicksIn.StM5_733_1 = StM5_733_1[i];
                    TicksIn.StM5_733_2 = StM5_733_2[i];
                    TicksIn.StM5_733_3 = StM5_733_3[i];
                    TicksIn.StM5_733_4 = StM5_733_4[i];


                    TicksIn.Stohastic_Param = TicksIn.StM5_733_3;
                }

                //Вычисление индикаторов//
                summ_dt_3 = TicksIn.timeStep[dim] + TicksIn.timeStep[dim - 1] + TicksIn.timeStep[dim - 2];

                TicksOut.AskSpeed[dim] = TicksIn.AskIncrement[dim] / TicksIn.timeStep[dim];

                TicksOut.Ask_F_avg_3[dim] = (TicksIn.AskIncrement[dim] + TicksIn.AskIncrement[dim - 1] + TicksIn.AskIncrement[dim - 2]) / Math.Pow(summ_dt_3, 2) * 100000.0;
                TicksOut.Ask_F_avg_6[dim] = (TicksIn.AskIncrement[dim] + TicksIn.AskIncrement[dim - 1] + TicksIn.AskIncrement[dim - 2] + TicksIn.AskIncrement[dim - 3] + TicksIn.AskIncrement[dim - 4]
                                            + TicksIn.AskIncrement[dim - 5]) / Math.Pow(summ_dt_3 + TicksIn.timeStep[dim - 3] + TicksIn.timeStep[dim - 4] + TicksIn.timeStep[dim - 5], 2) * 100000.0;


                //Пропускаем мимо ситуации, когда ордер открыт в момент ГЭПа по времени.
                if (WorkClasses.gaps_str.Contains(" " + Convert.ToString(TicksIn.Count) + " "))
                {
                    if (Math.Abs(flg_F_avg_6) == 1)
                    {
                        flg_F_avg_6 = 0;
                        TicksOut.ticks_activ_cnt = 0;
                        ticks_active_time = 0.0;

                        for (int j = 0; j < dim+1; j++)
                        {
                            TicksOut.Ask_F_avg_3[j] = 0;
                            TicksOut.Ask_F_avg_4[j] = 0;
                            TicksOut.Ask_F_avg_5[j] = 0;
                            TicksOut.Ask_F_avg_6[j] = 0;
                            TicksOut.Ask_F_avg_7[j] = 0;
                            TicksOut.Ask_F_avg_8[j] = 0;
                        }
                    }
                }
                //-------------------

                if (TicksIn.Count == 8990286)
                    TicksIn.Count = TicksIn.Count;


                if (Math.Abs(flg_F_avg_6) == 1)
                {
                    if (flg_F_avg_6 == 1)
                        TicksOut.local_Open_vs_Cur = TicksIn.Bid[dim] - TicksOut.OpenASK;
                    if (flg_F_avg_6 == -1)
                        TicksOut.local_Open_vs_Cur = TicksOut.OpenBID - TicksIn.Ask[dim];

                    if (StatInfo.MaxLocalProfitInOrder[cnt_closed_orders] < TicksOut.local_Open_vs_Cur * 100000) 
                        StatInfo.MaxLocalProfitInOrder[cnt_closed_orders] = TicksOut.local_Open_vs_Cur * 100000;
                    if (StatInfo.MinLocalProfitInOrder[cnt_closed_orders] > TicksOut.local_Open_vs_Cur * 100000)
                        StatInfo.MinLocalProfitInOrder[cnt_closed_orders] = TicksOut.local_Open_vs_Cur * 100000;

                    TicksOut.ticks_activ_cnt++;
                    ticks_active_time = ticks_active_time + TicksIn.timeStep[dim];

                    if (TicksOut.ticks_activ_cnt == 1)
                    {
                        StatInfo.AskIncremBigMassSummNegative[cnt_closed_orders] = TicksIn.AskIncremBigMassSumm_Negative;
                        StatInfo.AskIncremBigMassSummPositive[cnt_closed_orders] = TicksIn.AskIncremBigMassSumm_Positive;
                    }
                }
                else if (flg_F_avg_6 == 0)
                {
                    order_open_time = TicksIn.CurTime;
                    TicksOut.local_Open_vs_Cur = 0;
                }

                if (Math.Abs(TicksIn.Ask[dim] - TicksIn.Bid[dim]) * 100000.0 < 4.0)
                    //temp_help.WriteLine(TicksIn.Count + " " + TicksIn.Date + " " + TicksIn.CurTime + " " + (TicksIn.Ask[dim] - TicksIn.Bid[dim]).ToString());
                    temp_help.WriteLine(datastring);


/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
                flg_F_avg_6 = ParamVariants.ChouseTheSet(flg_F_avg_6, TicksIn, TicksOut, BaseCountToSkip, this.only_full_part.Checked);
/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
                


                if (Math.Abs(flg_F_avg_6) == 1 && TicksOut.ticks_activ_cnt == 0)
                {
                    Counts_On_Open[cnt_closed_orders] = TicksIn.Count;
                    StatInfo.StartSpread[cnt_closed_orders] = TicksIn.Spread[dim];
                    if (flg_F_avg_6 < 0) StatInfo.AskSpeedOpen[cnt_closed_orders] = -TicksOut.AskSpeed[TicksIn.dim - 1] * 100000.0;
                    else StatInfo.AskSpeedOpen[cnt_closed_orders] = TicksOut.AskSpeed[TicksIn.dim - 1] * 100000.0;
                    if (flg_F_avg_6 < 0) StatInfo.Ask_F_avg_3_Open[cnt_closed_orders] = -TicksOut.Ask_F_avg_3[TicksIn.dim - 1];
                    else StatInfo.Ask_F_avg_3_Open[cnt_closed_orders] = TicksOut.Ask_F_avg_3[TicksIn.dim - 1];
                }


                int count_for_Last_N_profit = 3;
                if (Math.Abs(flg_F_avg_6) == 2)
                {
                    if (flg_F_avg_6 == 2)
                        TicksOut.local_Open_vs_Cur = TicksIn.Bid[dim] - TicksOut.OpenASK;
                    if (flg_F_avg_6 == -2)
                        TicksOut.local_Open_vs_Cur = TicksOut.OpenBID - TicksIn.Ask[dim];

                    if (false)
                        TicksOut.local_Open_vs_Cur = TicksOut.local_Open_vs_Cur - 0.00010;

                    ticks_sum = ticks_sum + TicksOut.local_Open_vs_Cur;
                    TicksOut.Ticks_Sum_on_time[cnt_closed_orders] = ticks_sum * 100000;  //Присваиваем текущее значение профита для последующего МНК
                    TicksOut.Date_on_time[cnt_closed_orders] = TicksIn.Date;
                    TicksOut.Cur_Order_Profit[cnt_closed_orders] = (TicksOut.local_Open_vs_Cur) * 100000;
                    TicksOut.flg_F_avg_6[cnt_closed_orders] = flg_F_avg_6;
                    Counts_On_Close[cnt_closed_orders] = TicksIn.Count;

                    StatInfo.CloseSpread[cnt_closed_orders] = TicksIn.Spread[dim];

                    if (StatInfo.MaxLocalProfitInOrder[cnt_closed_orders] < TicksOut.local_Open_vs_Cur * 100000)
                        StatInfo.MaxLocalProfitInOrder[cnt_closed_orders] = TicksOut.local_Open_vs_Cur * 100000;
                    if (StatInfo.MinLocalProfitInOrder[cnt_closed_orders] > TicksOut.local_Open_vs_Cur * 100000)
                        StatInfo.MinLocalProfitInOrder[cnt_closed_orders] = TicksOut.local_Open_vs_Cur * 100000;

                    for (int j = 0; j < count_for_Last_N_profit - 1; j++)
                        Last_N_profit[j] = Last_N_profit[j + 1];
                    Last_N_profit[count_for_Last_N_profit - 1] = (TicksOut.local_Open_vs_Cur) * 100000;

                    double avg_Last_N_profit = 0.0;
                    for (int j = 0; j < count_for_Last_N_profit; j++) avg_Last_N_profit += Last_N_profit[j];
                    avg_Last_N_profit = avg_Last_N_profit / count_for_Last_N_profit;
                    if (cnt_closed_orders < count_for_Last_N_profit) avg_Last_N_profit = 0;
                    if (avg_Last_N_profit < 0.0) cnt_negative_avg_Last_N_profit++;

                    cnt_closed_orders++;

                    if (flg_F_avg_6 == 2)
                        ResultFile.WriteLine(TicksIn.Count.ToString() + " " + TicksIn.Date + " " + flg_F_avg_6.ToString() + " " + TicksIn.Bid[dim].ToString() + " " + TicksOut.OpenASK.ToString() 
                                    + " " + TicksOut.ticks_activ_cnt.ToString() + " " + ticks_active_time.ToString() + " " + order_open_time + " " + (TicksOut.local_Open_vs_Cur * 100000).ToString()
                                    + " " + (ticks_sum * 100000).ToString() + " " + avg_Last_N_profit.ToString());

                    if (flg_F_avg_6 == -2)
                        ResultFile.WriteLine(TicksIn.Count.ToString() + " " + TicksIn.Date + " " + flg_F_avg_6.ToString() + " " + TicksOut.OpenBID.ToString() + " " + TicksIn.Ask[dim].ToString() 
                                    + " " + TicksOut.ticks_activ_cnt.ToString() + " " + ticks_active_time.ToString() + " " + order_open_time + " " + (TicksOut.local_Open_vs_Cur * 100000).ToString() 
                                    + " " + (ticks_sum * 100000).ToString() + " " + avg_Last_N_profit.ToString());
                }


                if ((TicksIn.Count > 10373200 && TicksIn.Count <= 10373383))
                {
                    OutFile.WriteLine(TicksIn.Count + " " + TicksIn.CurTime + " " + TicksIn.timeStep[dim] + " " + TicksIn.Ask[dim] + " " + TicksIn.Bid[dim] + " " + (TicksIn.AskIncrement[dim] * 100000)
                                        + " " + flg_F_avg_6 + " " + TicksOut.ticks_activ_cnt + " " + ticks_active_time
                                        + " " + ((TicksIn.Bid[dim] - TicksOut.OpenASK) * 100000) + " " + ((TicksOut.OpenBID - TicksIn.Bid[dim]) * 100000)
                                        + " " + TicksOut.AskSpeed[dim] + " " + TicksOut.Ask_F_avg_3[dim] + " " + TicksOut.Ask_F_avg_6[dim]
                                        + " " + TicksIn.StM5_733_0 + " " + TicksIn.StM5_733_1 + " " + TicksIn.StM5_733_2 + " " + TicksIn.StM5_733_3);
                }

                if (flg_F_avg_6 == -2 || flg_F_avg_6 == 2)
                {
                    flg_F_avg_6 = 0;
                    TicksOut.ticks_activ_cnt = 0;
                    ticks_active_time = 0.0;
                }

                
            }
            temp_help.Close();




            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            int cnt_of_start_last_date = 0;
            double[] Cur_Day_Profit = new double[cnt_closed_orders], Ticks_Sum_on_Day = new double[cnt_closed_orders];
            for (int i = 0; i < cnt_closed_orders; i++)
            {
                if (TicksOut.Date_on_time[i] == TicksOut.Date_on_time[cnt_closed_orders - 1])
                {
                    cnt_of_start_last_date = i;
                    break;
                }
            }
            for (int i = 0; i <= cnt_of_start_last_date; i++)
            {
                string temp_date = TicksOut.Date_on_time[i];
                Cur_Day_Profit[i] = TicksOut.Cur_Order_Profit[i];
                if (i == 0) Ticks_Sum_on_Day[i] = TicksOut.Cur_Order_Profit[i];
                else Ticks_Sum_on_Day[i] = Ticks_Sum_on_Day[i - 1] + TicksOut.Cur_Order_Profit[i];

                for (int j = i + 1; j < cnt_closed_orders; j++)
                {
                    if (TicksOut.Date_on_time[j] != temp_date)
                    {
                        for (int t = j - 1; t > i; t--) { Cur_Day_Profit[t] = Cur_Day_Profit[i]; Ticks_Sum_on_Day[t] = Ticks_Sum_on_Day[i]; }
                        i = j - 1; break;
                    }

                    Cur_Day_Profit[i] += TicksOut.Cur_Order_Profit[j];
                    Ticks_Sum_on_Day[i] += TicksOut.Cur_Order_Profit[j];
                }
                if (i == cnt_of_start_last_date && cnt_of_start_last_date != (cnt_closed_orders - 1))
                {
                    for (int j = cnt_of_start_last_date+1; j < cnt_closed_orders; j++)
                    {
                        Ticks_Sum_on_Day[j] = Ticks_Sum_on_Day[cnt_of_start_last_date];
                    }
                }
            }

            

            //Аппроксимируем прямой
            double[] residuals = new double[cnt_closed_orders];
            double s_xy = 0.0, s_x = 0.0, s_y = 0.0, s_x2 = 0.0;
            for (int i = 0; i < cnt_closed_orders; i++)
            {
                s_x += i+1;
                s_y += TicksOut.Ticks_Sum_on_time[i];
                s_xy += TicksOut.Ticks_Sum_on_time[i] * (i + 1);
                s_x2 += (i + 1) * (i + 1);
            }
            double a = (cnt_closed_orders * s_xy - s_x * s_y) / (cnt_closed_orders * s_x2 - s_x * s_x);
            double b = (s_y - a * s_x) / (cnt_closed_orders);


            double[] Max_FALL = new double[cnt_closed_orders * cnt_closed_orders], Max_FALL_byDay = new double[cnt_closed_orders * cnt_closed_orders];
            for (int i = 0; i < cnt_closed_orders; i++)
            {
                StatInfo.residual_avg_MNK += Math.Abs((TicksOut.Ticks_Sum_on_time[i] - (a * (i + 1) + b)));
                StatInfo.residual_avg_MNM += Math.Abs((TicksOut.Ticks_Sum_on_time[i] - (a * (i + 1) + b)) * Math.Cos(Math.Atan(a)));

                if (TicksOut.flg_F_avg_6[i] == -2)
                    StatInfo.SELL_summ_slice += TicksOut.Cur_Order_Profit[i];
                if (TicksOut.flg_F_avg_6[i] == 2)
                    StatInfo.BUY_summ_slice += TicksOut.Cur_Order_Profit[i];

                for (int j = 0; j < i; j++)
                {
                    Max_FALL[i * i + j] = TicksOut.Ticks_Sum_on_time[i] - TicksOut.Ticks_Sum_on_time[i - j - 1];
                    Max_FALL_byDay[i * i + j] = Ticks_Sum_on_Day[i] - Ticks_Sum_on_Day[i - j - 1];
                }
            }
            StatInfo.MaxFALLbyOrder = Max_FALL.Min();
            if (StatInfo.MaxFALLbyOrder > -0.1) StatInfo.MaxFALLbyOrder = 1;
            StatInfo.MaxFALLbyDay = Max_FALL_byDay.Min();
            if (StatInfo.MaxFALLbyDay > -0.1) StatInfo.MaxFALLbyDay = 1;

            StatInfo.residual_avg_MNK = StatInfo.residual_avg_MNK / cnt_closed_orders;
            StatInfo.residual_avg_MNM = StatInfo.residual_avg_MNM / cnt_closed_orders;



            double Balance = 3000.0, Balance_Day = 3000.0;
            for (int i = 0; i < cnt_closed_orders; i++)
            {
                residuals[i] = Math.Abs((TicksOut.Ticks_Sum_on_time[i] - (a * (i + 1) + b)));

                if (i != 0) Balance_Day = Balance_Day + (TicksOut.Ticks_Sum_on_time[i] - TicksOut.Ticks_Sum_on_time[i - 1]) * Math.Min((0.10 * Balance_Day) / Math.Abs(StatInfo.MaxFALLbyDay), 100.0);
                else Balance_Day = Balance_Day + (TicksOut.Ticks_Sum_on_time[i]) * (0.10 * Balance_Day) / Math.Abs(StatInfo.MaxFALLbyDay);

                if (i != 0) Balance = Balance + (TicksOut.Ticks_Sum_on_time[i] - TicksOut.Ticks_Sum_on_time[i - 1]) * Math.Min((0.10 * Balance) / Math.Abs(StatInfo.MaxFALLbyOrder), 100.0);
                else Balance = Balance + (TicksOut.Ticks_Sum_on_time[i]) * (0.10 * Balance) / Math.Abs(StatInfo.MaxFALLbyOrder);

                helpFile.WriteLine((Counts_On_Open[i]) + " " + (Counts_On_Close[i]) + " " + TicksOut.Date_on_time[i]
                            + " " + TicksOut.flg_F_avg_6[i] + " " + StatInfo.MinLocalProfitInOrder[i] + " " + StatInfo.MaxLocalProfitInOrder[i]
                            + " " + StatInfo.StartSpread[i] + " " + StatInfo.CloseSpread[i] + " " + TicksOut.Cur_Order_Profit[i] + " " + TicksOut.Ticks_Sum_on_time[i]
                            + " " + Convert.ToString(Balance).Split('.')[0] + " " + Convert.ToString(Balance_Day).Split('.')[0] + " " + Convert.ToString(StatInfo.MaxFALLbyOrder).Split('.')[0] 
                            + " " + Convert.ToString(StatInfo.MaxFALLbyDay).Split('.')[0] + " " + cnt_closed_orders 
                            //+ " " + (a * (i + 1) + b) + " " + residuals[i] + " " + a + " " + StatInfo.residual_avg_MNK + " " + StatInfo.SELL_summ_slice + " " + StatInfo.BUY_summ_slice 
                            + " " + StatInfo.AskIncremBigMassSummNegative[i] + " " + StatInfo.AskIncremBigMassSummPositive[i]
                            + " " + StatInfo.AskSpeedOpen[i] + " " + StatInfo.Ask_F_avg_3_Open[i]);
            }


            //---ПО ДНЯМ, МЕСЯЦАМ---//
            Balance = 3000.0;
            double profit_by_day = 0.0, profit_by_month = 0.0;
            for (int i = 0; i < cnt_closed_orders; i++)
            {

                if (i != 0) Balance = Balance + (TicksOut.Ticks_Sum_on_time[i] - TicksOut.Ticks_Sum_on_time[i - 1]) * Math.Min((0.10 * Balance_Day) / Math.Abs(StatInfo.MaxFALLbyDay), 100.0);
                else Balance = Balance + (TicksOut.Ticks_Sum_on_time[i]) * Math.Min((0.10 * Balance_Day) / Math.Abs(StatInfo.MaxFALLbyDay), 100.0);

                if (i != 0)
                {
                    if (TicksOut.Date_on_time[i] != TicksOut.Date_on_time[i - 1])
                    {
                        //if (i != cnt_closed_orders - 1)
                            helpFile_byDays.WriteLine((Counts_On_Close[i]).ToString() + " " + TicksOut.Date_on_time[i - 1] + " " + profit_by_day.ToString() + " " + TicksOut.Ticks_Sum_on_time[i - 1].ToString()
                                        + " " + Convert.ToString(Balance).Split('.')[0] + " " + Convert.ToString(StatInfo.MaxFALLbyOrder).Split('.')[0] + " " + Convert.ToString(StatInfo.MaxFALLbyDay).Split('.')[0]);

                        profit_by_day = 0.0;
                    }

                    if (TicksOut.Date_on_time[i].Split('.')[1] != TicksOut.Date_on_time[i - 1].Split('.')[1])
                    {
                        if (i != cnt_closed_orders - 1)
                            helpFile_byMonth.WriteLine((Counts_On_Close[i]).ToString() + " " + TicksOut.Date_on_time[i - 1].Split('.')[0] + "." + TicksOut.Date_on_time[i - 1].Split('.')[1] 
                                        + " " + profit_by_month.ToString() + " " + TicksOut.Ticks_Sum_on_time[i - 1].ToString()
                                        + " " + Convert.ToString(Balance).Split('.')[0] + " " + Convert.ToString(StatInfo.MaxFALLbyOrder).Split('.')[0] + " " + Convert.ToString(StatInfo.MaxFALLbyDay).Split('.')[0]);

                        profit_by_month = 0.0;
                    }
                }

                profit_by_day += TicksOut.Cur_Order_Profit[i];
                profit_by_month += TicksOut.Cur_Order_Profit[i];

                if (i == cnt_closed_orders - 1)
                {
                    helpFile_byDays.WriteLine((Counts_On_Close[i]).ToString() + " " + TicksOut.Date_on_time[i] + " " + profit_by_day.ToString() + " " + TicksOut.Ticks_Sum_on_time[i].ToString()
                                    + " " + Convert.ToString(Balance).Split('.')[0] + " " + Convert.ToString(StatInfo.MaxFALLbyOrder).Split('.')[0] + " " + Convert.ToString(StatInfo.MaxFALLbyDay).Split('.')[0]);
                    helpFile_byMonth.WriteLine((Counts_On_Close[i]).ToString() + " " + TicksOut.Date_on_time[i].Split('.')[0] + "." + TicksOut.Date_on_time[i].Split('.')[1] 
                                    + " " + profit_by_month.ToString() + " " + TicksOut.Ticks_Sum_on_time[i].ToString()
                                    + " " + Convert.ToString(Balance).Split('.')[0] + " " + Convert.ToString(StatInfo.MaxFALLbyOrder).Split('.')[0] + " " + Convert.ToString(StatInfo.MaxFALLbyDay).Split('.')[0]);
                }
            }

            helpFile_byMonth.Close();


            DateTime start_end = DateTime.Now;

            Console.WriteLine(" ");
            Console.WriteLine("Итог: " + (ticks_sum * 100000).ToString().Split('.')[0] + " time = " + (start_end - start_time).ToString());
            Console.WriteLine(" ");

            ResultFile.Close(); helpFile.Close(); helpFile_byDays.Close();
            OutFile.Close(); DetailResultFile.Close();
            //this.Close();
        }













/*----------------------------------------------------------------------------------------------------------------------------------------------------------*/
/*----------------------------------------------------------------------------------------------------------------------------------------------------------*/
        

        private void test_cycle_Click(object sender, EventArgs e)
        {
            By_Cycle.Go_run_sycle(this.With_indicators.Checked, this.Only_BUY.Checked, this.Only_SELL.Checked, this.interval_ECN_since_22_07_14.Checked);
            this.Close();
        }




/*----------------------------------------------------------------------------------------------------------------------------------------------------------*/
/*----------------------------------------------------------------------------------------------------------------------------------------------------------*/
/*----------------------------------------------------------------------------------------------------------------------------------------------------------*/
/*----------------------------------------------------------------------------------------------------------------------------------------------------------*/



        private void AppendToBaseFile_Click(object sender, EventArgs e)
        {
            string datastring = null;
            string[] dataArray;

            StreamReader baseFile = new StreamReader(WorkClasses.dir_str + "//Ticks_base.txt");
            StreamReader baseFile_Indic = new StreamReader(WorkClasses.dir_str + "//Ticks_base_indicator.txt");
            StreamReader addFile = new StreamReader(WorkClasses.dir_str + "//To_append.txt");
            StreamWriter OutFileOnlyCountTime = new StreamWriter(WorkClasses.dir_str + "//Ticks_base_CountTime.txt");
            StreamWriter OutFile = new StreamWriter(WorkClasses.dir_str + "//Ticks_base_out.txt");
            StreamWriter OutFile_Indic = new StreamWriter(WorkClasses.dir_str + "//Ticks_base_indicator_out.txt");

            WorkClasses.Ticks_In TicksIn = new WorkClasses.Ticks_In();
            WorkClasses.Ticks_Out TicksOut = new WorkClasses.Ticks_Out();

            datastring = baseFile.ReadLine();
            OutFile.WriteLine("Date CurTime timeStep Ask AskIncrement Bid BidIncrement");
            

            for (int i = 0; ; i++)
            {
                if (baseFile.EndOfStream == true) break;

                datastring = baseFile.ReadLine();
                dataArray = datastring.Split(' ');

                TicksIn.Count = TicksIn.Count + 1;
                TicksIn.Date = dataArray[0];
                TicksIn.CurTime = dataArray[1];

                TicksIn.timeStep[TicksIn.dim - 1] = Convert.ToDouble(dataArray[2]);
                TicksIn.Ask[TicksIn.dim - 1] = Convert.ToDouble(dataArray[3]);
                TicksIn.Bid[TicksIn.dim - 1] = Convert.ToDouble(dataArray[5]);
                TicksIn.AskIncrement[TicksIn.dim - 1] = Convert.ToDouble(dataArray[4]);
                TicksIn.BidIncrement[TicksIn.dim - 1] = Convert.ToDouble(dataArray[6]);

                if (TicksIn.Count == 5379135 || TicksIn.Count == 4429261 || TicksIn.Count == 4185063 || TicksIn.Count == 1244670)
                    TicksIn.Count = TicksIn.Count;

                OutFile.WriteLine(TicksIn.Date + " " + TicksIn.CurTime.ToString() + " " + TicksIn.timeStep[TicksIn.dim - 1].ToString() + " " + TicksIn.Ask[TicksIn.dim - 1].ToString() + " " +
                                  TicksIn.AskIncrement[TicksIn.dim - 1].ToString() + " " + TicksIn.Bid[TicksIn.dim - 1].ToString() + " " + TicksIn.BidIncrement[TicksIn.dim - 1].ToString());

                if (i % 5 == 0)
                    OutFileOnlyCountTime.WriteLine(TicksIn.Count + " " + TicksIn.Date);
            }


            datastring = addFile.ReadLine();
            for (int i = 0; ; i++)
            {
                if (addFile.EndOfStream == true) break;

                datastring = addFile.ReadLine();
                dataArray = datastring.Split(' ');

                TicksIn.Count = TicksIn.Count + 1;
                TicksIn.Date = dataArray[1];
                TicksIn.CurTime = dataArray[2];

                TicksIn.timeStep[TicksIn.dim - 1] = Convert.ToDouble(dataArray[3]);
                TicksIn.Ask[TicksIn.dim - 1] = Convert.ToDouble(dataArray[4]);
                TicksIn.Bid[TicksIn.dim - 1] = Convert.ToDouble(dataArray[6]);
                TicksIn.AskIncrement[TicksIn.dim - 1] = Convert.ToDouble(dataArray[5]);
                TicksIn.BidIncrement[TicksIn.dim - 1] = Convert.ToDouble(dataArray[7]);

                OutFile.WriteLine(TicksIn.Date + " " + TicksIn.CurTime.ToString() + " " + TicksIn.timeStep[TicksIn.dim - 1].ToString() + " " + TicksIn.Ask[TicksIn.dim - 1].ToString() + " " +
                                  TicksIn.AskIncrement[TicksIn.dim - 1].ToString() + " " + TicksIn.Bid[TicksIn.dim - 1].ToString() + " " + TicksIn.BidIncrement[TicksIn.dim - 1].ToString());

                if (i % 5 == 0)
                    OutFileOnlyCountTime.WriteLine(TicksIn.Count + " " + TicksIn.Date);
            }

            OutFile.Close();
            addFile.Close();
            OutFileOnlyCountTime.Close();
            StreamReader addFile1 = new StreamReader(WorkClasses.dir_str + "//To_append.txt");

            TicksIn.Count = 0;


            //-------------------------------------------------------------------------------------------------------------------------------------------------//
            //-------------------------------------------------------------------------------------------------------------------------------------------------//
            //-------------------------------------------------------------------------------------------------------------------------------------------------//


            datastring = baseFile_Indic.ReadLine();
            OutFile_Indic.WriteLine("date time time_step ask ask_ink bid bid_ink StM5_733_0 StM5_733_1 StM5_522_0 StM5_522_1 StM1_733_0 StM1_733_1 Close_open_now Close_open_prev std_cur std_prev bulls bear");

            for (int i = 0; ; i++)
            {
                if (baseFile_Indic.EndOfStream == true) break;

                datastring = baseFile_Indic.ReadLine();
                dataArray = datastring.Split(' ');

                TicksIn.Count = TicksIn.Count + 1;
                TicksIn.Date = dataArray[0];
                TicksIn.CurTime = dataArray[1];


                TicksIn.timeStep[TicksIn.dim - 1] = Convert.ToDouble(dataArray[2]);
                TicksIn.Ask[TicksIn.dim - 1] = Convert.ToDouble(dataArray[3]);
                TicksIn.Bid[TicksIn.dim - 1] = Convert.ToDouble(dataArray[5]);
                TicksIn.AskIncrement[TicksIn.dim - 1] = Convert.ToDouble(dataArray[4]);
                TicksIn.BidIncrement[TicksIn.dim - 1] = Convert.ToDouble(dataArray[6]);

                TicksIn.StM5_733_0 = Math.Round(Convert.ToDouble(dataArray[7]), 2);
                TicksIn.StM5_733_1 = Math.Round(Convert.ToDouble(dataArray[8]), 2);
                TicksIn.StM5_532_0 = Math.Round(Convert.ToDouble(dataArray[9]), 2);
                TicksIn.StM5_532_1 = Math.Round(Convert.ToDouble(dataArray[10]), 2);
                TicksIn.StM1_733_0[0] = Math.Round(Convert.ToDouble(dataArray[11]), 2);
                TicksIn.StM1_733_1 = Math.Round(Convert.ToDouble(dataArray[12]), 2);
                TicksIn.DifOpenClose_0 = Convert.ToInt32(dataArray[13]);
                TicksIn.DifOpenClose_1 = Convert.ToInt32(dataArray[14]);
                TicksIn.StdDevM5_40_0 = Math.Round(Convert.ToDouble(dataArray[15]), 2);
                TicksIn.StdDevM5_40_1 = Math.Round(Convert.ToDouble(dataArray[16]), 2);
                TicksIn.iBullsM5_7_1 = Math.Round(Convert.ToDouble(dataArray[17]), 2);
                TicksIn.iBearM5_7_1 = Math.Round(Convert.ToDouble(dataArray[18]), 2);

                if (TicksIn.Count == 5379135 || TicksIn.Count == 4429261 || TicksIn.Count == 4185063 || TicksIn.Count == 1244670)
                    TicksIn.Count = TicksIn.Count;

                OutFile_Indic.WriteLine(TicksIn.Date + " " + TicksIn.CurTime.ToString() + " " + TicksIn.timeStep[TicksIn.dim - 1].ToString() + " " + TicksIn.Ask[TicksIn.dim - 1].ToString() + " " +
                                  TicksIn.AskIncrement[TicksIn.dim - 1].ToString() + " " + TicksIn.Bid[TicksIn.dim - 1].ToString() + " " + TicksIn.BidIncrement[TicksIn.dim - 1].ToString()
                                  + " " + TicksIn.StM5_733_0 + " " + TicksIn.StM5_733_1 + " " + TicksIn.StM5_532_0 + " " + TicksIn.StM5_532_1 + " " +
                                        TicksIn.StM1_733_0[0] + " " + TicksIn.StM1_733_1 + " " + TicksIn.DifOpenClose_0 + " " + TicksIn.DifOpenClose_1 + " " +
                                        TicksIn.StdDevM5_40_0 + " " + TicksIn.StdDevM5_40_1 + " " + TicksIn.iBullsM5_7_1 + " " + TicksIn.iBearM5_7_1);

                
            }

            string CurDate = TicksIn.Date;


            datastring = addFile1.ReadLine();
            StreamWriter ParsOpenAndCloseMoments = new StreamWriter(WorkClasses.dir_str + "//Ticks_appended_OpenVsClose.txt");
            ParsOpenAndCloseMoments.WriteLine("Count Date Time dttm_milli Aks Bid flag_avg_F_6 ticket Dif_int ticks_activ GetLastError _OrderOpenPrice _OrderClosePrice Time_Of_Tick");
            int cntOutputAfterClose = 0;
            for (int i = 0; ; i++)
            {
                if (addFile1.EndOfStream == true) break;

                datastring = addFile1.ReadLine();
                dataArray = datastring.Split(' ');

                TicksIn.Count = TicksIn.Count + 1;
                TicksIn.Date = dataArray[1];
                TicksIn.CurTime = dataArray[2];


                TicksIn.timeStep[TicksIn.dim - 1] = Convert.ToDouble(dataArray[3]);
                TicksIn.Ask[TicksIn.dim - 1] = Convert.ToDouble(dataArray[4]);
                TicksIn.Bid[TicksIn.dim - 1] = Convert.ToDouble(dataArray[6]);
                TicksIn.AskIncrement[TicksIn.dim - 1] = Convert.ToDouble(dataArray[5]);
                TicksIn.BidIncrement[TicksIn.dim - 1] = Convert.ToDouble(dataArray[7]);

                TicksIn.StM5_733_0 = Math.Round(Convert.ToDouble(dataArray[11]), 2);
                TicksIn.StM5_733_1 = Math.Round(Convert.ToDouble(dataArray[12]), 2);
                TicksIn.StM5_532_0 = Math.Round(Convert.ToDouble(dataArray[13]), 2);
                TicksIn.StM5_532_1 = Math.Round(Convert.ToDouble(dataArray[14]), 2);
                TicksIn.StM1_733_0[0] = Math.Round(Convert.ToDouble(dataArray[15]), 2);
                TicksIn.StM1_733_1 = Math.Round(Convert.ToDouble(dataArray[16]), 2);
                TicksIn.DifOpenClose_0 = Convert.ToInt32(dataArray[17]);
                TicksIn.DifOpenClose_1 = Convert.ToInt32(dataArray[18]);
                TicksIn.StdDevM5_40_0 = Math.Round(Convert.ToDouble(dataArray[19]), 2);
                TicksIn.StdDevM5_40_1 = Math.Round(Convert.ToDouble(dataArray[20]), 2);
                TicksIn.iBullsM5_7_1 = Math.Round(Convert.ToDouble(dataArray[21]), 2);
                TicksIn.iBearM5_7_1 = Math.Round(Convert.ToDouble(dataArray[22]), 2);


                //---выводим моменты открытия-закрытия из добавляемого файла
                cntOutputAfterClose--;
                if (Math.Abs(Convert.ToDouble(dataArray[8])) == 1)
                    cntOutputAfterClose = 10;
                if ( (Math.Abs(Convert.ToDouble(dataArray[8])) == 1 && Convert.ToDouble(dataArray[23]) >= 0 && Convert.ToDouble(dataArray[23]) < 10)
                        || ((Math.Abs(Convert.ToDouble(dataArray[8])) == 2 || Math.Abs(Convert.ToDouble(dataArray[8])) == 0) && cntOutputAfterClose > 0))
                {
                    ParsOpenAndCloseMoments.WriteLine(TicksIn.Count + " " + TicksIn.Date + " " + TicksIn.CurTime + " " + TicksIn.timeStep[TicksIn.dim - 1] + " " + TicksIn.Ask[TicksIn.dim - 1] + " " + TicksIn.Bid[TicksIn.dim - 1]
                         + " " + dataArray[8] + " " + dataArray[9] + " " + dataArray[10] + " " + dataArray[23] + " " + dataArray[24] + " " + dataArray[28] + " " + dataArray[29] + " " + dataArray[30]);
                }


                OutFile_Indic.WriteLine(TicksIn.Date + " " + TicksIn.CurTime.ToString() + " " + TicksIn.timeStep[TicksIn.dim - 1].ToString() + " " + TicksIn.Ask[TicksIn.dim - 1].ToString() + " " +
                                  TicksIn.AskIncrement[TicksIn.dim - 1].ToString() + " " + TicksIn.Bid[TicksIn.dim - 1].ToString() + " " + TicksIn.BidIncrement[TicksIn.dim - 1].ToString()
                                  + " " + TicksIn.StM5_733_0 + " " + TicksIn.StM5_733_1 + " " + TicksIn.StM5_532_0 + " " + TicksIn.StM5_532_1 + " " +
                                        TicksIn.StM1_733_0[0] + " " + TicksIn.StM1_733_1 + " " + TicksIn.DifOpenClose_0 + " " + TicksIn.DifOpenClose_1 + " " +
                                        TicksIn.StdDevM5_40_0 + " " + TicksIn.StdDevM5_40_1 + " " + TicksIn.iBullsM5_7_1 + " " + TicksIn.iBearM5_7_1);
            }

            OutFile.Close(); OutFile_Indic.Close(); baseFile_Indic.Close(); OutFile_Indic.Close();
            addFile1.Close(); ParsOpenAndCloseMoments.Close();



            Console.WriteLine("Последник каунт = " + TicksIn.Count);


            //datastring = null;

            //baseFile_Indic = new StreamReader(WorkClasses.dir_str + "//Ticks_base_indicator_out.txt");
            //OutFile_Indic = new StreamWriter(WorkClasses.dir_str + "//Ticks_base_ind_local.txt");

            //TicksIn = new WorkClasses.Ticks_In();
            //TicksOut = new WorkClasses.Ticks_Out();

            //datastring = baseFile_Indic.ReadLine();
            //OutFile_Indic.WriteLine("Count Date CurTime timeStep Ask AskIncrement Bid BidIncrement");

            //datastring = baseFile_Indic.ReadLine();
            //for (int i = 0; ; i++)
            //{
            //    if (baseFile_Indic.EndOfStream == true) break;

            //    datastring = baseFile_Indic.ReadLine();
            //    dataArray = datastring.Split(' ');

            //    TicksIn.Count = TicksIn.Count + 1;
            //    TicksIn.Date = dataArray[1];
            //    TicksIn.CurTime = dataArray[2];


            //    TicksIn.timeStep[TicksIn.dim - 1] = Convert.ToDouble(dataArray[3]);
            //    TicksIn.Ask[TicksIn.dim - 1] = Convert.ToDouble(dataArray[4]);
            //    TicksIn.Bid[TicksIn.dim - 1] = Convert.ToDouble(dataArray[6]);
            //    TicksIn.AskIncrement[TicksIn.dim - 1] = Convert.ToDouble(dataArray[5]);
            //    TicksIn.BidIncrement[TicksIn.dim - 1] = Convert.ToDouble(dataArray[7]);

            //    TicksIn.StM5_733_0 = Convert.ToDouble(dataArray[8]);
            //    TicksIn.StM5_733_1 = Convert.ToDouble(dataArray[9]);
            //    TicksIn.StM5_733_2 = Convert.ToDouble(dataArray[10]);
            //    TicksIn.StM5_733_3 = Convert.ToDouble(dataArray[11]);
            //    TicksIn.StM5_733_4 = Convert.ToDouble(dataArray[12]);
            //    //TicksIn.StM5_734_0 = Convert.ToDouble(dataArray[13]);
            //    //TicksIn.StM5_734_1 = Convert.ToDouble(dataArray[14]);
            //    //TicksIn.StM5_734_2 = Convert.ToDouble(dataArray[15]);
            //    //TicksIn.StM5_734_3 = Convert.ToDouble(dataArray[16]);
            //    //TicksIn.StM5_734_4 = Convert.ToDouble(dataArray[17]);
            //    //TicksIn.StM15_733_0 = Convert.ToDouble(dataArray[18]);
            //    //TicksIn.StM15_733_1 = Convert.ToDouble(dataArray[19]);
            //    //TicksIn.StM15_733_2 = Convert.ToDouble(dataArray[20]);
            //    //TicksIn.StM15_733_3 = Convert.ToDouble(dataArray[21]);
            //    //TicksIn.StM15_733_4 = Convert.ToDouble(dataArray[22]);
            //    //TicksIn.StM15_734_0 = Convert.ToDouble(dataArray[23]);
            //    //TicksIn.StM15_734_1 = Convert.ToDouble(dataArray[24]);
            //    //TicksIn.StM15_734_2 = Convert.ToDouble(dataArray[25]);
            //    //TicksIn.StM15_734_3 = Convert.ToDouble(dataArray[26]);
            //    //TicksIn.StM15_734_4 = Convert.ToDouble(dataArray[27]);

            //    OutFile_Indic.WriteLine(TicksIn.Count.ToString() + " " + TicksIn.Date + " " + TicksIn.CurTime.ToString() + " " + TicksIn.timeStep[TicksIn.dim - 1].ToString() + " " + TicksIn.Ask[TicksIn.dim - 1].ToString() + " " +
            //                      TicksIn.AskIncrement[TicksIn.dim - 1].ToString() + " " + TicksIn.Bid[TicksIn.dim - 1].ToString() + " " + TicksIn.BidIncrement[TicksIn.dim - 1].ToString()
            //                      + " " + TicksIn.StM5_733_0.ToString() + " " + TicksIn.StM5_733_1.ToString() + " " + TicksIn.StM5_733_2.ToString() + " " + TicksIn.StM5_733_3.ToString() + " " + TicksIn.StM5_733_4.ToString());
            //}

            //OutFile_Indic.Close();
            //baseFile_Indic.Close();
            baseFile.Close();

            File.Move(WorkClasses.dir_str + "//Ticks_base_indicator.txt", WorkClasses.dir_str + "//" + CurDate + "_Ticks_base_indicator.txt");
            File.Move(WorkClasses.dir_str + "//Ticks_base.txt", WorkClasses.dir_str + "//" + CurDate + "_Ticks_base.txt");

            File.Move(WorkClasses.dir_str + "//Ticks_base_indicator_out.txt", WorkClasses.dir_str + "//Ticks_base_indicator.txt");
            File.Move(WorkClasses.dir_str + "//Ticks_base_out.txt", WorkClasses.dir_str + "//Ticks_base.txt");

            CurDate = TicksIn.Date;
            File.Move(WorkClasses.dir_str + "//To_append.txt", WorkClasses.dir_str + "//Ticks_Log_" + CurDate + ".txt");
            File.Move(WorkClasses.dir_str + "//Ticks_Log_" + CurDate + ".txt", WorkClasses.dir_str + "//archive//" + CurDate.Split('.')[0] + "." + CurDate.Split('.')[1] + "//Ticks_Log_" + CurDate + ".txt");
            


            this.Close();
        }







        private void button1_Click(object sender, EventArgs e)
        {

            StreamReader addFile1 = new StreamReader("D://Ticks//Ticks_base.txt");
            StreamWriter OutFile_Indic = new StreamWriter("D://Ticks//Ticks_base_2.txt");

            string datastring = addFile1.ReadLine();
            string[] dataArray;

            WorkClasses.Ticks_In TicksIn = new WorkClasses.Ticks_In();

            double CurTime = 0.0, avg_timestep = 0.0 ;
            double[] temp = new double[20];
            int int1, int2;

            int CntToAvg = 100;
            double[] timeStep = new double[CntToAvg];



            //--- НУЖЕН ГРАФИК ПО ДНЯМ С МЕДИАНОЙ, СРЕДНЕМ И Т.Д. ЗА ОПРЕДЕЛЕННЫЕ ПРОМЕЖУТКИ ВРЕМЕНИ НА КАЖДЫЙ ДЕНЬ ---//
            for (int i = 0; ; i++)
            {
                if (addFile1.EndOfStream == true) break;

                datastring = addFile1.ReadLine();
                TicksIn.dataArray = datastring.Split(' ');

                TicksIn.Count = TicksIn.Count + 1;
                TicksIn.Date = TicksIn.dataArray[0];
                TicksIn.CurTime = TicksIn.dataArray[1];

                CurTime = Convert.ToInt32(TicksIn.CurTime.Split(':')[0]) * 3600.0 + Convert.ToInt32(TicksIn.CurTime.Split(':')[1]) * 60.0 + Convert.ToInt32(TicksIn.CurTime.Split(':')[2]);

                if (CurTime > 13 * 3600 && CurTime < 21 * 3600)
                {
                    //if (TicksIn.Count == 5969721)
                    //    TicksIn.Count = TicksIn.Count;

                    if (Convert.ToDouble(TicksIn.dataArray[2]) > 100.0) continue;

                    for (int j = 0; j < CntToAvg-1; j++)
                    {
                        timeStep[j] = timeStep[j + 1];
                    }
                    timeStep[CntToAvg-1] = Convert.ToDouble(TicksIn.dataArray[2]);

                    avg_timestep = 0.0;
                    for (int j = 10; j < CntToAvg; j++)
                    {
                        avg_timestep += timeStep[j];
                    }
                    avg_timestep = avg_timestep / (double)CntToAvg;

                    OutFile_Indic.WriteLine(TicksIn.Count + " " + TicksIn.Date + " " + timeStep[CntToAvg-1] + " " + avg_timestep);
                }
            }
            OutFile_Indic.Close(); addFile1.Close();
            //Close();

        //    StreamReader addFile1 = new StreamReader("D://Indicator_Statictics.txt");
        //    StreamWriter OutFile_Indic = new StreamWriter("D://Indicator_Statictics_2.txt");

        //    string datastring = addFile1.ReadLine();
        //    string[] dataArray;

        //    double[] temp = new double[20];
        //    int int1, int2;

        //    for (int i = 0; ; i++)
        //    {
        //        if (addFile1.EndOfStream == true) break;

        //        datastring = addFile1.ReadLine();
        //        dataArray = datastring.Split(' ');

        //        temp[0] = Math.Round(Convert.ToDouble(dataArray[3]), 2);
        //        temp[1] = Math.Round(Convert.ToDouble(dataArray[4]), 2);
        //        temp[2] = Math.Round(Convert.ToDouble(dataArray[5]), 2);
        //        temp[3] = Math.Round(Convert.ToDouble(dataArray[6]), 2);
        //        temp[4] = Math.Round(Convert.ToDouble(dataArray[7]), 2);
        //        temp[5] = Math.Round(Convert.ToDouble(dataArray[8]), 2);
        //        int1 = Convert.ToInt32(dataArray[9]);
        //        int2 = Convert.ToInt32(dataArray[10]);
        //        temp[10] = Math.Round(Convert.ToDouble(dataArray[11]), 2);
        //        temp[11] = Math.Round(Convert.ToDouble(dataArray[12]), 2);
        //        temp[12] = Math.Round(Convert.ToDouble(dataArray[13]), 2);
        //        temp[13] = Math.Round(Convert.ToDouble(dataArray[14]), 2);

        //        OutFile_Indic.WriteLine(dataArray[1] + " " + dataArray[2] + " " + temp[0] + " " + temp[1] + " " + temp[2] + " " + temp[3] + " " + temp[4] + " " + temp[5] + " " + int1 + " " + int2 + " " + temp[10] + " " + temp[11] + " " + temp[12] + " " + temp[13]);
        //    }
        //    OutFile_Indic.Close(); addFile1.Close();
        //    Close();
        }




        private void DoShortFile_Click(object sender, EventArgs e)
        {
            string datastring = "";

            WorkClasses.Ticks_In TicksIn = new WorkClasses.Ticks_In();

            WorkClasses.base_file_name = "//Ticks_base_indicator.txt";
            StreamReader baseFile = new StreamReader(WorkClasses.dir_str + WorkClasses.base_file_name);
            StreamWriter baseFileShort = new StreamWriter(WorkClasses.dir_str + "//Ticks_base_indicator_Short.txt");
            StreamWriter _i_BigSTD = new StreamWriter(WorkClasses.dir_str + "//_i_BigSTD.txt");

            datastring = baseFile.ReadLine();


            //1000 2000 50.0
            int intervalLeft = 1000;
            int intervalRight = 2000;
            double initStdDevM5_40_1 = 50.0;



            for (int i = 0; ; i++)
            {
                if (baseFile.EndOfStream == true) break;
                datastring = baseFile.ReadLine();
                TicksIn.dataArray = datastring.Split(' ');

                if (Convert.ToDouble(TicksIn.dataArray[16]) > initStdDevM5_40_1 && TicksIn.StdDevM5_40_1 != Convert.ToDouble(TicksIn.dataArray[16]))
                {
                    _i_BigSTD.WriteLine(i);
                }


                TicksIn.StdDevM5_40_0 = Convert.ToDouble(TicksIn.dataArray[15]);
                if (TicksIn.StM5_733_1 != Convert.ToDouble(TicksIn.dataArray[8]))
                {
                    TicksIn.StdDevM5_40_4 = TicksIn.StdDevM5_40_3;
                    TicksIn.StdDevM5_40_3 = TicksIn.StdDevM5_40_2;
                    TicksIn.StdDevM5_40_2 = TicksIn.StdDevM5_40_1;
                    TicksIn.StdDevM5_40_1 = Convert.ToDouble(TicksIn.dataArray[16]);
                }
            }
            _i_BigSTD.Close();




            int _dim_i_BigSTD = 0, _dim_i_BigSTD_full = 0, ji = 0;
            double temp = 0.0;
            StreamReader _i_BigSTD_Read = new StreamReader(WorkClasses.dir_str + "//_i_BigSTD.txt");

            for (int i = 0; ; i++)
            {
                if (_i_BigSTD_Read.EndOfStream == true) break;
                datastring = _i_BigSTD_Read.ReadLine();
                if (Convert.ToDouble(datastring) > temp + Math.Max(intervalLeft, intervalRight))
                {
                    _dim_i_BigSTD++;
                    temp = Convert.ToDouble(datastring);
                }

                _dim_i_BigSTD_full++;
            }

            temp = 0.0;
            _i_BigSTD_Read.BaseStream.Seek(0, SeekOrigin.Begin);
            double[] _array_i_BigSTD = new double[_dim_i_BigSTD];
            for (int i = 0; i < _dim_i_BigSTD_full; i++)
            {
                datastring = _i_BigSTD_Read.ReadLine();
                if (Convert.ToDouble(datastring) > temp + Math.Max(intervalLeft, intervalRight))
                {
                    _array_i_BigSTD[ji] = Convert.ToDouble(datastring);
                    temp = _array_i_BigSTD[ji];
                    ji++;
                }
            }
            _i_BigSTD_Read.Close();





            baseFile.BaseStream.Seek(0, SeekOrigin.Begin);
            datastring = baseFile.ReadLine();
            for (int i = 0; ; i++)
            {
                if (baseFile.EndOfStream == true) break;
                datastring = baseFile.ReadLine();
                TicksIn.dataArray = datastring.Split(' ');

                for (int j = 0; j < _dim_i_BigSTD; j++)
                    if (i > _array_i_BigSTD[j] - intervalLeft && i < _array_i_BigSTD[j] + intervalRight)
                    {
                        baseFileShort.WriteLine(datastring);
                        break;
                    }


                TicksIn.StdDevM5_40_0 = Convert.ToDouble(TicksIn.dataArray[15]);
                if (TicksIn.StM5_733_1 != Convert.ToDouble(TicksIn.dataArray[8]))
                {
                    TicksIn.StdDevM5_40_4 = TicksIn.StdDevM5_40_3;
                    TicksIn.StdDevM5_40_3 = TicksIn.StdDevM5_40_2;
                    TicksIn.StdDevM5_40_2 = TicksIn.StdDevM5_40_1;
                    TicksIn.StdDevM5_40_1 = Convert.ToDouble(TicksIn.dataArray[16]);
                }
            }
            baseFile.Close();
            baseFileShort.Close();
            this.Close();
        }





        //private void DoShortFile_Click(object sender, EventArgs e)
        //{
        //    string datastring = "";

        //    WorkClasses.Ticks_In TicksIn = new WorkClasses.Ticks_In();

        //    WorkClasses.base_file_name = "//Ticks_base_indicator.txt";
        //    StreamReader baseFile = new StreamReader(WorkClasses.dir_str + WorkClasses.base_file_name);
        //    StreamWriter baseFileShort = new StreamWriter(WorkClasses.dir_str + "//Ticks_base_indicator_Short.txt");
        //    StreamWriter _i_BigSTD = new StreamWriter(WorkClasses.dir_str + "//_i_BigSTD.txt");

        //    datastring = baseFile.ReadLine();

        //    int flag_OpenInteral = 0;
        //    int temp_i = 0;
        //    for (int i = 0; ; i++)
        //    {
        //        if (baseFile.EndOfStream == true) break;
        //        datastring = baseFile.ReadLine();
        //        TicksIn.dataArray = datastring.Split(' ');

        //        if (Convert.ToDouble(TicksIn.dataArray[16]) > 50.0 && flag_OpenInteral == 0)
        //        {
        //            temp_i = i;
        //            flag_OpenInteral = 1;
        //        }
        //        else if (Convert.ToDouble(TicksIn.dataArray[16]) < 50.0 && flag_OpenInteral == 1)
        //        {
        //            _i_BigSTD.WriteLine(temp_i + " " + i);
        //            flag_OpenInteral = 0;
        //        }
        //    }
        //    _i_BigSTD.Close();




        //    int _dim_i_BigSTD = 0;
        //    StreamReader _i_BigSTD_Read = new StreamReader(WorkClasses.dir_str + "//_i_BigSTD.txt");

        //    for (int i = 0; ; i++)
        //    {
        //        if (_i_BigSTD_Read.EndOfStream == true) break;
        //        datastring = _i_BigSTD_Read.ReadLine();
        //        _dim_i_BigSTD++;
        //    }

        //    _i_BigSTD_Read.BaseStream.Seek(0, SeekOrigin.Begin);
        //    double[] _array_i_BigSTD_Open = new double[_dim_i_BigSTD], _array_i_BigSTD_Close = new double[_dim_i_BigSTD];
        //    for (int i = 0; i < _dim_i_BigSTD; i++)
        //    {
        //        datastring = _i_BigSTD_Read.ReadLine();
        //        TicksIn.dataArray = datastring.Split(' ');

        //        _array_i_BigSTD_Open[i] = Convert.ToDouble(TicksIn.dataArray[0]);
        //        _array_i_BigSTD_Close[i] = Convert.ToDouble(TicksIn.dataArray[1]);
        //    }
        //    _i_BigSTD_Read.Close();





        //    baseFile.BaseStream.Seek(0, SeekOrigin.Begin);
        //    datastring = baseFile.ReadLine();
        //    for (int i = 0; ; i++)
        //    {
        //        if (baseFile.EndOfStream == true) break;
        //        datastring = baseFile.ReadLine();
        //        TicksIn.dataArray = datastring.Split(' ');

        //        //for (int j = 0; j < _dim_i_BigSTD; j++)
        //        //    if (i > _array_i_BigSTD_Open[j] - 500 && i < _array_i_BigSTD_Close[j] + 1500)
        //        //    {
        //        //        baseFileShort.WriteLine(datastring);
        //        //        break;
        //        //    }

        //        if (Convert.ToDouble(TicksIn.dataArray[16]) < 10.0)
        //            baseFileShort.WriteLine(datastring);


        //        TicksIn.StdDevM5_40_0 = Convert.ToDouble(TicksIn.dataArray[15]);
        //        if (TicksIn.StM5_733_1 != Convert.ToDouble(TicksIn.dataArray[8]))
        //        {
        //            TicksIn.StdDevM5_40_4 = TicksIn.StdDevM5_40_3;
        //            TicksIn.StdDevM5_40_3 = TicksIn.StdDevM5_40_2;
        //            TicksIn.StdDevM5_40_2 = TicksIn.StdDevM5_40_1;
        //            TicksIn.StdDevM5_40_1 = Convert.ToDouble(TicksIn.dataArray[16]);
        //        }
        //    }
        //    baseFile.Close();
        //    baseFileShort.Close();
        //    this.Close();
        //}
    }

}





