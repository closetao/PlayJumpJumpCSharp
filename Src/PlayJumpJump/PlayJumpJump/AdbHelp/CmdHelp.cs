using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ADBTool.AdbHelp
{
    public class CmdHelp
    {
        //委托(接收到原始数据)
        /// <summary>
        /// 委托(接收到原始数据)
        /// </summary>
        /// <param name="state"></param>
        public delegate void DelegateReceiveData(string data);
        //接收到原始数据
        /// <summary>
        /// 接收到原始数据
        /// </summary>
        public static event DelegateReceiveData EventReceiveData;


        //委托(接收到原始数据)
        /// <summary>
        /// 委托(接收到原始数据)
        /// </summary>
        /// <param name="state"></param>
        public delegate void DelegateReceiveThreadData(string data);
        //接收到原始数据
        /// <summary>
        /// 接收到原始数据
        /// </summary>
        public static event DelegateReceiveThreadData EventReceiveThreadData;


        //委托(接收到错误数据)
        /// <summary>
        /// 委托(接收到错误数据)
        /// </summary>
        /// <param name="state"></param>
        public delegate void DelegateReceiveThreadErrorData(string data);
        //接收到错误数据
        /// <summary>
        /// 接收到错误数据
        /// </summary>
        public static event DelegateReceiveThreadErrorData EventReceiveThreadErrorData;



        /**触发事件回调结果*/
        public static void PutResultToUI(string receiveMsg)
        {
            EventReceiveData(receiveMsg);
        }

        /**触发事件回调结果(线程)*/
        public static void PutResultThreadToUI(string receiveMsg)
        {
            EventReceiveThreadData(receiveMsg);
        }

        /**触发事件回调错误结果(线程)*/
        public static void PutResultThreadErrorToUI(string receiveMsg)
        {
            EventReceiveThreadErrorData(receiveMsg);
        }



        //adb路径
        public static string AdbExePath
        {
            get
            {
                return Path.Combine(Application.StartupPath, "AdbLib\\adb.exe");
            }
        }


        /// <summary>
        /// 发送adb 命令
        /// </summary>
        /// <returns></returns>
        public  void SendAdbCmd(string cmdStr)
        {
            CmdProcessHelper.AysnRun(AdbExePath, cmdStr);
        }


        /// <summary>
        /// 发送adb 命令
        /// </summary>
        /// <returns></returns>
        public void SendAdbCmd2(string cmdStr)
        {
            CmdProcessHelper.Run(AdbExePath, cmdStr);
        }


        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns></returns>
        public string[] GetDevices(string cmdStr)
        {
            var result = ProcessHelper.Run(AdbExePath, cmdStr);
            var itemsString = result.OutputString;
            EventReceiveData(itemsString);

            var items = itemsString.Split(new[] { "$", "#", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var itemsList = new List<String>();
            foreach (var item in items)
            {
                var tmp = item.Trim();
                //第一行不含\t所以排除
                if (tmp.IndexOf("\t") == -1)
                {
                    continue;
                }
                var tmps = item.Split('\t');
                itemsList.Add(tmps[0]);
            }
            itemsList.Sort();
            return itemsList.ToArray();
        }

        /// <summary>
        /// 获取设备APP
        /// </summary>
        /// <returns></returns>
        public string[] GetAPP(string cmdStr)
        {
            var result = ProcessHelper.Run(AdbExePath, cmdStr);
            var itemsString = result.OutputString;
            EventReceiveData(itemsString);
            string[] items = itemsString.Split(new[] { "$", "#", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return items;
        }

        /// <summary>
        /// 卸载软件
        /// </summary>
        /// <returns></returns>
        public  void APPUninstall(string packageName)
        {
            var result = ProcessHelper.Run(AdbExePath, "uninstall " + packageName);
            var itemsString = result.OutputString;
            EventReceiveData(itemsString);
        }


        #region 获取设备相关信息
        /// <summary>
        /// -s 0123456789ABCDEF shell getprop ro.product.brand
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <param name="propKey"></param>
        /// <returns></returns>
        public  string GetDeviceProp(string deviceNo, string propKey)
        {
            var result = ProcessHelper.Run(AdbExePath, string.Format("-s {0} shell getprop {1}", deviceNo, propKey));
            EventReceiveData(result.OutputString.Trim());
            return result.OutputString.Trim();
        }
        /// <summary>
        /// 型号：[ro.product.model]: [Titan-6575]
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public  string GetDeviceModel(string deviceNo)
        {
            return GetDeviceProp(deviceNo, "ro.product.model");
        }
        /// <summary>
        /// 牌子：[ro.product.brand]: [Huawei]
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public  string GetDeviceBrand(string deviceNo)
        {
            return GetDeviceProp(deviceNo, "ro.product.brand");
        }
        /// <summary>
        /// 设备指纹：[ro.build.fingerprint]: [Huawei/U8860/hwu8860:2.3.6/HuaweiU8860/CHNC00B876:user/ota-rel-keys,release-keys]
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public  string GetDeviceFingerprint(string deviceNo)
        {
            return GetDeviceProp(deviceNo, "ro.build.fingerprint");
        }
        /// <summary>
        /// 系统版本：[ro.build.version.release]: [4.1.2]
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public  string GetDeviceVersionRelease(string deviceNo)
        {
            return GetDeviceProp(deviceNo, "ro.build.version.release");
        }
        /// <summary>
        /// SDK版本：[ro.build.version.sdk]: [16]
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public  string GetDeviceVersionSdk(string deviceNo)
        {
            return GetDeviceProp(deviceNo, "ro.build.version.sdk");
        }
        #endregion


    }
}
