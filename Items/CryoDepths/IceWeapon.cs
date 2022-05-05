using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.CryoDepths
{
    public class IceProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = projectile.height = 16;
            projectile.timeLeft = 1200;
            projectile.tileCollide = false;
            projectile.penetrate = 2;
            projectile.friendly = true;
        }
        public ref float Offset => ref projectile.ai[0];
        public bool notcirclingplayer;
        public bool funnyBoolean = true;
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (Main.mouseLeft && funnyBoolean && !player.mouseInterface)
            {
                projectile.timeLeft = 120;
                Vector2 Value = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                Vector2 ShootVelocity = Vector2.Normalize(Value - projectile.Center);
                ShootVelocity *= Main.rand.NextFloat(18f, 22f);
                projectile.velocity = ShootVelocity;
                projectile.netUpdate = true;
                projectile.netSpam = 0;
                notcirclingplayer = true;
                funnyBoolean = false;
            }
            else if (funnyBoolean)
            {
                float num1 = 6.2831855f * Offset / 6f + Main.GlobalTime * 3.5f;
                Vector2 num2 = Utils.ToRotationVector2(num1) * new Vector2(1f, 0.2f);
                Vector2 num3 = new Vector2(player.Center.X - 7, player.Center.Y) + num2 * player.width * 1.25f;
                num3.Y += player.gfxOffY;
                projectile.position = num3;
            }
            if (notcirclingplayer)
            {
                projectile.velocity *= 0.99f;
                projectile.rotation += 0.1f * projectile.direction;
                for (int i = 0; i < 3; i++)
                {
                    int dusttype = Main.rand.Next(new int[] { DustID.IceRod, DustID.Ice });
                    Dust ice = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, dusttype, 0f, 0f, 25, default, 0.75f);
                    ice.noGravity = true;
                }
            }
            if (player.HeldItem.type != ModContent.ItemType<IceWeapon>())
            {
                projectile.Kill();
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            float num4 = 6.2831855f * Offset / 6f + Main.GlobalTime * 3.5f;
            float projalpha = MathHelper.Lerp(0.85f, 1.05f, (float)Math.Cos((Main.GlobalTime * 2.3f)) * 0.5f + 0.5f);
            projalpha *= Utils.InverseLerp(-0.75f, -0.51f, (float)Math.Sin(num4), true);
            if (notcirclingplayer)
            {
                projalpha = 1f;
            }
            return Color.White * projalpha;
        }
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsOverWiresUI.Add(index);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (notcirclingplayer)
            {
                Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    //special color drawing for funni effect
                    //color = Color.Lerp(Color.Blue, Color.DarkBlue, (float)Math.Sin(Main.GlobalTime * 5f) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
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
    }
    public class IceWeapon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Block Staff");
            Tooltip.SetDefault("Press LMB to fire all orbiting ice blocks");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            //idk when this is obtained so take these stats with gran of salt

            item.damage = 25;
            item.magic = true;
            item.knockBack = 2f;
            item.noMelee = true;
            item.mana = 9;
            item.width = 42;
            item.height = 42;
            item.scale = 1.25f;

            item.useTime = 55;
            item.useAnimation = 55;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = Item.sellPrice(gold: 1);
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item43;
            item.autoReuse = true;
            item.useTurn = false;

            item.shoot = 181;
            item.shootSpeed = 5f;
        }
        private int Timer;
        public override void HoldItem(Player player)
        {
            Timer++;
            if (Timer % 120 == 0 && player.ownedProjectileCounts[ModContent.ProjectileType<IceProjectile>()] < 6)
            {
                int offest = player.ownedProjectileCounts[ModContent.ProjectileType<IceProjectile>()];
                float MagicDamage = player.allDamage + player.magicDamage - 1f;
                Projectile.NewProjectile(player.position, Vector2.Zero, ModContent.ProjectileType<IceProjectile>(), (int)(item.damage * MagicDamage), item.knockBack, player.whoAmI, offest);
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return false;
        }
    }
}
