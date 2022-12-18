using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

namespace Utils
{
    public static class GameUtils
    {
        public const int TARGET_FPS = 60;
        public const float PAUSE_TIME_SCALE = 0;
        public const float DEFAULT_TIME_SCALE = 1f;
        public const float TOLERANCE = .01f;
        public const float CHECK_STATE_DELAY = .05f;
        
        public static object GetRandomObject(params object[] objects) => 
            objects.Length <= 0 ? null : objects[Random.Range(0, objects.Length)];

        public static async Task<Texture2D> GetTexture(string url)
        {
            using var uwr = UnityWebRequestTexture.GetTexture(url);
            var asyncOp = uwr.SendWebRequest();
               
            while(!asyncOp.isDone)
                await Task.Yield();

            if (uwr.result == UnityWebRequest.Result.Success)
                return DownloadHandlerTexture.GetContent(asyncOp.webRequest);
            
            Debug.Log(uwr.error);
                   
            return null;
        }

        public static Quaternion SetRotationAngle(Quaternion target, Vector3 angle)
        {
            var eulerAngles = new Vector3(angle.x, angle.y, angle.z);
            target.eulerAngles = eulerAngles;
            
            return target;
        }

        public static Vector3 MainCameraScreenToWorld(Vector2 position) => Camera.main!.ScreenToWorldPoint(position);
        
        public static Vector3 MainCameraWorldToScreen(Vector3 position) => Camera.main!.WorldToScreenPoint(position);

        public static int GetCurrentScreenRefreshRate() => Screen.currentResolution.refreshRate;

        public static int Range(int min, int max) => Random.Range(min, max + 1);

        public static float Range(float min, float max) => Random.Range(min, max);

        public static float GetWorldScreenWidth() => 
            Camera.main!.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        public static float GetWorldScreenHeight() => 
            Camera.main!.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

        public static float ClampAngle(float current, float min, float max)
        {
            var dtAngle = Mathf.Abs(((min - max) + 180) % 360 - 180);
            var hdtAngle = dtAngle * 0.5f;
            var midAngle = min + hdtAngle;
            var offset = Mathf.Abs(Mathf.DeltaAngle(current, midAngle)) - hdtAngle;
 
            if (offset > 0)
                current = Mathf.MoveTowardsAngle(current, midAngle, offset);
            
            return current;
        }

        /// <summary>
        /// Get chance by percent.
        /// </summary>
        /// <param name="percent">from 0 to 100.</param>
        /// <returns></returns>
        public static bool GetChance(float percent)
        {
            const float minPercent = 0;
            const float maxPercent = 100f;
            percent = Mathf.Clamp(percent, minPercent, maxPercent);
            
            return Random.Range(minPercent, maxPercent) <= percent;
        }
    }
}