# OutOfOffice

## О приложении

Это веб-приложение, разработанное с использованием ASP.NET Core MVC с Identity и реализующее Onion архитектуру. В интерфейсе используются библиотеки jQuery и Bootstrap.

### OutOfOffice.Core

Этот проект содержит все основные сущности приложения и интерфейсы, которые определяют контракты для взаимодействия с данными.

### OutOfOffice.Persistence

Проект отвечает за связь с базой данных с помощью класса `OutOfOfficeDbContext`. Он включает в себя классы конфигурации таблиц, управление ролями пользователей, миграции базы данных и репозитории, реализующие интерфейсы из `OutOfOffice.Core`. Также здесь находится класс `DbSeeder`, предназначенный для создания ролей и пользователей при первом запуске приложения.

### OutOfOffice.Application

Проект содержит сервисы приложения, которые используют методы репозиториев для выполнения бизнес-логики.

### OutOfOffice

Главный проект, объединяющий все вышеперечисленные части. Здесь реализована Identity, а также содержатся все представления и контроллеры приложения.

## Архитектура бази данных

![image](https://github.com/Palazaram/OutOfOffice/assets/108758569/37675830-709c-4649-8acf-94b7b70bff42)

### Изменения структуры бази данних 

Исходя из ТЗ, обнаружил некоторые несоответствия (по моему мнению).

### Таблица Employees 

Сделал поле `PeoplePartnerId` nullable, так как нам нужно создать первого пользователя, и это поле не может быть обязательным.

### Таблица ApprovalRequests

Сделал поле `ApproverId` nullable. Потому что, мы не сможем создать запись в таблицу `ApprovalRequests` при создании записи в таблицу `LeaveRequests`

## Запуск приложения

Для того, чтобы запустить приложение нужно поменять строку подлкючения к базе данным в файле `OutOfOffice --> appsettings.json` и `OutOfOffice --> appsettings.Development.json`.

Для создания базы данных нужно зайти в `Package Manager Console` `View --> Other Windows --> Package Manager Console`, обязательно выбрать проект `OutOfOffice.Persistence` в `Default project` и ввести команду `update-database`.
После чего у вас должна создаться база данных с названием, которое вы указали в строке подлкючение. 
`База данных: SSMS.`

## Роли и пользователи

### Administrator

Login: Administrator1

Password: Admin@123

### Project Manager

Login: ProjectManager1

Password: ProjMgr@123

### HR Manager

Login: HR_Manger1

Password: HRMgr@123

### Employee

Login: Employee1

Password: Emp@123

**Все новые зарегистрированные пользователи получают роль `Employee`.**

## Работа приложения (с ролью Администратор)

### Стартовая страница 

![image](https://github.com/Palazaram/OutOfOffice/assets/108758569/992db764-fa79-4f7e-ae4b-59edb838b375)

### Раздел Employees

![image](https://github.com/Palazaram/OutOfOffice/assets/108758569/d4694e5e-7808-4dfa-ae6e-ed0a7700aa1b)

### Раздел Projects

![image](https://github.com/Palazaram/OutOfOffice/assets/108758569/a8947e4f-e157-4fbb-a78e-297c5dc99496)

### Раздел LeaveRequests

![image](https://github.com/Palazaram/OutOfOffice/assets/108758569/138b8a9b-4ce2-4ecc-8015-4806cd907c1d)

### Раздел ApproveRequests

![image](https://github.com/Palazaram/OutOfOffice/assets/108758569/6cd0f479-9612-4118-a3b9-e6959ea6d305)
