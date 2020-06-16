namespace UnityCore.Page
{
    public enum PageType
    {
        // Page null state
        None = -1,

        // Splash screen and components
        Splash = 0,

        // Main menu (opening) and components
        StartMenu = 1000,
        
        // Loading page and components
        Loading = 2000,

        // Restart page and components
        Restart = 3000
    }
}