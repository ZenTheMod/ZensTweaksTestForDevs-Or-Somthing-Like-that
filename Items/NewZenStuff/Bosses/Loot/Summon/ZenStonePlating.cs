using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Helper;
using Terraria.Graphics.Shaders;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.Summon
{
    public class ZenStonePlating : ModItem
    {
        private int frameCounter = 0;
        private int frame = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zerg Basin");
            Tooltip.SetDefault("Looks cursed" +
                "\nDisturbs Peace when In Hell");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
        }
        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 76;
            item.rare = 8;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item53;
            item.consumable = true;
            item.maxStack = 20;
        }

        public override bool UseItem(Player player)
        {
            if ((player.ZoneUnderworldHeight) && !NPC.AnyNPCs(mod.NPCType("SparkGaurdian")))
            {
                for (int i = 0; i < (Main.expertMode ? 55 : 50); i++)
                {
                    Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<ZenStoneDust>(), player.velocity.X * 0.5f, player.velocity.Y * 0.5f);
                }
                Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<BossSparkle>(), player.velocity.X * 0.5f, player.velocity.Y * 0.5f);
                Main.NewText("You See A Sparkle In The Distance...", 127, 36, 64, false);
                CombatText.NewText(player.Hitbox, new Color(127, 36, 64), "You See A Sparkle In The Distance...", true, false);
                Projectile.NewProjectile(player.position, new Vector2(0, -2), ModContent.ProjectileType<PortalS>(), 0, 0);
                Main.PlaySound(SoundID.Roar, player.position, 0);
                return true;
            }
            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frameI, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            // change to match your animated texture of choice
            Texture2D texture = ModContent.GetTexture("ZensTweakstest/Items/NewZenStuff/Bosses/Loot/Summon/ZenStonePlatingAni");
            int timeFramesPerAnimationFrame = 5;
            int totalAnimationFrames = 8;

            Vector2 PositionNEG = new Vector2(-9, -19);

            spriteBatch.Draw(texture, position, item.GetCurrentFrame(ref frame, ref frameCounter, timeFramesPerAnimationFrame, totalAnimationFrames), Color.White, 0f, origin - PositionNEG, scale + 0.29f, SpriteEffects.None, 0);
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            // change to match your animated texture of choice
            Texture2D texture = ModContent.GetTexture("ZensTweakstest/Items/NewZenStuff/Bosses/Loot/Summon/ZenStonePlatingAni");
            int timeFramesPerAnimationFrame = 5;
            int totalAnimationFrames = 8;

            spriteBatch.Draw(texture, item.position - Main.screenPosition, item.GetCurrentFrame(ref frame, ref frameCounter, timeFramesPerAnimationFrame, totalAnimationFrames), lightColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            return false;
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.mod == "Terraria" && line.Name == "Tooltip0")
            { // replace # to the tooltip number in Tooltip.SetDefaults(), 0 is the first tooltip
              // we end and begin a Immediate spriteBatch for shader
                Vector2 messageSize = Helplul.MeasureString(line.text);
                Rectangle rec = new Rectangle(line.X - 40, line.Y - 2, (int)messageSize.X + 88, (int)messageSize.Y);
                Main.spriteBatch.BeginImmediate(true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), rec, Color.Black);
                Main.spriteBatch.BeginImmediate(true, true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                Color color = Helplul.CycleColor(Color.Red, Color.MidnightBlue);
                Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), rec, color);
                Main.spriteBatch.BeginImmediate(true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                // redraw the tooltip
                Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1);
                // we end and begin a deffered spriteBatch so it doesnt break everything
                Main.spriteBatch.BeginImmediate(true, true);
                // return false so vanilla doesnt draw it
                return false;
            }
            // this is the item name redrawing
            if (line.mod == "Terraria" && line.Name == "ItemName")
            {
                // we calculate the rectangle 
                Vector2 messageSize = Helplul.MeasureString(line.text);
                Rectangle rec = new Rectangle(line.X - 40, line.Y - 2, (int)messageSize.X + 88, (int)messageSize.Y);
                // we end and begin a Immediate spriteBatch for shader
                Main.spriteBatch.BeginImmediate(true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), rec, Color.Black);
                Main.spriteBatch.BeginImmediate(true, true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                Color color = Helplul.CycleColor(Color.Red, Color.MidnightBlue);
                Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), rec, color);
                // we end and begin a deffered spriteBatch so it doesnt break everything
                Main.spriteBatch.BeginImmediate(true, true);
                // return false so vanilla doesnt draw it
                Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(ItemID.TwilightDye), item, null); //use living rainbow dye shader
                Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1); //draw the tooltip manually
                Main.spriteBatch.End(); //then end and begin again to make remaining tooltip lines draw in the default way
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            // any other line just draw normally
            return true;
        }
    }
    public class PortalS : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.tileCollide = false;
            projectile.width = 256;
            projectile.height = 256;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            return true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
        }
        int tim = 0;
        public override void AI()
        {
            projectile.rotation += 0.3f;
            if (tim == 0)
            {
                projectile.alpha = 255;
            }
            tim++;
            if (projectile.alpha != 0)
            {
                projectile.alpha--;
            }
            else if (tim == 300)
            {
                NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, ModContent.NPCType<SparkGaurdian>());
                projectile.Kill();
                for (int f = 0; f < 5; f++)
                {
                    int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }
            }
        }
    }
}
