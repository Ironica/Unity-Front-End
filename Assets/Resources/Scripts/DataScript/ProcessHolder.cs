using System.Diagnostics;

namespace Resources.Scripts.DataScript
{
    public class ProcessHolder
    {
        private Process p;

        public void LaunchServer(string path, int port)
        {
            p = Process.Start("java",$"-jar {path} -port={port}");
        }

        public void StopServer()
        {
            p.Kill();
        }
    }
}