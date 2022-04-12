using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using System.Collections.Generic;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ID;

namespace ZensTweakstest.Items.NewNonZen.Erichus.Loot
{
    public class ToxicRevolverator : ModItem
    {
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Converts Ichor, Cursed, Meteor, Musket bullets into Toxic Cartridges.");
        }
        public override void SetDefaults()
		{
			item.crit = 2;
			item.damage = 60; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			item.ranged = true; // sets the damage type to ranged
			item.width = 40; // hitbox width of the item
			item.height = 20; // hitbox height of the item
			item.useTime = 10; // The item's use time in ticks (60 ticks == 1 second.)
			item.useAnimation = 10; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			item.value = 10000; // how much the item sells for (measured in copper)
			item.rare = ItemRarityID.Pink; // the color that the item's name will be in-game
			item.UseSound = SoundID.Item11; // The sound that this item plays when used.
			item.autoReuse = true; // if you can hold click to automatically use it again
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
			item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
		}
		/*public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			if (line.mod == "Terraria" && line.Name == "ItemName")
			{
				Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
				GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(ItemID.VoidDye), item, null); //use living rainbow dye shader
				Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1); //draw the tooltip manually
				Main.spriteBatch.End(); //then end and begin again to make remaining tooltip lines draw in the default way
				Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
				return false;
			}
			return true;
		}*/
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
			Vector2 Offset = new Vector2(-6, -4);
			Texture2D texture = Main.itemTexture[item.type];
			spriteBatch.Draw(texture, position + Offset, null, drawColor, 0f, origin, scale + 0.2f, SpriteEffects.None, 0f);
			if (item.color != Color.Transparent)
			{
				spriteBatch.Draw(texture, position + Offset, null, itemColor, 0f, origin, scale + 0.2f, SpriteEffects.None, 0f);
			}
			return false;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			if (type == ProjectileID.Bullet || type == ProjectileID.MeteorShot || type == ProjectileID.CursedBullet || type == ProjectileID.IchorBullet) // or ProjectileID.WoodenArrowFriendly
			{
				type = ModContent.ProjectileType<ToxetRoxic>(); // or ProjectileID.FireArrow;
			}
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDustDirect(position, item.width, item.height, DustID.GreenFairy);
			}
			return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
		}
	}
	public class ToxicRocket : ModItem
    {
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Toxic Cartridge");
        }
        public override void SetDefaults()
		{
			item.damage = 10;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 1.5f;
			item.value = 10;
			item.rare = ItemRarityID.Green;
			item.shoot = ModContent.ProjectileType<ToxetRoxic>();   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 16f;                  //The speed of the projectile
			item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
		}
	}
	public class ToxetRoxic : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Toxic Cartridge");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.GreenFairy);
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/NewNonZen/Erichus/Loot/RocketTAI"), drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);//projectile.scale - k / (float)projectile.oldPos.Length
			}
			return true;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<Toxiblast>(), projectile.damage, 2, projectile.owner);
		}
		public override void SetDefaults()
        {
			projectile.ranged = true;

			projectile.width = 10;
			projectile.height = 10;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.ignoreWater = false;

			projectile.aiStyle = 0;
			//aiType = 1;
		}
        public override void PostAI()
        {
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
		}
    }
}
