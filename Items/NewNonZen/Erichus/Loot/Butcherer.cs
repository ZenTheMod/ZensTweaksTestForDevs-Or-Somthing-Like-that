using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items.NewNonZen.Erichus.Loot
{
    public class Butcherer : ModItem
    {
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Spawns bones on a sucessful hit");
        }

        public override void SetDefaults()
        {
			item.damage = 120;
			item.melee = true;
			item.knockBack = 4;

			item.width = 26;
			item.height = 26;
			item.scale = 1.5f;

			item.useTime = 0;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(gold: 12);
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            for (int i = 0; i < Main.rand.Next(1, 3); i++)
			{
				Projectile.NewProjectile(target.Center, new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-12, -8)), ModContent.ProjectileType<Toxibone>(), item.damage * 2, 2, player.whoAmI);
			}
        }
    }
}
