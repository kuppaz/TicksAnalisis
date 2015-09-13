using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticks_analysis
{
    public static class StrtStp
    {
        //public static void Read_Indicators(WorkClasses.Ticks_In TicksIn)
        //{
        //    if (TicksIn.With_indicators == true)
        //    {
        //        string[] dataArray = TicksIn.dataArray[9].Split(' ');
        //        TicksIn.StM5_733_0 = Convert.ToDouble(TicksIn.dataArray[7]);

        //        TicksIn.StM5_733_0 = Convert.ToDouble(dataArray[0]);
        //        TicksIn.StM5_733_1 = Convert.ToDouble(dataArray[1]);
        //        TicksIn.StM5_733_2 = Convert.ToDouble(dataArray[2]);
        //        TicksIn.StM5_733_3 = Convert.ToDouble(dataArray[3]);
        //        TicksIn.StM5_733_4 = Convert.ToDouble(dataArray[4]);

        //        TicksIn.Stohastic_Param = TicksIn.StM5_733_3;
        //    }
        //}

        public static int Start_and_Stop_FLG(WorkClasses.Ticks_In TicksIn, WorkClasses.Ticks_Out TicksOut, double SELL_SPREAD, double BUY_SPREAD, int cur_in_flg, int count_parameter_sell, int count_parameter_buy, double Slice_Criteria_IN_SELL, double Slice_Criteria_IN_BUY,
                            double Slice_Criteria_OUT_sell, double Slice_Criteria_OUT_buy, int max_ticks_activ_cnt_sell, int max_ticks_activ_cnt_buy, double Stohastic_SellLimit, double Stohastic_BuyLimit,
                             string mode, string mode_out, int MaxCurProfitToClose_BUY, int MaxCurProfitToClose_SELL, int MaxCurStopLossToClose_BUY, int MaxCurStopLossToClose_SELL,
                            double VolBigMassSELL_negative, double VolBigMassSELL_positiv, double VolBigMassBUY_positive, double VolBigMassBUY_negative, int BigMassCnt)
        {
            if (MaxCurProfitToClose_BUY == 0) MaxCurProfitToClose_BUY = 5000;
            if (MaxCurProfitToClose_SELL == 0) MaxCurProfitToClose_SELL = 5000;

            if (MaxCurStopLossToClose_BUY == 0) MaxCurStopLossToClose_BUY = -1000;
            if (MaxCurStopLossToClose_SELL == 0) MaxCurStopLossToClose_SELL = -1000;

            double[] cur_in_Array = new double[TicksIn.dim];
            for (int i = 0; i < TicksIn.dim; i++)
                cur_in_Array[i] = TicksOut.Ask_F_avg_6[i];


            if (TicksIn.Count == 9811215)
                TicksIn.Count = TicksIn.Count;

            if (BigMassCnt != 0)
            {
                for (int j = 0; j < BigMassCnt - 1; j++)
                    TicksIn.AskIncremBigMass[j] = TicksIn.AskIncremBigMass[j + 1];

                TicksIn.AskIncremBigMassSumm_Positive = TicksIn.AskIncremBigMassSumm_Negative = 0.0;
                TicksIn.AskIncremBigMass[BigMassCnt - 1] = TicksIn.AskIncrement[TicksIn.dim - 1];

                if (VolBigMassSELL_positiv == 0 && VolBigMassBUY_negative == 0)
                {
                    TicksIn.AskIncremBigMassSumm_Positive = TicksIn.AskIncremBigMass.Sum();
                    TicksIn.AskIncremBigMassSumm_Negative = TicksIn.AskIncremBigMass.Sum();
                }
                else
                {
                    for (int i = 0; i < BigMassCnt; i++)
                    {
                        if (TicksIn.AskIncremBigMass[i] >= 0) TicksIn.AskIncremBigMassSumm_Positive += TicksIn.AskIncremBigMass[i];
                        if (TicksIn.AskIncremBigMass[i] <= 0) TicksIn.AskIncremBigMassSumm_Negative += TicksIn.AskIncremBigMass[i];
                    }
                }

                TicksIn.AskIncremBigMassSumm_Positive *= 100000.0;
                TicksIn.AskIncremBigMassSumm_Negative *= 100000.0;

                //for (int j = 1; j <= BigMassCnt; j++)
                //{
                //    double[] temp = new double[j];
                //    for (int ij = 0; ij < j; ij++) temp[ij] = TicksIn.AskIncremBigMass[BigMassCnt - 1 - ij];

                //    if (Math.Abs(TicksIn.AskIncremBigMassSumm) < Math.Abs(temp.Sum() * 100000.0))
                //        TicksIn.AskIncremBigMassSumm = temp.Sum() * 100000.0;
                //}
            }



            if (cur_in_flg == 0)
            {
                int ii_minus = 0;
                int ii_plus = 0;
                for (int j = 0; j < count_parameter_buy; j++)
                {
                    if (cur_in_Array[TicksIn.dim - 1 - j] > Slice_Criteria_IN_BUY)
                    {
                        ii_plus++;
                    }
                }
                for (int j = 0; j < count_parameter_sell; j++)
                {
                    if (cur_in_Array[TicksIn.dim - 1 - j] < -Slice_Criteria_IN_SELL)
                    {
                        ii_minus++;
                    }
                }






                if ((
                        (ii_minus == count_parameter_sell && TicksOut.AskSpeed[TicksIn.dim - 1] * 100000.0 < 0.0 && TicksOut.Ask_F_avg_3[TicksIn.dim - 1] < 0.0)
                        || (TicksIn.AskIncremBigMassSumm_Negative < -VolBigMassSELL_negative && (TicksIn.AskIncremBigMassSumm_Positive <= VolBigMassSELL_positiv || VolBigMassSELL_positiv == 0) && BigMassCnt != 0)
                    )
                    && Math.Abs(TicksIn.AskIncrement[TicksIn.dim - 1]) > -0.00300
                    )
                {
                    if (TicksIn.Spread[TicksIn.dim - 1] < SELL_SPREAD)
                    {
                        if (TicksIn.Only_BUY_flg) { }
                        else
                        {
                            bool new_mode = false;
                            if (TicksIn.Stohastic_Param > Stohastic_SellLimit && mode == "old")
                                new_mode = true;
                            else if ((TicksIn.Stohastic_Param > Stohastic_SellLimit || (TicksIn.Stohastic_Param <= Stohastic_SellLimit && TicksIn.StM5_733_3 >= TicksIn.StM5_733_2)) && mode == "new")
                                //else if ((TicksIn.StM5_733_3 >= TicksIn.StM5_733_2) && mode == "new")
                                new_mode = true;
                            else new_mode = false;

                            if (new_mode == true)
                            {
                                cur_in_flg = -1;
                                TicksOut.OpenOrderFLG = 1;
                                TicksOut.OpenASK = TicksIn.Ask[TicksIn.dim - 1];
                                TicksOut.OpenBID = TicksIn.Bid[TicksIn.dim - 1];
                            }
                        }
                    }
                    else
                        ii_minus = ii_minus;
                }
                else if ((
                            (ii_plus == count_parameter_buy && TicksOut.AskSpeed[TicksIn.dim - 1] * 100000.0 > 0.0 && TicksOut.Ask_F_avg_3[TicksIn.dim - 1] > 0.0)
                            || (TicksIn.AskIncremBigMassSumm_Positive > VolBigMassBUY_positive && (TicksIn.AskIncremBigMassSumm_Negative >= -VolBigMassBUY_negative || VolBigMassBUY_negative == 0) && BigMassCnt != 0)
                         )
                        //&& Math.Abs(TicksIn.AskIncrement[TicksIn.dim - 1]) < 0.00300
                        && TicksOut.AskSpeed[TicksIn.dim - 1] * 100000.0 < 380.0
                        )
                {
                    if (TicksIn.Spread[TicksIn.dim - 1] < BUY_SPREAD)
                    {
                        if (TicksIn.Only_SELL_flg) { }
                        else
                        {
                            bool new_mode = false;
                            if (TicksIn.Stohastic_Param < Stohastic_BuyLimit && mode == "old")
                                new_mode = true;
                            else if ((TicksIn.Stohastic_Param < Stohastic_BuyLimit || (TicksIn.Stohastic_Param >= Stohastic_BuyLimit && TicksIn.StM5_733_3 >= TicksIn.StM5_733_2)) && mode == "new")
                                //else if ((TicksIn.StM5_733_3 >= TicksIn.StM5_733_2) && mode == "new")
                                new_mode = true;
                            else new_mode = false;

                            if (new_mode == true)
                            {
                                cur_in_flg = 1;
                                TicksOut.OpenOrderFLG = 1;
                                TicksOut.OpenASK = TicksIn.Ask[TicksIn.dim - 1];
                                TicksOut.OpenBID = TicksIn.Bid[TicksIn.dim - 1];
                            }
                        }
                    }
                    else
                        ii_minus = ii_minus;
                }
            }
            else if (Math.Abs(cur_in_flg) != 2)
            {
                TicksOut.OpenOrderFLG = 0;

                //if  (TicksIn.flag_ByCycle == false)
                //    Read_Indicators(TicksIn);

                if (cur_in_flg == -1 && TicksIn.AskIncrement[TicksIn.dim - 1] > Slice_Criteria_OUT_sell) 
                    TicksOut.MoreThanSlice++;
                if (cur_in_flg == 1 && TicksIn.AskIncrement[TicksIn.dim - 1] < -Slice_Criteria_OUT_sell) 
                    TicksOut.MoreThanSlice++;

                if (TicksIn.Count == 10458330)
                    TicksIn.Count = TicksIn.Count;
                //if (TicksIn.AskIncrement[TicksIn.dim - 1] > Slice_Criteria_OUT_sell)
                //    TicksIn.Count = TicksIn.Count;

                if (cur_in_flg == -1 && (
                                               (TicksIn.AskIncrement[TicksIn.dim - 1] > Slice_Criteria_OUT_sell && (TicksIn.StM5_733_2 > TicksIn.StM5_733_1 - TicksIn.SellCustomSthCorrection || mode_out == "old" 
                                                        /*|| TicksIn.AskIncrement[TicksIn.dim - 1] > 0.00040*/))
                                            || (TicksOut.ticks_activ_cnt > max_ticks_activ_cnt_sell && max_ticks_activ_cnt_sell != 0)
                                            || ((TicksOut.local_Open_vs_Cur) * 100000 >= Convert.ToDouble(MaxCurProfitToClose_SELL))
                                            || ((TicksOut.local_Open_vs_Cur) * 100000 <= Convert.ToDouble(MaxCurStopLossToClose_SELL))
                                            //|| (TicksIn.StM1_733_0[0] < 10.0 && (TicksIn.StM1_733_0[0] - TicksIn.StM1_733_0[TicksIn.StM1_733_0.Length - 1]) >= 7.0 && TicksIn.StM5_733_1 <= 40.0 && TicksIn.StM5_733_2 < TicksIn.StM5_733_1)
                                        )
                                    && TicksIn.Spread[TicksIn.dim - 1] < 30.0
                                    //&& TicksOut.ticks_activ_cnt > 3 //Не проканало. Можно попробовать ввести еще проверку на состояние по индикаторам, т.к. при "3" есть ордер, который выходит в +240, хотя до этого в "-"
                    )
                {
                    cur_in_flg = -2;
                    TicksOut.MoreThanSlice = 0;

                    if (TicksIn.Spread[TicksIn.dim - 1] >= 20.0)
                        TicksIn.Spread = TicksIn.Spread;
                }

                if (cur_in_flg == 1 && (
                                              //(TicksIn.AskIncrement[TicksIn.dim - 1] < -Slice_Criteria_OUT && (TicksIn.StM5_733_2 < TicksIn.StM5_733_1 || mode_out == "old" || TicksIn.AskIncrement[TicksIn.dim - 1] < -0.00040)) 
                                              //(TicksIn.AskIncrement[TicksIn.dim - 1] < -Slice_Criteria_OUT && (TicksIn.StM5_733_3 > TicksIn.StM5_733_2 || mode_out == "old" || TicksIn.AskIncrement[TicksIn.dim - 1] < -0.00040)) 
                                              //(TicksIn.AskIncrement[TicksIn.dim - 1] < -Slice_Criteria_OUT || (TicksIn.StM5_733_0 < TicksIn.StM5_733_1 - 1 && (TicksIn.Bid[TicksIn.dim - 1] - TicksOut.OpenASK) * 100000 < 0.0 && TicksIn.AskIncrement[TicksIn.dim - 1] < -0.00008))
                                              (TicksIn.AskIncrement[TicksIn.dim - 1] < -Slice_Criteria_OUT_buy)
                                           || (TicksOut.ticks_activ_cnt > max_ticks_activ_cnt_buy && max_ticks_activ_cnt_buy != 0)
                                           || ((TicksOut.local_Open_vs_Cur) * 100000 >= Convert.ToDouble(MaxCurProfitToClose_BUY))
                                           || ((TicksOut.local_Open_vs_Cur) * 100000 <= Convert.ToDouble(MaxCurStopLossToClose_BUY))
                                           //|| (TicksIn.StM1_733_0[0] > 89.0 && (TicksIn.StM1_733_0[TicksIn.StM1_733_0.Length - 1] - TicksIn.StM1_733_0[0]) >= 7.0 && TicksIn.StM5_733_1 >= 60.0)
                                           //?|| (TicksIn.StM1_733_0[0] > 89.0 && (TicksIn.StM1_733_0.Max()- TicksIn.StM1_733_0.Min()) >= 7.0 && TicksIn.StM5_733_1 >= 60.0)
                                       )
                                    && TicksIn.Spread[TicksIn.dim - 1] < 30.0
                                    //&& TicksOut.ticks_activ_cnt > 3 //Не проканало. Можно попробовать ввести еще проверку на состояние по индикаторам, т.к. при "3" есть ордер, который выходит в +240, хотя до этого в "-"
                   )
                {
                    cur_in_flg = 2;
                    TicksOut.MoreThanSlice = 0;

                    if (TicksIn.Spread[TicksIn.dim - 1] >= 20.0)
                        TicksIn.Spread = TicksIn.Spread;
                }
            }

            return cur_in_flg;
        }







        public static int Start_and_Stop_FLG_13_09(WorkClasses.Ticks_In TicksIn, WorkClasses.Ticks_Out TicksOut, int cur_in_flg, int count_parameter, double[] cur_in_Array, double Slice_Criteria_IN_SELL, double Slice_Criteria_IN_BUY,
                            double Slice_Criteria_OUT, int max_ticks_activ_cnt, double Stohastic_SellLimit, double Stohastic_BuyLimit, string mode, string mode_out)
        {
            if (cur_in_flg == 0)
            {
                int ii_minus = 0;
                int ii_plus = 0;
                for (int j = 0; j < count_parameter; j++)
                {
                    if (cur_in_Array[TicksIn.dim - 1 - j] < -Slice_Criteria_IN_SELL)
                    {
                        ii_minus++;
                    }
                    if (cur_in_Array[TicksIn.dim - 1 - j] > Slice_Criteria_IN_BUY)
                    {
                        ii_plus++;
                    }
                }
                if (ii_minus == count_parameter && TicksOut.AskSpeed[TicksIn.dim - 1] * 100000.0 < 0.0 && TicksOut.Ask_F_avg_3[TicksIn.dim - 1] < 0.0)
                {
                    if (TicksIn.Only_BUY_flg) { }
                    else
                    {
                        bool new_mode = false;
                        if (TicksIn.Stohastic_Param > Stohastic_SellLimit && mode == "old")
                            new_mode = true;
                        else if ((TicksIn.Stohastic_Param > Stohastic_SellLimit || (TicksIn.Stohastic_Param <= Stohastic_SellLimit && TicksIn.StM5_733_3 >= TicksIn.StM5_733_2)) && mode == "new")
                            //else if ((TicksIn.StM5_733_3 >= TicksIn.StM5_733_2) && mode == "new")
                            new_mode = true;
                        else new_mode = false;

                        if (new_mode == true)
                        {
                            cur_in_flg = -1;
                            TicksOut.OpenOrderFLG = 1;
                            TicksOut.OpenASK = TicksIn.Ask[TicksIn.dim - 1];
                            TicksOut.OpenBID = TicksIn.Bid[TicksIn.dim - 1];
                        }
                    }
                }
                else if (ii_plus == count_parameter && TicksOut.AskSpeed[TicksIn.dim - 1] * 100000.0 > 0.0 && TicksOut.Ask_F_avg_3[TicksIn.dim - 1] > 0.0)
                {
                    if (TicksIn.Only_SELL_flg) { }
                    else
                    {
                        bool new_mode = false;
                        if (TicksIn.Stohastic_Param < Stohastic_BuyLimit && mode == "old")
                            new_mode = true;
                        else if ((TicksIn.Stohastic_Param < Stohastic_BuyLimit || (TicksIn.Stohastic_Param >= Stohastic_BuyLimit && TicksIn.StM5_733_3 >= TicksIn.StM5_733_2)) && mode == "new")
                            //else if ((TicksIn.StM5_733_3 >= TicksIn.StM5_733_2) && mode == "new")
                            new_mode = true;
                        else new_mode = false;

                        if (new_mode == true)
                        {
                            cur_in_flg = 1;
                            TicksOut.OpenOrderFLG = 1;
                            TicksOut.OpenASK = TicksIn.Ask[TicksIn.dim - 1];
                            TicksOut.OpenBID = TicksIn.Bid[TicksIn.dim - 1];
                        }
                    }
                }
            }
            else if (Math.Abs(cur_in_flg) != 2)
            {
                TicksOut.OpenOrderFLG = 0;

                if (cur_in_flg == -1 && TicksIn.AskIncrement[TicksIn.dim - 1] > Slice_Criteria_OUT)
                    TicksOut.MoreThanSlice++;
                if (cur_in_flg == 1 && TicksIn.AskIncrement[TicksIn.dim - 1] < -Slice_Criteria_OUT)
                    TicksOut.MoreThanSlice++;

                if (TicksIn.Count >= 9815038)
                    TicksIn.Count = TicksIn.Count;

                if (cur_in_flg == -1 && (
                                               (TicksIn.AskIncrement[TicksIn.dim - 1] > Slice_Criteria_OUT && (TicksIn.StM5_733_2 > TicksIn.StM5_733_1 || mode_out == "old" || TicksIn.AskIncrement[TicksIn.dim - 1] > 0.00040))
                                            || (TicksOut.ticks_activ_cnt > max_ticks_activ_cnt && max_ticks_activ_cnt != 0)
                                        ))
                {
                    cur_in_flg = -2;
                    TicksOut.MoreThanSlice = 0;
                }

                if (cur_in_flg == 1 && (
                    //(TicksIn.AskIncrement[TicksIn.dim - 1] < -Slice_Criteria_OUT && (TicksIn.StM5_733_2 < TicksIn.StM5_733_1 || mode_out == "old" || TicksIn.AskIncrement[TicksIn.dim - 1] < -0.00040)) 
                    //(TicksIn.AskIncrement[TicksIn.dim - 1] < -Slice_Criteria_OUT && (TicksIn.StM5_733_3 > TicksIn.StM5_733_2 || mode_out == "old" || TicksIn.AskIncrement[TicksIn.dim - 1] < -0.00040)) 
                    //(TicksIn.AskIncrement[TicksIn.dim - 1] < -Slice_Criteria_OUT || (TicksIn.StM5_733_0 < TicksIn.StM5_733_1 - 1 && (TicksIn.Bid[TicksIn.dim - 1] - TicksOut.OpenASK) * 100000 < 0.0 && TicksIn.AskIncrement[TicksIn.dim - 1] < -0.00008))
                                              (TicksIn.AskIncrement[TicksIn.dim - 1] < -Slice_Criteria_OUT)
                                           || (TicksOut.ticks_activ_cnt > max_ticks_activ_cnt && max_ticks_activ_cnt != 0)
                                       ))
                {
                    cur_in_flg = 2;
                    TicksOut.MoreThanSlice = 0;
                }
            }

            return cur_in_flg;
        }
    }
}








