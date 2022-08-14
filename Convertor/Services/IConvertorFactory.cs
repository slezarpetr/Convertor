namespace Convertor.Services;

public interface IConvertorFactory
{
    Type[] GetRegisteredDocumentTypes { get; }
    ConvertorService GetConvertorService(Type documentType);
}