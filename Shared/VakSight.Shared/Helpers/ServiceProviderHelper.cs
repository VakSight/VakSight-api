using System;

namespace VakSight.Shared.Helpers
{
    public static class ServiceProviderHelper
    {
        public static T Resolve<T>(this IServiceProvider serviceProvider)
        {
            return (T)serviceProvider.GetService(typeof(T));
        }
    }
}
