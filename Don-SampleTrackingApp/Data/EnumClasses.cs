using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Don_SampleTrackingApp
{
    public class EnumClasses
    {
    }

    // Selectie afisare date
    public enum DateProbaDeAfisat
    {
        Toate,
        ProbaIntrodusa,
        ProbaPreluata,
        ProbaRezultat,
        ProbaRNC
    }

    // Rol pentru user 
    public enum UserRol
    {
        Operator,
        OperatorCalitate,
        Admin
    }

    // Tratamente termice
    public enum TipTT
    {
        N,
        NL,
        QT,
        A,
        FP,
        L,
        SR,
    }

    // Selectie proba
    public enum CapBara
    {
        A,
        B,
        Mijloc
    }

    // Selectie Tip Proba
    public enum TipProba
    {
        Mecanica,
        Duritate,
        Chimie
    }

    // Selectie Rezultat 
    public enum Rezultat
    {
        OK,
        RNC
    }
}
