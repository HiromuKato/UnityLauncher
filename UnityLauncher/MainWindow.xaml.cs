using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace UnityLauncher
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // 検索のルートディレクトリパス
        // ★Unityのインストールディレクトリを変更している場合はここを変更してください
        private string rootPath = @"C:\Program Files\";

        public MainWindow()
        {
            InitializeComponent();
            CreateButtons();
        }

        /// <summary>
        /// rootPath配下のUnity文字列を含むディレクトリのある分だけボタンを生成する
        /// </summary>
        private void CreateButtons()
        {
            string[] dirs = GetUnityDirs();
            if(dirs == null)
            {
                Console.WriteLine("Unity directory is not found.");
                return;
            }

            for(int i = 0; i < dirs.Length; ++i)
            {
                string dir = dirs[i];
                string dirName = System.IO.Path.GetFileName(dir);

                Button btn = new Button();
                btn.Name = "Button" + i.ToString();
                btn.Content = dirName;
                btn.Width = 200;
                btn.Height = 30;
                Canvas.SetLeft(btn, 10);
                Canvas.SetTop(btn, 10 + i * 32);
                btn.Click += (s1, e1) =>
                {
                    //MessageBox.Show(dirName);
                    ExecUnity(dir);
                };
                canvas1.Children.Add(btn);
            }
        }

        /// <summary>
        /// rootPath配下でUnityという文字列が含まれているディレクトリのパスを取得する
        /// </summary>
        /// <returns>Unityという文字列が含まれているディレクトリパスの配列</returns>
        private string[] GetUnityDirs()
        {
            try
            {
                string[] dirs = Directory.GetDirectories(rootPath, "*Unity*");
                /*
                Console.WriteLine("The number of directories is {0}.", dirs.Length);
                foreach (string dir in dirs)
                {
                    Console.WriteLine(dir);
                }
                */
                return dirs;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            return null;
        }

        /// <summary>
        /// Unityを実行する
        /// </summary>
        /// <param name="path">実行するUnityのディレクトリパス</param>
        private void ExecUnity(string path)
        {
            Process.Start(path + @"\Editor\Unity.exe");
        }

    } // class
} // namespace
