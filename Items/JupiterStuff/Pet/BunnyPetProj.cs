using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using System;
using System.Numerics;

namespace ZensTweakstest.Items.JupiterStuff.Pet
{
    public class BunnyPetProj : ModProjectile
    {
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + new Vector2(13, 34);//+ new Vector2(26, 12) / 2f
                Color color = Color.White * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/JupiterStuff/Pet/Cloud"), drawPos, null, color, projectile.rotation, new Vector2(26, 12) / 2f, projectile.scale, SpriteEffects.None, 0f);//projectile.scale - k / (float)projectile.oldPos.Length
            }
            spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/JupiterStuff/Pet/Cloud"), projectile.Center - Main.screenPosition - new Vector2(0, -17), null, Color.White, projectile.rotation, new Vector2(26, 12) / 2f, projectile.scale, SpriteEffects.None, 0f);
            return true;
        }
        //I made dumb lore about my character...
        //Jupiter the void bunny
        //born in the stellar abyss, a limbo between dimensions
        //evolved to a humanoid form after finding a way to breach the limbo
        //- Jupiter
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Bunny");
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
        }
        private double ERICHUS = 0.0;
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.ZephyrFish);

            projectile.width = 32;
            projectile.height = 32;

            projectile.penetrate = -1;

            projectile.aiStyle = -1;

            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.position, Color.Magenta.ToVector3() * 1f);
            Player player = Main.player[projectile.owner];
            Charred_Life modPlayer = player.GetModPlayer<Charred_Life>();
            if (player.dead)
            {
                modPlayer.BunnyVoid = false;
            }
            if (modPlayer.BunnyVoid)
            {
                projectile.timeLeft = 2;
            }
            ERICHUS += 0.1;
            if (ERICHUS == 6.2)
            {
                ERICHUS = 0.0;
            }
            projectile.spriteDirection = Main.player[projectile.owner].direction * -1;
            projectile.Center = Main.player[projectile.owner].Center + Vector2.One.RotatedBy(ERICHUS) * 35;
        }
    }
    public class BunnyPetItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Carrot");
            Tooltip.SetDefault(@"Carrot resprite.
Summons a Void Bunny.");
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ZephyrFish);
            item.useTime = 30;
            item.useAnimation = 30;
            item.autoReuse = true;
            item.useTurn = false;
            item.width = 18;
            item.height = 32;
            item.value = Item.sellPrice(gold: 12);
            item.rare = ItemRarityID.Pink;
            item.shoot = ModContent.ProjectileType<BunnyPetProj>();
            item.buffType = ModContent.BuffType<BunnyPetBuff>();
        }
    }
    public class BunnyPetBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Void Bunny");
            Description.SetDefault("Something something bunny - Jupitererer");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<Charred_Life>().BunnyVoid = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<BunnyPetProj>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<BunnyPetProj>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
