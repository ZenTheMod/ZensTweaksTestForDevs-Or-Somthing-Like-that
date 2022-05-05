using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Items.Slime.Projectiles;
using ZensTweakstest.Items.Slime.Dusts;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using ZensTweakstest.Items.NewZenStuff.Bosses;
using System.Collections.Generic;
using ZensTweakstest.Items.NewNonZen;
using ZensTweakstest.Items.NewNonZen.Erichus.Boss;
using System;
using ZensTweakstest.Items.Nature;
using ZensTweakstest.Config;
using ZensTweakstest.Items.HMmechZen;
using Terraria.GameInput;
using Terraria.DataStructures;
using ZensTweakstest.Items.buffs;

namespace ZensTweakstest.Items
{
    public class Charred_Life : ModPlayer
    {
        public bool ichorpet;
        public int VoidSheathCoolDown = 0;
        public bool SlimeSetBounes;
        public bool AllZenSet;
        public bool Zenpet;
        public bool BunnyVoid;
        public bool EternalVoid;
        public bool EternalVoidTrail;
        public bool RubleTrail;
        public const int maxcharredlife = 10;
        public int charredlife;
        public bool DemonSoul;
        public bool ZenZone;
        public bool VoidSheathEQ;
        public bool CryoSpace;
        public override void CopyCustomBiomesTo(Player other)
        {
            Charred_Life modOther = other.GetModPlayer<Charred_Life>();
            modOther.ZenZone = ZenZone;
        }
        public override void GetDyeTraderReward(List<int> dyeItemIDsPool)
        {
            dyeItemIDsPool.Add(ModContent.ItemType<MovingMangoDye>());
            dyeItemIDsPool.Add(ModContent.ItemType<StrokeDye>());
            dyeItemIDsPool.Add(ModContent.ItemType<ZenDye>());
            dyeItemIDsPool.Add(ModContent.ItemType<GlyphDye>());
        }
        public override void FrameEffects()
        {
            if (EternalVoid || EternalVoidTrail)
            {
                player.armorEffectDrawShadow = true;
                player.armorEffectDrawOutlines = true;
            }
            if (RubleTrail)
            {
                player.armorEffectDrawShadow = true;
                //player.armorEffectDrawOutlines = true;
            }
        }
        public override void UpdateBiomes()
        {
            #region VoidSheathCooldown
            if (VoidSheathCoolDown >= 1)
            {
                VoidSheathCoolDown--;
            }
            #endregion
            ZenZone = ZenWorld.ZenTiles > 150;

            CryoSpace = ZenWorld.CryoTiles > 50;

            //I put this here because this hook updates every tick so.

            if (NPC.AnyNPCs(ModContent.NPCType<ErichusContainment>()))
            {
                Filters.Scene.Activate("ZensTweakstest:Eric");
            }
            else
            {
                Filters.Scene.Deactivate("ZensTweakstest:Eric");
            }

            if (NPC.AnyNPCs(ModContent.NPCType<SparkGaurdian>()))
            {
                Filters.Scene.Activate("ZensTweakstest:SparkGaurdian");
            }
            else
            {
                Filters.Scene.Deactivate("ZensTweakstest:SparkGaurdian");
            }

            if (ZenZone)
            {
                Filters.Scene.Activate("ZensTweakstest:BiomeZenFilter");
                if (!Main.dayTime)
                {
                    player.AddBuff(ModContent.BuffType<Peace>(), 10, true);
                }
            }
            else
            {
                Filters.Scene.Deactivate("ZensTweakstest:BiomeZenFilter");
            }

            if (NPC.AnyNPCs(ModContent.NPCType<ZenMechPortal>()))
            {
                Filters.Scene.Activate("ZensTweakstest:Portal");
                Filters.Scene["ZensTweakstest:Portal"].GetShader().UseColor(Main.DiscoR / 255f, Main.DiscoG / 255f, Main.DiscoB / 255f).UseOpacity(0.3f);
            }
            else
            {
                Filters.Scene.Deactivate("ZensTweakstest:Portal");
            }
        }
        /*if(anyNPC)
{
    if(!activatedFilter)
    {
        activateFilter();
    }
}
else
{
    if(activatedFilter)
    {
        DesactivateFilter();
    }
}*/
        public override Texture2D GetMapBackgroundImage()
        {
            if (ZenZone)
            {
                return mod.GetTexture("ZenMapBG");
            }
            return null;
        }
        public override bool CustomBiomesMatch(Player other)
        {
            Charred_Life modOther = other.GetModPlayer<Charred_Life>();
            return ZenZone == modOther.ZenZone;
            // If you have several Zones, you might find the &= operator or other logic operators useful:
            // bool allMatch = true;
            // allMatch &= ZoneExample == modOther.ZoneExample;
            // allMatch &= ZoneModel == modOther.ZoneModel;
            // return allMatch;
            // Here is an example just using && chained together in one statemeny 
            // return ZoneExample == modOther.ZoneExample && ZoneModel == modOther.ZoneModel;
        }
        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = ZenZone;
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            ZenZone = flags[0];
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (ZensTweakstest.SheathHotkey.JustPressed && VoidSheathEQ == true && VoidSheathCoolDown <= 0)
            {
                Main.PlaySound(SoundID.NPCDeath43, player.position);
                int numberofproj = 8;
                VoidSheathCoolDown = 90;
                for (int i = 0; i < numberofproj; i++)
                {
                    Vector2 spawnPos = player.Center;
                    Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));

