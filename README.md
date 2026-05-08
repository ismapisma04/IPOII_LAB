\# Cómo añadir un Pokémon nuevo al proyecto



Para que un Pokémon nuevo aparezca en la \*\*Pokédex\*\*, en la \*\*selección\*\* y en la \*\*arena de combate\*\*, se debe seguir una convención de nombres estricta y registrar el Pokémon en el catálogo general. 



\* La \*\*Pokédex\*\* carga todos los Pokémon desde `PokemonCatalogo.CrearPokemons()`.

\* La \*\*arena\*\* hace lo mismo para después buscarlos por nombre.

\* `PokemonFactory` crea automáticamente el `UserControl` y recompone las rutas de las imágenes usando el nombre del tipo de Pokémon.



\---



\## 1. Convención de carpetas y nombres



La estructura del Pokémon debe seguir el siguiente formato para que `PokemonFactory` pueda construir la ruta correcta de los assets bajo el patrón `Pokemons/{nombreTipo}/Assets{nombreTipo}`:



```text

ProyectoVacioUWP\_Base

└── Pokemons

&#x20;   └── PikachuAAA

&#x20;       ├── PikachuAAA.xaml

&#x20;       ├── PikachuAAA.xaml.cs

&#x20;       └── AssetsPikachuAAA

&#x20;           ├── sprite.png

&#x20;           ├── fondo.png

&#x20;           ├── escudo.png

&#x20;           └── ...



```



\### Reglas obligatorias:



\* La clase debe llamarse \*\*`PikachuAAA`\*\*.

\* El archivo XAML debe llamarse \*\*`PikachuAAA.xaml`\*\*.

\* El code-behind debe llamarse \*\*`PikachuAAA.xaml.cs`\*\*.

\* La carpeta del Pokémon debe llamarse \*\*`Pokemons/PikachuAAA`\*\*.

\* La carpeta de imágenes debe llamarse \*\*`Pokemons/PikachuAAA/AssetsPikachuAAA`\*\*.



\---



\## 2. Clase del Pokémon



El nuevo Pokémon debe ser un `UserControl` que implemente la interfaz `iPokemon`. El proyecto utiliza esta interfaz para acceder a propiedades clave (nombre, vida, energía, descripción) y a métodos públicos (animaciones o visibilidad del HUD).



\### Ejemplo del Code-Behind (`PikachuAAA.xaml.cs`):



```csharp

namespace ProyectoVacioUWP\_Base

{

&#x20;   public sealed partial class PikachuAAA : UserControl, iPokemon

&#x20;   {

&#x20;       public PikachuAAA()

&#x20;       {

&#x20;           this.InitializeComponent();

&#x20;       }



&#x20;       public string Nombre { get; set; }

&#x20;       public double Vida { get; set; }

&#x20;       public double Energia { get; set; }

&#x20;       public string Categoría { get; set; }

&#x20;       public string Tipo { get; set; }

&#x20;       public string Altura { get; set; }

&#x20;       public string Peso { get; set; }

&#x20;       public string Evolucion { get; set; }

&#x20;       public string Descripcion { get; set; }



&#x20;       // Aquí irían los métodos de iPokemon y las animaciones

&#x20;   }

}



```



\### Ejemplo de la cabecera XAML (`PikachuAAA.xaml`):



```xml

<UserControl

&#x20;   x:Class="ProyectoVacioUWP\_Base.PikachuAAA"

&#x20;   xmlns="\[http://schemas.microsoft.com/winfx/2006/xaml/presentation](http://schemas.microsoft.com/winfx/2006/xaml/presentation)"

&#x20;   xmlns:x="\[http://schemas.microsoft.com/winfx/2006/xaml](http://schemas.microsoft.com/winfx/2006/xaml)"

&#x20;   xmlns:local="using:ProyectoVacioUWP\_Base"

&#x20;   xmlns:d="\[http://schemas.microsoft.com/expression/blend/2008](http://schemas.microsoft.com/expression/blend/2008)"

&#x20;   xmlns:mc="\[http://schemas.openxmlformats.org/markup-compatibility/2006](http://schemas.openxmlformats.org/markup-compatibility/2006)"

&#x20;   mc:Ignorable="d"

&#x20;   Width="800" Height="600">

&#x20;   

&#x20;   </UserControl>



```



