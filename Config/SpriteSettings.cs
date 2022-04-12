using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ZensTweakstest.Config
{
    [Label("Settings")]
    [BackgroundColor(162, 59, 80, 192)]
    public class SpriteSettings : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(false)]
        [ReloadRequired]
        [Label("Classic Textures")]
        [BackgroundColor(208, 84, 92, 192)]
        [Tooltip("Resets most Zen realted textures to their pre 'Texture OverHaul' form.")]
        public bool MostClassicSprites;

        /*[DefaultValue(false)]
        [ReloadRequired]
        [Label("Nurse Heal Boss Cheat")]
        [BackgroundColor(208, 84, 92, 192)]
        [Tooltip("Turning this on disables Nurse heal when Modded Bosses are alive.")]
        public bool NurseHealDisableZenBosses;*/

        [DefaultValue(true)]
        [ReloadRequired]
        [Label("Title Revamp")]
        [BackgroundColor(208, 84, 92, 192)]
        [Tooltip("Changes the TML title.")]
        public bool TitleChange;
    }
}
