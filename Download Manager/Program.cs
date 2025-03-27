using System.Net;

class Program
{
    static void Main()
    {
        WebClient webClient = new WebClient();
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        Console.WriteLine("/* '##::::'##:'########:'##:::::::'##::::::::'#######::       */\r\n/*  ##:::: ##: ##.....:: ##::::::: ##:::::::'##.... ##:       */\r\n/*  ##:::: ##: ##::::::: ##::::::: ##::::::: ##:::: ##:       */\r\n/*  #########: ######::: ##::::::: ##::::::: ##:::: ##:       */\r\n/*  ##.... ##: ##...:::: ##::::::: ##::::::: ##:::: ##:       */\r\n/*  ##:::: ##: ##::::::: ##::::::: ##::::::: ##:::: ##:       */\r\n/*  ##:::: ##: ########: ########: ########:. #######::       */\r\n/* ..:::::..::........::........::........:::.......:::       */\r\n/* '########:'########::'########:'####:'##::: ##:'########:: */\r\n/*  ##.....:: ##.... ##: ##.....::. ##:: ###:: ##: ##.... ##: */\r\n/*  ##::::::: ##:::: ##: ##:::::::: ##:: ####: ##: ##:::: ##: */\r\n/*  ######::: ########:: ######:::: ##:: ## ## ##: ##:::: ##: */\r\n/*  ##...:::: ##.. ##::: ##...::::: ##:: ##. ####: ##:::: ##: */\r\n/*  ##::::::: ##::. ##:: ##:::::::: ##:: ##:. ###: ##:::: ##: */\r\n/*  ##::::::: ##:::. ##: ########:'####: ##::. ##: ########:: */\r\n/* ..::::::::..:::::..::........::....::..::::..::........::: */");
        while (true)
        {
            Console.WriteLine("1. Sigle Download");
            Console.WriteLine("2. Listing Download");
            Console.WriteLine("3. Exit App");

            string chooses = Console.ReadLine();

            if (chooses == "1")
            {
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
                    Console.WriteLine("2. Back");

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
                        Console.WriteLine("Back to main...");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Please choose a valid option!");
                    }
                }

            }

            if (chooses == "2")
            {
                Console.WriteLine("Enter links separated by spaces:");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("No links provided!");
                    return;
                }

                string[] linklist = input.Trim().Split(' ').Where(l => !string.IsNullOrEmpty(l)).ToArray();
                if (linklist.Length == 0)
                {
                    Console.WriteLine("No valid links entered!");
                    return;
                }

                string downloadFolder = @"C:\Users\Markazi.co\Downloads\";
                Directory.CreateDirectory(downloadFolder);

                foreach (string link in linklist)
                {
                    try
                    {
                        Uri uri = new Uri(link);
                        string fileName = Path.GetFileName(uri.LocalPath);
                        if (string.IsNullOrEmpty(fileName))
                        {
                            fileName = "downloaded_file_" + DateTime.Now.Ticks;
                        }

                        string fullPath = Path.Combine(downloadFolder, fileName);

                        Console.WriteLine($"📥 Starting download of {fileName} to {fullPath}...");

                        using (WebClient client = new WebClient())
                        {
                            client.Headers.Add("User-Agent", "Mozilla/5.0");

                            client.DownloadProgressChanged += (sender, e) =>
                            {
                                DrawProgressBar(e.ProgressPercentage);
                            };

                            client.DownloadFileCompleted += (sender, e) =>
                            {
                                Console.WriteLine($"\n✅ Download of {fileName} completed!");
                            };

                            client.DownloadFileTaskAsync(uri, fullPath).Wait();
                        }
                    }
                    catch (UriFormatException)
                    {
                        Console.WriteLine($"❌ Invalid URL: {link}");
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine($"❌ Download error for {link}: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Unexpected error for {link}: {ex.Message}");
                    }
                }
            }


        }

    }


    static void DrawProgressBar(int progress)
    {
        int totalBars = 100;
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
