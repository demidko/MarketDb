### MarketDb -- Игрушечная асинхронная NoSQL база данных
  * DbConsole - класс для непосредственного (ручного) тестирования работы базы.
  * TelegramUser - каждый объект представляет Telegram-пользователя и связан с базой данных.  
    Для сохранения в базу внесенных в объект изменений используется метод Save.
  * Db - класс представляющий базу данных.
#### Статус -- На текущий момент программа работает полностью корректно.
### Как собрать, как запускать?
  1. Скачиваем .NET Core https://dot.net/
  2. Переходим в директорию с проектом и пишем в терминал dotnet build
  3. Переходим в повявившуюся директорию /bin и пишем dotnet marketdb.dll
  4. Можно играть с базой! Есть понятный console user interface...
