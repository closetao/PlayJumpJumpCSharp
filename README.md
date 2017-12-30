# PlayJumpJumpCSharp

在电脑上用鼠标玩微信跳一跳

## 缘起
微信刚刚更新了一个版本，主推了一个叫跳一跳的小程序。看到有人搞出来了辅助[链接在这里](https://github.com/easyworld/PlayJumpJumpWithMouse)，但是程序是用Java写的，在Windows下用非常不方便。看了下实现原理，还比较简单，一时兴起就用C#写了这个小工具。

## 开发配置
> * C# 
> * Visual Studio 2013 
> * .Net Framework 4.0



## 使用方法
1. 将发布版目录下的文件下载到电脑；
2. 打开安卓手机的usb调试模式并授权连接的电脑
>  如果是小米手机，在USB调试下方有``USB调试（安全设置）``打开允许模拟点击 感谢[@wotermelon](https://github.com/wotermelon)
>  
> 可能需要安装Adb Interface驱动，已经放到发布版目录下了；

3. 打开微信跳一跳，并点击开始
4. 运行电脑上的 `跳一跳.exe` ，程序界面能够同步显示手机上的画面表示运行正常；
5. 设置下合理的弹力值，然后先点击小人底部适当位置，然后再点想要跳的箱子的位置即可；

## 原理
用usb调试安卓手机，用adb截图并用鼠标测量距离，然后计算按压时间后模拟按压。
```
adb shell input swipe <x1> <y1> <x2> <y2> [duration(ms)] (Default: touchscreen) # 模拟长按
adb shell screencap <filename> # 保存截屏到手机
adb pull /sdcard/screen.png # 下载截屏文件到本地
```


## 运行截图
![运行截图](https://github.com/closetao/PlayJumpJumpCSharp/raw/master/screenshot.gif)