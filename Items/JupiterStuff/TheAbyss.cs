using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZensTweakstest.Items.NewZenStuff.Projectilles;

namespace ZensTweakstest.Items.JupiterStuff
{
    public class TheAbyss : ModItem
    {
		private float Interval = 0f;
		private Color Test = new Color(255, 145, 206);
		private Color Test2 = new Color(135, 66, 255);
		public override bool CloneNewInstances => true;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyss");
			Tooltip.SetDefault(@"Rain down Abyssal Scythes from above.
Dedecated to Jupitererer.");
		}

		public override void SetDefaults()
		{
			item.damage = 220;
			item.noMelee = true;
			item.magic = true;
			item.mana = 8;
			item.rare = 8;
			item.width = 28;
			item.height = 30;
			item.useTime = 13;
			item.autoReuse = true;
			item.UseSound = SoundID.Item71;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 20f;
			item.useAnimation = 13;
			item.shoot = ModContent.ProjectileType<ZenSwordBeam>();
			item.value = Item.sellPrice(silver: 3);
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			for (int i = 0; i < 15; i++)
			{
				position = new Vector2(Main.MouseWorld.X + Main.rand.Next(-170, 170), player.position.Y - 650);
				Vector2 vel = Vector2.Normalize(Main.MouseWorld - position) * item.shootSpeed;
				speedX = vel.X;
				speedY = vel.Y;
				Projectile.NewProjectile(position, new Vector2(speedX, speedY), ProjectileID.DemonScythe, item.damage, item.knockBack, Main.myPlayer);
				Vector2 speedDust = Main.rand.NextVector2CircularEdge(1f, 1f);
				Dust d = Dust.NewDustPerfect(Main.MouseWorld, DustID.PurpleCrystalShard, speedDust * 5, Scale: 1.5f);
				d.noGravity = true;
			}
			return false;
        }
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (TooltipLine tooltipLine in tooltips)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = Color.Lerp(Test, Test2, Interval); //change the color accordingly to above
				}
			}
		}
		public override void UpdateInventory(Player player)
		{
			Interval += 0.01f;
			if (Interval >= 1f)
			{
				Interval = 0f;
			}
		}
		public override void PostUpdate()
		{
			Lighting.AddLight(item.Center, Color.Purple.ToVector3() * 0.75f * Main.essScale);
		}
        public override Color? GetAlpha(Color lightColor)
        {
			return Color.White;
        }
    }

	//public class AbyssRing : ModProjectile
    //{

    //}
}
