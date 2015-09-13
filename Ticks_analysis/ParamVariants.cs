using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticks_analysis;

namespace Ticks_analysis
{
    public static class ParamVariants
    {
        public static int ChouseTheSet(int flg_F_avg_6, WorkClasses.Ticks_In TicksIn, WorkClasses.Ticks_Out TicksOut, int BaseCountToSkip, bool only_full_part)
        {
            //---FullScan + Limit + Ind-----------------------------------------------------------------------------------------------------------------------------
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 20, flg_F_avg_6, 9, 9, 4.37, 4.37, 0.00011, 0.00011, 450, 450, 22, 89, "new", "new", 0, 0, 0, 0, 0); //3744	11195	12855	-267	-239	88
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 20, flg_F_avg_6, 9, 9, 4.65, 4.65, 0.00009, 0.00009, 450, 450, 22, 89, "new", "new", 0, 0, 0, 0, 0, 0, 0); //3811	20743	22277	-181	-174	73
            //окейный медленный
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 20, flg_F_avg_6, 11, 11, 3.44, 3.44, 0.00012, 0.00012, 0, 0, 22, 99, "old", "old", 0, 0, 0, 0, 0); //2695	10112	10112	-208	-208	85
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 20, flg_F_avg_6, 11, 11, 4.30, 4.30, 0.00010, 0.00012, 0, 0, 22, 89, "new", "new", 0, 0, 0, 0, 0, 0, 0, 0, 0); //(!!!!!)3163	143741	143741	-64	-64	39
            //*
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 20, flg_F_avg_6, 11, 11, 3.45, 3.45, 0.00009, 0.00009, 2310, 2310, 22, 89, "new", "new", 0, 0, 0, 0, 0); //3158	9931	16134	-247	-172	82
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 20, flg_F_avg_6, 11, 11, 3.75, 3.75, 0.00009, 0.00009, 2310, 2310, 22, 89, "new", "new", 0, 0, 0, 0, 0); //3240	20937	57748	-151	-95	67
            //Only SELL +IND-------------------------------------------------------------------------------------------------------------------------------------
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 20, flg_F_avg_6, 10, 10, 3.00, 3.00, 0.00014, 0.00014, 770, 770, 26, 97, "new", "new", 0, 0, 0, 0, 0,0,0); //4564	10030	12372	-362	-306	65
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 20, flg_F_avg_6, 10, 10, 3.03, 3.03, 0.00015, 0.00015, 2000, 2000, 22, 99, "old", "old", 0, 0, 0, 0, 0,0,0); //3249	13419	13419	-202	-202	61


            /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
            /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 20, flg_F_avg_6, 10, 10, 3.00, 4.52, 0.00014, 0.00012, 725, 250, 30, 97, "new", "new", 350, 620, 300.0, 0, 225.0, 0, 9); //8161	15028	19976	-490	-414	123
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 20, flg_F_avg_6, 10, 9, 3.08, 4.68, 0.00013, 0.00013, 725, 250, 30, 97, "new", "new", 350, 620, 300.0, 225.0, 9); //7746	14891	14891	-467	-467	124
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 20, flg_F_avg_6, 10, 10, 3.00, 4.62, 0.00013, 0.00012, 725, 250, 30, 97, "new", "new", 350, 620, 300.0, 225.0, 9); //7684	25946	46598	-341	-265	122
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 20, flg_F_avg_6, 10, 10, 3.00, 4.72, 0.00014, 0.00012, 725, 250, 30, 97, "new", "new", 350, 620, 300.0, 225.0, 9); //8169	23342	37884	-382	-306	119

            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 18, flg_F_avg_6, 11, 10, 3.59, 4.72, 13E-5, 11E-5, 725, 275, 30, 94, "new", "new", 350, 350, 300.0, 225.0, 9); //5569	38520	84001	-203	-151	86  (Хорош после 09.2013)


            //---Можно изменить правило выхода на: "TicksIn.StM5_733_2 > TicksIn.StM5_733_1 - 5.0"---//
            TicksIn.SellCustomSthCorrection = 0.0;
            TicksIn.SellCustomSthCorrection = 5.0;

            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 10, 3.09, 4.70, 13E-5, 11E-5, 800, 275, 30, 94, "new", "new", 350, 350, 0, 0, 0, 0, 0); //5049	11017	11017	-375	-375	96
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 10, 3.09, 4.70, 13E-5, 11E-5, 800, 275, 30, 94, "new", "new", 350, 350, 200, 60, 125, 30, 6); //6892	21333	21333	-333	-333

            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 11, 3.09, 4.34, 13E-5, 12E-5, 775, 375, 30, 94, "new", "new", 350, 350, 240, 0, 180, 0, 7); //7089	30053	30053	-292	-292
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 11, 3.09, 4.78, 13E-5, 12E-5, 775, 275, 30, 94, "new", "new", 350, 350, 240, 0, 180, 0, 7); //6970	28949	28949	-292	-292	124

            //flg_F_avg_6 = StrtStp.Start_and_StopFLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 10, 3.09, 4.70, 13E-5, 11E-5, 800, 275, 30, 94, "new", "new", 350, 350, 350, 0, 180, 0, 6); //7455	30433	72837	-307	-219	127
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 10, 3.09, 4.70, 13E-5, 11E-5, 800, 275, 30, 94, "new", "new", 350, 350, 220, 0, 180, 0, 6); //7443	18892	18892	-388	-388	139
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 10, 3.15, 4.85, 13E-5, 11E-5, 800, 275, 30, 94, "new", "new", 350, 350, -0, -0, 220, 0, 180, 0, 6); //(!)7281	82381	82381	-204	-204	130
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 10, 3.50, 3.90, 11E-5, 11E-5, 800, 275, 30, 94, "new", "new", 350, 350, 220, 0, 180, 0, 6); //5082	22147	36993	-234	-182	160


            //===После перевода на ECN 22.07.14===
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 0, flg_F_avg_6, 10, 0, 4.20, 0.0, 17E-5, 0E-5, 0, 0, 22, 82, "new", "new", 0, 300, -0, -0, 0, 0, 0, 0, 0); //(!)3334	18833	59525	-166	-96	39
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 0, 5.60, 0.0, 10E-5, 0E-5, 1200, 0, 30, 94, "new", "new", 0, 350, 0, 0, 0, 0, 0); //714	8863	10290	-57	-49	28 (но нет)


            //===НОВАЯ ИСТОРИЯ 2015 ===//
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 25, 21, flg_F_avg_6, 12, 9, 5.5, 4.5, 29E-5, 28E-5, 0, 0, 19, 96, "new", "new", 0, 300, -0, -600, /**/ 0, 0, 0, 0, 0); //5796	5967	7367	-809	-612
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 25, 21, flg_F_avg_6, 12, 9, 7.1, 4.5, 27E-5, 28E-5, 0, 0, 19, 86, "new", "new", 0, 150, -0, -0, /**/ 0, 0, 0, 0, 0); //5668	9092	24908	-494	-251
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 25, 21, flg_F_avg_6, 12, 9, 7.0, 4.5, 29E-5, 28E-5, 0, 0, 19, 96, "new", "new", 0, 150, -0, -500, /**/ 0, 0, 0, 0, 0); //5213	6230	10630	-691	-389
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 25, 21, flg_F_avg_6, 12, 9, 7.1, 4.5, 29E-5, 28E-5, 0, 0, 19, 86, "new", "new", 0, 150, -0, -450, /**/ 0, 0, 0, 0, 0); //5030	7887	13930	-494	-300
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 25, 21, flg_F_avg_6, 12, 9, 7.5, 4.5, 27E-5, 28E-5, 0, 0, 19, 86, "new", "new", 0, 200, -0, -0, /**/ 0, 0, 0, 0, 0); //4537	8243	15959	-428	-251
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 25, 21, flg_F_avg_6, 13, 9, 6.50, 4.5, 29E-5, 28E-5, 0, 0, 19, 96, "new", "new", 0, 200, -0, -500, /**/ 0, 0, 0, 0, 0); //4392	6944	6944	-502	-502 хорош в конце июля 2015
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 9, 9.50, 4.5, 28E-5, 28E-5, 0, 0, 19, 96, "new", "new", 150, 120, -0, -350, /**/100, 300, 100, 300, 9); //4300	4777	5684	-888	-636 bigmass
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 9, 9.50, 4.5, 28E-5, 28E-5, 0, 0, 19, 96, "new", "new", 150, 120, -0, -350, /**/ 0, 0, 0, 0, 0); //3748	4380	4982	-950	-698


            flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 9, 9.50, 4.5, 28E-5, 28E-5, 0, 0, 19, 96, "new", "new", 150, 120, -0, -350, /**/ 0, 0, 0, 0, 0);



            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 9, 9.50, 4.5, 28E-5, 28E-5, 0, 0, 19, 96, "new", "new", 150, 120, -0, -350, /**/ 0, 0, 0, 0, 0); //На этом можно прогонять подбор параметров определения трендов
            //flg_F_avg_6 = StrtStp.Start_and_Stop_FLG(TicksIn, TicksOut, 35, 21, flg_F_avg_6, 10, 11, 9.50, 6.5, 28E-5, 30E-5, 0, 0, 19, 96, "new", "new", 200, 120, -0, -350, /**/ 0, 0, 0, 0, 0); //Buy+Sell 6812	4614	14773	-1536	-374	123

            if (TicksIn.Count == 1 || (TicksIn.Count == BaseCountToSkip+1 && only_full_part == true))
                TicksIn.Count = TicksIn.Count;



            return flg_F_avg_6;
        }
    }
}
