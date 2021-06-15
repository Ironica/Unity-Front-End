using UnityEngine;

namespace Resources.Scripts
{
    
    /**
     * This class contains global variables to be used in Unity application
     */
    public class Global
    {
        
        // The port, which will be initialized in launching phase
        public static int port { get; set; }

        public static bool mouseActivated { get; set; } = true;
    }
}