Добавить миграцию:
dotnet ef migrations add Init --verbose -- "Data Source=NTER0152; Initial Catalog=Hospital; TrustServerCertificate=True; Integrated Security=False; user=Petr; password=Petr"

Обновить БД:
dotnet ef database update --verbose -- "Data Source=NTER0152; Initial Catalog=Hospital; TrustServerCertificate=True; Integrated Security=False; user=Petr; password=Petr"