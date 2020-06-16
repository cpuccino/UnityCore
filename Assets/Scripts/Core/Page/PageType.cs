namespace UnityCore.Page
{
    public enum PageType
    {
        // Page null state
        None = 0,

        // Splash screen and components
        Splash = 1,

        // Main menu (opening) and components
        StartMenu = 1001,
        
        // Loading page and components
        Loading = 2001,

        // Restart page and components
        Restart = 3001
    }
}