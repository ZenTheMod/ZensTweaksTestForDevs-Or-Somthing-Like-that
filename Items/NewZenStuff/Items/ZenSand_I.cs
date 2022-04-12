using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Tiles;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class ZenSand_I : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zen-Sand");
			Tooltip.SetDefault("His Favorate Water Type");
		}
		public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.rare = 3;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = ModContent.TileType<ZenSand>();
			//item.ammo = AmmoID.Sand; Using this Sand in the Sandgun would require PickAmmo code and changes to ExampleSandProjectile or a new ModProjectile.
		}
	}
}
