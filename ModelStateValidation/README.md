# ModelStateValidation

Ref.: [How to Use ModelState Validation in ASP.NET Core Web API](https://code-maze.com/aspnetcore-modelstate-validation-web-api/)

[Model Validation in ASP.NET Web API](https://learn.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api)

## Demo af ModelState Validation

### 1. Simpel Validation med DataAnnotations
Vis klassen CreateBookInputModel og dens DataAnnotations.
Test controlleren vha. .http filen, hvor der sendes en POST request med en CreateBookInputModel, der ikke overholder DataAnnotations.
Se at man får en 400 Bad Request response.

### 2. Custom Error Message
Vis klassen CreateBookInputModel og dens ISBN DataAnnotations, hvor der er tilføjet en custom error message.

### 3. Custom ModelState Validation
Indkommentér ModelState.AddModelError i CreateBook metoden i BooksController:
```csharp
        if (createBookInputModel.ISBN!.Length != 10 && createBookInputModel.ISBN.Length != 13)
        {
            ModelState.AddModelError(nameof(createBookInputModel.ISBN), "ISBN should be 10 or 13 numbers long!");
        }

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
```

Test controlleren vha. .http filen, hvor der sendes en POST request med en CreateBookInputModel med forskellige ISBN længder.

