using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Graphics;
using Terraria.ModLoader;
using Terraria.Enums;
using Terraria.GameContent;
using ZensTweakstest.Items.NewZenStuff.Bosses;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;
using ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class DualityStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Staff of Duality");
            Tooltip.SetDefault("Summons a Yin-Yang of Pure Zen to shoot for you" +
                "\nVery Violent");
            ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
            ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 103;
            item.knockBack = 3f;
            item.mana = 15;
            item.width = 50;
            item.height = 50;
            item.value = Item.buyPrice(0, 1, 50, 0);
            item.noMelee = true;
            item.summon = true;

            item.rare = ItemRarityID.Yellow;
            item.useTime = 36;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 36;
            item.UseSound = SoundID.Item113;
            item.buffType = ModContent.BuffType<YinBuff>();
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
            item.shoot = ModContent.ProjectileType<YinYangSum>();
        }

        public override void AddRecipes()
        {
            ModRecipe POOP = new ModRecipe(mod);

            POOP.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 10);
            POOP.AddIngredient(ModContent.ItemType<ZenStone_I>(), 10);
            POOP.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 100);
            POOP.AddTile(ModContent.TileType<ZCC_PLACED>());
            POOP.SetResult(this);
            POOP.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
            player.AddBuff(item.buffType, 2);

            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position.
            position = Main.MouseWorld;
            for (int i = 0; i < 55; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(player.position, ModContent.DustType<BlackStoneDust>(), speed * 5, Scale: 1f);
                d.noGravity = true;
            }
            for (int i = 0; i < 55; i++)
            {
                Vector2 speed2 = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d2 = Dust.NewDustPerfect(player.position, ModContent.DustType<ZenStoneFlameDust>(), speed2 * 5, Scale: 1f);
                d2.noGravity = true;
            }
            return true;
        }
    }
    public class YinYangSum : ModProjectile
    {
        private bool TargetFX = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("YinYang");
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode

            // These below are needed for a minion
            // Denotes that this projectile is a pet or minion
            Main.projPet[projectile.type] = true;
            // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            // Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
            ProjectileID.Sets.Homing[projectile.type] = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.alpha = 255;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.minionSlots = 1f;
        }


        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (TargetFX)
            {
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, Main.projectileTexture[projectile.type].Frame(1, Main.projFrames[projectile.type], 0, projectile.frame), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        public override void AI()
        {
            float strength = 2f;
            Lighting.AddLight(projectile.position, Color.Red.ToVector3() * strength);

            Player player = Main.player[projectile.owner];
            #region Active check
            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<YinBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<YinBuff>()))
            {
                projectile.timeLeft = 2;
            }
            #endregion

            #region General behavior
            Vector2 idlePosition = player.Center;

            // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
            // The index is projectile.minionPos
            float minionPositionOffsetY = (10 + projectile.minionPos * 40) * -player.direction;
            idlePosition.Y += minionPositionOffsetY; // Go behind the player

            // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

            // Teleport to player if distance is too big
            Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
            float distanceToIdlePosition = vectorToIdlePosition.Length();
            if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f)
            {
                // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
                // and then set netUpdate to true
                projectile.position = idlePosition;
                projectile.velocity *= 0.1f;
                projectile.netUpdate = true;
            }

            float overlapVelocity = 0.04f;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                // Fix overlap with other minions
                Projectile other = Main.projectile[i];
                if (i != projectile.whoAmI && other.active && other.owner == projectile.owner && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
                {
                    if (projectile.position.X < other.position.X) projectile.velocity.X -= overlapVelocity;
                    else projectile.velocity.X += overlapVelocity;

                    if (projectile.position.Y < other.position.Y) projectile.velocity.Y -= overlapVelocity;
                    else projectile.velocity.Y += overlapVelocity;
                }
            }
            #endregion

            projectile.rotation += 0.1f;
            if (Main.rand.Next(0, 10) == 7)
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<ZenStoneFlameDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            if (Main.rand.Next(0, 10) == 7)
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<BlackStoneDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            float speed = 17f;
            float inertia = 40f;
            float distanceFromTarget = 700f;
            Vector2 targetCenter = projectile.Center;
            bool foundTarget = false;
            TargetFX = false;
            if (!foundTarget)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, projectile.Center);
                        bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 100f;
                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                            TargetFX = true;
                        }
                    }
                }
            }
            projectile.friendly = foundTarget;

            Vector2 direction = targetCenter - projectile.Center;
            projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
            if (!foundTarget)
            {
                // Minion doesn't have a target: return to player and idle
                if (distanceToIdlePosition > 600f)
                {
                    // Speed up the minion if it's away from the player
                    speed = 22f;
                    inertia = 60f;
                }
            }
            else
            {
                // Slow down the minion if closer to the player
                speed = 4f;
                inertia = 80f;
            }
            if (distanceToIdlePosition > 20f)
            {
                // The immediate range around the player (when it passively floats about)

                // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                vectorToIdlePosition.Normalize();
                vectorToIdlePosition *= speed;
                projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
            }
            else if (projectile.velocity == Vector2.Zero)
            {
                // If there is a case where it's not moving at all, give it a little "poke"
                projectile.velocity.X = -0.15f;
                projectile.velocity.Y = -0.05f;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < (Main.expertMode ? 55 : 50); i++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<ZenStoneFlameDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<BlackStoneDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
        }
    }
    public class YinBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("The Balnce");
            Description.SetDefault("A Balnce of Pure Zen Will fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<YinYangSum>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
