using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Don_SampleTrackingApp
{
    public class ProbaModel
    {
        [Key]
        [DisplayName("Id")]
        public int ProbaModelId { get; set; }        
        [MaxLength(100)]
        [DisplayName("Data prelevare")]
        public string DataPrelevare { get; set; }
        [DisplayName("Sigla furnizor")]
        public string SiglaFurnizor { get; set; }
        public string Sarja { get; set; }
        public int Diametru { get; set; }
        public string Calitate { get; set; }
        [DisplayName("Nr cuptor")]
        public int NumarCuptor { get; set; }
        [DisplayName("Tip TT")]
        public TipTT TipTratamentTermic { get; set; }
        [DisplayName("Cap bara")]
        public CapBara TipCapBara { get; set; }
        [DisplayName("Tip proba")]
        public TipProba Tipproba { get; set; }
        [DisplayName("User name")]
        public string UserName { get; set; }
        [DisplayName("Obs operator")]
        public string ObservatiiOperator { get; set; }
        [DisplayName("Data preluare")]
        public string DataPreluare { get; set; } // De operator calitate
        [DisplayName("Data raspuns")]
        public string DataRaspunsCalitate { get; set; } // Ia automat cand introduce datele operator calitate
        [DisplayName("User name calitate")]
        public string NumeUtilizatorCalitate { get; set; } // Ia nume utilizator si completeazsa acest camp
        [DisplayName("Rezultat")]
        public Rezultat RezultatProba { get; set; }
        public int KV1 { get; set; }
        public int KV2 { get; set; }
        public int KV3 { get; set; }
        public int Temperatura { get; set; }
        [DisplayName("Duritate HB")]
        public int DuritateHB { get; set; }
        [DisplayName("Obs calitate")]
        public string ObservatiiCalitate { get; set; }
    }
}
