using Terraria;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot
{
    public class Cursed_Zen_Pet_Buff : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Pet Peeve");
            Description.SetDefault("A little baby Peeve is beside you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<Charred_Life>().Zenpet = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Cursed_Zen_Pet>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Cursed_Zen_Pet>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
