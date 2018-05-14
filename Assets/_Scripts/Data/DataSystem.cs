using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace GameUtils {
    namespace DataSystem {
        public static class DataSystem {

            public static void SaveData(GameData gd)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/rpggame.dat");
                bf.Serialize(file, gd);
                file.Close();
            }

            public static void NewGame()
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/rpggame.dat");

                GameData gamedata = new GameData("Rambo", 1, new Vector3(0, 0, 0), 20, 20, 10, 10, 1, 1, 1, 1, 1, 1, 1);
                bf.Serialize(file, gamedata);
                file.Close();
            }

            public static GameData LoadGame()
            {
                if (FileExist)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(Application.persistentDataPath + "/rpggame.dat", FileMode.Open);
                    GameData gamedata = (GameData)bf.Deserialize(file);
                    file.Close();
                    return gamedata;
                }
                return null;
            }

            public static void PopulateDataBlocks()
            {

            }

            public static bool FileExist
            {
                get { return File.Exists(Application.persistentDataPath + "/rpggame.dat"); }
            }
        }
    }
}
