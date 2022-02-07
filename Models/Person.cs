using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineSystem.Models {
    [Table("persons")]
    public class Person {
        [Key]
        [Column("id_person")]
        [Display(Name = "Código da pessoa")]
        public int id_person { get; set; }

        [Column("full_name")]
        [StringLength(60)]
        [Display(Name = "Nome da pessoa")]
        [Required(ErrorMessage = "O nome completo é obrigatório!")]
        public string full_name { get; set; }

        [Column("birth_date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy à\\s\\ hh:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "A data de nascimento é obrigatório!")]
        public DateTime birth_date { get; set; }

        [Column("sex")]
        [StringLength(1)]
        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "O sexo é obrigatório!")]
        public char sex { get; set; }

        public virtual ICollection<Vaccine> Vaccine { get; set; }
        public virtual ICollection<Address> Address { get; set; }
    }
}