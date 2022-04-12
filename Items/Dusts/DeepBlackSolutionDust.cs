using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items.Dusts
{
    public class DeepBlackSolutionDust : ModDust
    {
        public override void SetDefaults()
        {
            updateType = 110;
        }

        public override void OnSpawn(Dust dust)
        {
            dust.noLight = false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return Color.White;
        }
    }
}
