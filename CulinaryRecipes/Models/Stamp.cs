using System;

namespace CulinaryRecipes.Models
{
    class Stamp
    {
        private static string stringOfCharacters = "][";

        /// <summary>
        ///  Adds a character to an empty field (preventing it from moving up)
        /// </summary>
        public static string[] AddStamp(string[] tableName)
        {
            string[] newTable;

            if (tableName.Length == 0)
            {
                newTable = new string[tableName.Length + 1];
            }
            else
            {
                newTable = new string[tableName.Length];
                newTable = tableName;
            }

            for (int i = 0; i < newTable.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(newTable[i]))
                {
                    newTable[i] = stringOfCharacters;
                }
                else
                {
                    continue;
                }
            }

            return tableName = newTable;
        }

        /// <summary>
        /// Removes extra characters
        /// </summary>
        /// <param name="nameVariableForm1"></param>
        /// <returns></returns>
        public static string RemoveCharacters(string nameVariableForm1)
        {
            string newText = nameVariableForm1;
            if (nameVariableForm1.Contains(stringOfCharacters))
            {
               newText = nameVariableForm1.Replace(stringOfCharacters, "");
            }

            return newText;
        }

        /// <summary>
        /// Returns the extra character
        /// </summary>
        /// <returns></returns>
        public static string StampsCharacters()
        {
            return stringOfCharacters;
        }
    }
}
