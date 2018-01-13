using System;
using System.Drawing;
using System.Windows.Forms;

namespace SDVXScoreDetector
{
    public partial class MainForm : Form
    {
        private int score = 0;
        private string imgpath = null;

        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

#if DEBUG
            ofd.InitialDirectory = System.IO.Path.GetFullPath(@"..\..\assets\TestPattern");
#endif

            ofd.Filter = "イメージファイル(*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp|すべてのファイル(*.*)|*.*";
            ofd.Title = "プレーシェア画像を開く";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picBox_score.Image = new Bitmap(ofd.FileName);

                    score = Detecter.Detect(ofd.FileName, 4);
                    imgpath = ofd.FileName;
                    btnTweet.Enabled = true;

                    labScore.Text = "スコア：　" + score;
                }
                catch (Exception ex)
                {
#if DEBUG
                    MessageBox.Show("スコアを認識できませんでした。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                    MessageBox.Show("スコアを認識できませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif

                    labScore.Text = "スコア：　";
                }
            }

        }

        private void btnTweet_Click(object sender, EventArgs e)
        {
            // Tweeter.TweetWithImg(imgpath, score);
        }
    }

}
