using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using Utils.Serialization;

namespace StateSystem
{
    public class GameStateService
    {
        private const string SAVE_FILE_NAME = "State";

        public static readonly string SaveFile = $"{Application.persistentDataPath}/{SAVE_FILE_NAME}.save";
        public static readonly string BackupFile = $"{Application.persistentDataPath}/{SAVE_FILE_NAME}.backup";
        
        private static readonly string TempFile = $"{Application.persistentDataPath}/{SAVE_FILE_NAME}.tmp"; 

        public GameState State
        {
            get
            {
                if (_state == null) 
                    LoadAnyState();

                return _state;
            }
        }
        
        private GameState _state;

        public GameStateService() => LoadAnyState();

        public void Save()
        {
            Save(TempFile);

            if (File.Exists(SaveFile))
            {
                File.Delete(BackupFile);
                File.Move(SaveFile, BackupFile);
                File.Delete(SaveFile);
            }
            
            File.Move(TempFile, SaveFile);
            File.Delete(TempFile);
        }
        
        private void Save(string path)
        {
            var bf = new ZorgBinaryFormatter();
            var file = File.Create(path);

            if (!file.CanWrite)
            {
                file.Close();
                throw new Exception($"Failed save GameState to file with {path} - Can't Write.");
            }

            try
            {
                bf.Serialize(file, State);
            }
            catch (SerializationException ex)
            {
                file.Close();
                throw new Exception($"Failed save GameState to file with {path}:", ex);
            }
            
            file.Close();
        }

        private void LoadAnyState()
        {
            LoadWithPath(SaveFile);

            if (_state == null) 
                LoadWithPath(BackupFile);

            if (_state != null)
                return;

            _state = new GameState(); 
            
            ClearSavingFolder();
        }

        private void ClearSavingFolder()
        {
            var di = new DirectoryInfo(Application.persistentDataPath);

            foreach (var file in di.GetFiles()) 
                file.Delete();

            foreach (var directory in di.GetDirectories()) 
                directory.Delete(true);
        }

        internal void ClearState()
        {
            var path = SaveFile;
            
            if (File.Exists(path)) 
                File.Delete(path);

            path = BackupFile;
            
            if (File.Exists(path)) 
                File.Delete(path);

            PlayerPrefs.DeleteAll();
            
            _state = null;
        }

        private void LoadWithPath(string path)
        {
            if (!File.Exists(path))
                return;

            FileStream fs = null;
            
            try
            {
                var bf = new ZorgBinaryFormatter();
                fs = File.Open(path, FileMode.Open);
                
                _state = (GameState) bf.Deserialize(fs);
                fs.Close();
            }
            catch (SerializationException ex)
            {
                Debug.Log("[Zorg] Serialization failed: " + ex.Message);
                _state = null;
            }
            finally
            {
                fs?.Close();
            }
        }
    }
}