namespace SDVXScoreDetector
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpen = new System.Windows.Forms.Button();
            this.labScore = new System.Windows.Forms.Label();
            this.picBox_score = new System.Windows.Forms.PictureBox();
            this.labGrade = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_score)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(375, 111);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "参照";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // labScore
            // 
            this.labScore.AutoSize = true;
            this.labScore.Location = new System.Drawing.Point(274, 36);
            this.labScore.Name = "labScore";
            this.labScore.Size = new System.Drawing.Size(37, 12);
            this.labScore.TabIndex = 1;
            this.labScore.Text = "スコア：";
            // 
            // picBox_score
            // 
            this.picBox_score.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_score.Location = new System.Drawing.Point(16, 16);
            this.picBox_score.Name = "picBox_score";
            this.picBox_score.Size = new System.Drawing.Size(233, 353);
            this.picBox_score.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_score.TabIndex = 2;
            this.picBox_score.TabStop = false;
            // 
            // labGrade
            // 
            this.labGrade.AutoSize = true;
            this.labGrade.Location = new System.Drawing.Point(274, 70);
            this.labGrade.Name = "labGrade";
            this.labGrade.Size = new System.Drawing.Size(48, 12);
            this.labGrade.TabIndex = 4;
            this.labGrade.Text = "グレード：";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 383);
            this.Controls.Add(this.labGrade);
            this.Controls.Add(this.picBox_score);
            this.Controls.Add(this.labScore);
            this.Controls.Add(this.btnOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "SDVX Score Detector";
            ((System.ComponentModel.ISupportInitialize)(this.picBox_score)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label labScore;
        private System.Windows.Forms.PictureBox picBox_score;
        private System.Windows.Forms.Label labGrade;
    }
}

