#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("xUxt0GhJPcqrcJf9s9Io9WZEW6bAhfDwDfVkbNzlOTMqNb5VhNoFI7nFg4v+P5j2g8rMXFwWcHsR3roizEhv3SeFghk5xkfWFYBxm+RKeRt3E3LwGMFWQrmHeP7CmV8nAysBxdOwG4uzYdmBeC2/xA5EpjGco2RsUM7aT0Wd/z5QuSVcRCxj46KsAy0u6KuO+NSYEkvfboYDjTemytikk4kKBAs7iQoBCYkKCguptVgJGI7qO4kKKTsGDQIhjUON/AYKCgoOCwjj/z/x50j32P8l30S1DTkTfrb7NdPGLu66okFJd4WnJJC2XjyyobwvC/S1lq86paQaQRNhwXRhPJHPpkW5fplwQtdDcEMs6ehuy3XWMTkv/6xy1gZ8dxIBAAkICgsK");
        private static int[] order = new int[] { 10,12,9,8,5,9,13,8,12,10,11,11,13,13,14 };
        private static int key = 11;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
