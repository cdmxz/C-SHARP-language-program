using Microsoft.Win32;
using System;
using System.Diagnostics;

namespace 翻译神器.Utils
{
    public class Util
    {

        /// <summary>
        /// 是否有一个实例在运行
        /// </summary>
        /// <param name="processId">如果有，则返回正在运行的进程的进程Id</param>
        /// <returns></returns>
        public static bool IsRun(out int processId)
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processesByName = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process process in processesByName)
            {
                if (process.Id == currentProcess.Id)// 跳过当前进程
                    continue;
                string proPath = process.MainModule?.FileName ?? "";
                string curPath = currentProcess.MainModule?.FileName ?? "";
                // 判断是否为同一路径
                if (DelExtension(proPath) == DelExtension(curPath))
                {
                    processId = process.Id;
                    return true;
                }
            }
            processId = 0;
            return false;
        }

        /// <summary>
        /// 删除路径的扩展名
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public static string DelExtension(string fileName)
        {
            int num = fileName.LastIndexOf('.');
            if (num != -1)
            {
                return fileName[..num];
            }
            return fileName;
        }

        /// <summary>
        /// 设置自启动
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="value">值</param>
        /// <param name="mode">模式：添加、删除、查询</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool SetAutoStart(string name, string? value, SetMode mode)
        {
            using RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (registryKey == null)
                throw new Exception("无法打开注册表");
            if (mode == SetMode.Add)
            {
                if (value == null)
                    throw new Exception("Value应不为空");
                registryKey.SetValue(name, value);
            }
            else if (mode == SetMode.Del)
                registryKey.DeleteValue(name);
            else if (mode == SetMode.Query)
                return registryKey.GetValue(name) != null;

            return true;
        }

        /// <summary>
        /// 打开链接
        /// </summary>
        /// <param name="url"></param>
        public static void OpenUrl(string url) => Process.Start("explorer.exe", url);
    }
    public enum SetMode
    {
        Add,
        Del,
        Query
    }

}

