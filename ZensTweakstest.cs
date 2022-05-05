using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using ZensTweakstest;
using ZensTweakstest.Items;
using ZensTweakstest.Items.NewZenStuff.Bosses;
using ZensTweakstest.Items.NewNonZen.Erichus;
using ZensTweakstest.Config;

namespace ZensTweakstest
{
	public class ZensTweakstest : Mod
	{
        public static ModHotKey SheathHotkey;
        internal static ZensTweakstest instance;
        public static ZensTweakstest Instance;
        public override void AddRecipeGroups()
        {
            if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
            {
                int index = RecipeGroup.recipeGroupIDs["Wood"];
                RecipeGroup group = RecipeGroup.recipeGroups[index];
                group.ValidItems.Add(ItemType("IgnisWood"));
            }
        }
        public static Texture2D OldMiniMap;
        public static Texture2D NewMiniMap;

        public static Texture2D OldTitleLogo;
        public static Texture2D NewTitleLogo;

        public static int ZenTiles;
        public override void Load()
        {
            SheathHotkey = RegisterHotKey("Zen's Sheath HotKey", "L");
            NewMiniMap = ModContent.GetTexture("ZensTweakstest/MiniMapFrameZen");
            OldMiniMap = Main.miniMapFrameTexture;
            Main.miniMapFrameTexture = NewMiniMap;

            /*NewTitleLogo = ModContent.GetTexture("ZensTweakstest/LogoZen");
            OldTitleLogo = Main.logoTexture;
            Main.logoTexture = NewTitleLogo;
            Main.logo2Texture = NewTitleLogo;*/

            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> dye1Ref = new Ref<Effect>(GetEffect("Effects/LightBow"));
                GameShaders.Misc["ZensTweakstest:LightBow"] = new MiscShaderData(dye1Ref, "Light"); // Light is the name of the pass
                Ref<Effect> dye2Ref = new Ref<Effect>(GetEffect("Effects/Mango"));
                GameShaders.Misc["ZensTweakstest:Mango"] = new MiscShaderData(dye2Ref, "Light"); // Light is the name of the pass
                Ref<Effect> dye3Ref = new Ref<Effect>(GetEffect("Effects/Lazer"));
                GameShaders.Misc["ZensTweakstest:Lazer"] = new MiscShaderData(dye3Ref, "Lazer"); // Light is the name of the pass
                Ref<Effect> dyeZenRef = new Ref<Effect>(GetEffect("Effects/ZenDye"));
                GameShaders.Misc["ZensTweakstest:Zen"] = new MiscShaderData(dyeZenRef, "Zen"); // Light is the name of the pass
            }
            if (!Main.dedServ)
            {
                if (ModContent.GetInstance<SpriteSettings>().TitleChange)
                {
                    NewTitleLogo = ModContent.GetTexture("ZensTweakstest/LogoZen");
                    OldTitleLogo = Main.logoTexture;
                    Main.logoTexture = NewTitleLogo;
                    Main.logo2Texture = NewTitleLogo;
                }
                
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/TheBattleOfShine"), ItemType("SG_Box"), TileType("SG_Box_Placed"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/OddBlob"), ItemType("EricBox"), TileType("EricBoxPlaced"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/SparkNight"), ItemType("ZenNightMusicBoxI"), TileType("ZenNightMusicBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/GlitchGlitchy"), ItemType("PortalMusicBox"), TileType("PortalMusicBoxPlaced"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MechanicalDOOM"), ItemType("MechZenMusicBox"), TileType("MechZenMusicBoxPlaced"));

