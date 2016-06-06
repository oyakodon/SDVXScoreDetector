using System.IO;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Forms;

namespace SDVXScoreDetector
{
    public class Detecter
    {
        public static int Detect(string filename)
        {
            IplImage playShare = null;

            try
            {
                playShare = new IplImage(filename, LoadMode.Color);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("プレーシェア画像の読込中にエラーが発生しました。\n\n------------------------------\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                throw ex;
            }

            CvRect roi = new CvRect(115, 388, 220, 45);
            using (IplImage part = GetPartialImage(playShare, roi))
                MainForm.scoreImg = part.ToBitmap();

            var score = "";

            for (var i = 0; i < 8; i++)
            {
                var max = 0.0;
                var num = 0;

                for (var temp_num = 0; temp_num < 10; temp_num++)
                {
                    var file = "./template/" + temp_num + ".jpg";
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

                    var val = MatchTemplate(playShare, template, i);
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

        private static double MatchTemplate(IplImage src, IplImage template, int pos)
        {
            int X = 0, Y = 0, W = 0, H = 0;
            if (pos < 5)
            {
                // 大文字
                H = 36;
                W = 24;
                X = 145 + (pos * W);
                Y = 400;
            } else
            {
                // 小文字
                W = 20;
                H = 30;
                pos -= 5;
                X = 266 + (pos * (W + 3));
                Y = 405;

                // テンプレート画像の縮小
                var resized_img = new IplImage(
                    new CvSize((int)(template.Width * 0.8),
                    (int)(template.Height * 0.8)),
                    template.Depth, template.NChannels
                );
                Cv.Resize(template, resized_img, Interpolation.Linear);
                template = resized_img;
            }

            CvRect roi = new CvRect(X - 3, Y, W + 3, H + 3);
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
}
