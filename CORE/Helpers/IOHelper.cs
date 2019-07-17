﻿using System;
using System.Collections.Generic;
using System.IO;

namespace CORE.Helpers
{
    public static class IOHelper
    {
        public static void WriteLog(string pathRoot, params string[] content)
        {
            try
            {
                DateTime dt = DateTime.Now;

                string directoryPath = Path.Combine(pathRoot, dt.Year.ToString(), dt.Month.ToString());
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, "Log_" + dt.ToString("yyyyMMdd") + ".txt");
                StreamWriter sw;
                if (!File.Exists(filePath))
                {
                    sw = File.CreateText(filePath);
                }
                else
                {
                    sw = File.AppendText(filePath);
                    sw.WriteLine();
                }

                sw.WriteLine(dt.ToString("HH:mm:ss"));
                foreach (string item in content)
                {
                    sw.WriteLine(item);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void WriteLog(string pathRoot, IEnumerable<string> content)
        {
            try
            {
                DateTime dt = DateTime.Now;

                string directoryPath = Path.Combine(pathRoot, dt.Year.ToString(), dt.Month.ToString());
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, "Log_" + dt.ToString("yyyyMMdd") + ".txt");
                StreamWriter sw;
                if (!File.Exists(filePath))
                {
                    sw = File.CreateText(filePath);
                }
                else
                {
                    sw = File.AppendText(filePath);
                    sw.WriteLine();
                }

                sw.WriteLine(dt.ToString("HH:mm:ss"));
                foreach (string item in content)
                {
                    sw.WriteLine(item);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
