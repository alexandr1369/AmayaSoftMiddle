using UnityEngine;

namespace Utils
{
    public interface IFactory<out T> where T : MonoBehaviour
    {
        T Create();
    }
}