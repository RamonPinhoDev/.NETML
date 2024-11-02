using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Uninassau
{
    internal class LivroPrevisao
    {
        [ColumnName("Score")]
        public float PrevisaoIdade { get; set; }
    }
}
