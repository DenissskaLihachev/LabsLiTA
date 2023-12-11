using System;
using System.Text;

class MarkovAlgorithm
{
    private Dictionary<string, string> rules;

    public MarkovAlgorithm(Dictionary<string, string> rules)
    {
        this.rules = rules;
    }

    public string Apply(string input)
    {
        string result = input;

        while (true)
        {
            string originalResult = result;

            foreach (var rule in rules)
            {
                for (int i = 0; i < 5; i++)
                {
                    result = result.Replace(rule.Key, rule.Value);
                }
                //Console.WriteLine(result);
            }

            // Если не произошло изменений, завершаем цикл
            if (result == originalResult)
            {
                break;
            }
        }

        return result;
    }
}
class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("1) Практическая работа №8\n" +
                          "2) Практическая работа №9\n" +
                          "3) Практическая работа №10\n" +
                          "Ввод: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (choice)
            {
                case 1:
                    Pract8();
                    break;
                case 2:
                    Pract9();
                    break;
                case 3:
                    break;
            }
        }
    }

    static void Pract9()
    {
        Task1();
        Console.ReadKey();
        Task2_3_4();
        Console.ReadKey();
        Task5();
        Console.ReadKey();
        Console.Clear();
    }

    private static char[,] polybiusSquare =
    {
        {'А', 'Б', 'В', 'Г', 'Д', 'Е'},
        {'Ё', 'Ж', 'З', 'И', 'Й', 'К'},
        {'Л', 'М', 'Н', 'О', 'П', 'Р'},
        {'С', 'Т', 'У', 'Ф', 'Х', 'Ц'},
        {'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь'},
        {'Э', 'Ю', 'Я', ' ', '.', '-'}
    };
    private static readonly string RussianAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

    static void Task1()
    {
        Console.Write("\t\tЗАДАНИЕ 1\n");
        Console.Write("__________________________________________\n\n");
        Console.Write("\t\tШифр Цезаря\n\n");
        //Console.Write("Введите шифруемое слово: "); string word = Console.ReadLine();

        string text = "Причина страха - неизвестность.";
        int shift = 3;

        // Зашифровка
        string encryptedText = EncryptCesar(text, shift);
        Console.WriteLine($"Зашифрованный текст: {encryptedText}");
        // Расшифровка
        string decryptedText = DecryptCesar(encryptedText, shift);
        Console.WriteLine($"Расшифрованный текст: {decryptedText}");

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Console.ReadKey();
        Console.Write("__________________________________________\n\n");
        Console.Write("\t\tШифр Полибия\n\n");

        // Зашифровка
        encryptedText = EncryptPolybius(text.ToUpper());
        Console.WriteLine($"Зашифрованный текст: {encryptedText}");
        // Расшифровка
        decryptedText = DecryptPolybius(encryptedText);
        Console.WriteLine($"Расшифрованный текст: {decryptedText}");

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Console.ReadKey();
        Console.Write("__________________________________________\n\n");
        Console.Write("\t\tШифр Вижинера\n\n");

        string key = "КЛЮЧЮЧ";

        // Зашифровка
        encryptedText = EncryptVigenere(text, key);
        Console.WriteLine($"Зашифрованный текст: {encryptedText}");
        // Расшифровка
        decryptedText = DecryptVigenere(encryptedText, key);
        Console.WriteLine($"Расшифрованный текст: {decryptedText}");

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Console.ReadKey();
        Console.Write("__________________________________________\n\n");
        Console.Write("\t\tШифр простой перестановки\n\n");

        string tempText = "Причина страха - неизвестностьОО.";
        // Зашифровка
        encryptedText = SimpleEncrypt(tempText);
        Console.WriteLine($"Зашифрованный текст: {encryptedText}");
        // Расшифровка
        Console.WriteLine($"Расшифрованный текст: {text}");

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Console.ReadKey();
        Console.Write("__________________________________________\n\n");
        Console.Write("\t\tШифр простой столбцевой перестановки\n\n");

        // Зашифровка
        encryptedText = EncryptColumnarTransposition(text);
        Console.WriteLine($"Зашифрованный текст: {encryptedText}");
        // Расшифровка
        decryptedText = DecryptColumnarTransposition(encryptedText);
        Console.WriteLine($"Расшифрованный текст: {decryptedText}");

        //////////////////////////////////////////////////////методы/////////////////////////////////////////////////////

        static string EncryptColumnarTransposition(string text)
        {
            int numRows = Convert.ToInt32(Math.Ceiling((double)text.Length / 6));
            int numCols = 6;

            char[,] matrix = new char[numRows, numCols];

            // Заполняем матрицу символами из текста
            int index = 0;

            for (int col = 0; col < numCols; col++)
            {
                for (int row = 0; row < numRows; row++)
                {
                    if (index < text.Length)
                    {
                        matrix[row, col] = text[index++];
                    }
                    else
                    {
                        matrix[row, col] = ' '; // Дополняем пробелами, если символов не хватает
                    }
                }
            }

            StringBuilder result = new StringBuilder();

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    result.Append(matrix[row, col]);
                }
            }

            return result.ToString();
        }
        static string DecryptColumnarTransposition(string text)
        {
            int numRows = Convert.ToInt32(Math.Ceiling((double)text.Length / 6));
            int numCols = 6;

            char[,] matrix = new char[numRows, numCols];

            // Заполняем матрицу символами из текста
            int index = 0;

            for (int col = 0; col < numCols; col++)
            {
                for (int row = 0; row < numRows; row++)
                {
                    matrix[row, col] = text[index++];
                }
            }

            StringBuilder result = new StringBuilder();

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    result.Append(matrix[row, col]);
                }
            }

            return result.ToString();
        }

        static string SimpleEncrypt(string plaintext)
        {
            // Переворачиваем текст
            char[] reversedText = plaintext.ToCharArray();
            Array.Reverse(reversedText);

            string result = new string(reversedText);
            result = result.Replace(" ", "");
            char ch = ' ';            

            return InsertCharacterEveryNth(result, ch, 5);
        }
        static string InsertCharacterEveryNth(string input, char character, int n)
        {
            StringBuilder result = new StringBuilder(input.Length + input.Length / n);

            for (int i = 0; i < input.Length; i++)
            {
                result.Append(input[i]);

                if ((i + 1) % n == 0)
                {
                    result.Append(character);
                }
            }

            return result.ToString();
        }

        static string EncryptVigenere(string plaintext, string key)
        {
            StringBuilder result = new StringBuilder();

            plaintext = plaintext.ToUpper();
            key = key.ToUpper();

            for (int i = 0, j = 0; i < plaintext.Length; i++)
            {
                char currentChar = plaintext[i];

                if (RussianAlphabet.Contains(currentChar.ToString()))
                {
                    int shift = RussianAlphabet.IndexOf(key[j % key.Length]);
                    int newIndex = (RussianAlphabet.IndexOf(currentChar) + shift) % RussianAlphabet.Length;

                    result.Append(RussianAlphabet[newIndex]);
                    j++;
                }
                else
                {
                    result.Append(currentChar);
                }
            }

            return result.ToString();
        }
        static string DecryptVigenere(string ciphertext, string key)
        {
            StringBuilder result = new StringBuilder();

            ciphertext = ciphertext.ToUpper();
            key = key.ToUpper();

            for (int i = 0, j = 0; i < ciphertext.Length; i++)
            {
                char currentChar = ciphertext[i];

                if (RussianAlphabet.Contains(currentChar.ToString()))
                {
                    int shift = RussianAlphabet.IndexOf(key[j % key.Length]);
                    int newIndex = (RussianAlphabet.IndexOf(currentChar) - shift + RussianAlphabet.Length) % RussianAlphabet.Length;

                    result.Append(RussianAlphabet[newIndex]);
                    j++;
                }
                else
                {
                    result.Append(currentChar);
                }
            }

            return result.ToString();
        }

        static string EncryptPolybius(string plaintext)
        {
            StringBuilder result = new StringBuilder();

            foreach (char symbol in plaintext)
            {
                int[] coordinates = FindCoordinates(symbol);
                result.Append($"{coordinates[0]}{coordinates[1]} ");
            }

            return result.ToString().Trim();
        }
        static string DecryptPolybius(string ciphertext)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < ciphertext.Length; i += 3)
            {
                int x = int.Parse(ciphertext[i].ToString());
                int y = int.Parse(ciphertext[i + 1].ToString());
                result.Append(polybiusSquare[x - 1, y - 1]);
            }

            return result.ToString();
        }
        static int[] FindCoordinates(char symbol)
        {
            for (int i = 0; i < polybiusSquare.GetLength(0); i++)
            {
                for (int j = 0; j < polybiusSquare.GetLength(1); j++)
                {
                    if (polybiusSquare[i, j] == char.ToUpper(symbol))
                    {
                        return new int[] { i + 1, j + 1 };
                    }
                }
            }

            throw new ArgumentException($"Symbol '{symbol}' not found in the Polybius square.");
        }

        static string EncryptCesar(string plaintext, int shift)
        {
            char[] result = new char[plaintext.Length];

            for (int i = 0; i < plaintext.Length; i++)
            {
                if (char.IsLetter(plaintext[i]))
                {
                    char baseLetter = char.IsUpper(plaintext[i]) ? 'А' : 'а';
                    result[i] = (char)(((plaintext[i] + shift - baseLetter + 32) % 32) + baseLetter);
                }
                else
                {
                    result[i] = plaintext[i];
                }
            }

            return new string(result);
        }
        static string DecryptCesar(string ciphertext, int shift)
        {
            return EncryptCesar(ciphertext, -shift);
        }
    }
    static void Task2_3_4()
    {
        //НЕ РАБОТАЕТ ПОЧТИ (НЕТУ Ё)
        Console.WriteLine("Задание 2\n\nИспользовался шифр Цезаря\n\n");
        string originalText = "ФПЗОЮП ФЦЖЯДГ ТСПСЁГЗХ";

        string decryptedText = DecryptCesar(originalText, 3);
        Console.WriteLine($"Расшифрованный текст: {decryptedText}");

        Console.ReadKey();
        Console.Clear();

        //РАБОТАЕТ
        Console.WriteLine("Задание 3\n\nИспользовался шифр простой столбцевой перестановки\n\n");
        originalText = "ВВООСИТВЕС_Е_ИЧКЗТЕАА_Л_";

        decryptedText = DecryptColumnarTransposition(originalText);
        Console.WriteLine($"Расшифрованный текст: {decryptedText}");

        Console.ReadKey();
        Console.Clear();

        //СЛОВО ПРОГРАММА
        Console.WriteLine("Задание 4\n\nИспользовался шифр Полибия\n\n");
        originalText = "ВГВДВВАГВДААВАВААА";

        decryptedText = DecryptColumnarTransposition(originalText);
        Console.WriteLine($"Расшифрованный текст: {decryptedText}");

        //////////////////////////////////////////////////////методы/////////////////////////////////////////////////////

        static string DecryptColumnarTransposition(string text)
        {
            int numRows = Convert.ToInt32(Math.Ceiling((double)text.Length / 6));
            int numCols = 6;

            char[,] matrix = new char[numRows, numCols];

            // Заполняем матрицу символами из текста
            int index = 0;

            for (int col = 0; col < numCols; col++)
            {
                for (int row = 0; row < numRows; row++)
                {
                    matrix[row, col] = text[index++];
                }
            }

            StringBuilder result = new StringBuilder();

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    result.Append(matrix[row, col]);
                }
            }

            return result.ToString();
        }

        static string EncryptCesar(string plaintext, int shift)
        {
            char[] result = new char[plaintext.Length];

            for (int i = 0; i < plaintext.Length; i++)
            {
                if (char.IsLetter(plaintext[i]))
                {
                    char baseLetter = char.IsUpper(plaintext[i]) ? 'А' : 'а';
                    result[i] = (char)(((plaintext[i] + shift - baseLetter + 32) % 32) + baseLetter);
                }
                else
                {
                    result[i] = plaintext[i];
                }
            }

            return new string(result);
        }

        static string DecryptCesar(string ciphertext, int shift)
        {
            return EncryptCesar(ciphertext, -shift);
        }
    }
    static void Task5()
    {
        Console.WriteLine("\t\tЗеркально-Рандомный Шифр");
        Console.WriteLine("------------------------------------------------------\n\n");
        Console.WriteLine("Введите строку: "); string originalString = Console.ReadLine();
        //string originalString = "Пример строки для шифрования";
        Console.WriteLine("Зашифрованный текст: " + Encrypt(originalString));
        Console.WriteLine("Расшифрованный текст: " + Decrypt(Encrypt(originalString)));
        
        static string Encrypt(string text)
        {
            string russianAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            Random random = new Random();
            int randomIndex;
            char randomChar;

            char[] reversedText = text.ToCharArray();
            Array.Reverse(reversedText);
            string temp = new string(reversedText);

            StringBuilder stringWithInserts = new StringBuilder();
            for (int i = 0; i < temp.Length; i++)
            {
                stringWithInserts.Append(temp[i]);

                // Вставляем символ после каждого второго символа
                if ((i + 1) % 2 == 0)
                {
                    randomIndex = random.Next(0, russianAlphabet.Length);
                    randomChar = russianAlphabet[randomIndex];
                    stringWithInserts.Append(randomChar);
                }
            }
            string result = stringWithInserts.ToString();
            return result;
        }        
        static string Decrypt(string text)
        {

            StringBuilder stringWithoutEveryThirdChar = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                // Пропускаем каждый третий символ
                if ((i + 1) % 3 != 0)
                {
                    stringWithoutEveryThirdChar.Append(text[i]);
                }
            }
            string result = stringWithoutEveryThirdChar.ToString();

            char[] reversedText = result.ToCharArray();
            Array.Reverse(reversedText);
            result = new string(reversedText);

            return result;
        }
    }

    static void Pract8()
    {
        while (true)
        {
            Console.Write("1) Задание 1.1\n" +
                          "2) Задание 1.2\n" +
                          "3) Задание 1.3\n" +
                          "4) Задание 2.1\n" +
                          "5) Задание 2.2\n" +
                          "6) Задание 2.3\n" +
                          "0) ВЫХОД\n" +
                          "Ввод: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            if (choice == 0)
                break;

            switch (choice)
            {
                case 1:
                    Task1_1();
                    break;
                case 2:
                    Task1_2();
                    break;
                case 3:
                    Task1_3();
                    break;
                case 4:
                    Task2_1();
                    break;
                case 5:
                    Task2_2();
                    break;
                case 6:
                    Task2_3();
                    break;
            }
            Console.ReadKey();
            Console.Clear();

            //Задания
            static void Task2_3()
            {
                var rules = new Dictionary<string, string>
        {
            { "2 + 2", "4" },
            { "1 + 1", "2" },
            { "4 + 4", "8" }
        };

                var markovAlgorithm = new MarkovAlgorithm(rules);

                string input = "3 + 2 + 2 + 2 + 1 + 1";
                string result = markovAlgorithm.Apply(input);

                Console.WriteLine("Исходная строка: " + input);
                Console.WriteLine("Результат: " + result);
            }
            static void Task2_2()
            {
                var rules = new Dictionary<string, string>
        {
            { "К", "Р" },
            { "ЗА", "ЛИК" },
            { "С", "З" },
            { "РО", "Б" }
        };

                var markovAlgorithm = new MarkovAlgorithm(rules);

                string input = "КОСА";
                string result = markovAlgorithm.Apply(input);

                Console.WriteLine("Исходная строка: " + input);
                Console.WriteLine("Результат: " + result);
            }
            static void Task2_1()
            {
                var rules = new Dictionary<string, string>
        {
            { "ba", "ab" },
            { "ab", "-" }
        };

                var markovAlgorithm = new MarkovAlgorithm(rules);

                string input = "aabbaab";
                string result = markovAlgorithm.Apply(input);

                Console.WriteLine("Исходная строка: " + input);
                Console.WriteLine("Результат: " + result);
            }
            static void Task1_3()
            {
                Console.Write("Введите строку (только 'c' или 'd'): "); string word = Console.ReadLine();
                char[] wordArray = word.ToCharArray();

                Console.WriteLine("\nИсходное слово:\t\t" + word);

                char temp = wordArray[0];
                wordArray[0] = ' ';
                wordArray[1] = temp;

                Console.WriteLine("Результат:\t\t" + new string(wordArray));
            }
            static void Task1_2()
            {
                //исходное число
                string number = "2142044";
                char[] numArray = number.ToCharArray();
                Console.WriteLine("Исходное число: \t" + number);

                int currentIndex = numArray.Length - 1;

                numArray[currentIndex]++; //прибавляем единицу к последней цифре числа
                number = new string(numArray);
                Console.WriteLine("Прибавили единицу: \t" + number);

                while (currentIndex != -1)
                {
                    if (numArray[currentIndex] == '5')
                    {
                        numArray[currentIndex] = '0';
                        numArray[currentIndex - 1]++;
                    }
                    currentIndex--;
                }


                number = new string(numArray);
                Console.WriteLine("Результат: \t\t" + number);
            }
            static void Task1_1()
            {
                // Исходное слово
                string word = "№01%11000010№№111%";
                char[] wordArray = word.ToCharArray();

                // Индекс, указывающий на текущий символ
                int currentIndex = wordArray.Length - 1;

                while (currentIndex != -1)
                {
                    // Замена символов
                    if (wordArray[currentIndex] == '№')
                        wordArray[currentIndex] = '%';
                    else if (wordArray[currentIndex] == '%')
                        wordArray[currentIndex] = '№';

                    // Перемещение головки влево
                    currentIndex--;
                }

                string result = new string(wordArray);

                Console.WriteLine("Исходное слово: " + word);
                Console.WriteLine("Результат: " + result);
            }
        }
    }
}
