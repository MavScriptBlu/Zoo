using System;
using System.Text.RegularExpressions;
using Animals;
using People;
using Reproducers;

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

        /// <summary>
        /// Prompts the user for a string value and ensures it is not empty.
        /// </summary>
        /// <param name="prompt">The prompt to display.</param>
        /// <returns>The entered string value.</returns>
        public static string ReadStringValue(string prompt)
        {
            string result = null;
            bool found = false;

            while (!found)
            {
                Console.Write(prompt + "] ");
                string stringValue = Console.ReadLine().ToLower().Trim();
                Console.WriteLine();

                if (stringValue != string.Empty)
                {
                    result = stringValue;
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must have a value.");
                }
            }

            return result;
        }

        /// <summary>
        /// Prompts the user for an alphabetic value (letters and spaces only).
        /// </summary>
        /// <param name="prompt">The prompt to display.</param>
        /// <returns>The entered alphabetic value.</returns>
        public static string ReadAlphabeticValue(string prompt)
        {
            string result = null;
            bool found = false;

            while (!found)
            {
                result = ConsoleUtil.ReadStringValue(prompt);

                if (Regex.IsMatch(result, @"^[a-zA-Z ]+$"))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must contain only letters or spaces.");
                }
            }

            return result;
        }

        /// <summary>
        /// Prompts the user for an integer value.
        /// </summary>
        /// <param name="prompt">The prompt to display.</param>
        /// <returns>The entered integer value.</returns>
        public static int ReadIntValue(string prompt)
        {
            int result = 0;
            string stringValue = result.ToString();
            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (int.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be a whole number.");
                }
            }

            return result;
        }

        /// <summary>
        /// Prompts the user for a double value.
        /// </summary>
        /// <param name="prompt">The prompt to display.</param>
        /// <returns>The entered double value.</returns>
        public static double ReadDoubleValue(string prompt)
        {
            double result = 0;
            string stringValue = result.ToString();
            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (double.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be either a whole number or a decimal number.");
                }
            }

            return result;
        }

        /// <summary>
        /// Prompts the user for a gender value.
        /// </summary>
        /// <returns>The entered gender value.</returns>
        public static Gender ReadGender()
        {
            Gender result = Gender.Female;
            string stringValue = result.ToString();
            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Gender");
                stringValue = ConsoleUtil.InitialUpper(stringValue);

                if (Enum.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid gender.");
                }
            }

            return result;
        }

        /// <summary>
        /// Prompts the user for an animal type value.
        /// </summary>
        /// <returns>The entered animal type value.</returns>
        public static AnimalType ReadAnimalType()
        {
            AnimalType result = AnimalType.Dingo;
            string stringValue = result.ToString();
            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Animal type");
                stringValue = ConsoleUtil.InitialUpper(stringValue);

                if (Enum.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid animal type.");
                }
            }

            return result;
        }

        /// <summary>
        /// Prompts the user for a wallet color value.
        /// </summary>
        /// <returns>The entered wallet color value.</returns>
        public static WalletColor ReadWalletColor()
        {
            WalletColor result = WalletColor.Black;
            string stringValue = result.ToString();
            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Wallet color");
                stringValue = ConsoleUtil.InitialUpper(stringValue);

                if (Enum.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid wallet color.");
                }
            }

            return result;
        }
    }
}
