using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Resources.Scripts
{
    /**
     * This class will be called within the launching phase
     */
    public class Bootstrap
    {

        private static string serverLocation = "Assets/Resources/EmbeddedServer/simulatte-3.3.3.jar";

        // We launch the server before the program starts
        // We will find the next port available in TCP to establish the server
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoadRuntimeMethod()
        {
            LaunchServer();
            SetLanguageToLocale();
        }

        private static void LaunchServer()
        {
            var lis = new TcpListener(IPAddress.Loopback, 0);
            lis.Start();
            Global.port = ((IPEndPoint) lis.LocalEndpoint).Port;
            lis.Stop();

            Debug.Log($"Using port {Global.port}");

            var pros = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "java",
                    Arguments = $"-jar {serverLocation} -port={Global.port}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                }
            };

            pros.Start();
        }

        private static void SetLanguageToLocale()
        {
            var localLangId = System.Globalization.CultureInfo.InstalledUICulture.Name;
            LocalizationSystem.language = localLangId switch
            {
                "ja-JP" => LocalizationSystem.Language.Japanese,
                { } st when st.Contains("zh") => LocalizationSystem.Language.Chinese,
                { } st when st.Contains("fr") => LocalizationSystem.Language.French,
                _ => LocalizationSystem.Language.English,
            };
        }
    }
}
