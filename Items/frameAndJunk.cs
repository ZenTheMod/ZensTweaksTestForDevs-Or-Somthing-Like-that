using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;

namespace ZensTweakstest.Items
{
    static class frameAndJunk
    {
        public static int TimeToDisplay = 0;
        public static int NPCType = 0;
        private const int DisplayTimeMax = (int)(60 * 3.5); //3.5 seconds
        public static Rectangle GetCurrentFrame(this Item item, ref int frame, ref int frameCounter, int frameDelay, int frameAmt, bool frameCounterUp = true)
        {
            if (frameCounter >= frameDelay)
            {
                frameCounter = -1;
                frame = ((frame != frameAmt - 1) ? (frame + 1) : 0);
            }
            if (frameCounterUp)
            {
                frameCounter++;
            }
            return new Rectangle(0, item.height * frame, item.width, item.height);
        }
        public static void SetMerge(int type1, int type2, bool merge = true)
        {
            if (type1 != type2)
            {
                Main.tileMerge[type1][type2] = merge;
                Main.tileMerge[type2][type1] = merge;
            }
        }
        public static Point TileCoordsBottomLeft(this Player p, Vector2? offset = null)
        {
            Vector2 off = offset ?? Vector2.Zero;
            return new Point((int)((p.BottomLeft.X + off.X) / 16f), (int)((p.BottomLeft.Y + off.Y + 4) / 16f));
        }

        public static Point TileCoordsBottomRight(this Player p, Vector2? offset = null)
        {
            Vector2 off = offset ?? Vector2.Zero;
            return new Point((int)((p.BottomRight.X + off.X) / 16f), (int)((p.BottomRight.Y + off.Y + 4) / 16f));
        }
        public static void Resize(this Projectile projectile, int newWidth, int newHeight)
        {
            Vector2 oldCenter = projectile.Center;
            projectile.width = newWidth;
            projectile.height = newHeight;
            projectile.Center = oldCenter;
        }
        public static void ResizeScaleOfJellyNPC(this NPC npc, float extrasize)
        {
            Vector2 oldCenter = npc.Center;
            float SinWave = (float)Math.Sin(Main.GameUpdateCount / 35f);
            npc.scale = SinWave / 10f + 1f + extrasize;
            npc.Center = oldCenter;
        }
    }
}
