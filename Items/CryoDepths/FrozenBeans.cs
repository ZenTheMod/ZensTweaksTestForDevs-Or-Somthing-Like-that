using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using System;

namespace ZensTweakstest.Items.CryoDepths
{
    public class FrozenBeans : ModItem
    {
		public override void SetDefaults()
		{
			item.damage = 25;
			item.ranged = true;
			item.knockBack = 4;
			item.noMelee = false;

			item.width = 26;
			item.height = 48;
			item.scale = 1f;

			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(gold: 12);
			item.rare = 2;
			item.UseSound = SoundID.Item106;
			item.autoReuse = true;
			item.useTurn = false;

			item.shoot = ModContent.ProjectileType<FrozenBeansBean>();
			item.shootSpeed = 15f;
		}
	}
	public class FrozenBeansBean : ModProjectile
    {
		public override void SetDefaults()
		{
			projectile.width = 8;               //The width of projectile hitbox
			projectile.height = 8;              //The height of projectile hitbox
			projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.ranged = true;           //Is the projectile shoot by a ranged weapon?
			projectile.penetrate = 5;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			aiType = ProjectileID.WoodenArrowFriendly;           //Act exactly like default Bullet
		}
        public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Item10, projectile.position);
			for (int i = 0; i < 20; i++)
            {
				Dust.NewDust(projectile.Center, 8, 8, DustID.Water_Snow);
				Dust.NewDust(projectile.Center, 8, 8, DustID.BlueTorch);
			}
		}
    }
	public class BakedBeans : ModItem
    {
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Daluyan makes another mess.");
        }
        public override void SetDefaults()
		{
			item.damage = 35;
			item.ranged = true;
			item.knockBack = 4;
			item.noMelee = false;

			item.width = 26;
			item.height = 48;
			item.scale = 1f;

			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(gold: 15);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item106;
			item.autoReuse = true;
			item.useTurn = false;

			item.shoot = ModContent.ProjectileType<BakedBeansBean>();
			item.shootSpeed = 17f;
		}
        public override void AddRecipes()
        {
			ModRecipe A = new ModRecipe(mod);
			A.AddIngredient(ModContent.ItemType<FrozenBeans>(), 1);
			A.AddTile(TileID.Furnaces);
			A.SetResult(this);
			A.AddRecipe();
		}
    }
	public class BakedBeansBean : ModProjectile
    {
		public override void SetDefaults()
		{
			projectile.width = 15;               //The width of projectile hitbox
			projectile.height = 15;              //The height of projectile hitbox
			projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.ranged = true;           //Is the projectile shoot by a ranged weapon?
			projectile.penetrate = 5;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			aiType = ProjectileID.WoodenArrowFriendly;           //Act exactly like default Bullet
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item10, projectile.position);
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(projectile.Center, 8, 8, DustID.LavaMoss);
				Dust.NewDust(projectile.Center, 8, 8, DustID.OrangeTorch);
			}
		}
	}
}
