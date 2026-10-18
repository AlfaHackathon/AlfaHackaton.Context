# AlfaHackaton.Context

Контекст запроса и данные авторизованного пользователя для .NET 10.

## Создать контекст

```csharp
using AlfaHackaton.Context;

var ctx = new Context(
    authToken: $"Bearer {token}",
    locale: "ru",
    origin: "https://example.com",
    userAgent: "App/1.0",
    authorize: new Authorize(userId, userName, email),
    cancellationToken: cancellationToken);

ctx.AdditionalHeaders["X-Request-Id"] = requestId;
```

`AuthToken` — готовое значение заголовка `Authorization`. Данные пользователя передаются явно:
контекст не разбирает JWT. `UserId` хранится строкой; Guid можно передать через `ToString()`.

## Контекст без пользователя

```csharp
var ctx = Context.Empty();
var publicContext = Context.EmptyWithOrigin("https://example.com");
var tokenContext = Context.New($"Bearer {token}", cancellationToken);
```

`New` сохраняет токен и отмену, но не заполняет `Authorize`. Для фоновых операций можно использовать
`Empty`; для полной информации о запросе — конструктор. Коллекция заголовков у каждого контекста своя.

## Проверка

```powershell
dotnet test AlfaHackaton.Context.slnx
```

Сборка NuGet-пакета:

```powershell
dotnet pack AlfaHackaton.Context.csproj --configuration Release --output artifacts/nupkg
```
