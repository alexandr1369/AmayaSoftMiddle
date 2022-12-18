﻿using System.IO;
using StateSystem;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class StateMenu
    {
        [MenuItem("AmayaSoft/State/Remove Save File &R")]
        public static void RemoveSaveFile()
        {
            var path = GameStateService.SaveFile;
            
            if (File.Exists(path)) 
                File.Delete(path);

            path = GameStateService.BackupFile;
            
            if (File.Exists(path)) 
                File.Delete(path);

            PlayerPrefs.DeleteAll();
            
            Debug.Log("[Zorg] Game State has been fully cleared!");
        }
    }
}