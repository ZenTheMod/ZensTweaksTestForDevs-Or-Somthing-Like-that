using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QwertysRandomContent;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Items.NewZenStuff.Projectilles;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot
{
    public class ZenSwordBook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sparkling Ghast Blast");
            Tooltip.SetDefault("Its Like Your The Boss");
        }

        public override void SetDefaults()
        {
            item.damage = 100;
            item.magic = true;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 100;
            item.value = Item.buyPrice(gold: 5);
            item.value = Item.sellPrice(gold: 3);
            item.rare = ItemRarityID.Yellow;
            item.UseSound = SoundID.Item102;
            item.autoReuse = true;
            item.width = 28;
            item.height = 30;//e
            if (!Main.dedServ)
            {
                item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/NewZenStuff/Bosses/Loot/BagLoot/ZenSwordBook_Glow");
            }
            item.mana = 10;
            item.shoot = mod.ProjectileType("ZenSwordBeam");
            item.shootSpeed = 15;
            item.noMelee = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberofproj = 15;
            for (int i = 0; i < numberofproj; i++)
            {
                Vector2 spawnPos = player.Center;
                Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));


                VelPos.Normalize();
                Projectile.NewProjectile(spawnPos, VelPos * 8f, ModContent.ProjectileType<ZenSwordBeam>(), item.damage, 5f, Main.myPlayer);
                Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<ZenStoneDust>(), player.velocity.X * 0.5f, player.velocity.Y * 0.5f);
            }
            return false;
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/NewZenStuff/Bosses/Loot/BagLoot/ZenSwordBook_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}
