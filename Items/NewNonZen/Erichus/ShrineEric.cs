﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using ZensTweakstest.Items.NewNonZen.Erichus.Boss;
using Terraria.Graphics.Shaders;
using ZensTweakstest.Items.NewZenStuff.Bosses;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Helper;

namespace ZensTweakstest.Items.NewNonZen.Erichus
{
    public class ShrineEric : ModTile
    {
        private double ERICHUS = 0.00;
        private double ERICHUS1 = 2.06;
        private double ERICHUS2 = 4.13;
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18};
            TileObjectData.addTile(Type);
            dustType = DustID.GreenFairy;
            AddMapEntry(new Color(15, 168, 18));
        }
        /*public override void RandomUpdate(int i, int j)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<ErichusContainment>()))
            {
                if (Main.rand.Next(0, 7) <= 6)
                {
                    int tileHeight = 18;
                        if (Framing.GetTileSafely(i, j).frameX != 0)//WORKS 100%!!!!!!!!!! DO NOT CHANGE THIS LINE
                    i--;//WORKS 100%!!!!!!!!!! DO NOT CHANGE THIS LINE
                        if (Framing.GetTileSafely(i, j).frameY != 0)
                    j -= Framing.GetTileSafely(i, j).frameY / tileHeight;
                    int numberofproj = 16;
                    for (int L = 0; L < numberofproj; L++)
                    {
                        Vector2 spawnPos = new Vector2(i * 16, j * 16) + new Vector2(16, 0);
                        Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));

                        VelPos.Normalize();
                        Projectile.NewProjectile(spawnPos, VelPos * 12f, ModContent.ProjectileType<ErichusBonhus>(), 40, 9f);
                    }
                }//commented beacuse no work?
            }
        }*/
        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = ModContent.ItemType<ShrineEricI>();
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 64, 32, ModContent.ItemType<ShrineEricI>());
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            ERICHUS += 0.01;
            if (ERICHUS >= 6.2)
            {
                ERICHUS = 0.0;
            }

            ERICHUS1 += 0.01;
            if (ERICHUS1 >= 6.2)
            {
                ERICHUS1 = 0.0;
            }

            ERICHUS2 += 0.01;
            if (ERICHUS2 >= 6.2)
            {
                ERICHUS2 = 0.0;
            }

            Tile tile = Framing.GetTileSafely(i, j);
            int height = tile.frameY;// % animationFrameHeight == 36 ? 18 : 16
            float HoverSin = (float)Math.Sin(Main.GameUpdateCount / 20f);
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            Vector2 OffsetShrine = new Vector2(0, -18);
            if (NPC.AnyNPCs(ModContent.NPCType<ErichusContainment>()))
            {
                if (tile.frameX == 0 && tile.frameY == 0)
                {
                    Main.spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.EffectMatrix);
                    spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingBloom"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero + OffsetShrine + new Vector2(-7, -8) + new Vector2(0, HoverSin*3), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingGlow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero + OffsetShrine + new Vector2(0, HoverSin*3) + Vector2.One.RotatedBy(ERICHUS) * 5, null, Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingGlow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero + OffsetShrine + new Vector2(0, HoverSin*3) + Vector2.One.RotatedBy(ERICHUS1) * 5, null, Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingGlow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero + OffsetShrine + new Vector2(0, HoverSin*3) + Vector2.One.RotatedBy(ERICHUS2) * 5, null, Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    Main.spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.EffectMatrix);
                    spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/Thing"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero + OffsetShrine + new Vector2(0, HoverSin*3), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);//new Rectangle(tile.frameX, tile.frameY, 16, height)
                }
            }
            return true;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<ErichusContainment>()))
            {
                int tileHeight = 18;
                if (Framing.GetTileSafely(i, j).frameX != 0)//WORKS 100%!!!!!!!!!! DO NOT CHANGE THIS LINE
                    i--;//WORKS 100%!!!!!!!!!! DO NOT CHANGE THIS LINE
                if (Framing.GetTileSafely(i, j).frameY != 0)
                    j -= Framing.GetTileSafely(i, j).frameY / tileHeight;
                Lighting.AddLight(new Vector2(i * 16, j * 16) + new Vector2(16, 0), Color.Green.ToVector3());
            }
        }
    }
    public class ShrineEricI : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ominous Shrine");
            Tooltip.SetDefault("A ominous shrine, perhaps if ofering are gifted something will happen?" +
                "\nIts a boss summon idiot.");
        }
        public override void SetDefaults()
        {
            item.width = 32;//change
            item.height = 30;//change
            item.value = Item.sellPrice(0, 15, 5, 5);
            item.rare = ItemRarityID.Pink;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.maxStack = 99;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = ModContent.TileType<ShrineEric>();
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.mod == "Terraria" && line.Name == "Tooltip1")
            { // replace # to the tooltip number in Tooltip.SetDefaults(), 0 is the first tooltip
              // we end and begin a Immediate spriteBatch for shader
                Vector2 messageSize = Helplul.MeasureString(line.text);
                Rectangle rec = new Rectangle(line.X - 40, line.Y - 2, (int)messageSize.X + 88, (int)messageSize.Y);
                Main.spriteBatch.BeginImmediate(true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), rec, Color.Black);
                Main.spriteBatch.BeginImmediate(true, true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                Color color = Helplul.CycleColor(Color.Lime, Color.Green);
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
                Color color = Helplul.CycleColor(Color.Lime, Color.Green);
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
    public class SinProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;

            projectile.aiStyle = 0;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<Loot.Toxiblast>(), 0, 0);
        }
        public override void AI()
        {
            float HoverSin = (float)Math.Sin(Main.GameUpdateCount / 20f);
            Dust dust = Dust.NewDustPerfect(projectile.Center + new Vector2(HoverSin * 57, 0), DustID.GreenFairy, new Vector2(0, 0));
            float HoverSin2 = (float)Math.Sin(Main.GameUpdateCount / 2f);
            Dust dust2 = Dust.NewDustPerfect(projectile.Center + new Vector2(HoverSin2 * 25, 0), DustID.BlueFairy, new Vector2(0, 0));
            float HoverSin3 = (float)Math.Sin(Main.GameUpdateCount / 7f);
            Dust dust3 = Dust.NewDustPerfect(projectile.Center + new Vector2(HoverSin3 * 40, 0), DustID.PinkFairy, new Vector2(0, 0));
        }
    }
    public class OminousFlesh : ModItem
    {
        private double ERICHUS = 0.00;
        private double ERICHUS1 = 2.06;
        private double ERICHUS2 = 4.13;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Looks tasty?" +
                "\nA perfect offering.");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 32;
            item.rare = ItemRarityID.Pink;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item2;
            item.consumable = true;
            item.maxStack = 20;
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
                Color color = Helplul.CycleColor(Color.Lime, Color.Green);
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
                Color color = Helplul.CycleColor(Color.Lime, Color.Green);
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
        public override bool UseItem(Player player)
        {
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !NPC.AnyNPCs(ModContent.NPCType<ErichusContainment>()) && !Main.dayTime)
            {
                for (int x = 0; x < Main.maxTilesX; x++)
                {
                    for (int y = 0; y < Main.maxTilesY; y++)
                    {
                        if (Framing.GetTileSafely(x, y).active() && (Framing.GetTileSafely(x, y).type == ModContent.TileType<ShrineEric>()) && (Framing.GetTileSafely(x, y).frameX == 18) && (Framing.GetTileSafely(x, y).frameY == 0))
                        {
                            Vector2 Spawn = new Vector2(x * 16 + 8, y * 16 - 16);
                            if (Vector2.Distance(player.position, Spawn) <= 200)
                            {
                                Main.NewText("You feel like your going to faint.", 255, 1, 0, false);
                                NPC.NewNPC((int)Spawn.X, (int)Spawn.Y, ModContent.NPCType<ErichusContainment>());
                                player.CameraShake(23, 50);
                            }
                            return true;
                        }
                    }
                }
                return false;
            }
            return false;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            ERICHUS += 0.01;
            if (ERICHUS >= 6.2)
            {
                ERICHUS = 0.0;
            }

            ERICHUS1 += 0.01;
            if (ERICHUS1 >= 6.2)
            {
                ERICHUS1 = 0.0;
            }

            ERICHUS2 += 0.01;
            if (ERICHUS2 >= 6.2)
            {
                ERICHUS2 = 0.0;
            }
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.UIScaleMatrix);
            spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingBloom"), position + new Vector2(-7, -8), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingGlow"), position + Vector2.One.RotatedBy(ERICHUS) * 5, null, Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingGlow"), position + Vector2.One.RotatedBy(ERICHUS1) * 5, null, Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingGlow"), position + Vector2.One.RotatedBy(ERICHUS2) * 5, null, Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
            return true;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            ERICHUS += 0.01;
            if (ERICHUS >= 6.2)
            {
                ERICHUS = 0.0;
            }

            ERICHUS1 += 0.01;
            if (ERICHUS1 >= 6.2)
            {
                ERICHUS1 = 0.0;
            }

            ERICHUS2 += 0.01;
            if (ERICHUS2 >= 6.2)
            {
                ERICHUS2 = 0.0;
            }
            Texture2D texture = ModContent.GetTexture("ZensTweakstest/Items/NewNonZen/Erichus/ThingGlow");
            Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture.Height * 0.5f + 2f) - new Vector2(15, 16);
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingBloom"), position + new Vector2(-7, -8), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingGlow"), position + Vector2.One.RotatedBy(ERICHUS) * 5, null, Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingGlow"), position + Vector2.One.RotatedBy(ERICHUS1) * 5, null, Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingGlow"), position + Vector2.One.RotatedBy(ERICHUS2) * 5, null, Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            return true;
        }
    }
}
