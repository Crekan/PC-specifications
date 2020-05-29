using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Management;

namespace getHardwareInformation
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputResult("Процессор:", GetHardwareInfo("Win32_Processor", "Name"));
            OutputResult("Производитель:", GetHardwareInfo("Win32_Processor", "Manufacturer"));
            OutputResult("Описание:", GetHardwareInfo("Win32_Processor", "Description"));

            Console.WriteLine();

            OutputResult("Видеокарта:", GetHardwareInfo("Win32_VideoController", "Name"));
            OutputResult("Видеопроцессор:", GetHardwareInfo("Win32_VideoController", "VideoProcessor"));
            OutputResult("Версия драйвера:", GetHardwareInfo("Win32_VideoController", "DriverVersion"));
            OutputResult("Объем памяти (в байтах):", GetHardwareInfo("Win32_VideoController", "AdapterRAM"));

            Console.WriteLine();

            OutputResult("Название дисковода:", GetHardwareInfo("Win32_CDROMDrive", "Name"));
            OutputResult("Буква привода:", GetHardwareInfo("Win32_CDROMDrive", "Drive"));

            Console.WriteLine();

            OutputResult("Жесткий диск:", GetHardwareInfo("Win32_DiskDrive", "Caption"));
            OutputResult("Объем (в байтах):", GetHardwareInfo("Win32_DiskDrive", "Size"));

            Console.ReadLine();
        }

        private static List<string> GetHardwareInfo(string WIN32_Class, string ClassItemField)
        {
            List<string> result = new List<string>();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + WIN32_Class);

            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    result.Add(obj[ClassItemField].ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        private static void OutputResult(string info, List<string> result)
        {
            if (info.Length > 0)
                Console.WriteLine(info);

            if (result.Count > 0)
            {
                for (int i = 0; i < result.Count; ++i)
                    Console.WriteLine(result[i]);
            }
        }


    }
}