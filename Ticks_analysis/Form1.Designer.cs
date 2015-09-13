namespace Ticks_analysis
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AppendToBaseFile = new System.Windows.Forms.Button();
            this.StartAnalyse = new System.Windows.Forms.Button();
            this.test_cycle = new System.Windows.Forms.Button();
            this.only_full_part = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.With_indicators = new System.Windows.Forms.CheckBox();
            this.Only_SELL = new System.Windows.Forms.CheckBox();
            this.Only_BUY = new System.Windows.Forms.CheckBox();
            this.Ind_Local_file = new System.Windows.Forms.CheckBox();
            this.DoShortFile = new System.Windows.Forms.Button();
            this.UseShortFile = new System.Windows.Forms.CheckBox();
            this.ReadInMemory = new System.Windows.Forms.Button();
            this.interval_201304_201308 = new System.Windows.Forms.CheckBox();
            this.interval_201309_201311 = new System.Windows.Forms.CheckBox();
            this.interval_201312_201402 = new System.Windows.Forms.CheckBox();
            this.interval_ECN_since_22_07_14 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // AppendToBaseFile
            // 
            this.AppendToBaseFile.Location = new System.Drawing.Point(34, 107);
            this.AppendToBaseFile.Name = "AppendToBaseFile";
            this.AppendToBaseFile.Size = new System.Drawing.Size(103, 43);
            this.AppendToBaseFile.TabIndex = 2;
            this.AppendToBaseFile.Text = "Append to Base";
            this.AppendToBaseFile.UseVisualStyleBackColor = true;
            this.AppendToBaseFile.Click += new System.EventHandler(this.AppendToBaseFile_Click);
            // 
            // StartAnalyse
            // 
            this.StartAnalyse.Location = new System.Drawing.Point(164, 73);
            this.StartAnalyse.Name = "StartAnalyse";
            this.StartAnalyse.Size = new System.Drawing.Size(144, 65);
            this.StartAnalyse.TabIndex = 1;
            this.StartAnalyse.Text = "Start Analyse";
            this.StartAnalyse.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.StartAnalyse.UseVisualStyleBackColor = true;
            this.StartAnalyse.Click += new System.EventHandler(this.StartAnalyse_Click);
            // 
            // test_cycle
            // 
            this.test_cycle.Location = new System.Drawing.Point(34, 61);
            this.test_cycle.Name = "test_cycle";
            this.test_cycle.Size = new System.Drawing.Size(103, 40);
            this.test_cycle.TabIndex = 4;
            this.test_cycle.Text = "Analyse by Cycle";
            this.test_cycle.UseVisualStyleBackColor = true;
            this.test_cycle.Click += new System.EventHandler(this.test_cycle_Click);
            // 
            // only_full_part
            // 
            this.only_full_part.AutoSize = true;
            this.only_full_part.Location = new System.Drawing.Point(329, 121);
            this.only_full_part.Name = "only_full_part";
            this.only_full_part.Size = new System.Drawing.Size(83, 17);
            this.only_full_part.TabIndex = 5;
            this.only_full_part.Text = "last 2 month";
            this.only_full_part.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(329, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // With_indicators
            // 
            this.With_indicators.AutoSize = true;
            this.With_indicators.Checked = true;
            this.With_indicators.CheckState = System.Windows.Forms.CheckState.Checked;
            this.With_indicators.Location = new System.Drawing.Point(329, 75);
            this.With_indicators.Name = "With_indicators";
            this.With_indicators.Size = new System.Drawing.Size(96, 17);
            this.With_indicators.TabIndex = 7;
            this.With_indicators.Text = "With indicators";
            this.With_indicators.UseVisualStyleBackColor = true;
            // 
            // Only_SELL
            // 
            this.Only_SELL.AutoSize = true;
            this.Only_SELL.Location = new System.Drawing.Point(201, 169);
            this.Only_SELL.Name = "Only_SELL";
            this.Only_SELL.Size = new System.Drawing.Size(74, 17);
            this.Only_SELL.TabIndex = 8;
            this.Only_SELL.Text = "only SELL";
            this.Only_SELL.UseVisualStyleBackColor = true;
            // 
            // Only_BUY
            // 
            this.Only_BUY.AutoSize = true;
            this.Only_BUY.Location = new System.Drawing.Point(201, 192);
            this.Only_BUY.Name = "Only_BUY";
            this.Only_BUY.Size = new System.Drawing.Size(70, 17);
            this.Only_BUY.TabIndex = 9;
            this.Only_BUY.Text = "only BUY";
            this.Only_BUY.UseVisualStyleBackColor = true;
            // 
            // Ind_Local_file
            // 
            this.Ind_Local_file.AutoSize = true;
            this.Ind_Local_file.Checked = true;
            this.Ind_Local_file.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Ind_Local_file.Location = new System.Drawing.Point(329, 35);
            this.Ind_Local_file.Name = "Ind_Local_file";
            this.Ind_Local_file.Size = new System.Drawing.Size(73, 17);
            this.Ind_Local_file.TabIndex = 10;
            this.Ind_Local_file.Text = "Ind_Local";
            this.Ind_Local_file.UseVisualStyleBackColor = true;
            // 
            // DoShortFile
            // 
            this.DoShortFile.Location = new System.Drawing.Point(327, 192);
            this.DoShortFile.Name = "DoShortFile";
            this.DoShortFile.Size = new System.Drawing.Size(75, 23);
            this.DoShortFile.TabIndex = 11;
            this.DoShortFile.Text = "DoShort File";
            this.DoShortFile.UseVisualStyleBackColor = true;
            this.DoShortFile.Click += new System.EventHandler(this.DoShortFile_Click);
            // 
            // UseShortFile
            // 
            this.UseShortFile.AutoSize = true;
            this.UseShortFile.Location = new System.Drawing.Point(329, 98);
            this.UseShortFile.Name = "UseShortFile";
            this.UseShortFile.Size = new System.Drawing.Size(123, 17);
            this.UseShortFile.TabIndex = 12;
            this.UseShortFile.TabStop = false;
            this.UseShortFile.Text = "Use Short File (STD)";
            this.UseShortFile.UseVisualStyleBackColor = true;
            // 
            // ReadInMemory
            // 
            this.ReadInMemory.Location = new System.Drawing.Point(182, 29);
            this.ReadInMemory.Name = "ReadInMemory";
            this.ReadInMemory.Size = new System.Drawing.Size(107, 23);
            this.ReadInMemory.TabIndex = 13;
            this.ReadInMemory.Text = "Read in memory";
            this.ReadInMemory.UseVisualStyleBackColor = true;
            this.ReadInMemory.Click += new System.EventHandler(this.ReadInMemory_Click);
            // 
            // interval_201304_201308
            // 
            this.interval_201304_201308.AutoSize = true;
            this.interval_201304_201308.Location = new System.Drawing.Point(12, 169);
            this.interval_201304_201308.Name = "interval_201304_201308";
            this.interval_201304_201308.Size = new System.Drawing.Size(107, 17);
            this.interval_201304_201308.TabIndex = 14;
            this.interval_201304_201308.Text = "2013.04-2013.08";
            this.interval_201304_201308.UseVisualStyleBackColor = true;
            // 
            // interval_201309_201311
            // 
            this.interval_201309_201311.AutoSize = true;
            this.interval_201309_201311.Location = new System.Drawing.Point(12, 192);
            this.interval_201309_201311.Name = "interval_201309_201311";
            this.interval_201309_201311.Size = new System.Drawing.Size(107, 17);
            this.interval_201309_201311.TabIndex = 15;
            this.interval_201309_201311.Text = "2013.09-2013.11";
            this.interval_201309_201311.UseVisualStyleBackColor = true;
            // 
            // interval_201312_201402
            // 
            this.interval_201312_201402.AutoSize = true;
            this.interval_201312_201402.Location = new System.Drawing.Point(12, 215);
            this.interval_201312_201402.Name = "interval_201312_201402";
            this.interval_201312_201402.Size = new System.Drawing.Size(107, 17);
            this.interval_201312_201402.TabIndex = 16;
            this.interval_201312_201402.Text = "2013.04-2013.08";
            this.interval_201312_201402.UseVisualStyleBackColor = true;
            // 
            // interval_ECN_since_22_07_14
            // 
            this.interval_ECN_since_22_07_14.AutoSize = true;
            this.interval_ECN_since_22_07_14.Location = new System.Drawing.Point(12, 238);
            this.interval_ECN_since_22_07_14.Name = "interval_ECN_since_22_07_14";
            this.interval_ECN_since_22_07_14.Size = new System.Drawing.Size(173, 17);
            this.interval_ECN_since_22_07_14.TabIndex = 17;
            this.interval_ECN_since_22_07_14.Text = "interval_ECN_since_22_07_14";
            this.interval_ECN_since_22_07_14.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 362);
            this.Controls.Add(this.interval_ECN_since_22_07_14);
            this.Controls.Add(this.interval_201312_201402);
            this.Controls.Add(this.interval_201309_201311);
            this.Controls.Add(this.interval_201304_201308);
            this.Controls.Add(this.ReadInMemory);
            this.Controls.Add(this.UseShortFile);
            this.Controls.Add(this.DoShortFile);
            this.Controls.Add(this.Ind_Local_file);
            this.Controls.Add(this.Only_BUY);
            this.Controls.Add(this.Only_SELL);
            this.Controls.Add(this.With_indicators);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.only_full_part);
            this.Controls.Add(this.test_cycle);
            this.Controls.Add(this.StartAnalyse);
            this.Controls.Add(this.AppendToBaseFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AppendToBaseFile;
        private System.Windows.Forms.Button StartAnalyse;
        private System.Windows.Forms.Button test_cycle;
        private System.Windows.Forms.CheckBox only_full_part;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox With_indicators;
        private System.Windows.Forms.CheckBox Only_SELL;
        private System.Windows.Forms.CheckBox Only_BUY;
        private System.Windows.Forms.CheckBox Ind_Local_file;
        private System.Windows.Forms.Button DoShortFile;
        private System.Windows.Forms.CheckBox UseShortFile;
        private System.Windows.Forms.Button ReadInMemory;
        private System.Windows.Forms.CheckBox interval_201304_201308;
        private System.Windows.Forms.CheckBox interval_201309_201311;
        private System.Windows.Forms.CheckBox interval_201312_201402;
        private System.Windows.Forms.CheckBox interval_ECN_since_22_07_14;
    }
}

