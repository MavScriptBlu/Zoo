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
        /// Prompts the user for a non-empty string value.
        /// </summary>
        /// <param name="prompt">The prompt to display.</param>
        /// <returns>The entered string value.</returns>
        public static string ReadStringValue(string prompt)
        {
            string result = string.Empty;

            while (result == string.Empty)
            {
                Console.Write(prompt + ": ");
                result = Console.ReadLine().ToLower().Trim();

                if (result == string.Empty)
                {
                    Console.WriteLine(prompt + " must have a value.");
                }
            }

            return result;
        }

        /// <summary>
        /// Prompts the user for an alphabetic string value (letters and spaces only).
        /// </summary>
        /// <param name="prompt">The prompt to display.</param>
        /// <returns>The entered alphabetic string value.</returns>
        public static string ReadAlphabeticValue(string prompt)
        {
            string result = string.Empty;
            bool isValid = false;

            while (!isValid)
            {
                result = ReadStringValue(prompt);

                if (Regex.IsMatch(result, @"^[a-zA-Z ]+$"))
                {
                    isValid = true;
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
            bool isValid = false;

            while (!isValid)
            {
                string input = ReadStringValue(prompt);

                if (int.TryParse(input, out result))
                {
                    isValid = true;
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
            bool isValid = false;

            while (!isValid)
            {
                string input = ReadStringValue(prompt);

                if (double.TryParse(input, out result))
                {
                    isValid = true;
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
            bool isValid = false;

            while (!isValid)
            {
                string input = InitialUpper(ReadAlphabeticValue("Gender"));

                if (Enum.TryParse(input, out result))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid gender.");
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
            bool isValid = false;

            while (!isValid)
            {
                string input = InitialUpper(ReadAlphabeticValue("Wallet color"));

                if (Enum.TryParse(input, out result))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid wallet color.");
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
            bool isValid = false;

            while (!isValid)
            {
                string input = InitialUpper(ReadAlphabeticValue("Animal type"));

                if (Enum.TryParse(input, out result))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid animal type.");
                }
            }

            return result;
        }
    }
}
