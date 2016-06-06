using System;
using System.Drawing;
using System.Windows.Forms;

namespace SDVXScoreDetector
{
    public partial class MainForm : Form
    {
        public static Bitmap scoreImg;
        public const bool DEBUG = true;

        private string score = "";
        private string imgpath = null;

        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (DEBUG)
            {
                btnTweet.Enabled = true;
            }
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            if (DEBUG)
            {
                ofd.InitialDirectory = System.IO.Path.GetFullPath(@"..\..\assets\TestPattern");
            }

            ofd.Filter = "イメージファイル(*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp|すべてのファイル(*.*)|*.*";
            ofd.Title = "プレーシェア画像を開く";
            ofd.RestoreDirectory = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    score = "[ " + Detecter.Detect(ofd.FileName) + " ]";
                    imgpath = ofd.FileName;
                    btnTweet.Enabled = true;
                }
                catch (Exception)
                {
                    score = "[ Errored! ]";
                }

                labScore.Text = "スコア：　" + score;
                picBox_score.Image = scoreImg;
            }

        }

        private void btnTweet_Click(object sender, EventArgs e)
        {
            try
            {
                Tweeter.TweetWithImg(imgpath, score);
            } catch (Exception ex)
            {
                MessageBox.Show("ツイート中にエラーが発生しました。\n\n------------------------------\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
