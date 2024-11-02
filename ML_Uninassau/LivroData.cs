using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Uninassau
{
    internal class LivroData
    {
        [LoadColumn(5)] // Idade é a 6ª coluna, então o índice é 5
        public float Idade { get; set; } // A idade é um float
        [LoadColumn(4)] // Nome é a 5ª coluna
        public string Nome { get; set; }
        [LoadColumn(3)] // Status é a 4ª coluna
        public string Status { get; set; }
    }
}