//private int Start_and_Stop_Trade_FLG(Ticks_In TicksIn, Ticks_Out TicksOut, int cur_in_flg, int count_parameter, double[] cur_in_Array, double Slice_Criteria_IN, double[] Slice_Array,
//                    double Slice_Criteria_OUT, double local_Open_vs_Cur, int flg_was_local_min)
//{
//    if (cur_in_flg == 0 || cur_in_flg == -2 || cur_in_flg == 2)
//    {
//        int ii_minus = 0;
//        int ii_plus = 0;
//        for (int j = 0; j < count_parameter; j++)
//        {
//            if (cur_in_Array[TicksIn.dim - 1 - j] < -Slice_Criteria_IN)
//            {
//                ii_minus++;
//            }
//            if (cur_in_Array[TicksIn.dim - 1 - j] > Slice_Criteria_IN)
//            {
//                ii_plus++;
//            }
//        }
//        if (ii_minus == count_parameter && TicksOut.AskSpeed[TicksIn.dim - 1] < 0.0 && TicksOut.Ask_F_avg_3[TicksIn.dim - 1] < 0.0)
//        {
//            cur_in_flg = -1;
//            TicksOut.OpenOrderFLG = 1;
//            TicksOut.OpenASK = TicksIn.Ask[TicksIn.dim - 1];
//            TicksOut.OpenBID = TicksIn.Bid[TicksIn.dim - 1];
//        }
//        else if (ii_plus == count_parameter && TicksOut.AskSpeed[TicksIn.dim - 1] > 0.0 && TicksOut.Ask_F_avg_3[TicksIn.dim - 1] > 0.0)
//        {
//            cur_in_flg = 1;
//            TicksOut.OpenOrderFLG = 1;
//            TicksOut.OpenASK = TicksIn.Ask[TicksIn.dim - 1];
//            TicksOut.OpenBID = TicksIn.Bid[TicksIn.dim - 1];
//        }
//        else
//            cur_in_flg = 0;
//    }
//    else
//    {
//        TicksOut.OpenOrderFLG = 0;
//        //блин, сейчас критерий выхода только по абсолютному приращению, надо бы еще индикатор осреднения добавить?
//        //if (cur_in_flg == -1 && (Slice_Array[TicksIn.dim - 1] > Slice_Criteria_OUT || (local_Open_vs_Cur >= 0.00150 && flg_was_local_min == 1) ))
//        if (cur_in_flg == -1 && Slice_Array[TicksIn.dim - 1] > Slice_Criteria_OUT)
//            cur_in_flg = -2;

//        //if (cur_in_flg == 1 && (Slice_Array[TicksIn.dim - 1] < -Slice_Criteria_OUT || (local_Open_vs_Cur >= 0.00150 && flg_was_local_min == 1)))
//        if (cur_in_flg == 1 && Slice_Array[TicksIn.dim - 1] < -Slice_Criteria_OUT)
//            cur_in_flg = 2;
//    }

//    return cur_in_flg;
//}