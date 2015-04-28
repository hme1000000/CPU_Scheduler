namespace OperatingSystems_Scheduler
{
    partial class CPU_Scheduler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_OK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_num = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_select = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("Century", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OK.ForeColor = System.Drawing.Color.Maroon;
            this.btn_OK.Location = new System.Drawing.Point(607, 32);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(87, 28);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Algerian", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(220, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "CPU Scheduler";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_select);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_OK);
            this.groupBox1.Controls.Add(this.txt_num);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 58);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(701, 84);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Information";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Type of Scheduler";
            // 
            // txt_num
            // 
            this.txt_num.Location = new System.Drawing.Point(153, 34);
            this.txt_num.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_num.Name = "txt_num";
            this.txt_num.Size = new System.Drawing.Size(116, 23);
            this.txt_num.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Number of Processes";
            // 
            // cmb_select
            // 
            this.cmb_select.FormattingEnabled = true;
            this.cmb_select.Items.AddRange(new object[] {
            "Firt Come First Served",
            "Shortest job first (NP)",
            "Shortest job first (P)",
            "Priority (NP)",
            "Priority (P)",
            "Round Robin"});
            this.cmb_select.Location = new System.Drawing.Point(446, 34);
            this.cmb_select.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmb_select.Name = "cmb_select";
            this.cmb_select.Size = new System.Drawing.Size(140, 24);
            this.cmb_select.TabIndex = 6;
            // 
            // CPU_Scheduler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(757, 447);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CPU_Scheduler";
            this.Text = "CPU Scheduler";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_num;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_select;


    }
}

