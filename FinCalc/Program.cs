namespace FinCalc
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("=== ФИНАНСОВЫЙ КАЛЬКУЛЯТОР ===");
                Console.WriteLine("1. Расчет кредита");
                Console.WriteLine("2. Конвертер валют");
                Console.WriteLine("3. Калькулятор вкладов");
                Console.WriteLine("4. Выход");
                Console.Write("Выберите опцию: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        CreditCalculator();
                        break;
                    case "2":
                        CurrencyConverter();
                        break;
                    case "3":
                        DepositCalculator();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Ошибка: выберите пункт 1-4.\n");
                        break;
                }
            }
        }


        /// <summary>
        /// Выполняет расчет параметров кредита:
        /// - ежемесячного аннуитетного платежа,
        /// - общей суммы выплат,
        /// - переплаты по кредиту.
        /// </summary>
        static void CreditCalculator()
        {
            Console.Write("Введите сумму кредита (руб): ");
            double amount = double.Parse(Console.ReadLine());

            Console.Write("Введите срок кредита (месяцев): ");
            int months = int.Parse(Console.ReadLine());

            Console.Write("Введите процентную ставку (% годовых): ");
            double annualRate = double.Parse(Console.ReadLine());

            double monthlyRate = annualRate / 100 / 12;

            // Формула аннуитетного платежа
            double monthlyPayment =
                amount * (monthlyRate * Math.Pow(1 + monthlyRate, months)) /
                (Math.Pow(1 + monthlyRate, months) - 1);

            double totalPayment = monthlyPayment * months;
            double overpayment = totalPayment - amount;

            Console.WriteLine("\n=== Результат ===");
            Console.WriteLine($"Ежемесячный платеж: {monthlyPayment:F2} руб");
            Console.WriteLine($"Общая сумма выплат: {totalPayment:F2} руб");
            Console.WriteLine($"Переплата по кредиту: {overpayment:F2} руб\n");
        }


        /// <summary>
        /// Конвертирует сумму из одной валюты в другую, используя фиксированные курсы.
        /// </summary>
        static void CurrencyConverter()
        {
            Console.Write("Исходная валюта (RUB, USD, EUR): ");
            string from = Console.ReadLine().ToUpper();

            Console.Write("Целевая валюта (RUB, USD, EUR): ");
            string to = Console.ReadLine().ToUpper();

            Console.Write("Сумма для конвертации: ");
            double amount = double.Parse(Console.ReadLine());

            double result = ConvertCurrency(from, to, amount);

            Console.WriteLine($"\nРезультат: {result:F2} {to}\n");
        }

        static double ConvertCurrency(string from, string to, double amount)
        {
            // Фиксированные курсы
            double usdToRub = 90.0;
            double eurToRub = 98.5;
            double eurToUsd = 1.09;

            // Переводим в рубли
            double rubAmount = from switch
            {
                "RUB" => amount,
                "USD" => amount * usdToRub,
                "EUR" => amount * eurToRub,
                _ => throw new Exception("Неверная валюта")
            };

            // Переводим из рублей в целевую валюту
            return to switch
            {
                "RUB" => rubAmount,
                "USD" => rubAmount / usdToRub,
                "EUR" => rubAmount / eurToRub,
                _ => throw new Exception("Неверная валюта")
            };
        }


        /// <summary>
        /// Выполняет пересчёт валюты на основе фиксированных курсов:
        /// USD → RUB, EUR → RUB, EUR → USD.
        /// </summary>
        /// <param name="from">Исходная валюта</param>
        /// <param name="to">Целевая валюта</param>
        /// <param name="amount">Сумма</param>
        /// <returns>Преобразованная сумма</returns>
        static void DepositCalculator()
        {
            Console.Write("Введите сумму вклада (руб): ");
            double amount = double.Parse(Console.ReadLine());

            Console.Write("Введите срок вклада (месяцев): ");
            int months = int.Parse(Console.ReadLine());

            Console.Write("Введите процентную ставку (% годовых): ");
            double annualRate = double.Parse(Console.ReadLine());

            Console.Write("Тип вклада (1 — без капитализации, 2 — с капитализацией): ");
            string type = Console.ReadLine();

            double income, totalAmount;

            if (type == "1") // Без капитализации
            {
                income = amount * annualRate * months / 12 / 100;
                totalAmount = amount + income;
            }
            else // С капитализацией
            {
                double monthlyRate = annualRate / 100 / 12;
                totalAmount = amount * Math.Pow(1 + monthlyRate, months);
                income = totalAmount - amount;
            }

            Console.WriteLine("\n=== Результат ===");
            Console.WriteLine($"Доход по вкладу: {income:F2} руб");
            Console.WriteLine($"Итоговая сумма: {totalAmount:F2} руб\n");
        }
    }
}
