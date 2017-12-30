using System;
using System.Collections.Generic;
using System.Text;

namespace ADBTool.AdbHelp
{
    public class CmdAdbInfo
    {
        /**启动adb服务*/
        public static string adb_start_server = "start-server";
        /**停止adb服务*/
        public static string adb_kill_server = "kill-server";
        /**重启adb服务*/
        public static string adb_reboot = "reboot";
        /**获取adb的版本信息*/
        public static string adb_version = "version";
        /**获取连接的设备信息*/
        public static string adb_devices = "devices";
        /**列出手机装的所有app包名*/
        public static string adb_shell_pm_list_packages = "shell pm list packages";
        /**列出系统应用app包名*/
        public static string adb_shell_pm_list_packages_s = "shell pm list packages s";
        /**列出第三方应用app包名*/
        public static string adb_shell_pm_list_packages_3 = "shell pm list packages -3";
        /**安装软件*/
        public static string adb_install = "install";
        /**卸载软件*/
        public static string adb_uninstall = "uninstall";
        /**电池状况*/
        public static string adb_shell_dumpsys_battery="shell dumpsys battery";
        /**屏幕分辨率*/
        public static string adb_shell_wm_size="shell wm size";
        /**屏幕信息*/
        public static string adb_shell_dumpsys_window_displays = "shell dumpsys window displays";
        /**网络配置*/
        public static string adb_shell_netcfg = "shell netcfg";
        /**root*/
        public static string adb_root = "root";
        

        /**获取所有adb指令*/
        public static List<string> GetAllADBCmdList()
        {
            List<string> itemsList = new List<string>();
            itemsList.Add(adb_start_server);
            itemsList.Add(adb_kill_server);
            itemsList.Add(adb_reboot);
            itemsList.Add(adb_version);
            itemsList.Add(adb_devices);
            itemsList.Add(adb_shell_pm_list_packages);
            itemsList.Add(adb_shell_pm_list_packages_s);
            itemsList.Add(adb_shell_pm_list_packages_3);
            itemsList.Add(adb_install);
            itemsList.Add(adb_uninstall);
            itemsList.Add(adb_shell_dumpsys_battery);
            itemsList.Add(adb_shell_wm_size);
            itemsList.Add(adb_shell_dumpsys_window_displays);
            itemsList.Add(adb_shell_netcfg);
            itemsList.Add(adb_root);
            itemsList.Sort();
            return itemsList;
        }

    }
}
