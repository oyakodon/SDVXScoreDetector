namespace SDVXScoreDetector
{
    partial class GetPin
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
            this.btnPin = new System.Windows.Forms.Button();
            this.pin1 = new System.Windows.Forms.TextBox();
            this.pin2 = new System.Windows.Forms.TextBox();
            this.pin3 = new System.Windows.Forms.TextBox();
            this.pin4 = new System.Windows.Forms.TextBox();
            this.pin5 = new System.Windows.Forms.TextBox();
            this.pin6 = new System.Windows.Forms.TextBox();
            this.pin7 = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPin
            // 
            this.btnPin.Location = new System.Drawing.Point(66, 82);
            this.btnPin.Name = "btnPin";
            this.btnPin.Size = new System.Drawing.Size(71, 23);
            this.btnPin.TabIndex = 0;
            this.btnPin.Text = "認証";
            this.btnPin.UseVisualStyleBackColor = true;
            this.btnPin.Click += new System.EventHandler(this.btnPin_Click);
            // 
            // pin1
            // 
            this.pin1.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pin1.Location = new System.Drawing.Point(24, 29);
            this.pin1.MaxLength = 1;
            this.pin1.Multiline = true;
            this.pin1.Name = "pin1";
            this.pin1.Size = new System.Drawing.Size(27, 40);
            this.pin1.TabIndex = 1;
            this.pin1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pinInput_KeyPress);
            // 
            // pin2
            // 
            this.pin2.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pin2.Location = new System.Drawing.Point(66, 29);
            this.pin2.MaxLength = 1;
            this.pin2.Multiline = true;
            this.pin2.Name = "pin2";
            this.pin2.Size = new System.Drawing.Size(27, 40);
            this.pin2.TabIndex = 2;
            // 
            // pin3
            // 
            this.pin3.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pin3.Location = new System.Drawing.Point(109, 29);
            this.pin3.MaxLength = 1;
            this.pin3.Multiline = true;
            this.pin3.Name = "pin3";
            this.pin3.Size = new System.Drawing.Size(27, 40);
            this.pin3.TabIndex = 3;
            // 
            // pin4
            // 
            this.pin4.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pin4.Location = new System.Drawing.Point(151, 29);
            this.pin4.MaxLength = 1;
            this.pin4.Multiline = true;
            this.pin4.Name = "pin4";
            this.pin4.Size = new System.Drawing.Size(27, 40);
            this.pin4.TabIndex = 4;
            // 
            // pin5
            // 
            this.pin5.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pin5.Location = new System.Drawing.Point(193, 29);
            this.pin5.MaxLength = 1;
            this.pin5.Multiline = true;
            this.pin5.Name = "pin5";
            this.pin5.Size = new System.Drawing.Size(27, 40);
            this.pin5.TabIndex = 5;
            // 
            // pin6
            // 
            this.pin6.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pin6.Location = new System.Drawing.Point(235, 29);
            this.pin6.MaxLength = 1;
            this.pin6.Multiline = true;
            this.pin6.Name = "pin6";
            this.pin6.Size = new System.Drawing.Size(27, 40);
            this.pin6.TabIndex = 6;
            // 
            // pin7
            // 
            this.pin7.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pin7.Location = new System.Drawing.Point(278, 29);
            this.pin7.MaxLength = 1;
            this.pin7.Multiline = true;
            this.pin7.Name = "pin7";
            this.pin7.Size = new System.Drawing.Size(27, 40);
            this.pin7.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(191, 82);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // GetPin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 117);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pin7);
            this.Controls.Add(this.pin6);
            this.Controls.Add(this.pin5);
            this.Controls.Add(this.pin4);
            this.Controls.Add(this.pin3);
            this.Controls.Add(this.pin2);
            this.Controls.Add(this.pin1);
            this.Controls.Add(this.btnPin);
            this.MaximizeBox = false;
            this.Name = "GetPin";
            this.Text = "PIN入力";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPin;
        private System.Windows.Forms.TextBox pin1;
        private System.Windows.Forms.TextBox pin2;
        private System.Windows.Forms.TextBox pin3;
        private System.Windows.Forms.TextBox pin4;
        private System.Windows.Forms.TextBox pin5;
        private System.Windows.Forms.TextBox pin6;
        private System.Windows.Forms.TextBox pin7;
        private System.Windows.Forms.Button btnCancel;
    }
}