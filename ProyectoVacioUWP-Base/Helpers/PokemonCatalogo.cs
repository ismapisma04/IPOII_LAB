using System;
using System.Collections.Generic;

namespace ProyectoVacioUWP_Base
{
    public static class PokemonCatalogo
    {
        public static List<Type> ObtenerTiposPokemon()
        {
            return new List<Type>
            {
                typeof(EmpoleonARS),

                // NOTE: Agregar aqui los demas pokemons, siguiendo el mismo formato que el de arriba
            };
        }

        public static List<iPokemon> CrearPokemons()
        {
            List<iPokemon> lista = new List<iPokemon>();

            foreach (Type tipo in ObtenerTiposPokemon())
            {
                if (Activator.CreateInstance(tipo) is iPokemon pokemon)
                {
                    lista.Add(pokemon);
                }
            }

            return lista;
        }
    }
}