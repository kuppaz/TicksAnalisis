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

namespace Ticks_analysis
{
    class By_Cycle
    {
        public static void Go_run_sycle(bool With_indicators, bool Only_BUY, bool Only_SELL, bool interval_ECN_since_22_07_14)
        {
            string datastring = null;
            string[] dataArray;

            if (With_indicators == false) WorkClasses.base_file_name = "//Ticks_base.txt";
            if (With_indicators == true) WorkClasses.base_file_name = "//Ticks_base_indicator.txt";


            int BaseCount = 0;
            StreamReader baseFile_0 = new StreamReader(WorkClasses.dir_str + WorkClasses.base_file_name);
            for (int i = 0; ; i++)
            {
                datastring = baseFile_0.ReadLine();
                if (baseFile_0.EndOfStream == true) break;
                BaseCount++;
            }
            baseFile_0.Close();

            int jj;
            DateTime start_time1 = DateTime.Now;
            StreamReader baseFile_1 = new StreamReader(WorkClasses.dir_str + WorkClasses.base_file_name);
            datastring = baseFile_1.ReadLine();

            if (interval_ECN_since_22_07_14 == true)
            {
                int left = 14390211;
                BaseCount = BaseCount - left;
                for (int j = 0; j < left; j++)
                    datastring = baseFile_1.ReadLine();
            }
            else
            {
                for (jj = 0; jj < BaseCount - 10400000; jj++)
                {
                    datastring = baseFile_1.ReadLine();
                }
                BaseCount = BaseCount - jj - 10;
            }

            string[] CurTime = new string[BaseCount], Date_on_time = new string[BaseCount];
            double[] timeStep = new double[BaseCount], Ask = new double[BaseCount], Bid = new double[BaseCount], AskIncrement = new double[BaseCount], BidIncrement = new double[BaseCount];

            double[] StM5_733_0 = new double[BaseCount];
            double[] StM5_733_1 = new double[BaseCount];
            double[] StM5_733_2 = new double[BaseCount];
            double[] StM5_733_3 = new double[BaseCount];
            double[] StM5_733_4 = new double[BaseCount];


            
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

                if (With_indicators)
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


            double ticks_sum = 0, Last_MaxFALLbyOrder = 0;


            StreamWriter ResultFile = new StreamWriter(WorkClasses.dir_str + "//_Agregate_by_Cycle.txt");
            ResultFile.WriteLine("cnt_sell cnt_buy _out_sell _out_buy _in_sell _in_buy profit Add_Bal Add_BalDay MAX_FALL MAX_FALL_Day cnt_orders atitude_prcnt max_cnt_sell max_cnt_buy sth_low sth_max a BigMassCnt VolBMSELL_nega VolBMSELL_posi VolBMBUY_posi VolBMBUY_nega MAX_FALL_Round MNK MNM SELL_summ BUY_summ MaxCurProfitToClose_SELL MaxCurProfitToClose_BUY MaxCurStopLossToClose_SELL MaxCurStopLossToClose_BYU");

            //Сначала сканируем без BigMass. Затем начинаем варьировать.
            //лучше искать 1. по SELL, с найденным лучшим вариантом искать по 2.SELL+BUY. Затем можно воварьировать лучший найденный вариант


            int min_count_sell = 10, max_count_sell = 11, incr_count_sell = 1;
            int min_count_buy = 10, max_count_buy = 14, incr_count_buy = 1;
            int min_slice_sell = 10000, max_slice_sell = 10001, incr_slice_sell = 1;
            int min_slice_buy = 21, max_slice_buy = 31, incr_slice_buy = 3;
            int min_t_in_sell = 10000, max_t_in_sell = 10001, incr_t_in_sell = 10;
            int min_t_in_buy = 450, max_t_in_buy = 1051, incr_t_in_buy = 100;
            int min_sth_low = 190, max_sth_low = 191, incr_sth_low = 20;
            int min_sth_high = 860, max_sth_high = 861, incr_sth_high = 100;
            int min_max_ticks_activ_cnt = 10000, max_max_ticks_activ_cnt = 10001, incr_max_ticks_activ_cnt = 2000;
            int min_max_ticks_activ_cnt_buy = 0, max_max_ticks_activ_cnt_buy = 1, incr_max_ticks_activ_cnt_buy = 1000;
            int min_BigMassCnt = 0, max_BigMassCnt = 1, incr_BigMassCnt = 2;
            int min_VolBigMassSELL_negative = 0, max_VolBigMassSELL_negative = 1, incr_VolBigMassSELL_negative = 50;
            int min_VolBigMassSELL_positive = 0, max_VolBigMassSELL_positive = 1, incr_VolBigMassSELL_positive = 50;
            int min_VolBigMassBUY_positive = 0, max_VolBigMassBUY_positive = 1, incr_VolBigMassBUY_positive = 50;
            int min_VolBigMassBUY_negative = 0, max_VolBigMassBUY_negative = 1, incr_VolBigMassBUY_negative = 50;
            int min_MaxCurProfitToClose_BUY = 0, max_MaxCurProfitToClose_BUY = 501, incr_MaxCurProfitToClose_BUY = 100;
            int min_MaxCurProfitToClose_SELL = 10000, max_MaxCurProfitToClose_SELL = 10001, incr_MaxCurProfitToClose_SELL = 25;
            int min_MaxCurStopLossToClose_BUY = 0, max_MaxCurStopLossToClose_BUY = 501, incr_MaxCurStopLossToClose_BUY = 100;
            int min_MaxCurStopLossToClose_SELL = 10000, max_MaxCurStopLossToClose_SELL = 10001, incr_MaxCurStopLossToClose_SELL = 150;





            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 0, flg_F_avg_6, 10, 0, 4.20, 0.0, 17E-5, 0E-5, 0, 0, 22, 82, "new", "new", 0, 250, 0, 0, 0, 0, 0); //(!)3334	18833	59525	-166	-96	39

