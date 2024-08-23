using Microsoft.Extensions.DependencyInjection;

namespace 鹰眼OCR_WPFCore.Extensions
{
    /// <summary>
    /// ServiceProvider扩展类
    /// </summary>
    public static class ServiceProviderExtensions
    {

        // 映射字典 将key和Type建立映射关系
        private static readonly Dictionary<string, Type> servicesDictionary = new Dictionary<string, Type>();

        public static IServiceCollection AddSingletonEx<TService>(this IServiceCollection services, string key) where TService : class
        {
            if (servicesDictionary.ContainsKey(key))
            {
                throw new InvalidOperationException($"Key {key} has already been used");
            }
            servicesDictionary[key] = typeof(TService);
            services.AddSingleton<TService>();
            return services;
        }

        public static object GetRequiredKeyedServiceEx(this IServiceProvider provider, string key)
        {
            if (servicesDictionary.TryGetValue(key, out var serviceType))
            {
                return provider.GetRequiredService(serviceType);
            }
            throw new InvalidOperationException($"No service registered with key {key}");
        }
    }
}
