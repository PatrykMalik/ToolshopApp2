using System;
using System.IO;

namespace ToolshopApp2.Controllers
{
    public class LogSingleton
    {
        private static LogSingleton instance;
        private static object syncRoot = new Object();

        private string filename;
        private string path;

        private LogSingleton()
        {
            filename = "ToolshopApp2_Log.txt";
            path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
        /// <summary>
        /// zapisuje przekazaną wiadomość do pliku w Dokumentach użytkownika pod nazwą ToolshopApp_Log
        /// </summary>
        /// <param name="msg"></param>
        public void SaveLog(string msg)
        {
            writeToFile(msg);
        }
        private void writeToFile(string msg)
        {
            lock (syncRoot)
            {
                using (StreamWriter writer = File.AppendText(path + "\\" + filename))
                {
                    writer.WriteLine("");
                    writer.WriteLine(DateTime.Now.ToString() + ": " + msg);

                    writer.Flush();
                    writer.Close();
                }
            }
        }
        /// <summary>
        /// Tworzy instancję klasy statycznej
        /// </summary>
        public static LogSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new LogSingleton();
                    }
                }
                return instance;
            }
        }
    }
}
