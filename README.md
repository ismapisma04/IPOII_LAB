# Cómo añadir un Pokémon nuevo al proyecto

Para que un Pokémon nuevo aparezca en la **Pokédex**, en la **selección** y en la **arena de combate**, se debe seguir una convención de nombres estricta y registrar el Pokémon en el catálogo general.

- La **Pokédex** carga todos los Pokémon desde `PokemonCatalogo.CrearPokemons()`.
- La **arena** hace lo mismo para después buscarlos por nombre.
- `PokemonFactory` crea automáticamente el `UserControl` y recompone las rutas de las imágenes usando el nombre del tipo de Pokémon.

---

## 1. Convención de carpetas y nombres

La estructura del Pokémon debe seguir el siguiente formato para que `PokemonFactory` pueda construir la ruta correcta de los assets bajo el patrón `Pokemons/{nombreTipo}/Assets{nombreTipo}`:

```text
ProyectoVacioUWP_Base
└── Pokemons
    └── PikachuAAA
        ├── PikachuAAA.xaml
        ├── PikachuAAA.xaml.cs
        └── AssetsPikachuAAA
            ├── sprite.png
            ├── fondo.png
            ├── escudo.png
            └── ...
```

### Reglas obligatorias

- La clase debe llamarse **`PikachuAAA`**.
- El archivo XAML debe llamarse **`PikachuAAA.xaml`**.
- El code-behind debe llamarse **`PikachuAAA.xaml.cs`**.
- La carpeta del Pokémon debe llamarse **`Pokemons/PikachuAAA`**.
- La carpeta de imágenes debe llamarse **`Pokemons/PikachuAAA/AssetsPikachuAAA`**.

---

## 2. Clase del Pokémon

El nuevo Pokémon debe ser un `UserControl` que implemente la interfaz `iPokemon`. El proyecto utiliza esta interfaz para acceder a propiedades clave (`Nombre`, `Vida`, `Energía`, `Descripción`, etc.) y a métodos públicos, como animaciones o visibilidad del HUD.

### Ejemplo del code-behind (`PikachuAAA.xaml.cs`)

```csharp
namespace ProyectoVacioUWP_Base
{
    public sealed partial class PikachuAAA : UserControl, iPokemon
    {
        public PikachuAAA()
        {
            this.InitializeComponent();
        }

        public string Nombre { get; set; }
        public double Vida { get; set; }
        public double Energia { get; set; }
        public string Categoría { get; set; }
        public string Tipo { get; set; }
        public string Altura { get; set; }
        public string Peso { get; set; }
        public string Evolucion { get; set; }
        public string Descripcion { get; set; }

        // Aquí irían los métodos de iPokemon y las animaciones
    }
}
```

### Ejemplo de la cabecera XAML (`PikachuAAA.xaml`)

```xml
<UserControl
    x:Class="ProyectoVacioUWP_Base.PikachuAAA"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:ProyectoVacioUWP_Base"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    Width="800"
    Height="600">

</UserControl>
```

---

## 3. Registro en el catálogo

La clase que actúa como registro central del proyecto y que es necesario modificar es:

```text
PokemonCatalogo.cs
```

Esta clase devuelve la lista manual de tipos en el método `ObtenerTiposPokemon()`.

### Método a modificar

```csharp
public static List<Type> ObtenerTiposPokemon()
```

### Estado inicial

```csharp
public static List<Type> ObtenerTiposPokemon()
{
    return new List<Type>
    {
        typeof(EmpoleonARS),

        // NOTE: Agregar aquí los demás pokemons, siguiendo el mismo formato que el de arriba
    };
}
```

### Línea exacta a añadir

Si el Pokémon nuevo se llama `PikachuAAA`, añade la siguiente línea a la lista:

```csharp
typeof(PikachuAAA),
```

### Ejemplo final

```csharp
public static List<Type> ObtenerTiposPokemon()
{
    return new List<Type>
    {
        typeof(EmpoleonARS),
        typeof(PikachuAAA),
        typeof(CharizardJMN)
    };
}
```

---

## 4. Funcionamiento interno

### ¿Por qué con esto ya aparece en la Pokédex?

`PokedexPage` carga la lista de Pokémon llamando a `PokemonCatalogo.CrearPokemons()` en el método `CargarPokemons()`, y después los renderiza recorriendo esa lista en `MostrarPokemons(...)`.

Al hacer uso de `PokemonFactory.CrearControlPokemon(pokemon)`, si el nuevo tipo está registrado y respeta la convención de nombres, se cargará automáticamente.

### ¿Por qué con esto ya aparece en combate?

`ArenaCombatePage` crea la lista general llamando a `PokemonCatalogo.CrearPokemons()` en su constructor y utiliza `BuscarPokemonOriginalPorNombre(...)` para localizar el Pokémon seleccionado antes de instanciar su control visual.

### ¿Qué hace `PokemonFactory` automáticamente?

La versión actual de `PokemonFactory` **no requiere añadir condicionales (`if/else`)** por cada nuevo Pokémon. Utiliza reflexión para obtener el nombre del tipo en ejecución y resolver sus recursos:

```csharp
string nombreTipo = pokemon.GetType().Name;
string carpetaAssets = $"Pokemons/{nombreTipo}/Assets{nombreTipo}";
```

---

## 5. Pasos rápidos

1. **Crear la carpeta**: `Pokemons/PikachuAAA/`
2. **Añadir dentro**:
   - `PikachuAAA.xaml`
   - `PikachuAAA.xaml.cs`
   - Carpeta `AssetsPikachuAAA/` con sus imágenes (`sprite.png`, `fondo.png`, etc.)
3. **Implementar la interfaz** `iPokemon` en la clase `PikachuAAA`.
4. **Editar** `PokemonCatalogo.cs`.
5. **Añadir el tipo** en el método `ObtenerTiposPokemon()`:

```csharp
typeof(PikachuAAA),
```

---

# Posibles errores y consideraciones

- **Revisar el namespace**: asegúrate de que el namespace de tus nuevos archivos sea exactamente `ProyectoVacioUWP_Base`. Si usas un namespace diferente, la interfaz `iPokemon` podría no detectarse correctamente o incluso provocar errores de compilación en el proyecto.
- **Revisa** el ejemplo de `Empoleon` para comprobar cómo están definidos tanto el XAML como el archivo `.cs`.
