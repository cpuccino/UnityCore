namespace UnityCore.PersistentUI
{
    public enum PersistentUIType
    {
        // Persistent UI null state
        None = 0,

        // Splash screen and components
        Splash = 1,

        // Main menu (opening) and components
        StartMenu = 1001,
        
        // Loading persistent UI and components
        Loading = 2001,

        // Restart persistent UI and components
        Restart = 3001
    }
}