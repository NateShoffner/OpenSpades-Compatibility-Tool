namespace OpenSpadesCompatibilityTool
{
    internal static class Common
    {
        public static string TruncateVersion(string version)
        {
            while (version.EndsWith("0") || version.EndsWith("."))
                version = version.Remove(version.Length - 1, 1);

            if (!version.Contains("."))
                version = string.Format("{0}.0", version);

            return version;
        }
    }
}