\---



\## 3. Registro en el Catálogo



La clase que actúa como registro central del proyecto y que es necesario modificar es:



```text

PokemonCatalogo.cs



```



Esta clase devuelve la lista manual de tipos en el método `ObtenerTiposPokemon()`.



\### Método a modificar:



```csharp

public static List<Type> ObtenerTiposPokemon()



```



\### Estado inicial:



```csharp

public static List<Type> ObtenerTiposPokemon()

{

&#x20;   return new List<Type>

&#x20;   {

&#x20;       typeof(EmpoleonARS),



&#x20;       // NOTE: Agregar aquí los demás pokemons, siguiendo el mismo formato que el de arriba

&#x20;   };

}



```



\### Línea exacta a añadir:



Si el Pokémon nuevo se llama `PikachuAAA`, añade la siguiente línea a la lista:



```csharp

typeof(PikachuAAA),



```



\### Ejemplo final:



```csharp

public static List<Type> ObtenerTiposPokemon()

{

&#x20;   return new List<Type>

&#x20;   {

&#x20;       typeof(EmpoleonARS),

&#x20;       typeof(PikachuAAA),

&#x20;       typeof(CharizardJMN)

&#x20;   };

}



```



\---



\## 4. Funcionamiento Interno



\### ¿Por qué con esto ya aparece en la Pokédex?



`PokedexPage` carga la lista de Pokémon llamando a `PokemonCatalogo.CrearPokemons()` en el método `CargarPokemons()`, y después los renderiza recorriendo esa lista en `MostrarPokemons(...)`.

Al hacer uso de `PokemonFactory.CrearControlPokemon(pokemon)`, si el nuevo tipo está registrado y respeta la convención de nombres, se cargará automáticamente.



\### ¿Por qué con esto ya aparece en combate?



`ArenaCombatePage` crea la lista general llamando a `PokemonCatalogo.CrearPokemons()` en su constructor y utiliza `BuscarPokemonOriginalPorNombre(...)` para localizar el Pokémon seleccionado antes de instanciar su control visual.



\### ¿Qué hace `PokemonFactory` automáticamente?



La versión actual de `PokemonFactory` \*\*no requiere añadir condicionales (`if/else`)\*\* por cada nuevo Pokémon. Utiliza reflexión para obtener el nombre del tipo en ejecución y resolver sus recursos:



```csharp

string nombreTipo = pokemon.GetType().Name;

string carpetaAssets = $"Pokemons/{nombreTipo}/Assets{nombreTipo}";



```



\---



\## 5. Resumen rápido de pasos



1\. \*\*Crear la carpeta\*\*: `Pokemons/PikachuAAA/`

2\. \*\*Añadir dentro\*\*:

\* `PikachuAAA.xaml`

\* `PikachuAAA.xaml.cs`

\* Carpeta `AssetsPikachuAAA/` con sus imágenes (`sprite.png`, `fondo.png`, etc.)





3\. \*\*Implementar la interfaz\*\* `iPokemon` en la clase `PikachuAAA`.

4\. \*\*Editar\*\* `PokemonCatalogo.cs`.

5\. \*\*Añadir el tipo\*\* en el método `ObtenerTiposPokemon()`:

```csharp

typeof(PikachuAAA),



```







\---



\## ⚠️ Posibles errores y consideraciones



\* \*\*Revisar el Namespace\*\*: Asegúrate de que el namespace de tus nuevos archivos sea exactamente `ProyectoVacioUWP\_Base`. Si usas un namespace diferente, la interfaz `iPokemon` podría no ser detectada correctamente o fallar en la compilación del proyecto de forma global. Revisa el XAML y cs de Empoleon como se pone



```



```

