using System;

class Program
{
    static void Main()
    {
        // Начальный баланс для каждой сделки
        double balance = 10.0;
        // Процент прибыли для успешной сделки
        double profitPercentage = 122.5;
        // Процент убытка для неудачной сделки
        double lossPercentage = 100.0;
        // Общее количество сделок в день
        int totalTradesPerDay = 4;
        // Общее количество дней в месяце
        int daysPerMonth = 30;
        // Общее количество месяцев
        int totalMonths = 1;
        // Общая прибыль
        double totalProfit = 0.0;
        // Увеличение позиции после каждых 4 сделок
        double positionIncrease = 0.0;
        // Прибыль от последних 4 сделок
        double lastFourTradesProfit = 0.0;
        // Начальный баланс
        double startingBalance = balance;
        // Считаем общее количество сгенерированых месяцов
        int generatedMonths = 0;

        // Создаем объект для генерации случайных чисел
        Random random = new Random();

        // Основной цикл для генерации и попыток закрытия месяца с положительным балансом
        while (true)
        {
            // Сброс параметров для новой попытки
            balance = startingBalance;
            totalProfit = 0.0;
            positionIncrease = 0.0;
            lastFourTradesProfit = 0.0;

            // Цикл по месяцам
            for (int month = 1; month <= totalMonths; month++)
            {
                if (totalProfit >= 0)
                {
                    // Добавляем месяц к общему количеству каждый новый месяц
                    generatedMonths++;
                    // Цикл по дням
                    for (int day = 1; day <= daysPerMonth; day++)
                    {
                        // Цикл по сделкам
                        for (int trade = 1; trade <= totalTradesPerDay; trade++)
                        {
                            // Генерируем случайное число от 1 до 100
                            float randomNumber = random.Next(1, 100);

                            // Если случайное число больше 97, то обе позиции ликвидируются
                            if (randomNumber > 96.67)
                            {
                                totalProfit -= balance * 2;
                                // Возвращаем баланс к стартовому для снижения риска потери всего депозита 
                                balance = startingBalance;
                                Console.WriteLine("Случайное число больше 96.67. Обе позиции ликвидированы.");
                            }
                            else
                            {
                                // Иначе одна позиция приносит прибыль, а другая - убыток
                                double tradeProfit = (balance / 2) * profitPercentage / 100 - (balance / 2);
                                totalProfit += tradeProfit;
                                lastFourTradesProfit += tradeProfit;
                            }

                            // Если общая прибыль меньше 0, завершаем программу
                            if (totalProfit < 0)
                            {
                                Console.WriteLine("Баланс меньше 0. Завершение работы.");
                                break;
                            }

                            // Каждые 5 сделки позиция увеличивается и прибыль от последних 2 сделок добавляется к общей прибыли
                            if (trade % 5 == 0)
                            {
                                positionIncrease += lastFourTradesProfit / 2;
                                balance += positionIncrease;
                                lastFourTradesProfit = 0.0;
                            }

                            Console.WriteLine($"Текущий баланс после сделки {trade} в день {day} месяца {month}: {totalProfit}");
                        }

                        // Если общая прибыль меньше 0, выходим из цикла по дням
                        if (totalProfit < 0)
                            break;
                    }

                    // Если общая прибыль меньше 0, выходим из цикла по месяцам
                    if (totalProfit < 0)
                        break;
                }
            }


            // Если месяц закрыт с положительным балансом, выводим результат и завершаем программу
            if (totalProfit >= 0)
            {
                Console.WriteLine($"Всего месяцев {generatedMonths}. Доходность: {totalProfit}");

                double percentageChange = (totalProfit - startingBalance) / startingBalance * 100;
                Console.WriteLine($"Процентное изменение от начального баланса: {percentageChange}%");

                break;
            }
            else
            {
                Console.WriteLine("Неудачный месяц. Повторная попытка.");
            }
        }
    }
}