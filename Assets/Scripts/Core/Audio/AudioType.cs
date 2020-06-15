namespace Ukiyo.Unity.Core.Audio
{
    // Format (grouping)_(type)_(action?)
    // Grouping has gaps to allow for sub elements and subgrouping
    public enum AudioType
    {
        None = -1,
        // Soundtracks
        ST_Opening = 0, 
        ST_StartMenu = 1,
        
        // Sound FX
        SFX_Player_Spawn = 1000,
        SFX_Player_Defeat = 1050
    }
}