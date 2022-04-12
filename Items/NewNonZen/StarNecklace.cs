using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QwertysRandomContent;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Items.NewZenStuff.Projectilles;

namespace ZensTweakstest.Items.NewNonZen
{
    public class StarNecklace : ModItem
    {
        public override void SetDefaults()
        {
            item.accessory = true;
            item.width = 26;
            item.height = 26;
            item.value = Item.buyPrice(gold: 15);
            item.value = Item.sellPrice(gold: 13);
            item.rare = ItemRarityID.Red;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 40;
            player.magicDamage += 0.2f;
            player.magicCrit += 5;
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
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture.Height * 0.5f + 2f);
            // We redraw the item's sprite 4 times, each time shifted 2 pixels on each direction, using Main.DiscoColor to give it the color changing effect
            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position + offsetPositon, null, Main.DiscoColor, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
            var shader = GameShaders.Misc["ZensTweakstest:LightBow"]; // shader name
            shader.Apply();
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, shader.Shader, Main.GameViewMatrix.TransformationMatrix);
            return true;
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
        }
        // Same as above but for drawing inside the player's inventory
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Main.itemTexture[item.type];
            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position + offsetPositon, null, Main.DiscoColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            var shader = GameShaders.Misc["ZensTweakstest:LightBow"]; // shader name
            shader.Apply();
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, shader.Shader, Main.UIScaleMatrix);
            return true;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
            //Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }
    }
}
