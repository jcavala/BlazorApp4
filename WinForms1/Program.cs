
using System.Security.Cryptography;
using WinForms1.Data;

namespace WinForms1
{
    internal static class Program
    {
        public static User user;
        static public RSA serverRSA;
        public static Aes aes;
        public static RSA userRSA;
        private static string rsaFile = "rsa.xml";
        public static string serverUrl = "http://localhost:8087";
        internal static Token? token;

        public static string documentFolder = "C:\\dotnet\\WinForms1\\documents";
        public static string jsonFolder= "C:\\dotnet\\WinForms1\\jsonFolder";
        public static string xmlFolder= "C:\\dotnet\\WinForms1\\xmlFolder";
        public static string xmlFile = xmlFolder+"\\users.xml";
        internal static string jsonFile = jsonFolder + "\\documents.json";
        public static string Language { get; set; } = "";
        public static string Location { get; set; } = "";

      
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            user = new User() { };
            
            aes = Aes.Create();
            serverRSA = RSA.Create();
            userRSA = RSA.Create();

            IniFile ini = new IniFile();
            documentFolder = ini.Read("Document Folder");
            jsonFolder = ini.Read("Json Folder");
            xmlFolder = ini.Read("Xml Folder");
            
            UserSettingsHelper.load();
            
            if (File.Exists(rsaFile))
                userRSA.FromXmlString(File.ReadAllText(rsaFile));
            else { 
                userRSA = RSA.Create(); 
                File.WriteAllText(rsaFile, userRSA.ToXmlString(true)); }
            UserKeyManager.Read();
            Application.Run(new StartForm());
        }
    }
}