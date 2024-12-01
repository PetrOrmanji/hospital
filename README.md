# Medical Directory Management API

## Описание проекта
Этот проект представляет собой REST API для управления большим медицинским справочником. 
API позволяет выполнять CRUD-операции с ключевыми сущностями: **Город**, **Больница (Поликлиника)**, **Врач**, **Специализация**, **Пользователь**,  **Роль**.  

Проект ориентирован на использование в системах медицинского учета, автоматизации и администрирования.

---

## Использованные технологии
- C#
- .NET Core 8.0
- Entity Framework 8
- Serilog
- Auto Mapper
- Ensure That
- Refit Client
- Swagger
- JWT

## Основные возможности API

### Doctors (Врачи)
- **GET /Doctors/get/{id}** — Получение доктора по ID  
- **GET /Doctors/get** — Получение всех докторов  
- **POST /Doctors/add** — Добавление доктора  
- **DELETE /Doctors/remove/{id}** — Удаление доктора  
- **PUT /Doctors/update** — Обновление доктора  
- **POST /Doctors/addDoctorSpecialization** — Добавление специализации доктора  
- **DELETE /Doctors/removeDoctorSpecialization** — Удаление специализации доктора  
- **POST /Doctors/addDoctorPolyclinic** — Добавление поликлиники доктора  
- **DELETE /Doctors/removeDoctorPolyclinic** — Удаление поликлиники доктора  

---

### Polyclinics (Больницы/Поликлиники)
- **GET /Polyclinics/get/{id}** — Получение поликлиники по ID  
- **GET /Polyclinics/get** — Получение всех поликлиник  
- **POST /Polyclinics/add** — Добавление поликлиники  
- **DELETE /Polyclinics/remove/{id}** — Удаление поликлиники  
- **PUT /Polyclinics/update** — Обновление поликлиники  

---

### Roles (Роли)
- **GET /Roles/get/{id}** — Получение роли по ID  
- **GET /Roles/get** — Получение всех ролей  
- **POST /Roles/add** — Добавление роли  
- **DELETE /Roles/remove/{id}** — Удаление роли  
- **PUT /Roles/update** — Обновление роли  

---

### Specializations (Специализации)
- **GET /Specializations/get/{id}** — Получение специализации по ID  
- **GET /Specializations/get** — Получение всех специализаций  
- **POST /Specializations/add** — Добавление специализации  
- **DELETE /Specializations/remove/{id}** — Удаление специализации  
- **PUT /Specializations/update** — Обновление специализации  

---

### Towns (Города)
- **GET /Towns/get/{id}** — Получение города по ID  
- **GET /Towns/get** — Получение всех городов  
- **POST /Towns/add** — Добавление города  
- **DELETE /Towns/remove/{id}** — Удаление города  
- **PUT /Towns/update** — Обновление города  

---

### User (Пользователи)
- **GET /User/get/{id}** — Получение пользователя по ID  
- **GET /User/get** — Получение всех пользователей  
- **DELETE /User/remove/{id}** — Удаление пользователя  
- **POST /User/addUserRole** — Добавление роли пользователю  
- **DELETE /User/removeUserRole** — Удаление роли пользователя  
- **POST /User/register** — Регистрация пользователя  
- **POST /User/login** — Вход пользователя  

---

В проекте реализована аутентификация и авторизация пользователя с помощью JWT токена. Подавляющее большинство возможностей API доступно лишь с использованием JWT токена авторизации.


## Установка и использование
1. Копирование репозитория - git clone https://github.com/PetrOrmanji/hospital.git
2. Если на ПК не установлен SQL Server, необходимо скачать и установить.
3. В appsettings.json необходимо указать в соответствующих блоках необходимые данные: DbConnectionString - строку для подключения к серверу SQL. ClientSecret - секретный ключ, который будет использоваться в процессах авторизации.
4. Запуск приложения.
