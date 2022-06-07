using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

namespace LevelManagement.Data
{
    public class JsonSaver
    {
        private static readonly string _fileName = "saveData1.sav";

        public static string GetSaveFileName() {
            return Application.persistentDataPath + "/" + _fileName;
        }

        public void Save(SaveData data) {
            data.hashValue = String.Empty;
            string json = JsonUtility.ToJson(data);
            data.hashValue = GetSHA256(json);
            json = JsonUtility.ToJson(data);
            string saveFileName = GetSaveFileName();

            FileStream fileStream = new FileStream(saveFileName, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fileStream)) {
                writer.Write(json);
            }
        }

        public bool Load(SaveData data) {
            string loadFileName = GetSaveFileName();
            if (File.Exists(loadFileName)) {
                using (StreamReader reader = new StreamReader(loadFileName)) {
                    string json = reader.ReadToEnd();

                    if (CheckData(json)) {
                        //Save files are OK
                        JsonUtility.FromJsonOverwrite(json, data);
                    } else {
                        //Someone changed the save files (Hacked)
                        Debug.LogWarning("JSONSAVER Load: invalid hash.");
                    }
                }
                return true;
            }
            return false;
        }

        private bool CheckData(string json) {
            SaveData tempSaveData = new SaveData();
            JsonUtility.FromJsonOverwrite(json, tempSaveData);

            string oldHash = tempSaveData.hashValue;
            tempSaveData.hashValue = String.Empty;

            string tempJson = JsonUtility.ToJson(tempSaveData);
            string newHash = GetSHA256(tempJson);

            return ( oldHash == newHash );
        }

        public void Delete() {
            File.Delete(GetSaveFileName());
        }

        public string GetHexStringFromHash(byte[] hash) {
            string hexString = String.Empty;
            foreach (byte b in hash) {
                hexString += b.ToString("x2");
            }
            return hexString;
        }

        private string GetSHA256(string text) {
            byte [ ] textToBytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed mySHA256 = new SHA256Managed();
            byte [ ] hashValue = mySHA256.ComputeHash(textToBytes);

            return GetHexStringFromHash(hashValue);
        }
    }
}