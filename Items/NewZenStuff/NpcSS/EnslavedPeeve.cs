using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.Dusts;
using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace ZensTweakstest.Items.NewZenStuff.NpcSS
{
    public class EnslavedPeeve : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Clustered Zen-Stone Peeve");
            Main.npcFrameCount[npc.type] = 3;
            NPCID.Sets.TrailCacheLength[npc.type] = 7;
            NPCID.Sets.TrailingMode[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 34;
            npc.aiStyle = -1;
            npc.damage = 25;
            npc.defense = 1;
            npc.lifeMax = 250;
            npc.knockBackResist = 0.9f;
            npc.value = 125f;

            npc.buffImmune[BuffID.Burning] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;

            animationType = NPCID.DungeonSpirit;

            npc.HitSound = SoundID.NPCHit35;
            npc.DeathSound = SoundID.NPCDeath39;
        }
        public override void NPCLoot()
        {
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);

            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZenStone_I"), 10);

            if (Main.rand.Next(0, 100) >= 75)
            {
                if (NPC.downedPlantBoss)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZenitrinOre_I"), 15);
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZenStone_I"), 5);
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Zen_Peeve_Essence"), 5);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            for (int num93 = 1; num93 < npc.oldPos.Length; num93++)
            {
                Vector2 drawPos = npc.oldPos[num93] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = npc.GetAlpha(drawColor) * ((float)(NPCID.Sets.TrailCacheLength[npc.type] - num93) / (float)NPCID.Sets.TrailCacheLength[npc.type]);
                spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/NewZenStuff/NpcSS/Souless"), drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            }
            return true;
        }


        public override void AI()
        {
            Player player = Main.player[npc.target];
            if ((npc.life) >= 125)
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                npc.rotation = (npc.Center - player.Center).ToRotation();
                npc.TargetClosest();  //Get a target
                Vector2 direction = npc.DirectionTo(player.Center);  //Get a direction to the player from the NPC
                npc.velocity = direction * 6.5f;
            }
            else
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                npc.rotation = (npc.Center - player.Center).ToRotation();
                npc.TargetClosest();  //Get a target
                Vector2 direction = npc.DirectionTo(player.Center);  //Get a direction to the player from the NPC
                npc.velocity = direction * 10f;
            }
        }
    }
}
