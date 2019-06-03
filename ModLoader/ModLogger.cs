using Newtonsoft.Json;
using UnityEngine;

namespace ModLoader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class ModLogger : TextWriter
    {
        private static string logPath = Directory.GetCurrentDirectory().ToString() + Path.DirectorySeparatorChar + "Mods" + Path.DirectorySeparatorChar + "Mod_Log.txt";
        private static FileStream _filestream = new FileStream(logPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        private static StreamWriter _streamwriter = new StreamWriter(_filestream) { AutoFlush = true };
        private static List<TextWriter> _writers = new List<TextWriter>() { _streamwriter, Console.Out };

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }

        public class Error : ModLogger
        {
            new public static void Write(string value)
            {
                GameConsole.Console.singleton.AddLog(value, Color.red, false);
                foreach (var writer in ModLogger._writers)
                {
                    ConsoleColor originalColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    writer.Write("[ERROR] " + value);
                    Console.ForegroundColor = originalColor;
                }
            }

            new public static void WriteLine(string value)
            {
                GameConsole.Console.singleton.AddLog(value, Color.red, false);
                foreach (var writer in ModLogger._writers)
                {
                    ConsoleColor originalColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    writer.WriteLine("[ERROR] " + value);
                    Console.ForegroundColor = originalColor;
                }
            }

            new public static void WriteLine(Object obj)
            {
                GameConsole.Console.singleton.AddLog(JsonConvert.SerializeObject(obj), Color.red, false);
                foreach (var writer in ModLogger._writers)
                {
                    ConsoleColor originalColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    writer.WriteLine("[ERROR] " + obj);
                    Console.ForegroundColor = originalColor;
                }
            }
        }

        new public static void Write(string value)
        {
            GameConsole.Console.singleton.AddLog(value, Color.green, false);
            foreach (var writer in ModLogger._writers)
            {
                writer.Write("[INFO] " + value);
            }
        }

        new public static void WriteLine(string value)
        {
            GameConsole.Console.singleton.AddLog(value, Color.green, false);
            foreach (var writer in ModLogger._writers)
            {
                writer.WriteLine("[INFO] " + value);
            }
        }
        new public static void WriteLine(Object obj)
        {
            GameConsole.Console.singleton.AddLog(JsonConvert.SerializeObject(obj), Color.green, false);
            foreach (var writer in ModLogger._writers)
            {
                writer.WriteLine("[INFO] " + obj);
            }
        }

        public static void Init()
        {
            Console.SetOut(new ModLogger());
            Console.SetError(new ModLogger.Error());
        }
    }
}
