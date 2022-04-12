using System;
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
using System.IO;

namespace ZensTweakstest.Items.NewNonZen.Cards
{
    public class ThrowingCards : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 6));
            Tooltip.SetDefault(@"Fire at random!
Hearts - Life Steal, Diamonds - Bounce and Peirce, Clubs - Fire Multiple - and - Spades - Homeing Ai.");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 32;
            item.damage = 200;
            item.rare = ItemRarityID.Cyan;
            item.thrown = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 10;
            item.autoReuse = true;
            item.useTime = 10;
            item.UseSound = SoundID.Item39;
            item.shootSpeed = 10f;
            item.shoot = ModContent.ProjectileType<HeartsCard>();
            item.noUseGraphic = true;
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
            int randomType = Main.rand.Next(new int[] { ModContent.ProjectileType<HeartsCard>(), ModContent.ProjectileType<DiamondsCard>(), ModContent.ProjectileType<ClubsCard>(), ModContent.ProjectileType<SpadesCard>() });
            type = randomType;
            if (type == ModContent.ProjectileType<ClubsCard>())
            {
                float numberProjectiles = 3;
                float rotation = MathHelper.ToRadians(10);
                position += Vector2.Normalize(new Vector2(speedX, speedY)) * 10f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * item.shootSpeed;
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
                return false;
            }
            else
            {
                if (type == ModContent.ProjectileType<SpadesCard>())
                {
                    damage = damage - 30;
                }
                return true;
            }
        }
    }
    public class HeartsCard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 6;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player p = Main.player[projectile.owner];
            int healingAmount = damage / 30; //decrease the value 30 to increase heal, increase value to decrease. Or you can just replace damage/x with a set value to heal, instead of making it based on damage.
            p.statLife += healingAmount;
            p.HealEffect(healingAmount, true);
        }
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 300;

            //1: projectile.penetrate = 1; // Will hit even if npc is currently immune to player
            //2a: projectile.penetrate = -1; // Will hit and unless 3 is use, set 10 ticks of immunity
            //2b: projectile.penetrate = 3; // Same, but max 3 hits before dying
            //5: projectile.usesLocalNPCImmunity = true;
            //5a: projectile.localNPCHitCooldown = -1; // 1 hit per npc max
            //5b: projectile.localNPCHitCooldown = 20; // o
            projectile.aiStyle = 1;
        }
        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.WhiteTorch, new Vector2(0, 0));
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.WhiteTorch);
            }
        }
    }
    public class DiamondsCard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 300;

            //1: projectile.penetrate = 1; // Will hit even if npc is currently immune to player
            //2a: projectile.penetrate = -1; // Will hit and unless 3 is use, set 10 ticks of immunity
            //2b: projectile.penetrate = 3; // Same, but max 3 hits before dying
            //5: projectile.usesLocalNPCImmunity = true;
            //5a: projectile.localNPCHitCooldown = -1; // 1 hit per npc max
            //5b: projectile.localNPCHitCooldown = 20; // o
            projectile.aiStyle = 1;
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
        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.WhiteTorch, new Vector2(0, 0));
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.WhiteTorch);
            }
        }
    }
    public class ClubsCard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 300;

            //1: projectile.penetrate = 1; // Will hit even if npc is currently immune to player
            //2a: projectile.penetrate = -1; // Will hit and unless 3 is use, set 10 ticks of immunity
            //2b: projectile.penetrate = 3; // Same, but max 3 hits before dying
            //5: projectile.usesLocalNPCImmunity = true;
            //5a: projectile.localNPCHitCooldown = -1; // 1 hit per npc max
            //5b: projectile.localNPCHitCooldown = 20; // o
            projectile.aiStyle = 1;
        }
        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.WhiteTorch, new Vector2(0, 0));
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.WhiteTorch);
            }
        }
    }
    public class SpadesCard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.timeLeft = 300;

            //1: projectile.penetrate = 1; // Will hit even if npc is currently immune to player
            //2a: projectile.penetrate = -1; // Will hit and unless 3 is use, set 10 ticks of immunity
            //2b: projectile.penetrate = 3; // Same, but max 3 hits before dying
            //5: projectile.usesLocalNPCImmunity = true;
            //5a: projectile.localNPCHitCooldown = -1; // 1 hit per npc max
            //5b: projectile.localNPCHitCooldown = 20; // o
            projectile.aiStyle = -1;
        }
        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.WhiteTorch, new Vector2(0, 0));
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
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
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.WhiteTorch);
            }
        }
    }
}
