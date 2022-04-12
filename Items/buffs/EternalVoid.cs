using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.buffs
{
    public class EternalVoid : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Eternal Void");
            Description.SetDefault(@"The Void Encloses You.
Increasing Life, Life Regen, Defence And Damage.");
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<Charred_Life>().EternalVoid = true;
        }
    }
}
