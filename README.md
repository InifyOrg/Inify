<div id="header" align="center">
  <img src="https://github.com/user-attachments/assets/580b3d49-e431-4ac8-9fb2-712a0a077161" width="200"/>
</div>

---

### Про проект:

Inify це сервіс для ведення обліку та аналітики активів користувача у світі web3 та криптовалют.

Проблема яку вирішує наш проект: в web3 існує безліч різних крипто гаманців, бірж та інших засобів перегляду та управління своїми активами, наш сервіс допомагає користувачеві збирати та об'єднувати інформацію з усіх засобів зберігання активів в одне ціле і подає цю інформацію у вигляді зручної статистики.

---

### Функціонал та архітектура:

1. Авторизація та автентифікація. А також повне керування аккаунтом користувача.
2. Прив'язування будь-якої кількості гаманців до облікового запису користувача для подальшої аналітики.
3. Аналітика одного або одночасно всіх гаманців прив'язаних до облікового запису користувача. Проект безпосередньо в реальному часі взаємодіє з блокчейном, щоб аналізувати транзакції користувача і дізнаватися скільки в нього грошей у певних токенах та гаманцях.

UseCase діаграма за якою робився проект:

![Untitled](https://github.com/user-attachments/assets/80eb60bc-10bf-4d78-a07e-2892a8dd5159)

Перші нариси інтерфейсу:

![Untitled2](https://github.com/user-attachments/assets/67108f22-e6f1-439a-bce3-7673fc9d53ff)
![Untitled3](https://github.com/user-attachments/assets/ca2fbf6c-8b2e-479b-8544-25b29ec03646)
![Untitled4](https://github.com/user-attachments/assets/bc76208d-f13b-4d1b-917e-bb66cea4e14d)

Sequence діаграма, перша для того, щоб розуміти як буде працювати основна функція нашого додатка для аналітики:

![Untitled5](https://github.com/user-attachments/assets/ca94d295-efde-4b79-916b-b2907481ee86)

Архітектура програми:

Проект складається з 4 мікросервісів:
- UsersMs (що відповідає за повне керування користувачами)
- WalletsMs (керує гаманцями прив'язаними до користувача)
- TokensMs (мікросервіс, що автоматично поповнює базу даних токенами, які може парсити наш проект, щоб створити білий лист і не парсить токени, які створені для розлучення).
- BlockchainParsersMs (мікросервіс, що займається парсингом кількості та ціни токенів у реальному часі)

Діаграма:

![Untitled6](https://github.com/user-attachments/assets/bb5868cd-ae63-418e-a815-85341f4b3d5a)

Технології, що використовуються для написання проекту
- Мови програмування
  - C#
  - JavaScript
- Фреймворки/бібліотеки
  - ASP.NET
  - React
  - Nethereum
- Сторонні сервіси
  - Infura (надає нам ноду для надсилання запитів у блокчейн)
  - CoinMarketCap (надає нам список довірених токенів, а також ціни в USD на одиницю певного токена)
- Інструменти розробки
  - GIT
  - Docker
  - Microsoft SQL Server
  - NuGet

---

### Гайд для розгортання та першого запуску програми:

Спочатку потрібно зробити клон репозиторію з submodules за допомогою команди нижче або розархівувати архів із проектом у потрібну папку.

```properties
git clone --recurse-submodules git@github.com:InifyOrg/Inify.git
```

Далі необхідно зайти в хост кожного з мікросервісів та зібрати для них Docker image за допомогою команд нижче (це не зроблено в compose.yaml тому що він не дозволяє запустити білд з прапором -f без якого не утворитися image)

1. Відкрийте консоль в основній, першій папці проекту

Для мікросервісу usersms:
```properties
cd UsersMS/UsersMS.Host

docker build -f Dockerfile .. -t usersmshost

cd ..
cd ..
```

Для мікросервісу walletsms:
```properties
cd WalletsMS/WalletsMS.Host

docker build -f Dockerfile .. -t walletsmshost

cd ..
cd ..
```

Для мікросервісу tokensms:
```properties
cd TokensMS/TokensMS.Host

docker build -f Dockerfile .. -t tokensmshost

cd ..
cd ..
```

Для мікросервісу blockchainparsersms:
```properties
cd BlockchainParsersMS/BlockchainParsersMS.Host

docker build -f Dockerfile .. -t blockchainparsersmshost

cd ..
cd ..
```

Для мікросервісу apigateway:
```properties
cd ApiGateway/ApiGateway.Host

docker build -f Dockerfile .. -t apigatewayhost

cd ..
cd ..
```

2. З головної папки проекту запустіть наступну команду

```properties
docker compose up
```

і дочекайтеся поки команда закінчить свою роботу і збере всі контейнери.

3. Коли закінчився процес створення контейнерів перевірте чи працюю вони за допомогою команди

```properties
docker ps
```

якщо запущено 7 контейнерів, все успішно запустилося та можна тестувати.
