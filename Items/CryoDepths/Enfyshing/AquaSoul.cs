using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QwertysRandomContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Helper;

namespace ZensTweakstest.Items.CryoDepths.Enfyshing
{
    public class AquaSoul : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 10;
            npc.height = 10;
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

            npc.HitSound = SoundID.NPCHit35;
            npc.DeathSound = SoundID.NPCDeath39;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            //Reset the counter every 130 ticks
            if (npc.frameCounter >= 20)
                npc.frameCounter = 0;

            //Swap the frame used every 5 ticks
            if (npc.frameCounter < 5)
                npc.frame.Y = 0 * frameHeight;//1

            else if (npc.frameCounter < 10)
                npc.frame.Y = 1 * frameHeight;//2

            else if (npc.frameCounter < 15)
                npc.frame.Y = 2 * frameHeight;//3

            else if (npc.frameCounter < 20)
                npc.frame.Y = 3 * frameHeight;//4
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.GetModPlayer<Charred_Life>().CryoSpace)
            {
                return 4f;
            }
            else
            {
                return 0.0f;
            }
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            npc.TargetClosest();  //Get a target
            Vector2 direction = npc.DirectionTo(player.Center + new Vector2(0, -25));  //Get a direction to the player from the NPC
            npc.velocity += direction * 10f / 60f;//SPEEEED
            if (npc.velocity.LengthSquared() > 10 * 10)
                npc.velocity = Vector2.Normalize(npc.velocity) * 10;
            npc.rotation = npc.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(1,100) == 99)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<JellyMembrane>(), 1);
            }
        }
        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }
    }
    public class GlobalSoulsNPC : GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.GetModPlayer<Charred_Life>().CryoSpace)
            {
                pool[NPCID.BlueJellyfish] = 12;
            }
        }
    }
    public class AquaSoulProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 46;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.aiStyle = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
            projectile.velocity += new Vector2(0, -0.1f);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
