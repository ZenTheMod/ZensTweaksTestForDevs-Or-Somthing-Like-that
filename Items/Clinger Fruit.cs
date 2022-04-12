using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using QwertysRandomContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZensTweakstest.Items.NewZenStuff.Items;

namespace ZensTweakstest.Items
{
    public class Clinger_Fruit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen's Holy Shard");
            Tooltip.SetDefault("It reasonates with fragmented energy. " +
                "Increases your max life by 5");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.LifeFruit);
            if (!Main.dedServ)
            {
                item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/gl_Glow");
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/gl_Glow");
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
        public override bool CanUseItem(Player player)
        {
            return player.statLifeMax >= 500 && player.GetModPlayer<Charred_Life>().charredlife < Charred_Life.maxcharredlife;
        }

        public override bool UseItem(Player player)
        {
            player.statLifeMax2 += 5;
            player.statLife += 5;
            player.GetModPlayer<Charred_Life>().charredlife += 1;
            if (Main.myPlayer == player.whoAmI)
            {
                player.HealEffect(5, true);
            }
            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2Unit((float)MathHelper.Pi / 4, (float)MathHelper.Pi / 2);
                Dust d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.BlueCrystalShard, speed * 5, Scale: 1.5f);
                d.noGravity = true;

                Vector2 speed2 = Main.rand.NextVector2Unit((float)MathHelper.Pi / -4, (float)MathHelper.Pi / -2);
                Dust d2 = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Clentaminator_Red, speed2 * 5, Scale: 1.5f);
                d2.noGravity = true;
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe ppdr = new ModRecipe(mod);
            ppdr.AddIngredient(ItemID.BeetleHusk, 10);
            ppdr.AddIngredient(ItemID.VioletHusk, 1);
            ppdr.AddIngredient(ItemID.CyanHusk, 1);
            ppdr.AddIngredient(ItemID.RedHusk, 1);
            ppdr.AddIngredient(ItemID.LifeFruit, 3);
            ppdr.AddIngredient(ModContent.ItemType<ZenStone_I>(), 40);
            ppdr.AddTile(TileID.DemonAltar);
            ppdr.SetResult(this, 1);
            ppdr.AddRecipe();
        }
    }
}
