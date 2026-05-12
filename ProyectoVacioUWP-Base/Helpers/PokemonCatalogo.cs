using ProyectoUWPenBlanco;
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
                typeof(EmpoleonARS), typeof(SnorlaxISU), typeof(RotomPVA), typeof(GengarMBGCS), typeof(BrasitalEHA)
                
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