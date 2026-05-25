using ProyectoUWPenBlanco;
using IPOkemon_CompuTerror; // Ponerlo con ProyectoUWPenBlanco
using System;
using System.Collections.Generic;
using IPOkemon;
using IPOkemon2_P1_2026;



namespace ProyectoVacioUWP_Base
{
    public static class PokemonCatalogo
    {
        public static List<Type> ObtenerTiposPokemon()
        {
            return new List<Type>
{
    // Los que ya tenías configurados
    typeof(EmpoleonARS),
    typeof(SnorlaxISU),
    typeof(RotomPVA),
    typeof(GengarMBGCS),
    typeof(BrasitalEHA),
    typeof(ComputerrorDLM),

    // Nuevos Pokémon añadidos desde la imagen
    typeof(DittoAEJ),
    typeof(DollivPS),
    typeof(DragoniteDGM),
    typeof(DrifloonShinyARG),
    typeof(FennekinRRM),
    typeof(HankRCC),
    typeof(HoothootJDCF),
    typeof(NatuCAAA),
    typeof(OddishNP),
    typeof(Oshawott),
    //typeof(PokemonUC), //ESte es piplup Funciona pero rompe el filtrado de los pk en la pokedex
    typeof(PoliwagJAM),
    typeof(MonfernoCRS), 
    typeof(PsyduckADM), 
    typeof(RhynotronControl),
    typeof(SquirtleMRGM),
    typeof(TorchicCGM),
    typeof(UnownOVG),
    typeof(KingdraJFA),   //No está bien implementado la interfaz Ipokemon
    //typeof(MarillADO), // Este hijo de puta lo revienta todo
    //typeof(ZapdosRGS) //(Da excepción)
    // TOTAL 26 POKEMON
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