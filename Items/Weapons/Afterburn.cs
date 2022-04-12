using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.Weapons
{
    //Delete all the comments and port classes to unique files, please!

    //Item
    public class Afterburn : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Chases after your enemies.");
        }
        public override void SetDefaults()
        {
            //Attack stats
			item.damage = 90;
            item.knockBack = 7;
            item.thrown = true;
			item.noMelee = true;

            //Shoot
            item.shoot = ModContent.ProjectileType<AfterburnPro>();
			item.shootSpeed = 15f;

            //Visuals
            item.width = 44;
			item.height = 40;
			item.scale = 1.5f;

            //Use appearence
            item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = false;
			item.noUseGraphic = true;

            //Value
            item.value = Item.sellPrice(gold: 12, silver: 88);
			item.rare = 4;
		}
	}

    //Projectile
	public class AfterburnPro : ModProjectile
    {
        public override string Texture => "ZensTweakstest/Items/Weapons/Afterburn";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Afterburn");
        }

        public override void SetDefaults()
        {
            //Attack Stats
            projectile.penetrate = -1;
            projectile.thrown = true;

            //Visuals
            projectile.width = 44;
            projectile.height = 44;
            drawOffsetX = 11;
            drawOriginOffsetY = 13;
            projectile.scale = 1.5f;

            //Relationship
            projectile.friendly = true;
            projectile.hostile = false;

            //Collision
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            //Other stuff
            projectile.aiStyle = 0;
        }

        //Boomerang AI variables
        private int spinDirection;
        private float speed;
        private float maxSpeed;
        private readonly float sling = 10f;
        private bool runOnce = true;
        private bool returnToPlayer;
        bool target = false;

        //Boomerang AI
        public override void AI()
        {
            //Player
            Player player = Main.player[projectile.owner];

            #region Dust
            if (Main.rand.NextBool(1))
            {
                Dust dust;
                dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.LifeDrain, projectile.velocity.X / 2, projectile.velocity.Y / 2, 0, default, 1.5f);
                dust.noGravity = true;
            }
            if (Main.rand.NextBool(2))
            {
                Dust dust;
                dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Granite, projectile.velocity.X / 2, projectile.velocity.Y / 2, 0, default, 1.5f);
                dust.noGravity = true;
            }
            #endregion

            //Check runOnce
            if (runOnce)
            {
                spinDirection = player.direction;
                this.speed = projectile.velocity.Length() * 2;
                maxSpeed = this.speed;
                runOnce = false;
            }

            //Rotation
            projectile.rotation += MathHelper.ToRadians(maxSpeed * spinDirection);

            #region Homing AI
            if (!returnToPlayer)
            {
                if (projectile.localAI[0] == 0f)
                {
                    AdjustMagnitude(ref projectile.velocity);
                    projectile.localAI[0] = 1f;
                }

                Vector2 move = Vector2.Zero;
                float distance = 400f;

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
                    projectile.velocity = (8 * projectile.velocity + move) / 9f;
                    AdjustMagnitude(ref projectile.velocity);
                }
            }
            #endregion

            #region Boomerang AI
            if (!target || returnToPlayer)
            {
                if (returnToPlayer)
                {
                    if (Collision.CheckAABBvAABBCollision(player.position, player.Size, projectile.position, projectile.Size))
                    {
                        projectile.Kill();
                    }

                    projectile.velocity = ModBoomerang.PolarVector(this.speed, (player.Center - projectile.Center).ToRotation());
                    if (this.speed > maxSpeed)
                    {
                        this.speed = maxSpeed;
                    }
                }
                else
                {
                    projectile.velocity = projectile.velocity.SafeNormalize(-Vector2.UnitY) * speed;

                    projectile.ai[0] += 1f;
                    if (projectile.ai[0] >= 25f)
                    {
                        projectile.ai[0] = 25f;
                        projectile.netUpdate = true;

                        this.speed -= sling;
                        if (this.speed < 1f)
                        {
                            this.speed = 40;
                            returnToPlayer = true;
                        }
                    }
                }
            }
            #endregion
        }

        //Adjust homing magnitude
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 30f)
            {
                vector *= 30f / magnitude;
            }
        }

        //Return on hit
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            returnToPlayer = true;
        }
    }

    //Polarizing the vectors
    public static class ModBoomerang
    {
        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }
    }
}
