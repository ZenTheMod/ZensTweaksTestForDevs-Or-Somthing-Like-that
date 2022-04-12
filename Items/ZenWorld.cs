using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Localization;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.IO;
using System.IO;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;
using ZensTweakstest.Items.NewZenStuff.Tiles;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using Terraria.ObjectData;
using Terraria.Enums;
using ZensTweakstest.Items.CryoDepths;

namespace ZensTweakstest.Items
{
    public class ZenWorld : ModWorld
    {
        public static int NonWorldGenDungeonX;
        public static int IceX;
        public static bool DownedZenGaurd = false;
        public static bool DownedEric = false;
        public static bool DownedMechZen = false;
        public static int ZenTiles;
        public static int CryoTiles;
        public override void Initialize()
        {
            DownedZenGaurd = false;
        }
        public override void ResetNearbyTileEffects()
        {
            ZenTiles = 0;
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            // Here we count various tiles towards ZoneExample
            ZenTiles = tileCounts[ModContent.TileType<ZenStone>()] + tileCounts[ModContent.TileType<ZenDirtTile>()] + tileCounts[ModContent.TileType<ZenSand>()];
            CryoTiles = tileCounts[ModContent.TileType<Cryostone>()] + tileCounts[ModContent.TileType<EndoSlush>()] + tileCounts[ModContent.TileType<BlueIce>()];
        }
        public override TagCompound Save()//DownedMechZen
        {
            var Downed = new List<string>();
            if (DownedZenGaurd) Downed.Add("SparkGaurdian");
            if (DownedEric) Downed.Add("ErichusContainment");
            if (DownedMechZen) Downed.Add("MechZen");

            return new TagCompound
            {
                {
                    "Version", 0
                },
                {
                    "Downed", Downed
                },
                {
                    "DungeonX", NonWorldGenDungeonX
                },
                {
                    "IceX", IceX
                }
            };
        }

        public override void Load(TagCompound tag)//DownedMechZen
        {
            var Downed = tag.GetList<string>("Downed");
            DownedZenGaurd = Downed.Contains("SparkGaurdian");
            DownedEric = Downed.Contains("ErichusContainment");
            DownedMechZen = Downed.Contains("MechZen");
            NonWorldGenDungeonX = tag.GetInt("DungeonX");
            IceX = tag.GetInt("IceX");
        }

        public override void LoadLegacy(BinaryReader reader)//DownedMechZen
        {
            int LoadVersion = reader.ReadInt32();
            if (LoadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                DownedZenGaurd = flags[0];
                DownedEric = flags[1];
                DownedMechZen = flags[2];
            }
        }

