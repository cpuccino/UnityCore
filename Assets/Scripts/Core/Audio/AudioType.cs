namespace UnityCore.Audio
{
    // Format (grouping)_(type)_(action?)
    // Grouping has gaps to allow for sub elements and subgrouping
    public enum AudioType
    {
        None = 0,

        // Soundtracks
        ST_Opening = 1,

        ST_StartMenu = 2,

        // Sound FX
        SFX_Player_Spawn = 1001,

        SFX_Player_Defeat = 1051
    }
}