            //Ask_F_avg_3
            for (int count_sell = min_count_sell; count_sell < max_count_sell; count_sell = count_sell + incr_count_sell)
            {
                for (int count_buy = min_count_buy; count_buy < max_count_buy; count_buy = count_buy + incr_count_buy)
                {
                    for (int slice_sell = min_slice_sell; slice_sell < max_slice_sell; slice_sell = slice_sell + incr_slice_sell)
                    {
                        for (int slice_buy = min_slice_buy; slice_buy < max_slice_buy; slice_buy = slice_buy + incr_slice_buy)
                        {
                            for (int t_in_sell = min_t_in_sell; t_in_sell < max_t_in_sell; t_in_sell = t_in_sell + incr_t_in_sell)
                            {
                                for (int t_in_buy = min_t_in_buy; t_in_buy < max_t_in_buy; t_in_buy = t_in_buy + incr_t_in_buy)
                                {
                                    for (int sth_low = min_sth_low; sth_low < max_sth_low; sth_low = sth_low + incr_sth_low)
                                    {
                                        for (int sth_high = min_sth_high; sth_high < max_sth_high; sth_high = sth_high + incr_sth_high)
                                        {
                                            for (int max_ticks_activ_cnt = min_max_ticks_activ_cnt; max_ticks_activ_cnt < max_max_ticks_activ_cnt; max_ticks_activ_cnt = max_ticks_activ_cnt + incr_max_ticks_activ_cnt)
                                            {
                                                //if (max_ticks_activ_cnt == 950) 
                                                //    max_ticks_activ_cnt = 3350;

                                                for (int max_ticks_activ_cnt_buy = min_max_ticks_activ_cnt_buy; max_ticks_activ_cnt_buy < max_max_ticks_activ_cnt_buy; max_ticks_activ_cnt_buy = max_ticks_activ_cnt_buy + incr_max_ticks_activ_cnt_buy)
                                                {
                                                    for (int BigMassCnt = min_BigMassCnt; BigMassCnt < max_BigMassCnt; BigMassCnt = BigMassCnt + incr_BigMassCnt)
                                                    {
                                                        //---------------------
                                                        for (double VolBigMassSELL_negative = min_VolBigMassSELL_negative; VolBigMassSELL_negative < max_VolBigMassSELL_negative; VolBigMassSELL_negative = VolBigMassSELL_negative + incr_VolBigMassSELL_negative)
                                                        {
                                                            for (double VolBigMassSELL_positive = min_VolBigMassSELL_positive; VolBigMassSELL_positive < max_VolBigMassSELL_positive; VolBigMassSELL_positive = VolBigMassSELL_positive + incr_VolBigMassSELL_positive)//обычно заполняется нулем
                                                            {
                                                                for (double VolBigMassBUY_positive = min_VolBigMassBUY_positive; VolBigMassBUY_positive < max_VolBigMassBUY_positive; VolBigMassBUY_positive = VolBigMassBUY_positive + incr_VolBigMassBUY_positive)
                                                                {
                                                                    for (double VolBigMassBUY_negative = min_VolBigMassBUY_negative; VolBigMassBUY_negative < max_VolBigMassBUY_negative; VolBigMassBUY_negative = VolBigMassBUY_negative + incr_VolBigMassBUY_negative)//обычно заполняется нулем
                                                                    {
                                                        //---------------------
                                                                        //double SellCustomSthCorrection = 0.0;
                                                                        double SellCustomSthCorrection = 5.0;

                                                                        double SELL_SPREAD = 25;
                                                                        double BUY_SPREAD = 21;

                                                                        for (int MaxCurProfitToClose_BUY = min_MaxCurProfitToClose_BUY; MaxCurProfitToClose_BUY < max_MaxCurProfitToClose_BUY; MaxCurProfitToClose_BUY = MaxCurProfitToClose_BUY + incr_MaxCurProfitToClose_BUY)
                                                                        {
                                                                            for (int MaxCurProfitToClose_SELL = min_MaxCurProfitToClose_SELL; MaxCurProfitToClose_SELL < max_MaxCurProfitToClose_SELL; MaxCurProfitToClose_SELL = MaxCurProfitToClose_SELL + incr_MaxCurProfitToClose_SELL)
                                                                            {


                                                                                for (int MaxCurStopLossToClose_BUY = min_MaxCurStopLossToClose_BUY; MaxCurStopLossToClose_BUY < max_MaxCurStopLossToClose_BUY; MaxCurStopLossToClose_BUY = MaxCurStopLossToClose_BUY + incr_MaxCurStopLossToClose_BUY)
                                                                                {
                                                                                    for (int MaxCurStopLossToClose_SELL = min_MaxCurStopLossToClose_SELL; MaxCurStopLossToClose_SELL < max_MaxCurStopLossToClose_SELL; MaxCurStopLossToClose_SELL = MaxCurStopLossToClose_SELL + incr_MaxCurStopLossToClose_SELL)
                                                                                    {
                                                                                        DateTime start_time = DateTime.Now;

                                                                                        if (Last_MaxFALLbyOrder < -1500.0 || ticks_sum * 100000.0 < 500.0)
                                                                                        {
                                                                                            Last_MaxFALLbyOrder = 10000.0;
                                                                                            ticks_sum = 10000.0;

                                                                                            DateTime end_time1 = DateTime.Now;
                                                                                            Console.WriteLine("Skip iteration. " + (start_time - end_time1).ToString());

                                                                                            continue;
                                                                                        }

                                                                                        double t_divide_by = 100.0;
                                                                                        WorkClasses.Ticks_In TicksIn = new WorkClasses.Ticks_In();
                                                                                        WorkClasses.Ticks_Out TicksOut = new WorkClasses.Ticks_Out();
                                                                                        WorkClasses.Stat_Info StatInfo = new WorkClasses.Stat_Info();

                                                                                        TicksIn.flag_ByCycle = true;
                                                                                        TicksIn.SellCustomSthCorrection = SellCustomSthCorrection;

                                                                                        if (Only_BUY) TicksIn.Only_BUY_flg = true;
                                                                                        if (Only_SELL) TicksIn.Only_SELL_flg = true;

                                                                                        int dim = TicksIn.dim - 1;
                                                                                        for (int i = 0; i < dim + 1; i++) TicksIn.timeStep[i] = 1;

                                                                                        int flg_F_avg_6 = 0;
                                                                                        ticks_sum = 0;
                                                                                        double summ_dt_3;

                                                                                        double[] Last_N_profit = new double[20];
                                                                                        int count_for_Last_N_profit = 3, cnt_closed_orders = 0, cnt_negative_avg_Last_N_profit = 0;
                                                                                        int[] Counts_On_Close = new int[50000];

                                                                                        for (int i = 0; i < BaseCount; i++)
                                                                                        //for (int i = 4000000; i < BaseCount; i++)
                                                                                        {
                                                                                            TicksIn.Count = TicksIn.Count + 1;
                                                                                            TicksIn.CurTime = CurTime[i];
                                                                                            TicksIn.Date = Date_on_time[i];

                                                                                            for (int j = 0; j < dim; j++)
                                                                                            {
                                                                                                TicksIn.timeStep[j] = TicksIn.timeStep[j + 1];
                                                                                                TicksIn.Ask[j] = TicksIn.Ask[j + 1];
                                                                                                TicksIn.AskIncrement[j] = TicksIn.AskIncrement[j + 1];
                                                                                                TicksIn.Spread[j] = TicksIn.Spread[j + 1];

                                                                                                //TicksOut.AskSpeed[j] = TicksOut.AskSpeed[j + 1];
                                                                                                TicksOut.Ask_F_avg_6[j] = TicksOut.Ask_F_avg_6[j + 1];
                                                                                            }




                                                                                            TicksIn.timeStep[dim] = timeStep[i];
                                                                                            TicksIn.Ask[dim] = Ask[i];
                                                                                            TicksIn.Bid[dim] = Bid[i];
                                                                                            TicksIn.AskIncrement[dim] = AskIncrement[i];
                                                                                            TicksIn.Spread[dim] = Math.Abs(TicksIn.Ask[dim] - TicksIn.Bid[dim]) * 100000.0;




                                                                                            if (With_indicators == true)
                                                                                            {
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

                                                                                                    for (int j = 0; j < dim + 1; j++)
                                                                                                    {
                                                                                                        TicksOut.Ask_F_avg_3[j] = 0;
                                                                                                        //TicksOut.Ask_F_avg_4[j] = 0;
                                                                                                        //TicksOut.Ask_F_avg_5[j] = 0;
                                                                                                        TicksOut.Ask_F_avg_6[j] = 0;
                                                                                                        //TicksOut.Ask_F_avg_8[j] = 0;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            //----------------------


                                                                                            //------------//
                                                                                            //------------//

                                                                                            if (count_sell > TicksIn.dim) break;

                                                                                            if (flg_F_avg_6 == 1)
                                                                                            {
                                                                                                TicksOut.local_Open_vs_Cur = TicksIn.Bid[dim] - TicksOut.OpenASK;
                                                                                                if (StatInfo.MaxLocalProfitInOrder[cnt_closed_orders] < TicksOut.local_Open_vs_Cur * 100000)
                                                                                                    StatInfo.MaxLocalProfitInOrder[cnt_closed_orders] = TicksOut.local_Open_vs_Cur * 100000;
                                                                                                TicksOut.ticks_activ_cnt++;
                                                                                                if (TicksOut.ticks_activ_cnt == 1)
                                                                                                {
                                                                                                    StatInfo.AskIncremBigMassSummNegative[cnt_closed_orders] = TicksIn.AskIncremBigMassSumm_Negative;
                                                                                                    StatInfo.AskIncremBigMassSummPositive[cnt_closed_orders] = TicksIn.AskIncremBigMassSumm_Positive;
                                                                                                }
                                                                                            }
                                                                                            else if (flg_F_avg_6 == -1)
                                                                                            {
                                                                                                TicksOut.local_Open_vs_Cur = TicksOut.OpenBID - TicksIn.Ask[dim];
                                                                                                if (StatInfo.MaxLocalProfitInOrder[cnt_closed_orders] < TicksOut.local_Open_vs_Cur * 100000)
                                                                                                    StatInfo.MaxLocalProfitInOrder[cnt_closed_orders] = TicksOut.local_Open_vs_Cur * 100000;
                                                                                                TicksOut.ticks_activ_cnt++;

                                                                                                if (TicksOut.ticks_activ_cnt == 1)
                                                                                                {
                                                                                                    StatInfo.AskIncremBigMassSummNegative[cnt_closed_orders] = TicksIn.AskIncremBigMassSumm_Negative;
                                                                                                    StatInfo.AskIncremBigMassSummPositive[cnt_closed_orders] = TicksIn.AskIncremBigMassSumm_Positive;
                                                                                                }
                                                                                            }
                                                                                            else if (flg_F_avg_6 == 0)
                                                                                            {
                                                                                                TicksOut.local_Open_vs_Cur = 0;
                                                                                            }




                                                                                            flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, SELL_SPREAD, BUY_SPREAD, flg_F_avg_6, count_sell, count_buy, t_in_sell / t_divide_by, t_in_buy / t_divide_by,
                                                                                                    slice_sell / 100000.0, slice_buy / 100000.0, max_ticks_activ_cnt, max_ticks_activ_cnt_buy, sth_low / 10.0, sth_high / 10.0, "new", "new",
                                                                                                    MaxCurProfitToClose_BUY, MaxCurProfitToClose_SELL, -MaxCurStopLossToClose_BUY, -MaxCurStopLossToClose_SELL,
                                                                                                    VolBigMassSELL_negative, VolBigMassSELL_positive, VolBigMassBUY_positive, VolBigMassBUY_negative, BigMassCnt);




                                                                                            if (Math.Abs(flg_F_avg_6) == 1 && TicksOut.ticks_activ_cnt == 0)
                                                                                                StatInfo.StartSpread[cnt_closed_orders] = TicksIn.Spread[dim];

                                                                                            count_for_Last_N_profit = 3;
                                                                                            if (Math.Abs(flg_F_avg_6) == 2)
                                                                                            {
                                                                                                if (flg_F_avg_6 == 2)
                                                                                                    TicksOut.local_Open_vs_Cur = TicksIn.Bid[dim] - TicksOut.OpenASK;
                                                                                                if (flg_F_avg_6 == -2)
                                                                                                    TicksOut.local_Open_vs_Cur = TicksOut.OpenBID - TicksIn.Ask[dim];

                                                                                                ticks_sum = ticks_sum + TicksOut.local_Open_vs_Cur;
                                                                                                TicksOut.Ticks_Sum_on_time[cnt_closed_orders] = ticks_sum * 100000;  //Присваиваем текущее значение профита для последующего МНК
                                                                                                TicksOut.Date_on_time[cnt_closed_orders] = TicksIn.Date;
                                                                                                TicksOut.Cur_Order_Profit[cnt_closed_orders] = (TicksOut.local_Open_vs_Cur) * 100000;
                                                                                                TicksOut.flg_F_avg_6[cnt_closed_orders] = flg_F_avg_6;
                                                                                                Counts_On_Close[cnt_closed_orders] = TicksIn.Count;

                                                                                                if (StatInfo.MaxLocalProfitInOrder[cnt_closed_orders] < TicksOut.local_Open_vs_Cur * 100000)
                                                                                                    StatInfo.MaxLocalProfitInOrder[cnt_closed_orders] = TicksOut.local_Open_vs_Cur * 100000;

                                                                                                for (int j = 0; j < count_for_Last_N_profit - 1; j++)
                                                                                                    Last_N_profit[j] = Last_N_profit[j + 1];
                                                                                                Last_N_profit[count_for_Last_N_profit - 1] = (TicksOut.local_Open_vs_Cur) * 100000;

                                                                                                double avg_Last_N_profit = 0.0;
                                                                                                for (int j = 0; j < count_for_Last_N_profit; j++) avg_Last_N_profit += Last_N_profit[j];
                                                                                                avg_Last_N_profit = avg_Last_N_profit / count_for_Last_N_profit;
                                                                                                if (cnt_closed_orders < count_for_Last_N_profit) avg_Last_N_profit = 0;
                                                                                                if (avg_Last_N_profit < 0.0) cnt_negative_avg_Last_N_profit++;

                                                                                                cnt_closed_orders++;
                                                                                            }

                                                                                            if (flg_F_avg_6 == -2 || flg_F_avg_6 == 2)
                                                                                            {
                                                                                                flg_F_avg_6 = 0;
                                                                                                TicksOut.ticks_activ_cnt = 0;
                                                                                            }
                                                                                        }

                                                                                        DateTime end_time_mid = DateTime.Now;

                                                                                        if (cnt_closed_orders == 0) continue;

                                                                                        double atitude_pecents = Convert.ToDouble(cnt_negative_avg_Last_N_profit) / (cnt_closed_orders + 1) * 100.0;

                                                                                        if (cnt_closed_orders > 1000)
                                                                                        {
                                                                                            Console.WriteLine("cnt_closed_orders > 1000");
                                                                                            continue;
                                                                                        }


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
                                                                                                for (int j = cnt_of_start_last_date + 1; j < cnt_closed_orders; j++)
                                                                                                {
                                                                                                    Ticks_Sum_on_Day[j] = Ticks_Sum_on_Day[cnt_of_start_last_date];
                                                                                                }
                                                                                            }
                                                                                        }


                                                                                        //---Аппроксимируем прямой---
                                                                                        double[] residuals = new double[cnt_closed_orders];
                                                                                        double s_xy = 0.0, s_x = 0.0, s_y = 0.0, s_x2 = 0.0;
                                                                                        for (int i = 0; i < cnt_closed_orders; i++)
                                                                                        {
                                                                                            s_x += i + 1;
                                                                                            s_y += TicksOut.Ticks_Sum_on_time[i];
                                                                                            s_xy += TicksOut.Ticks_Sum_on_time[i] * (i + 1);
                                                                                            s_x2 += (i + 1) * (i + 1);
                                                                                        }
                                                                                        double a = (cnt_closed_orders * s_xy - s_x * s_y) / (cnt_closed_orders * s_x2 - s_x * s_x);
                                                                                        double b = (s_y - a * s_x) / (cnt_closed_orders);


                                                                                        double SELL_summ_slice = 0.0, BUY_summ_slice = 0.0;
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


                                                                                        //StreamWriter helpFile = new StreamWriter(WorkClasses.dir_str + "////help_out.txt");
                                                                                        //Смотрим, сколько было бы "+" прибыли, если бы выставили как 10% от баланса по макс.падению
                                                                                        double Balance = 3000.0, Balance_Day = 3000.0;
                                                                                        for (int i = 0; i < cnt_closed_orders; i++)
                                                                                        {
                                                                                            residuals[i] = Math.Abs((TicksOut.Ticks_Sum_on_time[i] - (a * (i + 1) + b)));

                                                                                            if (i != 0) Balance_Day = Balance_Day + (TicksOut.Ticks_Sum_on_time[i] - TicksOut.Ticks_Sum_on_time[i - 1]) * Math.Min((0.10 * Balance_Day) / Math.Abs(StatInfo.MaxFALLbyDay), 100.0);
                                                                                            else Balance_Day = Balance_Day + (TicksOut.Ticks_Sum_on_time[i]) * (0.10 * Balance_Day) / Math.Abs(StatInfo.MaxFALLbyDay);

                                                                                            if (i != 0) Balance = Balance + (TicksOut.Ticks_Sum_on_time[i] - TicksOut.Ticks_Sum_on_time[i - 1]) * Math.Min((0.10 * Balance) / Math.Abs(StatInfo.MaxFALLbyOrder), 100.0);
                                                                                            else Balance = Balance + (TicksOut.Ticks_Sum_on_time[i]) * (0.10 * Balance) / Math.Abs(StatInfo.MaxFALLbyOrder);

                                                                                            //    helpFile.WriteLine((Counts_On_Close[i]).ToString() + " " + TicksOut.Date_on_time[i] + " " + TicksOut.flg_F_avg_6[i].ToString() + " " + TicksOut.Cur_Order_Profit[i].ToString() + " " + StatInfo.MaxLocalProfitInOrder[i].ToString() + " " + TicksOut.Ticks_Sum_on_time[i].ToString()
                                                                                            //+ " " + Convert.ToString(Balance).Split('.')[0] + " " + Convert.ToString(Balance_Day).Split('.')[0] + " " + Convert.ToString(StatInfo.MaxFALLbyOrder).Split('.')[0] + " " + Convert.ToString(StatInfo.MaxFALLbyDay).Split('.')[0]
                                                                                            //+ " " + cnt_closed_orders.ToString() + " " + (a * (i + 1) + b).ToString() + " " + residuals[i] + " " + a.ToString() + " " + StatInfo.residual_avg_MNK.ToString() + " " + StatInfo.residual_avg_MNM.ToString()
                                                                                            //+ " " + StatInfo.SELL_summ_slice.ToString() + " " + StatInfo.BUY_summ_slice.ToString()
                                                                                            //+ " " + (Math.Round(StatInfo.MaxFALLbyOrder / 1000.0, 1) * 1000.0).ToString());
                                                                                        }
                                                                                        //helpFile.Close();

                                                                                        //-------------------------

                                                                                        DateTime end_time = DateTime.Now;

                                                                                        Console.WriteLine((count_sell).ToString() + " " + (slice_sell / 100000.0).ToString() + " " + (slice_buy / 100000.0).ToString()
                                                                                            + " " + (t_in_sell / t_divide_by).ToString() + " " + (t_in_buy / t_divide_by).ToString()
                                                                                            + " " + max_ticks_activ_cnt.ToString() + " " + max_ticks_activ_cnt_buy.ToString()
                                                                                            + " " + atitude_pecents.ToString() 
                                                                                            + " " + (start_time - end_time).ToString()
                                                                                            + " " + (start_time - end_time_mid).ToString());

                                                                                        //if (ticks_sum * 100000.0 > 100.0 && a > 5.0 && Max_FALL1 > -500.0 && atitude_pecents < 55.0
                                                                                        if (ticks_sum * 100000.0 > 10.0 && a > 0.0
                                                                                                && cnt_closed_orders > 10 && StatInfo.MaxFALLbyOrder > -3000.0
                                                                                            //            //&& atitude_pecents < 55.0 && StatInfo.residual_avg_MNK < 1000.0
                                                                                            //    //&& (Balance - 3000.0) > 1000.0
                                                                                            )
                                                                                        {
                                                                                            ResultFile.WriteLine((count_sell).ToString() + " " + (count_buy).ToString() + " " + slice_sell.ToString() + " " + slice_buy.ToString() + " " + (t_in_sell / t_divide_by).ToString()
                                                                                                     + " " + (t_in_buy / t_divide_by).ToString() + " " + (ticks_sum * 100000.0).ToString()
                                                                                                     + " " + Convert.ToString(Balance).Split('.')[0] + " " + Convert.ToString(Balance_Day).Split('.')[0]
                                                                                                     + " " + Convert.ToString(StatInfo.MaxFALLbyOrder).Split('.')[0] + " " + Convert.ToString(StatInfo.MaxFALLbyDay).Split('.')[0] + " " + cnt_closed_orders.ToString() + " " + (atitude_pecents).ToString()
                                                                                                     + " " + max_ticks_activ_cnt.ToString() + " " + max_ticks_activ_cnt_buy.ToString()
                                                                                                     + " " + (sth_low / 10.0).ToString() + " " + (sth_high / 10.0).ToString() + " " + a.ToString()
                                                                                                     + " " + BigMassCnt + " " + VolBigMassSELL_negative + " " + VolBigMassSELL_positive + " " + VolBigMassBUY_positive + " " + VolBigMassBUY_negative
                                                                                                     + " " + (Math.Round(StatInfo.MaxFALLbyOrder / 1000.0, 1) * 1000.0).ToString()
                                                                                                     + " " + StatInfo.residual_avg_MNK.ToString() + " " + StatInfo.residual_avg_MNM.ToString()
                                                                                                     + " " + StatInfo.SELL_summ_slice.ToString() + " " + StatInfo.BUY_summ_slice.ToString()
                                                                                                     + " " + MaxCurProfitToClose_SELL.ToString() + " " + MaxCurProfitToClose_BUY.ToString()
                                                                                                     + " " + MaxCurStopLossToClose_SELL.ToString() + " " + MaxCurStopLossToClose_BUY.ToString()
                                                                                                     );
                                                                                        }


                                                                                        Last_MaxFALLbyOrder = StatInfo.MaxFALLbyOrder;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }

                }
            }

            ResultFile.Close();
        }
    }
}