        public override void NetSend(BinaryWriter writer)//DownedMechZen
        {
            BitsByte flags = new BitsByte();
            flags[0] = DownedZenGaurd;
            flags[1] = DownedEric;
            flags[2] = DownedMechZen;

            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)//DownedMechZen
        {
            BitsByte flags = reader.ReadByte();
            DownedZenGaurd = flags[0];
            DownedEric = flags[1];
            DownedMechZen = flags[2];
        }
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int DungeonIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Dungeon"));
            if (DungeonIndex != -1)
            {
                tasks.Insert(DungeonIndex + 1, new PassLegacy("Zen Stone Evils...", DungeonX));
            }
            int IceIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Slush"));
            if (IceIndex != -1)
            {
                tasks.Insert(IceIndex + 1, new PassLegacy("Cryo Depths", CryoDepths));
            }
        }
        private void CryoDepths(GenerationProgress progress)
        {//what terraria does
            //code be like
            int dungeonSide = 0;
            if (Main.dungeonX > Main.maxTilesX / 2f)
            {
                dungeonSide = 1;
            }
            int ValIce1 = WorldGen.genRand.Next(Main.maxTilesX);
            if (dungeonSide == 1)
            {
                while ((float)ValIce1 < (float)Main.maxTilesX * 0.55f || (float)ValIce1 > (float)Main.maxTilesX * 0.7f)
                {
                    ValIce1 = WorldGen.genRand.Next(Main.maxTilesX);
                }
            }
            else
            {
                while ((float)ValIce1 < (float)Main.maxTilesX * 0.3f || (float)ValIce1 > (float)Main.maxTilesX * 0.45f)
                {
                    ValIce1 = WorldGen.genRand.Next(Main.maxTilesX);
                }
            }
            float ValIce3 = Main.maxTilesX / 4200;
            int ValIce2 = WorldGen.genRand.Next(50, 90);
            ValIce2 += (int)((float)WorldGen.genRand.Next(20, 40) * ValIce3);
            ValIce2 += (int)((float)WorldGen.genRand.Next(20, 40) * ValIce3);
            progress.Message = "Cryo Depths";
            IceX = ValIce1 - ValIce2;

            int RadiusBiome = 78;
            int RadiusBiomeInner = 50;
            int biomeX = IceX;
            int biomeY = WorldGen.lavaLine - 150;
            for (int x = biomeX - RadiusBiome; x < biomeX + RadiusBiome; x++)
            {
                for (int y = biomeY - RadiusBiome; y < biomeY + RadiusBiome; y++)
                {
                    if (Vector2.Distance(new Vector2(biomeX, biomeY), new Vector2(x, y)) < RadiusBiome)
                    {
                        if (Framing.GetTileSafely(x, y).active() && (Framing.GetTileSafely(x, y).type == TileID.Stone || Framing.GetTileSafely(x, y).type == TileID.BlueMoss || Framing.GetTileSafely(x, y).type == TileID.BrownMoss || Framing.GetTileSafely(x, y).type == TileID.GreenMoss || Framing.GetTileSafely(x, y).type == TileID.LavaMoss || Framing.GetTileSafely(x, y).type == TileID.PurpleMoss || Framing.GetTileSafely(x, y).type == TileID.RedMoss || Framing.GetTileSafely(x, y).type == TileID.Tin || Framing.GetTileSafely(x, y).type == TileID.Copper || Framing.GetTileSafely(x, y).type == TileID.Gold || Framing.GetTileSafely(x, y).type == TileID.Platinum))
                        {
                            WorldGen.KillTile(x, y);
                            WorldGen.PlaceTile(x, y, ModContent.TileType<Cryostone>());
                            //WorldGen.KillWall(x, y);
                            //WorldGen.PlaceWall(x, y, ModContent.WallType<BlueIceWall>());
                        }
                        if (Framing.GetTileSafely(x, y).active() && (Framing.GetTileSafely(x, y).type == TileID.Dirt || Framing.GetTileSafely(x, y).type == TileID.Mud || Framing.GetTileSafely(x, y).type == TileID.Slush || Framing.GetTileSafely(x, y).type == TileID.Silt))
                        {
                            WorldGen.KillTile(x, y);
                            WorldGen.PlaceTile(x, y, ModContent.TileType<EndoSlush>());
                        }
                        if (Framing.GetTileSafely(x, y).active() && (Framing.GetTileSafely(x, y).type == TileID.IceBlock || Framing.GetTileSafely(x, y).type == TileID.SnowBlock))
                        {
                            WorldGen.KillTile(x, y);
                            WorldGen.PlaceTile(x, y, ModContent.TileType<BlueIce>());
                        }
                        //WorldGen.KillWall(x, y);
                        //WorldGen.PlaceWall(x, y, ModContent.WallType<ZSWT>());
                    }
                }
            }

            for (int x = biomeX - RadiusBiomeInner; x < biomeX + RadiusBiome; x++)
            {
                for (int y = biomeY - RadiusBiomeInner; y < biomeY + RadiusBiome; y++)
                {
                    if (Vector2.Distance(new Vector2(biomeX, biomeY), new Vector2(x, y)) < RadiusBiomeInner)
                    {
                        WorldGen.KillTile(x, y);
                    }
                }
            }
            StructureHelper.Generator.GenerateStructure("WorldGen/CryoCrapyoo", new Point16(biomeX - 5, biomeY - 1), ModContent.GetInstance<ZensTweakstest>());//This Structure Line Is After The Biome Changes To Prevent Wall Distruction...
            int RadiusBiomeW = 76;
            for (int x = biomeX - RadiusBiomeW; x < biomeX + RadiusBiomeW; x++)
            {
                for (int y = biomeY - RadiusBiomeW; y < biomeY + RadiusBiomeW; y++)
                {
                    if (Vector2.Distance(new Vector2(biomeX, biomeY), new Vector2(x, y)) < RadiusBiomeW)
                    {
                        WorldGen.KillWall(x, y);
                        WorldGen.PlaceWall(x, y, ModContent.WallType<BlueIceWall>());
                    }
                }
            }
        }
        private void DungeonX(GenerationProgress progress)
        {
            progress.Message = "Zen Stone Evils...";
            NonWorldGenDungeonX = Main.dungeonX;

            //StructureHelper.Generator.GenerateStructure("WorldGen/SpaceBubbles", new Point16(120, 120), ModContent.GetInstance<ZensTweakstest>());//This Structure Line Is After The Biome Changes To Prevent Wall Distruction...

            int RadiusBiome = 55;
            int biomeX = NonWorldGenDungeonX;
            int biomeY = Main.maxTilesY - 250 - RadiusBiome;
            StructureHelper.Generator.GenerateStructure("WorldGen/SpaceBubbles", new Point16(NonWorldGenDungeonX, 80), ModContent.GetInstance<ZensTweakstest>());//This Structure Line Is After The Biome Changes To Prevent Wall Distruction...
            for (int x = biomeX - RadiusBiome; x < biomeX + RadiusBiome; x++)
            {
                for (int y = biomeY - RadiusBiome; y < biomeY + RadiusBiome; y++)
                {
                    if (Vector2.Distance(new Vector2(biomeX, biomeY), new Vector2(x, y)) < RadiusBiome)
                    {
                        if (Framing.GetTileSafely(x, y).active() && (Framing.GetTileSafely(x, y).type == TileID.Stone || Framing.GetTileSafely(x, y).type == TileID.BlueMoss || Framing.GetTileSafely(x, y).type == TileID.BrownMoss || Framing.GetTileSafely(x, y).type == TileID.GreenMoss || Framing.GetTileSafely(x, y).type == TileID.LavaMoss || Framing.GetTileSafely(x, y).type == TileID.PurpleMoss || Framing.GetTileSafely(x, y).type == TileID.RedMoss))
                        {
                            WorldGen.KillTile(x, y);
                            WorldGen.PlaceTile(x, y, ModContent.TileType<ZenStone>());
                        }
                        if (Framing.GetTileSafely(x, y).active() && (Framing.GetTileSafely(x, y).type == TileID.Silt || Framing.GetTileSafely(x, y).type == TileID.Sand || Framing.GetTileSafely(x, y).type == TileID.Slush))
                        {
                            WorldGen.KillTile(x, y);
                            WorldGen.PlaceTile(x, y, ModContent.TileType<ZenSand>());
                        }
                        if (Framing.GetTileSafely(x, y).active() && (Framing.GetTileSafely(x, y).type == TileID.Dirt || Framing.GetTileSafely(x, y).type == TileID.Mud))
                        {
                            WorldGen.KillTile(x, y);
                            WorldGen.PlaceTile(x, y, ModContent.TileType<ZenDirtTile>());
                        }
                        if (Framing.GetTileSafely(x, y).active() && (Framing.GetTileSafely(x, y).type == TileID.Copper || Framing.GetTileSafely(x, y).type == TileID.Tin || Framing.GetTileSafely(x, y).type == TileID.Iron || Framing.GetTileSafely(x, y).type == TileID.Lead || Framing.GetTileSafely(x, y).type == TileID.Silver || Framing.GetTileSafely(x, y).type == TileID.Tungsten || Framing.GetTileSafely(x, y).type == TileID.Gold || Framing.GetTileSafely(x, y).type == TileID.Platinum || Framing.GetTileSafely(x, y).type == TileID.Demonite || Framing.GetTileSafely(x, y).type == TileID.Crimtane))
                        {
                            WorldGen.KillTile(x, y);
                            WorldGen.PlaceTile(x, y, ModContent.TileType<ZenitrinOre>());
                        }
                        if (Framing.GetTileSafely(x, y).active() && Framing.GetTileSafely(x, y).type == TileID.Stalactite && Framing.GetTileSafely(x, y).frameX == 18 || Framing.GetTileSafely(x, y).frameX == 0 && Framing.GetTileSafely(x, y).frameY == 0)
                        {
                            WorldGen.KillTile(x, y);
                            WorldGen.PlaceTile(x, y - 1, ModContent.TileType<AmbeintZen1X2T>());
                        }
                        //WorldGen.KillWall(x, y);
                        //WorldGen.PlaceWall(x, y, ModContent.WallType<ZSWT>());
                    }
                }
            }
            int RadiusBiomeW = 54;
            for (int x = biomeX - RadiusBiomeW; x < biomeX + RadiusBiomeW; x++)
            {
                for (int y = biomeY - RadiusBiomeW; y < biomeY + RadiusBiomeW; y++)
                {
                    if (Vector2.Distance(new Vector2(biomeX, biomeY), new Vector2(x, y)) < RadiusBiomeW)
                    {
                        WorldGen.KillWall(x, y);
                        WorldGen.PlaceWall(x, y, ModContent.WallType<ZSWT>());
                    }
                }
            }
            StructureHelper.Generator.GenerateStructure("WorldGen/ZenHouse", new Point16(biomeX - 8, biomeY), ModContent.GetInstance<ZensTweakstest>());//This Structure Line Is After The Biome Changes To Prevent Wall Distruction...
            StructureHelper.Generator.GenerateStructure("WorldGen/WalledShrine", new Point16(biomeX - 5 + Main.rand.Next(-35, 35), biomeY + 28), ModContent.GetInstance<ZensTweakstest>());//This Structure Line Is After The Biome Changes To Prevent Wall Distruction...
        }
        //private void EndothermicTest(GenerationProgress progress)
        //{

