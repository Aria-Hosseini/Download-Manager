using System;
using System.Net;
using System.IO;

class Program
{
    static void Main()
    {
        WebClient webClient = new WebClient();
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        Console.WriteLine("/* '##::::'##:'########:'##:::::::'##::::::::'#######::       */\r\n/*  ##:::: ##: ##.....:: ##::::::: ##:::::::'##.... ##:       */\r\n/*  ##:::: ##: ##::::::: ##::::::: ##::::::: ##:::: ##:       */\r\n/*  #########: ######::: ##::::::: ##::::::: ##:::: ##:       */\r\n/*  ##.... ##: ##...:::: ##::::::: ##::::::: ##:::: ##:       */\r\n/*  ##:::: ##: ##::::::: ##::::::: ##::::::: ##:::: ##:       */\r\n/*  ##:::: ##: ########: ########: ########:. #######::       */\r\n/* ..:::::..::........::........::........:::.......:::       */\r\n/* '########:'########::'########:'####:'##::: ##:'########:: */\r\n/*  ##.....:: ##.... ##: ##.....::. ##:: ###:: ##: ##.... ##: */\r\n/*  ##::::::: ##:::: ##: ##:::::::: ##:: ####: ##: ##:::: ##: */\r\n/*  ######::: ########:: ######:::: ##:: ## ## ##: ##:::: ##: */\r\n/*  ##...:::: ##.. ##::: ##...::::: ##:: ##. ####: ##:::: ##: */\r\n/*  ##::::::: ##::. ##:: ##:::::::: ##:: ##:. ###: ##:::: ##: */\r\n/*  ##::::::: ##:::. ##: ########:'####: ##::. ##: ########:: */\r\n/* ..::::::::..:::::..::........::....::..::::..::........::: */");
        Console.WriteLine("input your link:");
        string link = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(link))
        {
            Console.WriteLine("no link provided!");
            return;
        }

        while (true)
        {
            Console.WriteLine("1. Start download");
            Console.WriteLine("2. Exit");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                string downloadFolder = @"C:\Users\Markazi.co\Downloads\";

                string fileName = Path.GetFileName(new Uri(link).LocalPath);
                string fullPath = Path.Combine(downloadFolder, fileName);

                Console.WriteLine($"📥 Starting download to {fullPath}...");

                webClient.DownloadProgressChanged += (sender, e) =>
                {
                    DrawProgressBar(e.ProgressPercentage);
                };
                webClient.DownloadFileCompleted += (sender, e) =>
                {
                    Console.WriteLine("\n✅ Download completed!");
                };

                webClient.DownloadFileAsync(new Uri(link), fullPath);
                Console.ReadLine();
            }
            else if (choice == "2")
            {
                Console.WriteLine("Bye Bye!");
                break;
            }
            else
            {
                Console.WriteLine("Error: Please choose a valid option!");
            }
        }
    }

    static void DrawProgressBar(int progress)
    {
        int totalBars = 50;
        int filledBars = (progress * totalBars) / 100;

        Console.CursorLeft = 0;
        Console.Write("\r");
        Console.Write(new string(' ', totalBars + 10));
        Console.Write("\r[");

        Console.ForegroundColor = ConsoleColor.Green; 

        Console.Write(new string('█', filledBars));
        Console.ForegroundColor = ConsoleColor.Gray; 

        Console.Write(new string('-', totalBars - filledBars));
        Console.Write($"] {progress}%");
    }
}
