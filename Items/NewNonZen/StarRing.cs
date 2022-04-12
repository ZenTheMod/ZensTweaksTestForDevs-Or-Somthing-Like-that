using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QwertysRandomContent;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Items.NewZenStuff.Projectilles;

namespace ZensTweakstest.Items.NewNonZen
{
    public class StarRing : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Ring");
            Tooltip.SetDefault(@"Stars from the Universe and beyond fused into a ring.
It emanates with ancient God essence...
Homes in on enimies does not pierce.");
        }

        public override void SetDefaults()
        {
            item.damage = 200;
            item.magic = true;

            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.knockBack = 100;
            item.value = Item.buyPrice(gold: 15);
            item.value = Item.sellPrice(gold: 13);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item104;
            item.autoReuse = true;
            item.width = 28;
            item.height = 30;
            item.mana = 10;
            item.shoot = ModContent.ProjectileType<StarBound>();
            item.shootSpeed = 15;
            item.noMelee = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine tooltipLine in tooltips)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(182, 230, 99);
                }
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberofproj = 10;
            for (int i = 0; i < numberofproj; i++)
            {
                Vector2 spawnPos = player.Center;
                Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));


                VelPos.Normalize();
                Projectile.NewProjectile(spawnPos, VelPos * 8f, ModContent.ProjectileType<StarBound>(), item.damage, 5f, Main.myPlayer);
            }
            return false;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture.Height * 0.5f + 2f);
            // We redraw the item's sprite 4 times, each time shifted 2 pixels on each direction, using Main.DiscoColor to give it the color changing effect
            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position + offsetPositon, null, Main.DiscoColor, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
            var shader = GameShaders.Misc["ZensTweakstest:LightBow"]; // shader name
            shader.Apply();
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, shader.Shader, Main.GameViewMatrix.TransformationMatrix);
            return true;
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
        }
        // Same as above but for drawing inside the player's inventory
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Main.itemTexture[item.type];
            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position + offsetPositon, null, Main.DiscoColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            var shader = GameShaders.Misc["ZensTweakstest:LightBow"]; // shader name
            shader.Apply();
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, shader.Shader, Main.UIScaleMatrix);
            return true;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
            //Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }
    }
    public class StarBound : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 9;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;        //The recording mode
        }
        public override void SetDefaults()
        {
            projectile.timeLeft = 300;
            projectile.width = 50;
            projectile.height = 50;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Main.DiscoColor;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 55; i++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 212, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 0, Main.DiscoColor);
            }
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation += 0.03f;
            Lighting.AddLight(projectile.position, Color.White.ToVector3() * 2f);

            if (projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref projectile.velocity);
                projectile.localAI[0] = 1f;
            }
            Vector2 move = Vector2.Zero;
            float distance = 400f;
            bool target = false;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {
                    Vector2 newMove = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            if (target)
            {
                AdjustMagnitude(ref move);
                projectile.velocity = (15 * projectile.velocity + move) / 16f;
                AdjustMagnitude(ref projectile.velocity);
            }
        }

        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 12f)
            {
                vector *= 12f / magnitude;
            }
        }
    }
}
