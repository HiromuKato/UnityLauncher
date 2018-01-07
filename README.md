# UnityLauncher

複数バージョンのUnityがインストールされた環境で利用できるUnityランチャーです。



***DEMO***  

![demo](https://raw.githubusercontent.com/HiromuKato/UnityLauncher/media/UnityLauncher.gif)



## 動作概要  

rootPath(デフォルトではC:\Program Files)配下の[Unity]という文字列が含まれるディレクトリをすべて取得し、ディレクトリ名をもとにボタンを生成します。ボタンをクリックするとそのディレクトリ配下のEditor/Unity.exeを実行します。

Unityのインストールディレクトリを変更している場合は、MainWindow.xaml.csの以下行を変更してください。

```private string rootPath = @"C:\Program Files\";```


## Auther
[Hiromu Kato](https://github.com/HiromuKato)

## Lisence
[MIT](https://github.com/HiromuKato/UnityLauncher/blob/master/LICENSE)