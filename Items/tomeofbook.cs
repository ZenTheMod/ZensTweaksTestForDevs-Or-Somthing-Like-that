using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QwertysRandomContent;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Dusts;

namespace ZensTweakstest.Items
{
    public class tomeofbook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tome of Infinite Doors");
            Tooltip.SetDefault("An open wound is a parasites door to your blood stream.");
        }

        public override void SetDefaults()
        {
            item.damage = 360;
            item.magic = true;

            item.useTime = 20;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.knockBack = 100;
            item.value = Item.buyPrice(gold: 15);
            item.value = Item.sellPrice(gold: 15);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item102;
            item.autoReuse = true;
            item.width = 18;
            item.height = 30;
            if (!Main.dedServ)
            {
                item.GetGlobalItem<ItemUseGlow>().glowTexture =  mod.GetTexture("Items/tomeofbook_Glow");
            }
            item.mana = 10;
            item.shoot = mod.ProjectileType("DoorP");
            item.shootSpeed = 20;
            item.noMelee = true;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/tomeofbook_Glow");
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

            DUM.AddIngredient(ItemID.LunarBrick, 5);
            DUM.AddIngredient(ItemID.WoodenDoor, 1);
            DUM.AddIngredient(ItemID.SpellTome, 1);
            DUM.AddIngredient(ItemID.FragmentNebula, 20);
            DUM.AddTile(TileID.LunarCraftingStation);
            DUM.SetResult(this);
            DUM.AddRecipe();
        }
    }

    public class DoorP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Door");
        }

        public override void SetDefaults()
        {
            projectile.width = 88;
            projectile.height = 88;

            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.penetrate = 5;
            projectile.ranged = true;
            projectile.light = 1;
            projectile.timeLeft = 425;
        }
        public override void AI()
        {
            projectile.velocity.Y += projectile.ai[0];
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<DoorPinkPar>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                projectile.ai[0] += 0.1f;
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                projectile.velocity *= 0.75f;
                Main.PlaySound(SoundID.Item10, projectile.position);
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<DoorPinkPar>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
            Main.PlaySound(SoundID.Item118, projectile.position);
        }
    }
    public class DoorZen : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Door Of Zen");
        }

        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 48;

            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.penetrate = 5;
            projectile.ranged = true;
            projectile.light = 1;
            projectile.timeLeft = 425;
        }
        public override void AI()
        {
            projectile.velocity.Y += projectile.ai[0];
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<ZenStoneDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                projectile.ai[0] += 0.1f;
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                projectile.velocity *= 0.75f;
                Main.PlaySound(SoundID.Item10, projectile.position);
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<ZenStoneDust>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
            Main.PlaySound(SoundID.Item118, projectile.position);
        }
    }
}
