using Convertor.Models;

namespace Convertor.Services
{
    public class ConvertorFactory : IConvertorFactory
    {
        private readonly Dictionary<Type, Func<ConvertorService>> _convertors;

        public ConvertorFactory(Dictionary<Type, Func<ConvertorService>> convertors)
        {
            _convertors = convertors;
        }

        public Type[] GetRegisteredDocumentTypes => _convertors.Keys.ToArray();

        public ConvertorService GetConvertorService(Type documentType)
        {
            if (!_convertors.TryGetValue(documentType, out var factory) || factory is null)
                throw new ArgumentOutOfRangeException(nameof(documentType), $"type '{documentType}' is not registered");
            return factory();
        }

    }
}
