using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items.NewNonZen.Erichus.Loot
{
    public class ToxicGrenade : ModItem
    {
        public override void SetDefaults()
        {
			item.damage = 120;
			item.thrown = true;
			item.knockBack = 4;
			item.noMelee = true;

			item.width = 26;
			item.height = 26;
			item.scale = 1f;

			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(gold: 12);
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = false;
			item.noUseGraphic = true;

			item.shoot = ModContent.ProjectileType<ToxicGrenadeProj>();
			item.shootSpeed = 18f;
		}
    }
}
