using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.CryoDepths
{
    public class FrozenConstruct : ModItem
    {
        public override void SetDefaults()
        {
            item.width = (item.height = 60);
            item.damage = 27;
            item.melee = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.useAnimation = 55;
            item.useTime = 55;
            item.useTurn = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6.25f;
            item.autoReuse = true;
            item.rare = ItemRarityID.Green;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 15f;
            item.value = Item.sellPrice(gold: 2);
        }
        private float Combo = 1f;
        private int shoot;
        private float Spin;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Combo != -1f && Combo != 1f)
            {
                Combo = 1f;
            }
            shoot++;
            Combo *= -1f;
            if (shoot >= 3)
            {
                Spin = 1f;
                shoot = 0;
            }
            Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), ModContent.ProjectileType<IceSwordSwing>(), damage, knockBack, player.whoAmI, Combo, Spin);
            Spin = 0f;
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<IceSwordSwing>()] <= 0;
        }
    }
    public static class SwordUtilsClass
    {
        public enum EasingType
        {
            Linear,
            PolyInOut,
            ExpOut,
        }
        public struct CurveSegment
        {
            public CurveSegment(EasingType MODE, float ORGX, float ORGY, float DISP, int DEG = 1)
            {
                mode = MODE;
                originX = ORGX;
                originY = ORGY;
                displacement = DISP;
                degree = DEG;
            }
            public EasingType mode;
            public float originX;
            public float originY;
            public float displacement;
            public int degree;
        }
        public static float PiecewiseAnimation(float progress, CurveSegment[] segments)
        {
            if (segments.Length == 0)
            {
                return 0f;
            }
            if (segments[0].originX != 0f)
            {
                segments[0].originX = 0f;
            }
            progress = MathHelper.Clamp(progress, 0f, 1f);
            float ratio = 0f;
            for (int i = 0; i <= segments.Length - 1; i++)
            {
                CurveSegment segment = segments[i];
                float startPoint = segment.originX;
                float endPoint = 1f;
                if (progress >= segment.originX)
                {
                    if (i < segments.Length - 1)
                    {
                        if (segments[i + 1].originX <= progress)
                        {
                            goto IL_454;
                        }
                        endPoint = segments[i + 1].originX;
                    }
                    float segmentLenght = endPoint - startPoint;
                    float segmentProgress = (progress - segment.originX) / segmentLenght;
                    ratio = segment.originY;
                    switch (segment.mode)
                    {
                        case EasingType.Linear:
                            return ratio + segmentProgress * segment.displacement;

                        case EasingType.PolyInOut:
                            return ratio + ((segmentProgress < 0.5f) ? ((float)Math.Pow(2.0, (double)(segment.degree - 1)) * (float)Math.Pow((double)segmentProgress, (double)segment.degree)) : (1f - (float)Math.Pow((double)(-2f * segmentProgress + 2f), (double)segment.degree) / 2f)) * segment.displacement;

                        case EasingType.ExpOut:
                            return ratio + ((segmentProgress == 1f) ? 1f : (1f - (float)Math.Pow(2.0, (double)(-10f * segmentProgress)))) * segment.displacement;
                        default:
                            return ratio;
                    }
                }
            IL_454:;
            }
            return ratio;
        }
        public static Item GetActiveItem(this Player player)
        {
            if (!Main.mouseItem.IsAir)
            {
                return Main.mouseItem;
            }
            return player.HeldItem;
        }
    }
    public class IceSwordSwing : ModProjectile
    {
        public override string Texture
        {
            get
            {
                return "ZensTweakstest/Items/CryoDepths/FrozenConstruct";
            }
        }

        public float SwingDirection
        {
            get
            {
                return projectile.ai[0] * Math.Sign(direction.X);
            }
        }
        public ref float HasFired
        {
            get
            {
                return ref projectile.localAI[0];
            }
        }
        public Vector2 DistanceFromPlayer
        {
            get
            {
                return direction * 30f;
            }
        }
        public float Timer
        {
            get
            {
                return Owner.GetActiveItem().useAnimation - projectile.timeLeft;
            }
        }
        public Player Owner
        {
            get
            {
                return Main.player[projectile.owner];
            }
        }
        public ref float Spin => ref projectile.ai[1];
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Sword");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = (projectile.height = 50);
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 35;
            projectile.timeLeft = 2;
            projectile.melee = true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collisionPoint = 0f;
            float bladeLength = 65f * projectile.scale;
            Vector2 holdPoint = DistanceFromPlayer.Length() * Utils.ToRotationVector2(projectile.rotation);
            return new bool?(Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), Owner.Center + holdPoint, Owner.Center + holdPoint + Utils.ToRotationVector2(projectile.rotation) * bladeLength, 24f, ref collisionPoint));
        }

        internal float SwingRatio()
        {
            return SwordUtilsClass.PiecewiseAnimation(Timer / Owner.GetActiveItem().useAnimation, new SwordUtilsClass.CurveSegment[]
            {
                exponential,
                poly,
                linear
            });
        }

        public override void AI()
        {
            if (!init)
            {
                projectile.timeLeft = Owner.GetActiveItem().useAnimation;
                Main.PlaySound(SoundID.DD2_SonicBoomBladeSlash.WithVolume(4f), projectile.Center);
                direction = projectile.velocity;
                direction.Normalize();
                projectile.rotation = Utils.ToRotation(direction);
                init = true;
                projectile.netUpdate = true;
                projectile.netSpam = 0;
                SwingWidth = Spin == 1f ? 7.5f : 2.5f;
            }
            projectile.Center = Owner.Center + DistanceFromPlayer;
            projectile.rotation = Utils.ToRotation(projectile.velocity) + MathHelper.Lerp(SwingWidth / 2f * SwingDirection, -SwingWidth / 2f * SwingDirection, SwingRatio());
            projectile.scale = 1.5f + (float)Math.Sin((SwingRatio() * 3.1415927f)) * 0.6f * 0.6f;
            HasFired++;
            if (Owner.whoAmI == Main.myPlayer && Spin == 1f && HasFired % 10 == 0)
            {
                Main.PlaySound(SoundID.Item30, Owner.Center);
                Vector2 vel = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                while (vel.X == 0f && vel.Y == 0f)
                {
                    vel = new Vector2(Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                }
                vel.Normalize();
                vel *= (float)Main.rand.Next(70, 101) * 0.1f;
                Projectile ice = Projectile.NewProjectileDirect(Owner.Center + direction * 5f, vel, ModContent.ProjectileType<IceSwordProjectile>(), (int)(projectile.damage * 0.5f), projectile.knockBack, projectile.owner);
                ice.friendly = true;
                ice.hostile = false;
            }
            Owner.heldProj = projectile.whoAmI;
            Owner.direction = Math.Sign(projectile.velocity.X);
            Owner.itemRotation = projectile.rotation;
            if (Owner.direction != 1)
            {
                Owner.itemRotation -= 3.1415927f;
            }
            Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D sword = ModContent.GetTexture("ZensTweakstest/Items/CryoDepths/FrozenConstruct");
            SpriteEffects flip = (SpriteEffects)((Owner.direction < 0) ? 1 : 0);
            float extraAngle = (Owner.direction < 0) ? 1.5707964f : 0f;
            float drawAngle = projectile.rotation;
            float drawRotation = projectile.rotation + 0.7853982f + extraAngle;
            Vector2 drawOrigin = new Vector2((Owner.direction < 0) ? (sword.Width) : 0f, sword.Height);
            Vector2 drawOffset = Owner.Center + Utils.ToRotationVector2(drawAngle) * 10f - Main.screenPosition;
            if (Timer > ProjectileID.Sets.TrailCacheLength[projectile.type])
            {
                for (int i = 0; i < projectile.oldRot.Length; i++)
                {
                    Color color = Color.Lerp(Color.Blue, Color.LightBlue, (float)Math.Sin(Main.GlobalTime * 25f));
                    float afterimageRotation = projectile.oldRot[i] + 0.7853982f;
                    spriteBatch.Draw(sword, drawOffset, null, color * 0.1f, afterimageRotation + extraAngle, drawOrigin, projectile.scale - 0.5f * ((float)i / (float)projectile.oldRot.Length), flip, 0f);
                }
            }
            spriteBatch.Draw(sword, drawOffset, null, lightColor, drawRotation, drawOrigin, projectile.scale, flip, 0f);
            spriteBatch.Draw(sword, drawOffset, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation, drawOrigin, projectile.scale, flip, 0f);
            return false;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(init);
            Utils.WriteVector2(writer, direction);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            init = reader.ReadBoolean();
            direction = Utils.ReadVector2(reader);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.PlaySound(SoundID.Item27, projectile.position);
        }

        private bool init;
        private Vector2 direction = Vector2.Zero;
        private float SwingWidth = 2.5f;
        public SwordUtilsClass.CurveSegment exponential = new SwordUtilsClass.CurveSegment(SwordUtilsClass.EasingType.ExpOut, 0f, 0f, 0.15f, 1);
        public SwordUtilsClass.CurveSegment poly = new SwordUtilsClass.CurveSegment(SwordUtilsClass.EasingType.PolyInOut, 0.1f, 0.15f, 0.85f, 3);
        public SwordUtilsClass.CurveSegment linear = new SwordUtilsClass.CurveSegment(SwordUtilsClass.EasingType.Linear, 0.5f, 1f, 0.2f, 1);
    }
    public class IceSwordProjectile : ModProjectile
    {
        public override string Texture
        {
            get
            {
                return "ZensTweakstest/Items/CryoDepths/FrozenConstructRock";
            }
        }
        public override void SetDefaults()
        {
            projectile.timeLeft = 120;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 25;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.height = projectile.width = 16;
            projectile.alpha = 0;
        }
        public override void AI()
        {
            projectile.velocity *= 0.99f;
            projectile.rotation += 0.2f * projectile.direction;
            for (int i = 0; i < 3; i++)
            {
                int dusttype = Main.rand.Next(new int[] { DustID.IceRod, DustID.Ice });
                Dust ice = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, dusttype, 0f, 0f, 25, default, 0.75f);
                ice.noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item27, projectile.position);
            for (int i = 0; i < 8; i++)
            {
                int dusttype = Main.rand.Next(new int[] { DustID.IceRod, DustID.Ice });
                Dust ice = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, dusttype, 0f, 0f, 25, default, 0.85f);
                ice.noGravity = false;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Lerp(Color.DarkBlue, Color.LightBlue, (float)Math.Sin(Main.GlobalTime * 5f));
        }
    }
}
