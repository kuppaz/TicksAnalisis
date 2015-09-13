using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticks_analysis
{
    public static class WorkClasses
    {
        public static int DIM = 15;

        public static string dir_str = "D://Ticks", base_file_name = "";
        public static string gaps_str = " 39500 82640 91642 116417 161781 195793 290030 441569 481689 523403 692348 742611 787949 797451 801662 851037 1000546 1043122 1267699 1311487 1422669 1478488 1532326 2743131 3506370 5969719 6468630 7623097 8166941";


        public partial class Ticks_In
        {
            public int dim = DIM;
            public int Count = 0;
            public string CurTime, Date;
            public double[] timeStep = new double[DIM];
            public double[] Ask = new double[DIM];
            public double[] Bid = new double[DIM];
            public double[] AskIncrement = new double[DIM];
            public double[] BidIncrement = new double[DIM];
            public double[] Spread = new double[DIM];

            public double AskIncremBigMassSumm_Positive, AskIncremBigMassSumm_Negative;
            public double[] AskIncremBigMass = new double[50];

            public string[] dataArray, dataArrayFor2, dataArray_1;

            public bool Only_SELL_flg = false, Only_BUY_flg = false, With_indicators = false, flag_ByCycle = false;

            public double Stohastic_Param = 50.0;
            public double StM5_733_0, StM5_733_1, StM5_733_2, StM5_733_3, StM5_733_4;
            public double StM5_734_0, StM5_734_1, StM5_734_2, StM5_734_3, StM5_734_4;
            public double StM15_733_0, StM15_733_1, StM15_733_2, StM15_733_3, StM15_733_4;
            public double StM15_734_0, StM15_734_1, StM15_734_2, StM15_734_3, StM15_734_4;

            public double StM5_532_0, StM5_532_1, StM5_532_2, StM5_532_3, StM5_532_4;

            public double[] StM1_733_0 = new double[80];
            public double StM1_733_1, StM1_733_2, StM1_733_3, StM1_733_4;

            public int DifOpenClose_0, DifOpenClose_1, DifOpenClose_2, DifOpenClose_3, DifOpenClose_4;
            public double StdDevM5_40_0, StdDevM5_40_1, StdDevM5_40_2, StdDevM5_40_3, StdDevM5_40_4;
            public double iBullsM5_7_1, iBullsM5_7_2, iBullsM5_7_3, iBullsM5_7_4;
            public double iBearM5_7_1, iBearM5_7_2, iBearM5_7_3, iBearM5_7_4;
            public double SellCustomSthCorrection;
        }

        public partial class Ticks_Out
        {
            public int dim = DIM, ticks_activ_cnt = 0;
            public double[] AskSpeed = new double[DIM];
            public double[] Ask_V_3 = new double[DIM], Ask_V_6 = new double[DIM];
            public double[] Ask_V_avg_3 = new double[DIM], Ask_V_avg_4 = new double[DIM], Ask_V_avg_5 = new double[DIM], Ask_V_avg_6 = new double[DIM], Ask_V_avg_7 = new double[DIM], Ask_V_avg_8 = new double[DIM];
            public double[] Ask_F_avg_3 = new double[DIM], Ask_F_avg_4 = new double[DIM], Ask_F_avg_5 = new double[DIM], Ask_F_avg_6 = new double[DIM], Ask_F_avg_7 = new double[DIM], Ask_F_avg_8 = new double[DIM];

            public int OpenOrderFLG, MoreThanSlice = 0;
            public int Flag_Anti=0;
            public double OpenASK, OpenBID;

            public double local_Open_vs_Cur = 0.0;

            public double[] Ticks_Sum_on_time = new double[50000];
            public double[] Cur_Order_Profit = new double[50000];
            public int[] flg_F_avg_6 = new int[50000];
            public string[] Date_on_time = new string[50000];

            public bool flg_needed_local_sum = false;

        }

        public partial class Stat_Info
        {
            public double[] MaxLocalProfitInOrder = new double[50000], MinLocalProfitInOrder = new double[50000], StartSpread = new double[50000], CloseSpread = new double[50000];
            public double[] AskIncremBigMassSummNegative = new double[50000], AskIncremBigMassSummPositive = new double[50000];
            public double[] AskSpeedOpen = new double[50000], Ask_F_avg_3_Open = new double[50000];

            public double residual_avg_MNK = 0.0, residual_avg_MNM = 0.0;

            public double SELL_summ_slice = 0.0, BUY_summ_slice = 0.0;

            public double MaxFALLbyOrder = 0.0, MaxFALLbyDay = 0.0;
            public double OpenStdDevM5_40_1 = 0.0;

        }
    }
}
