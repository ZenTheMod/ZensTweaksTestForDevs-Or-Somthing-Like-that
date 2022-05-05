using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QwertysRandomContent;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;
using ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class EvadingFlame : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 175;
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
            item.height = 30;
            if (!Main.dedServ)
            {
                item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/NewZenStuff/Items/EV_Glow");
            }
            item.mana = 10;
            item.shoot = ModContent.ProjectileType<RedYang>();
            item.shootSpeed = 15f;
            item.noMelee = true;
        }
        public override void AddRecipes()
        {
            ModRecipe POOP = new ModRecipe(mod);

            POOP.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 10);
            POOP.AddIngredient(ModContent.ItemType<ZenStone_I>(), 5);
            POOP.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 20);
            POOP.AddTile(ModContent.TileType<ZCC_PLACED>());
            POOP.SetResult(this);
            POOP.AddRecipe();
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/NewZenStuff/Items/EV_Glow");
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
