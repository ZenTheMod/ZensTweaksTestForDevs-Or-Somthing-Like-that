using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using ZensTweakstest.Items.NewZenStuff.Items;

namespace ZensTweakstest.Items.NewZenStuff.Items2Because1IsTooFull
{
    public class ZenitrinGun : ModItem
    {
		public override void SetDefaults()
		{
			item.damage = 60; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			item.ranged = true; // sets the damage type to ranged
			item.width = 74; // hitbox width of the item
			item.height = 38; // hitbox height of the item
			item.useTime = 20; // The item's use time in ticks (60 ticks == 1 second.)
			item.useAnimation = 20; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			item.value = 10000; // how much the item sells for (measured in copper)
			item.rare = ItemRarityID.Yellow; // the color that the item's name will be in-game
			item.UseSound = SoundID.Item11; // The sound that this item plays when used.
			item.autoReuse = true; // if you can hold click to automatically use it again
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 12f; // the speed of the projectile (measured in pixels per frame)
			item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 20f;
			Vector2 muzzleOffset2 = Vector2.Normalize(new Vector2(speedX, speedY)) * 55f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			for (int k = 0; k < 10; k++)
			{
				Dust dust = Dust.NewDustPerfect(position + muzzleOffset2, DustID.Clentaminator_Red, new Vector2(Main.rand.NextFloat(-0.4f, 0.4f), Main.rand.NextFloat(-1f, -2f)));
			}
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<ZenStone_I>(), 85);
			recipe.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 20);
			recipe.AddTile(ModContent.TileType<Bosses.SparkArmor.ZCC_PLACED>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
