using System;
using System.Drawing;
using System.Windows.Forms;

namespace SDVXScoreDetector
{
    public partial class MainForm : Form
    {
        private DetectResult result = null;
        private string imgpath = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
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

                    result = Detecter.Detect(ofd.FileName);
                    imgpath = ofd.FileName;

                    labScore.Text = "スコア：　" + result.Score;
                    labGrade.Text = "グレード：　" + result.Grade.ToStringFromGrade();
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
    }

}
