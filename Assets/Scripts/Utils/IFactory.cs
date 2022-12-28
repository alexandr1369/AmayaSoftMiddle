namespace Utils
{
    public interface IFactory<out T>
    {
        T Create();
    }
}