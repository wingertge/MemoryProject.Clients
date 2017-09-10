using AppKit;

namespace MemoryClient.Desktop.MacOS
{
    public class Application
    {
        public static void Main(string[] args)
        {
            NSApplication.Init();
            NSApplication.SharedApplication.Delegate = new AppDelegate();
            NSApplication.Main(args);
        }
    }
}