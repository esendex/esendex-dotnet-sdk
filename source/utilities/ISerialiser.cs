
namespace com.esendex.sdk.utilities
{
    internal interface ISerialiser
    {
        string Serialise<T>(T obj);
        T Deserialise<T>(string source);
    }
}
