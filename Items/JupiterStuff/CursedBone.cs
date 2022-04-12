using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.JupiterStuff
{
    public class CursedBone : ModItem
    {
        private float Interval = 0f;
        private Color Test = new Color(255, 145, 206);
        private Color Test2 = new Color(135, 66, 255);
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Very Cursed");
        }
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.width = 20;
            item.height = 34;
            item.rare = 3;
            item.value = Item.buyPrice(silver: 15);
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = ModContent.GetTexture("ZensTweakstest/Items/JupiterStuff/CursedOutline");
            Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture.Height * 0.5f + 2f);
            // We redraw the item's sprite 4 times, each time shifted 2 pixels on each direction, using Main.DiscoColor to give it the color changing effect
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(texture, position, null, Color.Lerp(Test, Test2, Interval), rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
            // Return true so the original sprite is drawn right after
            return true;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = ModContent.GetTexture("ZensTweakstest/Items/JupiterStuff/CursedOutline");
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(texture, position, null, Color.Lerp(Test, Test2, Interval), 0, origin, scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Interval += 0.01f;
            if (Interval >= 1f)
            {
                Interval = 0f;
            }
            /*if (!UpDown)
            {
                Interval += 0.01f;
            }
            else if (UpDown)
            {
                Interval -= 0.01f;
            }
            if (Interval >= 1f)
            {
                UpDown = true;
            }
            if (Interval <= 0f)
            {
                UpDown = false;
            }*/
        }
        public override void UpdateInventory(Player player)
        {
            Interval += 0.01f;
            if (Interval >= 1f)
            {
                Interval = 0f;
            }
        }
    }
}
