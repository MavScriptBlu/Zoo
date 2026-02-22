namespace ZooConsole
{
    /// <summary>
    /// The class which provides utility methods for the console application.
    /// </summary>
    internal static class ConsoleUtil
    {
        /// <summary>
        /// Capitalizes the first letter of a string.
        /// </summary>
        /// <param name="str">The string to capitalize.</param>
        /// <returns>The string with its first letter capitalized.</returns>
        public static string InitialUpper(string str)
        {
            if (str == null || str.Length == 0)
            {
                return str;
            }

            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
