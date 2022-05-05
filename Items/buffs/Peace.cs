using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.buffs
{
    public class Peace : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("True Peace");
            Description.SetDefault(@"Calm Quiet.
Decreases enemy spawns.");
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<Charred_Life>().EternalVoid = true;
        }
    }
    public class glubahenpeece : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.HasBuff(ModContent.BuffType<Peace>()))
            {
                spawnRate *= 4;
            }
        }
    }
}
