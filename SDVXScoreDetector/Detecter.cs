using System.IO;
using System.Windows.Forms;

using OpenCvSharp;
using Newtonsoft.Json;

namespace SDVXScoreDetector
{
    public class Detecter
    {

        /// <summary>
        /// プレーシェア画像から各種情報を検出して返します。
        /// </summary>
        /// <param name="filename">ファイル名</param>
        /// <param name="version">SDVXのバージョン(3: III GW, 4: IV HH)</param>
        /// <returns>スコア</returns>
        public static int Detect(string filename, int version)
        {
            if (version != 3 && version != 4)
            {
                throw new System.ArgumentException("version is not valid.");
            }

            IplImage playShare = null;
            DetectProperty prop = null;

            try
            {
                playShare = new IplImage(filename, LoadMode.Color);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("プレーシェア画像の読込中にエラーが発生しました。\n\n------------------------------\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                throw ex;
            }

            try
            {
                string json = "";
                string jsonfile = "./.settings_v" + version;

                using (var sr = new StreamReader(jsonfile, System.Text.Encoding.UTF8))
                {
                    json = sr.ReadToEnd();
                }

                prop = JsonConvert.DeserializeObject<DetectProperty>(json);

            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("設定の読込中にエラーが発生しました。\n\n------------------------------\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                throw ex;
            }

            string score = "";

            for (var i = 0; i < 8; i++)
            {
                var max = 0.0;
                var num = 0;

                for (var temp_num = 0; temp_num < 10; temp_num++)
                {
                    var file = prop.TemplatesPath + "/" + temp_num + ".jpg";
                    IplImage template = null;
                    try
                    {
                        template = new IplImage(file, LoadMode.Color);
                    }
                    catch (FileNotFoundException ex)
                    {
                        MessageBox.Show("テンプレート画像の読込中にエラーが発生しました。\nテンプレートフォルダの存在を確認してください。\n\n------------------------------\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        throw ex;
                    }

                    var val = MatchTemplate(playShare, template, prop, i);
                    if (val > max)
                    {
                        max = val;
                        num = temp_num;
                    }
                }

                score += num;
            }

            return int.Parse(score);
        }

        /// <summary>
        /// 
        /// </summary>
        private static double MatchTemplate(IplImage src, IplImage template, DetectProperty prop, int pos)
        {
            int X = 0, Y = 0, W = 0, H = 0;
            if (pos < 5)
            {
                // 大文字
                H = prop.ScoreNum_Large.H;
                W = prop.ScoreNum_Large.W;
                X = prop.ScoreNum_Large.X + (pos * W);
                Y = prop.ScoreNum_Large.Y;
            }
            else
            {
                // 小文字
                W = prop.ScoreNum_Small.W;
                H = prop.ScoreNum_Small.H;
                X = prop.ScoreNum_Small.X + ((pos - 5) * (W + 3));
                Y = prop.ScoreNum_Small.Y;

                // テンプレート画像の縮小
                var resized_img = new IplImage(
                    new CvSize((int)(template.Width * ((double)prop.ScoreNum_Small.W / prop.ScoreNum_Large.W)),
                    (int)(template.Height * ((double)prop.ScoreNum_Small.W / prop.ScoreNum_Large.W))),
                    template.Depth, template.NChannels
                );
                Cv.Resize(template, resized_img, Interpolation.Linear);
                template = resized_img;
            }

            CvRect roi = new CvRect(X - 5, Y, W + 5, H);
            using (IplImage part = GetPartialImage(src, roi))
            {
                CvSize dstSize = new CvSize(part.Width - template.Width + 1, part.Height - template.Height + 1);
                using (IplImage dst = new IplImage(dstSize, BitDepth.F32, 1))
                {
                    Cv.MatchTemplate(part, template, dst, MatchTemplateMethod.CCoeffNormed);
                    double minVal, maxVal;
                    Cv.MinMaxLoc(dst, out minVal, out maxVal);
                    return maxVal;
                }
            }
        }

        private static IplImage GetPartialImage(IplImage src, CvRect roi)
        {
            src.SetROI(roi);
            IplImage part = new IplImage(roi.Size, src.Depth, src.NChannels);
            Cv.Copy(src, part);
            src.ResetROI();
            return part;
        }
    }

    public class DetectProperty
    {
        public Rect ScoreNum_Large;
        public Rect ScoreNum_Small;

        public string TemplatesPath;
    }

    public class Rect
    {
        public int X;
        public int Y;
        public int W;
        public int H;

        public Rect(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        public CvRect ToCvRect()
        {
            return new CvRect(X, Y, W, H);
        }
    }

}