                GameShaders.Armor.BindShader(ModContent.ItemType<Items.NewNonZen.MovingMangoDye>(), new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/Mango")), "Light"));
                GameShaders.Armor.BindShader(ModContent.ItemType<Items.NewNonZen.StrokeDye>(), new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/Lazer")), "Lazer"));
                GameShaders.Armor.BindShader(ModContent.ItemType<Items.NewNonZen.ZenDye>(), new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/ZenDye")), "Zen"));
                GameShaders.Armor.BindShader(ModContent.ItemType<Items.NewNonZen.GlyphDye>(), new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/WaveWrapArmor")), "WaveWrapArmor"));
                Filters.Scene["ZensTweakstest:SparkGaurdian"] = new Filter(new SparkShaderData("FilterMiniTower").UseColor(0.972f, 0.254f, 0.305f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                Filters.Scene["ZensTweakstest:Eric"] = new Filter(new SparkShaderData("FilterMiniTower").UseColor(0.058f, 0.658f, 0.070f).UseOpacity(0.6f), EffectPriority.VeryHigh);
                Filters.Scene["ZensTweakstest:BiomeZenFilter"] = new Filter(new SparkShaderData("FilterMiniTower").UseColor(0.219f, 0.149f, 0.298f).UseOpacity(0.6f), EffectPriority.VeryHigh);

                Filters.Scene["ZensTweakstest:Portal"] = new Filter(new SparkShaderData("FilterMiniTower").UseColor(Main.DiscoR / 255f, Main.DiscoG / 255f, Main.DiscoB / 255f).UseOpacity(0.3f), EffectPriority.VeryHigh);
                Ref<Effect> shaderRef = new Ref<Effect>(GetEffect("Effects/overlay"));
                shaderRef = new Ref<Effect>(GetEffect("Effects/WaveWrapZ"));
                GameShaders.Misc["WaveWrapZ"] = new MiscShaderData(shaderRef, "WaveWrap");
                Filters.Scene["WaveWrapZ"] = new Filter(new ScreenShaderData(shaderRef, "WaveWrap"), EffectPriority.Medium);
                //Filters.Scene["ZensTweakstest:SparkGaurdian"].Load();
            }
        }
        public override void Unload()
        {
            SheathHotkey = null;
            Main.miniMapFrameTexture = OldMiniMap;
            if (ModContent.GetInstance<SpriteSettings>().TitleChange)
            {
                Main.logoTexture = OldTitleLogo;
                Main.logo2Texture = OldTitleLogo;
            }
        }
        /*public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
        {
            if (ZenWorld.ZenTiles <= 0)
            {
                return;
            }

            float exampleStrength = ZenWorld.ZenTiles / 200f;
            exampleStrength = Math.Min(exampleStrength, 1f);

            int sunR = backgroundColor.R;
            int sunG = backgroundColor.G;
            int sunB = backgroundColor.B;
            // Remove some green and more red.
            sunG -= (int)(180f * exampleStrength * (backgroundColor.G / 255f));
            sunR = Utils.Clamp(sunR, 15, 255);
            sunG = Utils.Clamp(sunG, 15, 255);
            sunB = Utils.Clamp(sunB, 15, 255);
            backgroundColor.R = (byte)sunR;
            backgroundColor.G = (byte)sunG;
            backgroundColor.B = (byte)sunB;
        }*/
        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBoss", 10.15f, NPCType("SparkGaurdian"), this, "Spark Guardian", (Func<bool>)(() => ZenWorld.DownedZenGaurd), ItemType("ZenStonePlating"),
                    new List<int> { ItemType("SG_Trophy_I"), ItemType("SG_Box"), ItemType("SparkMask"), ItemType("SG_LORE") },
                    new List<int> { ItemType("SG_Bag"), ItemType("Cursed_Zen_Peeve_Essence"), ItemType("ZenSwordBook"), ItemType("ZSS_Vanity"), ItemType("Zen_Peeve_Essence"), ItemType("Zen_Stone_Trident"), ItemType("GlobeStaff"), ItemType("HangingVoid"), ItemType("SparkBow"), ItemID.GreaterHealingPotion },//HangingVoid
                    "Drink the [i:" + ItemType("ZenStonePlating") + "] while in Hell.", null, "ZensTweaksTest/Items/NewZenStuff/Bosses/SparkGaurdian_Checklist", "ZensTweaksTest/Items/NewZenStuff/Bosses/SparkGaurdian_Head_Boss");

                bossChecklist.Call("AddBoss", 9.6f, NPCType("ErichusContainment"), this, "Erichus Containment", (Func<bool>)(() => ZenWorld.DownedEric), ItemType("ShrineEricI"),
                    new List<int> { ItemType("TrophyEric"), ItemType("EricBox"), ItemType("ErichusContainmentMask"), ItemType("Erichus") },
                    new List<int> { ItemType("ErichusBag"), ItemType("Butcherer"), ItemType("ToxicRevolverator"), ItemType("ToxicBarrel"), ItemType("NuclearRotation"), ItemType("ToxicRuble"), ItemID.GreaterHealingPotion },//HangingVoid
                    "Place the [i:" + ItemType("ShrineEricI") + "] and activate it at night. (Bought from the Steampunker)", null, "ZensTweaksTest/Items/NewNonZen/Erichus/Boss/EricChecklist", "ZensTweaksTest/Items/NewNonZen/Erichus/Boss/ErichusContainment_Head_Boss");;

                bossChecklist.Call("AddBoss", 9.8f, NPCType("MechZen"), this, "Mech Zen", (Func<bool>)(() => ZenWorld.DownedMechZen), ItemType("BeaconOfWar"),
                    new List<int> { ItemType("ZenTrophy"), ItemType("MechZenMusicBox"), ItemType("MechZenMask"), ItemType("MechZenL") },
                    new List<int> { ItemType("ZenBag"), ItemType("VoidSheath"), ItemType("StellarUmbrella"), ItemType("ChaosVigil"), ItemType("VoidSlash"), ItemType("Afterburn"), ItemType("ThePoker"), ItemType("ZenicFlamethrower"), ItemType("PrismaticStroke"), ItemType("AfflictionDagger"), ItemID.GreaterHealingPotion },//HangingVoid
                    "Use [i:" + ItemType("BeaconOfWar") + "] at night.", null, "ZensTweaksTest/Items/HMmechZen/MechZenBC", "ZensTweaksTest/Items/HMmechZen/MechZen_Head_Boss"); ;
            }

            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            if (yabhb != null)
            {
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("BossBars/ZenBarStart"),
                    GetTexture("BossBars/ZenBarMiddle"),
                    GetTexture("BossBars/ZenBarEnd"),
                    GetTexture("BossBars/ZenBarFill"));
                yabhb.Call("hbSetColours",
                    new Color(0.9725f, 0.2549f, 0.3058f), // 100%
                    new Color(0.6784f, 0.0823f, 0.2666f), // 50%
                    new Color(0.6784f, 0.0823f, 0.2666f));// 0%
                //yabhb.Call("hbSetMidBarOffset", 20, 12);
                //yabhb.Call("hbSetBossHeadCentre", 22, 34);
                //yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", ModContent.NPCType<Items.HMmechZen.MechZen>());
            }
        }
        public ZensTweakstest()
        {
            Instance = this;
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer == -1 || Main.gameMenu || !Main.LocalPlayer.active)
            {
                return;
            }
            if (Main.LocalPlayer.GetModPlayer<Charred_Life>().ZenZone)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/TwistingPalace");
                priority = MusicPriority.BiomeLow;
            }
            if (Main.LocalPlayer.ZoneOverworldHeight && Main.LocalPlayer.GetModPlayer<Charred_Life>().ZenZone && !Main.dayTime)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/SparkNight");
                priority = MusicPriority.BiomeMedium;
            }
            if (Main.LocalPlayer.GetModPlayer<Charred_Life>().CryoSpace)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/ReverseBubble");
                priority = MusicPriority.BiomeHigh;
            }
        }
    }
}