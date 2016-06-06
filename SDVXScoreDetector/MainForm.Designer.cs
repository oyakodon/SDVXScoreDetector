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
            this.btnDetect = new System.Windows.Forms.Button();
            this.labScore = new System.Windows.Forms.Label();
            this.picBox_score = new System.Windows.Forms.PictureBox();
            this.btnTweet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_score)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDetect
            // 
            this.btnDetect.Location = new System.Drawing.Point(276, 71);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(75, 23);
            this.btnDetect.TabIndex = 0;
            this.btnDetect.Text = "検出";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // labScore
            // 
            this.labScore.AutoSize = true;
            this.labScore.Location = new System.Drawing.Point(274, 16);
            this.labScore.Name = "labScore";
            this.labScore.Size = new System.Drawing.Size(37, 12);
            this.labScore.TabIndex = 1;
            this.labScore.Text = "スコア：";
            // 
            // picBox_score
            // 
            this.picBox_score.Location = new System.Drawing.Point(16, 16);
            this.picBox_score.Name = "picBox_score";
            this.picBox_score.Size = new System.Drawing.Size(233, 78);
            this.picBox_score.TabIndex = 2;
            this.picBox_score.TabStop = false;
            // 
            // btnTweet
            // 
            this.btnTweet.Enabled = false;
            this.btnTweet.Location = new System.Drawing.Point(363, 71);
            this.btnTweet.Name = "btnTweet";
            this.btnTweet.Size = new System.Drawing.Size(75, 23);
            this.btnTweet.TabIndex = 3;
            this.btnTweet.Text = "ツイートする";
            this.btnTweet.UseVisualStyleBackColor = true;
            this.btnTweet.Click += new System.EventHandler(this.btnTweet_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 107);
            this.Controls.Add(this.btnTweet);
            this.Controls.Add(this.picBox_score);
            this.Controls.Add(this.labScore);
            this.Controls.Add(this.btnDetect);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "SDVX Score Detector";
            ((System.ComponentModel.ISupportInitialize)(this.picBox_score)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.Label labScore;
        private System.Windows.Forms.PictureBox picBox_score;
        private System.Windows.Forms.Button btnTweet;
    }
}

