using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using ZensTweakstest.Items.Dusts;

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
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("SparkGaurdian"));
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
    }
}
