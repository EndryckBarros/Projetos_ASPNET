using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModels
{
    public class Reserva
    {
        [Key]
        public int ReservaID { get; set; }
        [Required]
        public DateTime Data { get; set; }


        public int ServicoID { get; set; }
        public int ClienteID { get; set; }

        // --- RELACIONAMENTO _RESERVA -> SERVICO E CLIENTE
        public virtual Servico _Servico { get; set; }
        public virtual Cliente _Cliente { get; set; }

    }
}
