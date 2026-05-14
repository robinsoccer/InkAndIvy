using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace InkAndIvy
{
    public class PlayerData
    {
        public string Name;
        public Vector2 Position;

        public PlayerData() { }

        public void SaveGame(PlayerData data, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
            using (StreamWriter writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, data);
            }
        }

        public PlayerData LoadGame(string filename)
        {
            if (!File.Exists(filename)) return new PlayerData();

            XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
            using (StreamReader reader = new StreamReader(filename))
            {
                return (PlayerData)serializer.Deserialize(reader);
            }
        }
    }
}
