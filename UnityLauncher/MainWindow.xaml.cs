using Microsoft.Win32;
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

        // リストボックスで選択したプロジェクト
        private string selectedPJ = "";

        public MainWindow()
        {
            InitializeComponent();
            CreateButtons();
            GetCurrentProject();
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
                Canvas.SetTop(btn, i * 32);
                btn.Click += (s1, e1) =>
                {
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
            if(selectedPJ == null || selectedPJ.Equals(""))
            {
                Process.Start(path + @"\Editor\Unity.exe");
            }
            else
            {
                // 選択したプロジェクトを開く
                Process.Start(path + @"\Editor\Unity.exe", "-projectPath " + selectedPJ);
            }
        }

        /// <summary>
        /// 最近開いたプロジェクトをレジストリから取得する
        /// </summary>
        private void GetCurrentProject()
        {
            string regPath = @"Software\Unity Technologies\Unity Editor 5.x";
            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(regPath, false))
            {
                if (regKey == null)
                {
                    MessageBox.Show("null");
                    return;
                }

                foreach (string valName in regKey.GetValueNames())
                {
                    if (valName.Contains("RecentlyUsedProjectPaths"))
                    {
                        byte[] bt = (byte[])regKey.GetValue(valName);
                        Console.WriteLine(bt.Length);
                        string pj = System.Text.Encoding.UTF8.GetString(bt);
                        listbox.Items.Add(pj.TrimEnd('\0'));
                    }
                }
            };
        }

        /// <summary>
        /// リストボックスの選択が変更されたときに呼ばれるコールバック
        /// </summary>
        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPJ = listbox.SelectedValue.ToString();
        }

    } // class
} // namespace