                    //VoidSheathRing

                    VelPos.Normalize();
                    Projectile.NewProjectile(spawnPos, VelPos * 8f, ModContent.ProjectileType<ZenFlameSheath>(), 45, 5f, Main.myPlayer);
                }
                Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<VoidSheathRing>(), 0, 0f, Main.myPlayer);
                for (int f = 0; f < 5; f++)
                {
                    int goreIndex = goreIndex = Gore.NewGore(new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), mod.GetGoreSlot("Gores/ZenTreeFX"), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), mod.GetGoreSlot("Gores/ZenTreeFX"), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), mod.GetGoreSlot("Gores/ZenTreeFX"), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), mod.GetGoreSlot("Gores/ZenTreeFX"), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }
            }
            if (ZensTweakstest.SheathHotkey.JustPressed && VoidSheathEQ == true && VoidSheathCoolDown >= 1)
            {
                Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<VoidSheathRing>(), 0, 0f, Main.myPlayer);
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/FailedHit").WithPitchVariance(.02f), player.position);
            }
        }
        public override void ResetEffects()
        {
            SlimeSetBounes = false;
            RubleTrail = false;
            AllZenSet = false;
            EternalVoid = false;
            EternalVoidTrail = false;
            ichorpet = false;
            Zenpet = false;
            BunnyVoid = false;
            player.statLifeMax2 += charredlife * 5;
            VoidSheathEQ = false;
        }
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write(charredlife);
            packet.Send(toWho, fromWho);
        }
        public override TagCompound Save()
        {
            return new TagCompound
            {
                {"charredlife",charredlife }
            };
        }
        public override void Load(TagCompound tag)
        {
            charredlife = tag.GetInt("charredlife");
        }
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (SlimeSetBounes && !player.dead)
            {
                if (Main.rand.Next(0, 17) == 4)
                {
                    Vector2 speed = new Vector2(0, -10);
                    for (int i = 0; i < 15; i++)
                    {
                        speed = speed.RotatedByRandom(MathHelper.ToRadians(90));
                        Projectile.NewProjectile(player.Top, speed, ModContent.ProjectileType<SlimeSpike>(), 3, 1, Main.myPlayer);
                    }
                }
            }
        }
        public override bool ModifyNurseHeal(NPC nurse, ref int health, ref bool removeDebuffs, ref string chatText)
        {
            /*if (ModContent.GetInstance<SpriteSettings>().NurseHealDisableZenBosses)
            {

            }*/
            if (NPC.AnyNPCs(ModContent.NPCType<ErichusContainment>()))
            {
                chatText = "Seriously??";
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<SparkGaurdian>()))
            {
                chatText = "Seriously??";
                return false;
            }
            return base.ModifyNurseHeal(nurse, ref health, ref removeDebuffs, ref chatText);
        }
            public static readonly PlayerLayer MiscEffectsBack = new PlayerLayer("ZensTweakstest", "MiscEffectsBack", PlayerLayer.MiscEffectsBack, delegate (PlayerDrawInfo drawInfo)
            {

                Player drawPlayer = drawInfo.drawPlayer;
                Mod mod = ModLoader.GetMod("ZensTweakstest");
                Charred_Life modPlayer = drawPlayer.GetModPlayer<Charred_Life>();

                Texture2D texture = mod.GetTexture("Void");

                int drawX = (int)(drawInfo.position.X - Main.screenPosition.X + 10);
                int drawY = (int)(drawInfo.position.Y - Main.screenPosition.Y - 10);

                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, Color.White, 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);

                Main.playerDrawData.Add(data);

            });
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if (EternalVoid)
                MiscEffectsBack.visible = true;
            else
                MiscEffectsBack.visible = false;
            layers.Insert(0, MiscEffectsBack);
        }
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (SlimeSetBounes)
            {
                if (Main.rand.Next(0, 12) == 4)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, ModContent.DustType<SlimeDust>(), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1.3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
            }
            if (VoidSheathEQ == true)
            {
                if (Main.rand.Next(0, 4) == 3)
                {
                    Dust dust = Dust.NewDustDirect(player.Center, 0, player.height, DustID.Firework_Red);
                    Dust dust2 = Dust.NewDustDirect(player.Center + new Vector2(15, 0), 0, player.height, DustID.Firework_Red);
                    Dust dust3 = Dust.NewDustDirect(player.Center - new Vector2(15, 0), 0, player.height, DustID.Firework_Red);
                }
            }
            if (RubleTrail)
            {
                /*float HoverSin = (float)Math.Sin(Main.GameUpdateCount / 5f);
                if (player.velocity != new Vector2(0, 0))
                {
                    Dust dustT = Dust.NewDustPerfect(Vector2.Normalize(player.velocity).RotatedBy(HoverSin) * new Vector2(20, 30), DustID.PinkFairy, new Vector2(0, 0));
                    Dust dusty = Dust.NewDustPerfect(player.Center + new Vector2(0, HoverSin * 30), DustID.GreenFairy, new Vector2(0, 0));
                    Dust dustx = Dust.NewDustPerfect(player.Center + new Vector2(HoverSin * 20, 0), DustID.GreenFairy, new Vector2(0, 0));
                }*/
                float HoverSin = (float)Math.Sin(Main.GameUpdateCount / 5f);
                if (player.velocity != new Vector2(0, 0))
                {
                    int maxSeparation = 2 * 16;
                    Vector2 offset = Vector2.Normalize(player.velocity.RotatedBy(MathHelper.PiOver2)) * HoverSin * maxSeparation;
                    Vector2 dustVelocity = offset / 16;
                    Dust dusty = Dust.NewDustPerfect(player.Center + offset, DustID.GreenFairy, -dustVelocity);
                    Dust dustx = Dust.NewDustPerfect(player.Center - offset, DustID.GreenFairy, dustVelocity);
                }

                Vector2 vector = new Vector2(Main.rand.Next(-5, 5) * (0.003f * 40 - 10), Main.rand.Next(-5, 5) * (0.003f * 40 - 10));
                Dust chargeDust = Main.dust[Dust.NewDust(player.Center + vector, 1, 1, 74, 0, 0, 255, Color.Green, 0.8f)];//ModContent.DustType<EDust>()
                chargeDust.velocity = -vector / 12;
                chargeDust.velocity -= player.velocity / 8;
                chargeDust.noLight = true;
                chargeDust.noGravity = true;
            }
            if (AllZenSet)
            {
                if (Main.rand.Next(0, 12) == 4)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, ModContent.DustType<ZenStoneDust>(), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1.3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
            }
        }

        public override void UpdateLifeRegen()
        {
            if (EternalVoid)
            {
                player.lifeRegen += 3;
            }
        }
        public override void PreUpdate()
        {
            Point left = player.TileCoordsBottomLeft();
            Point right = player.TileCoordsBottomRight();

            if (player.whoAmI == Main.myPlayer && player.velocity.Y >= 0 && (StuffForStuff.SolidTopTile(left.X, left.Y) || StuffForStuff.SolidTopTile(right.X, right.Y)))
                TileFloor(left, right, Framing.GetTileSafely(left.X, left.Y).type, Framing.GetTileSafely(right.X, right.Y).type);
        }
        private void TileFloor(Point left, Point right, int lType, int rType)
        {
            bool lValid = lType == ModContent.TileType<BounceShroom>() && StuffForStuff.SolidTopTile(left.X, left.Y);
            bool rValid = rType == ModContent.TileType<BounceShroom>() && StuffForStuff.SolidTopTile(right.X, right.Y);
            if (lValid || rValid)
            {
                Player player = Main.player[Main.myPlayer];
                if (!player.controlDown)
                    Main.PlaySound(SoundID.NPCHit1, player.position);//
                float newVel = -10f;
                if (player.controlJump) //Bigger jump if jumping
                    newVel = -13.5f;
                if (player.controlDown) //NO jump if holding down
                    return;
                player.velocity.Y = newVel;

                int offsetX = 0;
                int offsetY = 0;
                if (lValid)
                {
                    offsetX = left.X - Framing.GetTileSafely(left.X, left.Y).frameX / 18;
                    offsetY = left.Y - Framing.GetTileSafely(left.X, left.Y).frameY / 18;
                }
                else if (rValid)
                {
                    offsetX = right.X - Framing.GetTileSafely(right.X, right.Y).frameX / 18;
                    offsetY = right.Y - Framing.GetTileSafely(right.X, right.Y).frameY / 18;
                }

                for (int i = 0; i < 3; ++i)
                {
                    for (int j = 0; j < 2; ++j)
                    {
                        if (Main.rand.Next(5) <= 3 && j == 0)
                        {
                            int type = Main.rand.Next(2) == 0 ? mod.GetGoreSlot("Gores/BounceShroomGore2") : mod.GetGoreSlot("Gores/BounceShroomGore");
                            Gore.NewGore((new Vector2(offsetX + i, offsetY + j) * 16) + new Vector2(Main.rand.Next(18), Main.rand.Next(18)), Vector2.Zero, type);
                            //Dust.NewDust(player.position, player.width, player.height, 15, 0f, 0f, 150, default(Color), 1.1f);
                            for (int D = 0; D < 5; ++D)
                            {
                                Dust.NewDust(new Vector2(offsetX + i, offsetY + j) * 16, 20, 10, DustID.GlowingMushroom);
                            }
                        }
                    }
                }
            }
        }
    }
    public class StuffForStuff
    {
        public static bool SolidTopTile(int i, int j) => Framing.GetTileSafely(i, j).active() && (Main.tileSolidTop[Framing.GetTileSafely(i, j).type] || Main.tileSolid[Framing.GetTileSafely(i, j).type]);
    }
    public class VoidSheathRing : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.timeLeft = 60;
            projectile.width = 260;
            projectile.height = 260;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.aiStyle = -1;
            projectile.scale = 0.1f;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        private float scale = 0.1f;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            projectile.alpha += 10;
            scale += 0.1f;
            Texture2D Portal = mod.GetTexture("Items/VoidSheathRing");
            spriteBatch.Draw(Portal, projectile.Center - Main.screenPosition, null, lightColor * (1f - projectile.alpha / 255f), 0, new Vector2(Portal.Width / 2f,Portal.Height / 2f), scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}