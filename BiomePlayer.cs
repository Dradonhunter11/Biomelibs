using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BiomeLibrary
{
    public class BiomePlayer : ModPlayer
    {
        public override void Load(TagCompound tag)
        {
            
            BiomeLibs.player = Main.LocalPlayer.GetModPlayer<BiomePlayer>();
        }

        public override void UpdateBiomes()
        {
            for (var i = 0; i < BiomeLibs.name.Count; i++)
                BiomeLibs.biomeRegistery[BiomeLibs.name[i]] = BiomeLibs.BiomeList[BiomeLibs.name[i]].isValid();
        }

        public bool Inbiome(string biomeName)
        {
            return BiomeLibs.BiomeList[biomeName].isValid() && Main.player[Main.myPlayer].active;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            var flags = new BitsByte();
            for (var i = 0; i < BiomeLibs.name.Count; i++)
                flags[i] = BiomeLibs.biomeRegistery[BiomeLibs.name[i]];
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flag = reader.ReadByte();
            for (var i = 0; i < BiomeLibs.name.Count; i++)
                BiomeLibs.biomeRegistery[BiomeLibs.name[i]] = flag[i];
        }
    }
}
