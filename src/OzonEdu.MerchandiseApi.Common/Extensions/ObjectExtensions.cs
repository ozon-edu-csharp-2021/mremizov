namespace OzonEdu.MerchandiseApi.Common
{
    public static class ObjectExtensions
    {
        public static bool Is<T>(this object obj) where T : class
        {
            return obj is T;
        }

        public static T To<T>(this object obj)
        {
            return (T)obj;
        }

        public static T As<T>(this object obj) where T : class
        {
            return obj as T;
        }
    }
}