        //}
        public override void PostWorldGen()
        {
            int[] itemsToPlaceInDungeonChests = { ModContent.ItemType<ZenedSlanger>() };
            int itemsToPlaceInDungeonChestChoice = 0;
            int zenedSlangerSpawns = 0;

            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                if (zenedSlangerSpawns > 2)
                {
                    return;
                }
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 2 * 36)
                {
                    int InventoryIndex = 0;
                    chest.item[InventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInDungeonChests));
                    itemsToPlaceInDungeonChestChoice = (itemsToPlaceInDungeonChestChoice + 1) % itemsToPlaceInDungeonChests.Length;
                    zenedSlangerSpawns++;
                    break;
                }
            }

            int[] itemsToPlaceInIceChests = { ModContent.ItemType<JellyMembrane>() };
            int itemsToPlaceInIceChestChoice = 0;
            int IceMemSpawns = 0;

            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                if (IceMemSpawns > 4)
                {
                    return;
                }
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 11 * 36)
                {
                    int InventoryIndex = 0;
                    chest.item[InventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
                    itemsToPlaceInIceChestChoice = (itemsToPlaceInIceChestChoice + 1) % itemsToPlaceInIceChests.Length;
                    IceMemSpawns++;
                    break;
                }
            }
        }
    }
    public class AmbeintZen1X2T : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
            disableSmartCursor = true;

            ModTranslation name = CreateMapEntryName();
            AddMapEntry(new Color(116, 94, 114), name);
        }
        public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
        {
            if (i % 2 == 1)
                effects = SpriteEffects.FlipHorizontally;
        }
    }
}