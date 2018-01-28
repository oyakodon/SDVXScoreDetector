using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

using OpenCvSharp;
using Newtonsoft.Json;

namespace SDVXScoreDetector
{
    public class Detector
    {
        /// <summary>
        /// 設定ファイルのパス
        /// </summary>
        private const string JSONPATH = "./settings.json";

        /// <summary>
        /// プレーシェア画像から各種情報を検出して返します。
        /// </summary>
        /// <param name="filename">ファイル名</param>
        /// <returns>スコア</returns>
        public static DetectResult Detect(string filename)
        {
            Mat playShare = null;
            DetectProperty prop = null;

            try
            {
                playShare = new Mat(filename, ImreadModes.Color);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("プレーシェア画像の読込中にエラーが発生しました。\n\n------------------------------\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                throw ex;
            }

            try
            {
                string json = "";

                using (var sr = new StreamReader(JSONPATH, System.Text.Encoding.UTF8))
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

            if (prop == null)
            {
                throw new NullReferenceException();
            }

            var score = MatchScoreTemplate(playShare, prop);
            var grade = MatchGradeTemplate(playShare, prop);

            return new DetectResult(score, grade);
        }

        /// <summary>
        /// 画像からスコアを検出します。
        /// </summary>
        /// <param name="playShare">元画像(プレーシェア画像)</param>
        /// <param name="prop">設定情報</param>
        /// <returns>スコア</returns>
        private static int MatchScoreTemplate(Mat playShare, DetectProperty prop)
        {
            string _score = "";

            for (var i = 0; i < 8; i++)
            {
                var max = 0.0;
                var num = 0;

                for (var t = 0; t < 10; t++)
                {
                    var file = prop.TemplatesPath + "/" + t + ".jpg";
                    Mat template = null;

                    try
                    {
                        template = new Mat(file, ImreadModes.Color);
                    }
                    catch (FileNotFoundException ex)
                    {
                        MessageBox.Show("テンプレート画像の読込中にエラーが発生しました。\nテンプレートフォルダの存在を確認してください。\n\n------------------------------\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        throw ex;
                    }

                    var part = new Rect();

                    if (i < 5)
                    {
                        // 大文字
                        part.H = prop.ScoreNum_Large.H;
                        part.W = prop.ScoreNum_Large.W;
                        part.X = prop.ScoreNum_Large.X + (i * part.W);
                        part.Y = prop.ScoreNum_Large.Y;
                    }
                    else
                    {
                        // 小文字
                        part.W = prop.ScoreNum_Small.W;
                        part.H = prop.ScoreNum_Small.H;
                        part.X = prop.ScoreNum_Small.X + ((i - 5) * (part.W + 3));
                        part.Y = prop.ScoreNum_Small.Y;

                        // テンプレート画像の縮小
                        var resized_img = new Mat(
                            new Size((int)(template.Width * ((double)prop.ScoreNum_Small.W / prop.ScoreNum_Large.W)),
                            (int)(template.Height * ((double)prop.ScoreNum_Small.W / prop.ScoreNum_Large.W))),
                            template.Type()
                        );
                        Cv2.Resize(template, resized_img, resized_img.Size());
                        template = resized_img;
                    }

                    var val = MatchTemplate(playShare, template, part);

                    if (val > max)
                    {
                        max = val;
                        num = t;
                    }
                }

                _score += num;
            }

            return int.Parse(_score);
        }

        /// <summary>
        /// 画像からグレードを検出します。
        /// </summary>
        /// <param name="playShare">元画像(プレーシェア画像)</param>
        /// <param name="prop">設定情報</param>
        /// <returns>グレード</returns>
        private static Grade MatchGradeTemplate(Mat playShare, DetectProperty prop)
        {
            Grade _grade = Grade.D;
            var max = 0.0;

            for (var t = 0; t < 10; t++)
            {
                var file = prop.TemplatesPath + "/" + ((Grade)t).ToStringFromGrade() + ".jpg";
                Mat template = null;

                try
                {
                    template = new Mat(file, ImreadModes.Color);
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show("テンプレート画像の読込中にエラーが発生しました。\nテンプレートフォルダの存在を確認してください。\n\n------------------------------\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    throw ex;
                }

                // ゲージ部分を黒で塗りつぶす(誤認識防止)
                Cv2.Rectangle(playShare, prop.GaugeArea.ToCvRect(), Scalar.Black);

                var val = MatchTemplate(playShare, template, prop.GradeArea);

                if (val > max)
                {
                    max = val;
                    _grade = (Grade)t;
                }
            }

            // A, AA, AAAの場合 +の有り無しを判定
            if (_grade == Grade.A || _grade == Grade.AA || _grade == Grade.AAA)
            {
                var file = prop.TemplatesPath + "/Plus.jpg";
                Mat template = null;

                try
                {
                    template = new Mat(file, ImreadModes.Color);
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show("テンプレート画像の読込中にエラーが発生しました。\nテンプレートフォルダの存在を確認してください。\n\n------------------------------\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    throw ex;
                }

                int idx = 0;
                switch (_grade)
                {
                    case Grade.A: idx = 0; break;
                    case Grade.AA: idx = 1; break;
                    case Grade.AAA: idx = 2; break;
                    default: break;
                }

                var area = prop.GradePlusArea;
                area.X = prop.GradePlusX[idx];
                var val = MatchTemplate(playShare, template, area);

                // 類似度が0.5以上の時、+を付ける
                if (val > 0.5)
                {
                    switch(_grade)
                    {
                        case Grade.A: _grade = Grade.A_Plus; break;
                        case Grade.AA: _grade = Grade.AA_Plus; break;
                        case Grade.AAA: _grade = Grade.AAA_Plus; break;
                        default: break;
                    }
                }

            }

            return _grade;
        }

        /// <summary>
        /// 元画像から指定された範囲とテンプレート画像とをテンプレートマッチングします。
        /// </summary>
        /// <param name="src">元画像</param>
        /// <param name="template">テンプレート画像</param>
        /// <param name="partialArea">テンプレートマッチングしたい範囲</param>
        /// <returns>類似度</returns>
        private static double MatchTemplate(Mat src, Mat template, Rect partialArea)
        {
            using (var part = src.Clone( new OpenCvSharp.Rect(partialArea.X - 5, partialArea.Y - 5, partialArea.W + 5, partialArea.H + 5) ))
            {
                var dstSize = new Size(part.Width - template.Width + 1, part.Height - template.Height + 1);
                using (var dst = new Mat(dstSize, MatType.CV_32FC(1)))
                {
                    Cv2.MatchTemplate(part, template, dst, TemplateMatchModes.CCoeffNormed);
                    double minVal, maxVal;
                    Cv2.MinMaxLoc(dst, out minVal, out maxVal);
                    return maxVal;
                }
            }
        }
    }

    public class DetectProperty
    {
        public Rect ScoreNum_Large;
        public Rect ScoreNum_Small;
        public Rect GradeArea;
        public Rect GaugeArea;
        public Rect GradePlusArea;
        public List<int> GradePlusX;

        public string TemplatesPath;
    }

    public class DetectResult
    {
        public Grade Grade { get; private set; }
        public int Score { get; private set; }

        public DetectResult(int score, Grade grade)
        {
            Score = score;
            Grade = grade;
        }

    }

    public enum Grade : int
    {
        S = 0,
        AAA_Plus,
        AAA,
        AA_Plus,
        AA,
        A_Plus,
        A,
        B,
        C,
        D
    }

    public static class GradeExt
    {
        // Gradeの拡張メソッド
        public static string ToStringFromGrade(this Grade grade)
        {
            var names = new[] { "S", "AAA+", "AAA", "AA+", "AA", "A+", "A", "B", "C", "D" };
            return names[(int)grade];
        }
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

        public Rect()
        {
            X = 0;
            Y = 0;
            W = 0;
            H = 0;
        }

        public OpenCvSharp.Rect ToCvRect()
        {
            return new OpenCvSharp.Rect(X, Y, W, H);
        }
    }

}
