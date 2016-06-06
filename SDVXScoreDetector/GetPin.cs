using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SDVXScoreDetector
{
    public partial class GetPin : Form
    {
        public GetPin()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void btnPin_Click(object sender, EventArgs e)
        {
            var pins = new List<TextBox>{ pin1, pin2, pin3, pin4, pin5, pin6, pin7 };
            var code = "";
            foreach (var p in pins)
            {
                if (p.Text != "")
                    code += p.Text;
                else
                {
                    throw new FormatException("PINが入力されていません");
                }
            }
            Tweeter.pinCode = code;
            
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void pinInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || '9' < e.KeyChar)
            {
                //押されたキーが 0～9でない場合は、イベントをキャンセルする
                e.Handled = true;
            }
        }

    }
}
