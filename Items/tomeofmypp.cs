using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QwertysRandomContent;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items
{
    public class tomeofmypp : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tome of Infinite Drunk Doors");
            Tooltip.SetDefault("An OpEn Wo... Un  d IIS A PaR  . A SitS DoOr TooooO..." + 
                "\n-MEME ITEM-");
        }

        public override void SetDefaults()
        {
            item.damage = 360;
            item.magic = true;

            item.useTime = 35;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.knockBack = 100;
            item.value = Item.buyPrice(gold: 15);
            item.value = Item.sellPrice(gold: 15);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.DD2_BetsyDeath;
            item.autoReuse = true;
            item.width = 18;
            item.height = 30;
            if (!Main.dedServ)
            {
                item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/tomeofbookpp_Glow");
            }
            item.mana = 10;
            item.shoot = mod.ProjectileType("DoorPP");
            item.shootSpeed = 1;
            item.noMelee = true;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/tomeofbookpp_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
        public override void AddRecipes()
        {
            ModRecipe DUM = new ModRecipe(mod);

            DUM.AddIngredient(ItemID.LunarBrick, 50);
            DUM.AddIngredient(ItemID.WoodenDoor, 10);
            DUM.AddIngredient(ItemID.SpellTome, 1);
            DUM.AddIngredient(ItemID.Ale, 5);
            DUM.AddIngredient(ItemID.FragmentNebula, 70);
            DUM.AddTile(TileID.LunarCraftingStation);
            DUM.SetResult(this);
            DUM.AddRecipe();
        }
    }

    public class DoorPP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drunk Door");
            Main.projFrames[projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            aiType = ProjectileID.Bullet;
            projectile.width = 28;
            projectile.height = 48;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 60 * 3;
        }

        public int dustTimer;
        public int timer;
        private bool runOnce = true;
        private float iceRuneSpeed = 10;
        private Projectile door1;
        private Projectile door2;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.frame = (int)projectile.ai[1];
            dustTimer++;
            timer++;

            if (runOnce)
            {
                float startDistance = 120;

                door1 = Main.projectile[Projectile.NewProjectile(projectile.Center.X + (float)Math.Cos(99) * startDistance, projectile.Center.Y + (float)Math.Sin(6) * startDistance, 0, 0, mod.ProjectileType("DoorPPP"), projectile.damage, 3f, Main.myPlayer)];
                door2 = Main.projectile[Projectile.NewProjectile(player.Center.X + (float)Math.Cos(Math.PI) * startDistance, player.Center.Y + (float)Math.Sin(Math.PI) * startDistance, 0, 0, mod.ProjectileType("DoorPPP"), projectile.damage, 3f, Main.myPlayer)];
                runOnce = false;
            }
            door1.rotation += (float)((2 * Math.PI) / (Math.PI * 7 * 188 / iceRuneSpeed));
            door1.velocity.X = iceRuneSpeed * (float)Math.Cos(door1.rotation) + projectile.velocity.X;
            door1.velocity.Y = iceRuneSpeed * (float)Math.Sin(door1.rotation) + projectile.velocity.Y;

            door2.rotation += (float)((2 * Math.PI) / (Math.PI * 4 * 150 / iceRuneSpeed));
            door2.velocity.X = iceRuneSpeed * (float)Math.Cos(door2.rotation) + projectile.velocity.X;
            door2.velocity.Y = iceRuneSpeed * (float)Math.Sin(door2.rotation) + projectile.velocity.Y;

            if (dustTimer > 5)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("IceRuneDeath"), 0, 0, 0, default(Color), .2f);
                dustTimer = 0;
            }

        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, texture.Size() * .5f, Vector2.One, 0, 0);
            return false;
        }
    }
}
